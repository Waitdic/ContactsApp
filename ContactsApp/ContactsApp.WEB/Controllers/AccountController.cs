using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using ContactsApp.BLL.Interfaces;
using ContactsApp.WEB.Errors;
using ContactsApp.BLL.Models;

namespace ContactsApp.WEB.Controllers
{
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
        public ActionResult<List<Contact>> ContactsList()
        {
            return null;
        }

        /// <summary>Получить контакт.</summary>
        /// <returns>Контакт.</returns>
        [HttpGet]
        public ActionResult<Contact> GetContact()
        {
            return this.contactManager.GetContact();
        }

        /// <summary>Добавить контакт.</summary>
        [HttpPut]
        [CustomExceptionFilter]
        public IActionResult AddContact(Contact contact)
        {
            this.contactManager.AddContact(contact);
            return null;
        }
    }
}
