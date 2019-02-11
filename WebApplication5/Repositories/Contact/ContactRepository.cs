using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Repositories.Contact
{
    public class ContactRepository: IContactRepository
    {
        private IMongoCollection<Models.Contact> _contacts;

        public ContactRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");
            _contacts = database.GetCollection<Models.Contact>("Contact");
        }

        public IEnumerable<Models.Contact> GetAllContacts()
        {
            var contacts = _contacts.Find(_ => true).ToList();

            return contacts;
        }
    }
}