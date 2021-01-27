using System;
using System.Configuration;
using System.Linq;
using ContactsApp.BLL.Models;
using ContactsApp.DAL.Repository;
using ContactsApp.WEB.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace UnitTests
{
    public class AccountControllerTests
    {
        private readonly ContactManager contactManager = new ContactManager(new ContactRepository());
        private readonly AccountController accountController = new AccountController(new ContactManager(new ContactRepository()));

        [SetUp]
        public void Initialize()
        {
            ConfigurationManager.AppSettings.Set("DbFolder", @"..\..\json.txt");
        }

        /// <summary>
        /// Тест на создание добавления нового контакта.
        /// </summary>
        [Test]
        public async void AddNewContactTest()    
        {
            // Arrange
            var newContact = this.AddNewContactViewModel();

            // Act
            var result = this.accountController.AddContact(newContact);
            var model = result..AsEnumerable().OrderBy(x => x.Id).LastOrDefault();;
       
            // Assert
            Assert.AreEqual(typeof(OkResult), result.Result.GetType());
            Assert.NotNull(model);
            Assert.AreEqual(newContact.Name, model.Name);
            Assert.AreEqual(newContact.Surname, model.Surname);
            Assert.AreEqual(newContact.Birthday, model.Birthday);
            Assert.AreEqual(newContact.Email, model.Email);
            Assert.AreEqual(newContact.Phone, model.Phone);
            Assert.AreEqual(newContact.Vk, model.Vk);
        }

        /// <summary>
        /// Тест на получения списка контактов.
        /// </summary>
        [Test]
        public void GetContactsTest()
        {
            // Arrange
            var number = this.contactManager.GetContacts().Count;
            this.contactManager.AddContact(this.AddNewContactViewModel());

            // Act
            var contact = this.accountController.GetContacts();

            // Assert    
            Assert.NotNull(contact);
            Assert.AreEqual(number + 1, contact.Value.Count);
        }
                 
        /// <summary>
        /// Тест на редактирование контакта.
        /// </summary>
        [Test]
        public void EditContactTest()
        {
            // Arrange
            var contact = this.AddNewContactViewModel();
            this.contactManager.AddContact(contact);
            var contactID = this.contactManager.GetContacts()
                .FirstOrDefault(x => x.Name == contact.Name && x.Surname == contact.Surname)?.Id;

            var newContact = this.AddNewContactViewModel();
            newContact.Id = contactID;

            // Act
            var result = this.accountController.EditContact(newContact);
            var contactList = this.accountController.GetContacts();

            // Assert    
            Assert.AreEqual(typeof(OkResult), result.GetType());
            Assert.NotNull(contactList.Value);

            var model = contactList.Value.FirstOrDefault(c => c.Id == newContact.Id);
            Assert.AreEqual(newContact.Name, model?.Name);
            Assert.AreEqual(newContact.Surname, model?.Surname);
            Assert.AreEqual(newContact.Birthday, model?.Birthday);
            Assert.AreEqual(newContact.Email, model?.Email);
            Assert.AreEqual(newContact.Phone, model?.Phone);
            Assert.AreEqual(newContact.Vk, model?.Vk);
        }

        /// <summary>
        /// Тест на получение контакта по id.
        /// </summary>
        [Test]
        public void GetContactByIdTest()
        {
            // Arrange
            this.contactManager.AddContact(this.AddNewContactViewModel());

            // Act
            var contact = this.contactManager.GetContacts();

            // Assert    
            Assert.NotNull(contact);
        }

        /// <summary>
        /// Тест на удаление контакта.
        /// </summary>
        [Test]
        public void DeleteContact()
        {
            // Arrange
            var contact = this.AddNewContactViewModel();
            this.contactManager.AddContact(contact);
            var contactID = this.contactManager.GetContacts()
                .FirstOrDefault(x => x.Name == contact.Name && x.Surname == contact.Surname)?.Id;

            // Act
            var result = this.accountController.DeleteContact(contactID.Value);
            var contactList = this.accountController.GetContacts().Value;

            // Assert    
            Assert.True(contactList.All(c => c.Id != contactID));
        }

        private ContactViewModel AddNewContactViewModel()
        {
            return new ContactViewModel()
            {
                Name = "Name" + Guid.NewGuid().ToString(),
                Surname = "Surname" + Guid.NewGuid().ToString(),
                Birthday = DateTime.Now.Date,
                Phone = "8-111-111-11-11",
                Vk = "daniska1616",
                Email = "danis161616@yandex.ru",
            };
        }
    }
}
