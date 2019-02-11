using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication5.Repositories.Contact;

namespace WebApplication5.Controllers
{
    public class ContactController : ApiController
    {
        private readonly IContactRepository _contactRepository = new ContactRepository();

        [HttpGet]
        [Route("api/contacts")]
        public IHttpActionResult GetAllContacts()
        {
            var contacts = _contactRepository.GetAllContacts();

            return Ok(contacts);
        }
    }
}
