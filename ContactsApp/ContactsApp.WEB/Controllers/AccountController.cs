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
            return View(this.contactManager.GetContacts());
        }

        /// <summary>Получить контакт.</summary>
        /// <returns>Контакт.</returns>
        [HttpGet]
        [CustomExceptionFilter]
        public ActionResult<Contact> GetContact(int id)
        {
            return null;
            // return View(this.contactManager.GetContactById(id));
        }

        /// <summary>Добавить контакт.</summary>
        [HttpGet]
        [CustomExceptionFilter]
        public IActionResult AddContact()
        {
            return PartialView(new Contact());
        }

        /// <summary>Добавить контакт.</summary>
        [HttpPut]
        [CustomExceptionFilter]
        public IActionResult AddContact(Contact contact)
        {
            this.contactManager.AddContact(contact);
            return Redirect("ContactsList");
        }
    }
}
