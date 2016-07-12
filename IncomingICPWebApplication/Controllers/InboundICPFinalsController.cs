using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using IncomingICPWebApplication.Models;
using System.Web.Http;
using System.Net.Http;
using System.Text; 

//using System.Web.Http.ModelBinding;
//using System.Web.Http.Routing;

namespace IncomingICPWebApplication.Controllers
{
    public class InboundICPFinalsController : ApiController
    {
      //  private IncomingICPWebApplicationContext db = new IncomingICPWebApplicationContext();

        // GET: api/InboundICPFinals
        //public IQueryable<InboundICPFinal> GetInboundICPFinals()
        //{
        //    return db.InboundICPFinals;
        //}

        // GET: api/InboundICPFinals/5
        [ResponseType(typeof(InboundICPFinal))]
        //public async Task<IHttpActionResult> GetInboundICPFinal(int id)
        //{
        //    InboundICPFinal inboundICPFinal = await db.InboundICPFinals.FindAsync(id);
        //    if (inboundICPFinal == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(inboundICPFinal);
        //}

        // PUT: api/InboundICPFinals/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInboundICPFinal(int id, InboundICPFinal inboundICPFinal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inboundICPFinal.ID)
            {
                return BadRequest();
            }

          //  db.Entry(inboundICPFinal).State = EntityState.Modified;

            //try
            //{
            //    await db.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!InboundICPFinalExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

          //  return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(System.Net.HttpStatusCode.NoContent); 
        }

        // POST: api/InboundICPFinals
        [ResponseType(typeof(InboundICPFinal))]
        public async Task<IHttpActionResult> PostInboundICPFinal(InboundICPFinal inboundICPFinal)
        {
            RiverWatchEntities RWDE = new RiverWatchEntities();
            int res = 0;
            int id = 0;
            string strDebug = "";

           System.Diagnostics.Debug.WriteLine("here in start of post code"); 
           string[] parts = UTF8Encoding.UTF8.GetString(Convert.FromBase64String(Request.Headers.Authorization.Parameter)).Split(':');


        // I think this is all we need to do here

           if (parts[0] != "Bill")
               return Unauthorized();         

           //if(parts[1] != "Password1")
           //    return Unauthorized();            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                RWDE.InboundICPFinals.Add(inboundICPFinal);
                res = await   RWDE.SaveChangesAsync();
                id = inboundICPFinal.ID; // get from last write                
            }
            catch(Exception ex)
            {
                string msg = string.Format("Data base write failed with error: {0}", ex.Message); 
                return BadRequest(msg); 
            }
            
            //db.InboundICPFinals.Add(inboundICPFinal);
            //await db.SaveChangesAsync();            

            return CreatedAtRoute("DefaultApi", new { id = inboundICPFinal.ID }, inboundICPFinal);
        }

        // DELETE: api/InboundICPFinals/5
   //     [ResponseType(typeof(InboundICPFinal))]
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
      //  }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool InboundICPFinalExists(int id)
        //{
        //    return db.InboundICPFinals.Count(e => e.ID == id) > 0;
        //}

        //public static string GetHeader(this HttpRequestMessage request, string key)
        //{
        //    IEnumerable<string> keys = null;
        //    if (!request.Headers.TryGetValues(key, out keys))
        //        return null;

        //    return keys.First();
        //}
        }
}