using System;
using System.Linq;
using ContactsApp.BLL.Models;
using ContactsApp.DAL.Repository;
using ContactsApp.WEB.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Assert = Xunit.Assert;

namespace UnitTests
{
    public class AccountControllerTests
    {
        private readonly ContactManager contactManager = new ContactManager(new ContactRepository());
        private readonly AccountController accountController = new AccountController(new ContactManager(new ContactRepository()));

        /// <summary>
        /// Тест на создание добавления нового контакта.
        /// </summary>
        [Fact]
        public void AddNewContactTest()
        {
            // Arrange
            var newContact = this.AddNewContactViewModel();

            // Act
            var result = this.accountController.AddContact(newContact);
            var models = this.contactManager.GetContacts();
       
            // Assert
            Assert.Equal(typeof(OkResult), result.GetType());
            Assert.Contains(models, x => x.Name == newContact.Name);

            var model = models.FirstOrDefault(x => x.Name == newContact.Name);
            Assert.Equal(newContact.Surname, model.Surname);
            Assert.Equal(newContact.Birthday, model.Birthday);
            Assert.Equal(newContact.Email, model.Email);
            Assert.Equal(newContact.Phone, model.Phone);
            Assert.Equal(newContact.Vk, model.Vk);
        }

        /// <summary>
        /// Тест на получения списка контактов.
        /// </summary>
        [Fact]
        public void GetContactsTest()
        {
            // Arrange
            var number = this.contactManager.GetContacts().Count;
            this.contactManager.AddContact(this.AddNewContactViewModel());

            // Act
            var contact = this.accountController.GetContacts();

            // Assert    
            Assert.NotNull(contact);
            Assert.Equal(number + 1, contact.Value.Count);
        }

        /// <summary>
        /// Тест на редактирование контакта.
        /// </summary>
        [Fact]
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
            Assert.Equal(typeof(OkResult), result.GetType());
            Assert.NotNull(contactList.Value);

            var model = contactList.Value.FirstOrDefault(c => c.Id == newContact.Id);
            Assert.Equal(newContact.Name, model?.Name);
            Assert.Equal(newContact.Surname, model?.Surname);
            Assert.Equal(newContact.Birthday, model?.Birthday);
            Assert.Equal(newContact.Email, model?.Email);
            Assert.Equal(newContact.Phone, model?.Phone);
            Assert.Equal(newContact.Vk, model?.Vk);
        }

        /// <summary>
        /// Тест на получение контакта по id.
        /// </summary>
        [Fact]
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
        [Fact]
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
