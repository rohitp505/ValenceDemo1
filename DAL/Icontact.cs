using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValenceDemo.Models;

namespace ValenceDemo.DAL
{
    interface Icontact : IDisposable
    {
        Task<IEnumerable<ContactDetail>> GetDetails();
        Task<ContactDetail> GetContactDetailsById(string id);
        void InsertContact(ContactDetail con);
        void DeleteContact(string id);
        void UpdateContact(ContactDetail con);
        void Save();
        bool Isexist(string id);
    }
}
