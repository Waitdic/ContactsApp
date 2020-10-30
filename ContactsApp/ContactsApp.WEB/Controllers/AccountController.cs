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
            return View(new List<Contact> {this.contactManager.GetContacts()});
        }

        [HttpPut]
        [CustomExceptionFilter]
        public IActionResult AddContact(Contact contact)
        {
            this.contactManager.AddContact(contact);
            return null;
        }
    }
}
