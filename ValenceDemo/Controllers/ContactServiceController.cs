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
using ValenceDemo.Models;

namespace ValenceDemo.Controllers
{
    public class ContactServiceController : ApiController
    {
        private VALENCEDBEntitiesTest db = new VALENCEDBEntitiesTest();

        // GET: api/ContactService
        public IQueryable<ContactDetail> GetContactDetails()
        {
            return db.ContactDetails;
        }

        // GET: api/ContactService/5
        [ResponseType(typeof(ContactDetail))]
        public async Task<IHttpActionResult> GetContactDetail(string id)
        {
            ContactDetail contactDetail = await db.ContactDetails.FindAsync(id);
            if (contactDetail == null)
            {
                return NotFound();
            }

            return Ok(contactDetail);
        }

        // PUT: api/ContactService/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContactDetail( ContactDetail contactDetail)
        {
            string id = contactDetail.Email;
            contactDetail.CreateDate = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactDetail.Email)
            {
                return BadRequest();
            }

            db.Entry(contactDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ContactService
        [ResponseType(typeof(ContactDetail))]
        public async Task<IHttpActionResult> PostContactDetail(ContactDetail contactDetail)
        {
            contactDetail.CreateDate = DateTime.Now;
          //contactDetail.Status=0;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContactDetails.Add(contactDetail);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContactDetailExists(contactDetail.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = contactDetail.Email }, contactDetail);
        }

        // DELETE: api/ContactService/5
        [ResponseType(typeof(ContactDetail))]
        public async Task<IHttpActionResult> DeleteContactDetail(string id)
        {
            ContactDetail contactDetail = await db.ContactDetails.FindAsync(id);
            contactDetail.Status = false;
            if (contactDetail == null)
            {
                return NotFound();
            }

            db.Entry(contactDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch
            {

            }

            return Ok(contactDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactDetailExists(string id)
        {
            return db.ContactDetails.Count(e => e.Email == id) > 0;
        }
    }
}