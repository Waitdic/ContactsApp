using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using ContactsApp.BLL.Interfaces;
using ContactsApp.WEB.Errors;
using ContactsApp.BLL.Models;
using ContactsApp.DAL.Models;

namespace ContactsApp.WEB.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class AccountController : Controller
    {
        private readonly IContactManager contactManager;

        public AccountController(IContactManager contactManager)
        {
            this.contactManager = contactManager;
        }

        /// <summary>Получить все контакты все контакты.</summary>
        /// <returns>Список контактов.</returns>
        [HttpGet]
        public ActionResult<List<Contact>> GetContacts()
        {
            return this.contactManager.GetContacts();
        }

        /// <summary>Получить контакт.</summary>
        /// <param name="id">Id контакта.</param>
        /// <returns>Контакт.</returns>
        [HttpGet]
        [CustomExceptionFilter]
        public ActionResult<Contact> GetContact(int id)
        {
            return null;
        }

        /// <summary>Добавить контакт.</summary>
        /// <param name="contact">Контакт.</param>
        [HttpPut]
        [CustomExceptionFilter]
        public IActionResult AddContact(Contact contact)
        {
            this.contactManager.AddContact(contact);
            return null;
        }

        /// <summary>Добавить контакт.</summary>
        /// <param name="contact">Контакт</param>
        [HttpPut]
        [CustomExceptionFilter]
        public IActionResult EditContact(Contact contact)
        {
            this.contactManager.AddContact(contact);
            return null;
        }

        /// <summary>Добавить контакт.</summary>
        /// <param name="id">Id контанта.</param>
        [HttpPut]
        [CustomExceptionFilter]
        public IActionResult DeleteContact(int id)
        {
            return null;
        }
    }
}
