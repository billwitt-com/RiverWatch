using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1;

namespace WebApplication1.Controllers
{
    public class InboundICPFinalsController : ApiController
    {
        private RiverWatchEntities db = new RiverWatchEntities();   // our local entity for data base

        // GET: api/InboundICPFinals/5
        // get a single ipcfinal result from ID
        [ResponseType(typeof(InboundICPFinal))]
        public async Task<IHttpActionResult> GetInboundICPFinal(int id)
        {
            InboundICPFinal inboundICPFinal = await db.InboundICPFinals.FindAsync(id);
            if (inboundICPFinal == null)
            {
                return NotFound();
            }

            return Ok(inboundICPFinal);
        }


        // POST: api/InboundICPFinals
        // writes a new record and returns a copy of data written, or failure messages 
        [ResponseType(typeof(InboundICPFinal))]      
        public async Task<IHttpActionResult> PostInboundICPFinal(InboundICPFinal inboundICPFinal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            System.Diagnostics.Debugger.Log(3, "message", "This is the message");

            db.InboundICPFinals.Add(inboundICPFinal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = inboundICPFinal.ID }, inboundICPFinal);
        }

        private bool InboundICPFinalExists(int id)
        {
            return db.InboundICPFinals.Count(e => e.ID == id) > 0;
        }

        // check to see if the barcode is already known in icpFinals or metalbarcodes. 
        private bool InboundICPBCExists(string bc)
        {
            if( db.InboundICPFinals.Count(e => e.CODE == bc) > 0)
            {
                return true;
            }
            if(db.MetalBarCodes.Count(z => z.LabID == bc) > 0)
            {
                return true;
            }

            return false; 
        }

        // check sample table 
        private bool InboundICPSampleExists(string samp)
        {
            if(db.Samples.Count(e => e.NumberSample == samp) > 0)
            {
                return true; 
            }

            return false;
        }




        // DELETE: api/InboundICPFinals/5
        // we won't be using this any time soon... 
        //[ResponseType(typeof(InboundICPFinal))]
        //public async Task<IHttpActionResult> DeleteInboundICPFinal(int id)
        //{
        //    InboundICPFinal inboundICPFinal = await db.InboundICPFinals.FindAsync(id);
        //    if (inboundICPFinal == null)
        //    {
        //        return NotFound();
        //    }

        //    db.InboundICPFinals.Remove(inboundICPFinal);
        //    await db.SaveChangesAsync();

        //    return Ok(inboundICPFinal);
        //}

        // won't be useing this any time soon, either... 
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}


        // PUT: api/InboundICPFinals/5
        // update a record by ID
        // not used in this application
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutInboundICPFinal(int id, InboundICPFinal inboundICPFinal)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != inboundICPFinal.ID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(inboundICPFinal).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!InboundICPFinalExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}


        // GET: api/InboundICPFinals
        // gets them all, not really useful
        //public IQueryable<InboundICPFinal> GetInboundICPFinals()
        //{
        //    return db.InboundICPFinals;
        //}


    }
}