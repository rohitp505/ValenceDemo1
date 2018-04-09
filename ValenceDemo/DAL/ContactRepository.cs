using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ValenceDemo.Models;

namespace ValenceDemo.DAL
{
    public class ContactRepository : Icontact
    {
        /// <summary>
        /// Singleton pattern
        /// </summary>
        private static ContactRepository instancecontactrepository = new ContactRepository();

        private ContactRepository() { }

        public static ContactRepository Instance
        {
            get
            {
                return instancecontactrepository;
            }
        }

        private VALENCEDBEntitiesTest _context;
       
        public ContactRepository(VALENCEDBEntitiesTest contactcontext)
        {
            this._context = contactcontext;
        }
        public void DeleteContact(string id)
        {

            ContactDetail contactdetail = _context.ContactDetails.Find(id);
            _context.ContactDetails.Remove(contactdetail);
        }


        public async Task<ContactDetail> GetContactDetailsById(string id)
        {
            return await _context.ContactDetails.FindAsync(id);

        }

        public async Task<IEnumerable<ContactDetail>> GetDetails()
        {
            return await _context.ContactDetails.ToListAsync();
        }

        public void InsertContact(ContactDetail contactdetail)
        {
            _context.ContactDetails.Add(contactdetail);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateContact(ContactDetail contactdetail)
        {

            _context.Entry(contactdetail).State = EntityState.Modified;

        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool Isexist(string id)
        {
           return _context.ContactDetails.Count(e => e.Email == id) > 0;
        }
    }
}