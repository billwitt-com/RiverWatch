using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RWInbound2.Validation
{
    public static class UpdateNutrients
    {

        public static void Update(string userName)
        {
            try
            {
                RiverWatchEntities RWE = new RiverWatchEntities();
                NutrientData NData = null;
                bool existingRecord = false;
                // first process the blanks and dups and tests etc. as we don't need them for validation
                var L = from l in RWE.Lachats
                        where l.Valid == true & l.CODE != "05" & l.CODE != "25" & l.CODE != "35" & l.BlkDup == false
                        select l;

                if (L.Count() > 0)
                {
                    int count = L.Count();
                    foreach (var ll in L)
                    {
                        ll.Validated = true;
                        ll.BlkDup = true;
                    }
                }
                RWE.SaveChanges();  

                // now get list of barcodes to process

                List<string> BC = (from bc in RWE.Lachats
                                   where bc.SampleType.ToUpper().Contains("RW") == true & bc.CODE == "05" | bc.CODE == "25" | bc.CODE == "35" & bc.Validated == false & bc.Valid == true
                                   select bc.SampleType).Distinct().ToList<string>();
                BC.Sort();
                BC.Reverse(); // put newest elements first

                if (BC != null)
                {
                    int bcCount = BC.Count;
                    foreach (string bcode in BC)
                    {
                        // get raw data from lachat table for this bar code, could be no values or could be six or more

                        var D = from d in RWE.Lachats
                                where d.SampleType == bcode
                                select d;

                        // see if this bar code exists in the nutrient data table, if not, insert it

                        NutrientData TEST = (NutrientData)(from nd in RWE.NutrientDatas
                                                           where nd.BARCODE == bcode & nd.Valid == true & nd.Validated == false
                                                           select nd).FirstOrDefault();

                        if (TEST != null)  // there is no existing bar code, so we need to make one 
                        {
                            NData = TEST; // we have existing entries, so use this entity
                            existingRecord = true; // flag for later
                        }
                        else
                        {
                            NData = new NutrientData();
                            existingRecord = false;
                        }
                        if (D != null)
                        {
                            foreach (var item in D)
                            {
                                NData.Ammonia_CH = false;   // preload so there are no null values for control display
                                NData.Sulfate_CH = false;
                                NData.NitrateNitrite_CH = false;
                                NData.OrthoPhos_CH = false;
                                NData.ChlorA_CH = false;
                                NData.Chloride_CH = false;
                                NData.DOC_CH = false;
                                NData.TotalNitro_CH = false;
                                NData.TotalPhos_CH = false;
                                NData.TSS_CH = false;

                                switch (item.Parameter.ToUpper())
                                {
                                    case "AMMONIA":
                                        NData.Ammonia = item.Result;
                                        NData.Ammonia_CH = item.CONHI ?? false;  
                                        break;

                                    case "SULFATE":
                                        NData.Sulfate = item.Result;
                                        NData.Sulfate_CH = item.CONHI ?? false; 
                                        break;

                                        // NITRATE-NITRITE
                                    case "NITRATE-NITRITE":
                                        NData.NitrateNitrite = item.Result;
                                        NData.NitrateNitrite_CH = item.CONHI ?? false; 
                                        break;

                                    case "ORTHOPHOS":
                                        NData.OrthoPhos = item.Result;
                                        NData.OrthoPhos_CH = item.CONHI ?? false; 
                                        break;

                                    case "CHLORA":
                                        NData.ChlorA = item.Result;
                                        NData.ChlorA_CH = item.CONHI ?? false; 
                                        break;

                                    case "CHLORIDE":
                                        NData.Chloride = item.Result;
                                        NData.Chloride_CH = item.CONHI ?? false; 
                                        break;

                                    case "DOC":
                                        NData.DOC = item.Result;
                                        NData.DOC_CH = item.CONHI ?? false; 
                                        break;

                                    case "TOTALNITRO":
                                        NData.TotalNitro = item.Result;
                                        NData.TotalNitro_CH = item.CONHI ?? false; 
                                        break;
                                    // Total Phosphorus
                                    case "TOTAL PHOSPHORUS":
                                        NData.TotalPhos = item.Result;
                                        NData.TotalPhos_CH = item.CONHI ?? false; 
                                        break;

                                    case "TSS":
                                        NData.TSS = item.Result;
                                        NData.TSS_CH = item.CONHI ?? false; 
                                        break;

                                    default:
                                        break;
                                }   // end of switch

                                // fill in the rest
                                NData.TypeCode = item.CODE;
                                NData.Batch = item.Batch;
                            }   // end of foreach
                        }   // end of if 

                        // add search for sample id in nutrient bar code table

                        string SN = (from z in RWE.NutrientBarCodes
                                where z.LabID.ToUpper().Equals(bcode.ToUpper())
                                select z.SampleNumber).FirstOrDefault();

                        if (SN != null)
                            NData.SampleNumber = SN; 

                        NData.BARCODE = bcode;
                        NData.Valid = true;
                        NData.Validated = false; 
                        NData.CreatedBy = userName;
                        NData.DateCreated = DateTime.Now;
                        if (!existingRecord)
                        {
                            RWE.NutrientDatas.Add(NData);
                            existingRecord = false;
                        }

                        RWE.SaveChanges();

                    }   // end of foreach                    
                }
            }
            catch (Exception ex)
            {
                string name = "";
                if (userName.Length < 3)
                    name = "Not logged in";
                else
                    name = userName;
                string msg = ex.Message;
                LogError LE = new LogError();
                LE.logError(msg, "Method UpdateNutrients", ex.StackTrace.ToString(), name, "");
            }
        }
    }
}