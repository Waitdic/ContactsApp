using System.Configuration;
using System.Linq;
using ContactsApp.BLL.Models;
using ContactsApp.DAL.Repository;
using ContactsApp.WEB.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace UnitTests
{
    /// <summary>
    /// Тесты на проверку AccountController
    /// </summary>
    [TestFixture]
    public class AccountControllerTests
    {
        private readonly ContactManager contactManager = new ContactManager(new ContactRepository());
        private readonly AccountController accountController = new AccountController(new ContactManager(new ContactRepository()));
        
        private static void Initialize()
        {
            ConfigurationManager.AppSettings.Set("DbFolder", @"..\..\json.txt");
        }
        
        private static void Clean()
        {
            ContactHelper.CleanDb();
        }

        /// <summary>
        /// Тест на создание добавления нового контакта.
        /// </summary>
        [Test]
        public void AddContact_NewContact_CorrectResult()    
        {
            // SetUp
            Initialize();
            var newContact = ContactHelper.AddNewContactViewModel();

            // Act
            this.accountController.AddContact(newContact);
            var model = this.contactManager.GetContacts().OrderBy(x => x.Id).LastOrDefault();
       
            // Assert
            Assert.NotNull(model);
            Assert.AreEqual(newContact.Name, model.Name);
            Assert.AreEqual(newContact.Surname, model.Surname);
            Assert.AreEqual(newContact.Birthday, model.Birthday);
            Assert.AreEqual(newContact.Email, model.Email);
            Assert.AreEqual(newContact.Phone, model.Phone);
            Assert.AreEqual(newContact.Vk, model.Vk);
            Clean();
        }

        /// <summary>
        /// Тест на получения списка контактов.
        /// </summary>
        [Test]
        public void GetContacts_CorrectResult()
        {
            // SetUp
            Initialize();
            this.contactManager.AddContact(ContactHelper.AddNewContactViewModel());
            var number = this.contactManager.GetContacts().Count;
            this.contactManager.AddContact(ContactHelper.AddNewContactViewModel());

            // Act
            var contact = this.accountController.GetContacts();

            // Assert    
            Assert.NotNull(contact);
            Assert.AreEqual(number + 1, contact.Value.Count);
            Clean();
        }
                 
        /// <summary>
        /// Тест на редактирование контакта.
        /// </summary>
        [Test]
        public void EditContact_Contact_CorrectResult()
        {
            // SetUp
            Initialize();
            var contact = ContactHelper.AddNewContactViewModel();
            this.contactManager.AddContact(contact);
            var contactID = this.contactManager.GetContacts()
                .FirstOrDefault(x => x.Name == contact.Name && x.Surname == contact.Surname)?.Id;

            var newContact = ContactHelper.AddNewContactViewModel();
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
            Clean();
        }

        /// <summary>
        /// Тест на получение контакта по id.
        /// </summary>
        [Test]
        public void GetContact_Id_CorrectResult()
        {
            // SetUp
            Initialize();
            this.contactManager.AddContact(ContactHelper.AddNewContactViewModel());
            var contacts = this.contactManager.GetContacts();
            var id = contacts.LastOrDefault()?.Id;
            
            // Act
            var contact = this.accountController.GetContact(id.GetValueOrDefault());

            // Assert    
            Assert.NotNull(contact);
            Clean();
        }

        /// <summary>
        /// Тест на удаление контакта.
        /// </summary>
        [Test]
        public void DeleteContact_Id_CorrectResult()
        {
            // SetUp
            Initialize();
            this.contactManager.AddContact(ContactHelper.AddNewContactViewModel());
            this.contactManager.AddContact(ContactHelper.AddNewContactViewModel());
            var contact = ContactHelper.AddNewContactViewModel();
            this.contactManager.AddContact(contact);
            var contactID = this.contactManager.GetContacts()
                .FirstOrDefault(x => x.Name == contact.Name && x.Surname == contact.Surname)?.Id;

            // Act
            accountController.DeleteContact(contactID.Value);
            var contactList = this.accountController.GetContacts().Value;

            // Assert    
            Assert.True(contactList.All(c => c.Id != contactID));
            Clean();
        }
    }
}
