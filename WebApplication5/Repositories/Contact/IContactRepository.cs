using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication5.Repositories.Contact
{
    public interface IContactRepository
    {
        IEnumerable<Models.Contact> GetAllContacts();
    }
}
