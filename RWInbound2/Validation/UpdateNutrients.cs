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
            int recordsProcessed = 0;
            try
            {
                RiverWatchEntities RWE = new RiverWatchEntities();
                NutrientData NData = null;
                bool existingRecord = false;
                string conStr = RWE.Database.Connection.ConnectionString;
                string SampNumber = "";
                List<string> BC = null; // keep these in scope
                NutrientData TEST = null;
                int recordsSaved = 0;
                
                // first process the blanks and dups and tests etc. as we don't need them for validation
                //string name = "profile";
                //string msg = string.Format("Starting Lachat Query Start process at {0}", DateTime.Now);
                //LogError LE = new LogError();
                //LE.logError(msg, "UpdateNutrients", "", name, "Profiling");

                // we do all of these, every time, but it is quite fast
                try
                {
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
                    //msg = string.Format("Lachats not marked valid marked valid at {0} for {1} records", DateTime.Now, L.Count());
                    //LE.logError(msg, "UpdateNutrients", "", name, "Profiling");

                    // now get list of not validated but valid real sample barcodes to process
                    BC = (from bc in RWE.Lachats
                                       where bc.SampleType.ToUpper().Contains("RW") == true & (bc.CODE == "05" | bc.CODE == "25" | bc.CODE == "35")
                                       & bc.Validated == false & bc.Valid == true & bc.BlkDup == false
                                       select bc.SampleType).Distinct().ToList<string>();
                    BC.Sort();
                    BC.Reverse(); // put newest elements first
                    recordsProcessed = BC.Count;
                }
                catch(Exception ex)
                {
                    string e = ex.Message;
                }


                //LogError LE = new LogError();
                //string msg = string.Format("Processing {1} Lachats barcodes marked Valid to nutrient Data at {0} ", DateTime.Now, recordsProcessed);
                //LE.logError(msg, "UpdateNutrients", "", conStr, "Profiling");
                
                if (BC != null)
                {
                    recordsProcessed = 0; 
                    int bcCount = BC.Count;                    
                    
                    foreach (string bcode in BC)
                    {
                        // get raw data from lachat table for this bar code, could be no values or could be six or more
                        //msg = string.Format("Start {1} Lachats not marked Valid to nutrient Data at {0} ", DateTime.Now, recordsProcessed);
                        //LE.logError(msg, "UpdateNutrients", "", name, "Profiling");
                        

                        var D = from d in RWE.Lachats
                                where d.SampleType.ToUpper() == bcode.ToUpper() & d.Valid == true & d.Validated == false & d.CODE != null & d.SampleType != null
                                select d;

                        string SN = (from z in RWE.NutrientBarCodes
                                     where z.LabID.ToUpper().Equals(bcode.ToUpper())
                                     select z.SampleNumber).FirstOrDefault();
                        if (SN != null)
                            SampNumber = SN;
                        else
                            continue; // no sample number, so we don't care... 

                        // see if this bar code exists in the nutrient data table, if not, insert it

                        // I don't think we need to test for valid or validated. If the BC is there, modify it. If not, make a new one
                        // this is not correct
                        // XXXX or is it, why would we put an already validated record into this new table?
                        //NutrientData TEST = (NutrientData)(from nd in RWE.NutrientDatas
                        //                                   where nd.BARCODE == bcode & nd.Valid == true & nd.Validated == false
                        //                                   select nd).FirstOrDefault();

                        // get all entries so we don't build dups

                        TEST = (NutrientData)(from nd in RWE.NutrientDatas
                                                           where nd.BARCODE.ToUpper() == bcode.ToUpper() // & nd.Valid == true & nd.Validated == false 
                                                           select nd).FirstOrDefault();

                        // we will overwrite any existing data, which should not happen often, but perhaps in a partial update where there are addtional samples
                        if (TEST != null)  
                        {
                            NData = TEST; // we have existing entries, so use this entity
                            existingRecord = true; // flag for later
                        }
                        else
                        {
                            NData = new NutrientData();
                            existingRecord = false;
                        }

                        // build one nutrientdata row for each group of barcodes in lachat table.
                        if (D != null)
                        {
                            foreach (var item in D) // there are multiple ROWS for a barcode in the lachat table, process each one
                            {
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
                                NData.TypeCode = ""; // force this since there seems to be some junk in the types in lachet data
                                
                                if (item.CODE != null)
                                {
                                    if(item.CODE.Length > 0)
                                        NData.TypeCode = item.CODE.Trim();
                                    else
                                        NData.TypeCode = ""; 
                                }
                                else
                                {
                                    NData.TypeCode = ""; 
                                }
                                if (item.Batch != null)
                                {
                                    if (item.Batch.Length > 0)
                                        NData.Batch = item.Batch;
                                    else
                                        NData.Batch = "";
                                }
                                else
                                {
                                    NData.Batch = ""; 
                                }


                            }   // end of foreach
                        }   // end of if 

                        // add search for sample id in nutrient bar code table

                        //string SN = (from z in RWE.NutrientBarCodes
                        //        where z.LabID.ToUpper().Equals(bcode.ToUpper())
                        //        select z.SampleNumber).FirstOrDefault();

                       // if (SN != null)
                        NData.SampleNumber = SampNumber; 

                        NData.BARCODE = bcode;
                        // set these values just in case
                        //NData.Valid = true;
                        //NData.Validated = false;
                        if (!existingRecord)    // only update these if this is a new record
                        {
                            NData.Valid = true;
                            NData.Validated = false; // we can mark false since we only chose Lachat data that was not validated
                        }
                        NData.CreatedBy = userName;
                        NData.DateCreated = DateTime.Now;

                        // we are processing a single lachat row
                        // some of these will be written if case stmt matched. Not all case statements will be 'hit'
                        // preload so there are no null values for control display

                        if (NData.Ammonia_CH == null)
                            NData.Ammonia_CH = false;
                        if (NData.Sulfate_CH == null)
                            NData.Sulfate_CH = false;
                        if (NData.NitrateNitrite_CH == null)
                            NData.NitrateNitrite_CH = false;
                        if (NData.OrthoPhos_CH == null)
                            NData.OrthoPhos_CH = false;
                        if (NData.ChlorA_CH == null)
                            NData.ChlorA_CH = false;
                        if(NData.Chloride_CH == null)
                            NData.Chloride_CH = false;
                        if (NData.DOC_CH == null)
                            NData.DOC_CH = false;
                        if (NData.TotalNitro_CH == null)
                            NData.TotalNitro_CH = false;
                        if (NData.TotalPhos_CH == null)
                            NData.TotalPhos_CH = false;
                        if (NData.TSS_CH == null)
                            NData.TSS_CH = false;
                       
                        if (!existingRecord)
                        {
                            RWE.NutrientDatas.Add(NData);
                            existingRecord = false;
                        }
                        try
                        {
                            recordsSaved = RWE.SaveChanges();
                        }
                        catch(Exception ex)
                        {
                            string stop = "stop";

                        }
                        recordsProcessed++; 
                        //msg = string.Format("Finished writing {1} Lachat to nutrient Data at {0} ", DateTime.Now, recordsProcessed);
                        //LE.logError(msg, "UpdateNutrients", "", name, "Profiling");

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
            LogError LE1 = new LogError();
            string msg1 = string.Format("Finished Processing {1} Lachats barcodes marked Valid to nutrient Data at {0} ", DateTime.Now, recordsProcessed);
            LE1.logError(msg1, "UpdateNutrients", "", "DEV" , "Profiling");
        }
    }
}