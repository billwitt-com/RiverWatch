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
using RWInbound2;
using System.Net.Http.Headers; 

//using System.Web.Http.ModelBinding;
//using System.Web.Http.Routing;

namespace IncomingICPWebApplication.Controllers
{
    public class InboundICPFinalsController : ApiController
    {
      // we are only implementing POST here,

        // POST: api/InboundICPFinals
        [ResponseType(typeof(InboundICPFinal))]
        public async Task<IHttpActionResult> PostInboundICPFinal(InboundICPFinal inboundICPFinal)
        {
           // RiverWatchEntities RWDE = new RiverWatchEntities();
            int res = 0;
            int id = 0;
            string strDebug = "";


            return BadRequest(ModelState);
           System.Diagnostics.Debug.WriteLine("here in start of post code"); // would like this to work!!        
           System.Diagnostics.Trace.WriteLine("whatever");
          

           if (!ModelState.IsValid)
           {
               return BadRequest(ModelState);
           }
  
            // cut from msdn web site
            HttpRequestMessage request = ControllerContext.Request; // this seems to be same as request 
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            if (authorization == null)
            {
                // No authentication was attempted (for this authentication method).
                // Do not set either Principal (which would indicate success) or ErrorResult (indicating an error).
                return Unauthorized(); 
            }

            if (authorization.Scheme != "Basic")
            {
                // No authentication was attempted (for this authentication method).
                // Do not set either Principal (which would indicate success) or ErrorResult (indicating an error).
                return Unauthorized(); 
            }

            if (String.IsNullOrEmpty(authorization.Parameter))
            {
                // Authentication was attempted but failed. Set ErrorResult to indicate an error.
                return Unauthorized();  // XXXX do this for now
                //Context.ErrorResult = new AuthenticationFailureResult("Missing credentials", request);
                //return;
                return Unauthorized(); 
            }

         

            byte[] credentialBytes;

            try
            {
                credentialBytes = Convert.FromBase64String(authorization.Parameter);
            }
            catch (FormatException)
            {
                return null;
            }

            // The currently approved HTTP 1.1 specification says characters here are ISO-8859-1.
            // However, the current draft updated specification for HTTP 1.1 indicates this encoding is infrequently
            // used in practice and defines behavior only for ASCII.
            Encoding encoding = Encoding.ASCII;
            // Make a writable copy of the encoding to enable setting a decoder fallback.
            encoding = (Encoding)encoding.Clone();
            // Fail on invalid bytes rather than silently replacing and continuing.
            encoding.DecoderFallback = DecoderFallback.ExceptionFallback;
            string decodedCredentials;

            try
            {
                decodedCredentials = encoding.GetString(credentialBytes);
            }
            catch (DecoderFallbackException)
            {
                return null;
            }

            if (String.IsNullOrEmpty(decodedCredentials))
            {
                return null;
            }

            int colonIndex = decodedCredentials.IndexOf(':');

            if (colonIndex == -1)
            {
                return null;
            }

            string userName = decodedCredentials.Substring(0, colonIndex);
            string password = decodedCredentials.Substring(colonIndex + 1);        

        // I think this is all we need to do here
           if (userName != "Bill")
               return Unauthorized();

           if (password != "Password1")
               return Unauthorized();    

            //try
            //{
            //    RWDE.InboundICPFinals.Add(inboundICPFinal);
            //    res = await   RWDE.SaveChangesAsync();
            //    id = inboundICPFinal.ID; // get from last write                
            //}
            //catch(Exception ex)
            //{
            //    string msg = string.Format("Data base write failed with error: {0}", ex.Message); 
            //    return BadRequest(msg); 
            //}
            
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