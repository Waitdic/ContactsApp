using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using ContactsApp.BLL.Interfaces;
using ContactsApp.WEB.Errors;
using ContactsApp.BLL.Models;

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
        [HttpGet("")]
        public ActionResult<List<ContactViewModel>> GetContacts()
        {
            return this.contactManager.GetContacts();
        }

        /// <summary>Получить контакт.</summary>
        /// <param name="id">Id контакта.</param>
        /// <returns>Контакт.</returns>
        [HttpGet("{id}")]
        [CustomExceptionFilter]
        public ActionResult<ContactViewModel> GetContact(int id)
        {
            return this.contactManager.GetContactById(id);
        }

        /// <summary>Добавить контакт.</summary>
        /// <param name="contact">Контакт.</param>
        [HttpPost]
        [CustomExceptionFilter]
        public IActionResult AddContact(ContactViewModel contact)
        {
            this.contactManager.AddContact(contact);
            return Ok();
        }

        /// <summary>Добавить контакт.</summary>
        /// <param name="contact">Контакт</param>
        [HttpPut]
        [CustomExceptionFilter]
        public IActionResult EditContact(ContactViewModel contact)
        {
            this.contactManager.EditContact(contact);
            return Ok();
        }

        /// <summary>Добавить контакт.</summary>
        /// <param name="id">Id контанта.</param>
        [HttpDelete("{id}")]
        [CustomExceptionFilter]
        public IActionResult DeleteContact(int id)
        {
            this.contactManager.DeleteContact(id);
            return Ok();
        }
    }
}
