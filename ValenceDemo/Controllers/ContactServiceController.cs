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
using ValenceDemo.DAL;
using HandleLog;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using ValenceDemo.Filters;

namespace ValenceDemo.Controllers
{
    /// <summary>
    /// Attribute base routing,Singleton pattern ,Repository used for crud operation,
    /// Exception filter with different dll(HandleLog),Filter,Async and await call,
    /// </summary>
    [RoutePrefix("api/ContactService")]
    public class ContactServiceController : ApiController
    {
        private ILogException _ILogException;

        private ContactRepository _contactrepository;

        public ContactServiceController()
        {
           
            this._contactrepository = new ContactRepository(new VALENCEDBEntitiesTest());
            this._ILogException = logGenerator.CreateInstance;
        }

        [Route("AllContacts")]
        //logging and exception handeling
        [LogExceptionFilter]
        public async Task<IHttpActionResult> GetContactDetails()
        {
            var d = 0;
            d = 5 / d;
            IEnumerable<ContactDetail> contactdetail = await _contactrepository.GetDetails();

            return Ok(contactdetail);
        }

        [Route("Contact/{id}")]
        [ResponseType(typeof(ContactDetail))]
        public async Task<IHttpActionResult> GetContactDetail(string id)
        {
            ContactDetail contactDetail = await _contactrepository.GetContactDetailsById(id);
            if (contactDetail == null)
            {
                return NotFound();
            }


            return Ok(contactDetail);
        }

        [Route("UpdateContact")]
        [HttpPut]
        [ResponseType(typeof(void))]
        [LogExceptionFilter]
        public IHttpActionResult PutContactDetail(ContactDetail contactDetail)
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

            _contactrepository.UpdateContact(contactDetail);

            try
            {
                _contactrepository.Save();
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

        [Route("CreateContact")]
        [ResponseType(typeof(ContactDetail))]
        [LogExceptionFilter]
        public IHttpActionResult PostContactDetail(ContactDetail contactDetail)
        {
            contactDetail.CreateDate = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _contactrepository.InsertContact(contactDetail);

            try
            {
                _contactrepository.Save();
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

        [Route("DeleteContact/{id}")]
        [ResponseType(typeof(ContactDetail))]
        [LogExceptionFilter]
        public async Task<IHttpActionResult> DeleteContactDetail(string id)
        {
            // ContactDetail contactdetail = _contactrepository.ContactDetails.Where(x => x.Email.Equals(id) && x.Status == true).FirstOrDefault();
            ContactDetail contactdetail = await _contactrepository.GetContactDetailsById(id);
            contactdetail.Status = true;



            if (contactdetail == null)
            {
                return NotFound();
            }

            _contactrepository.UpdateContact(contactdetail);

            try
            {
                _contactrepository.Save();
            }
            catch
            {

            }

            return Ok(contactdetail);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _contactrepository.Dispose();
            }
            base.Dispose(disposing);
        }

        [LogExceptionFilter]
        private bool ContactDetailExists(string id)
        {
            return _contactrepository.Isexist(id);
        }




    }
}