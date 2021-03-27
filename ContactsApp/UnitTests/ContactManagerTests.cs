using System;
using System.Configuration;
using System.Linq;
using ContactsApp.BLL.Models;
using ContactsApp.DAL.Repository;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class ContactManagerTests
    {
        private readonly ContactManager contactManager = new ContactManager(new ContactRepository());
        private readonly ContactRepository contactRepository = new ContactRepository();

        private ContactViewModel contact;
        
        [SetUp]
        public void Initialize()
        {
            ConfigurationManager.AppSettings.Set("DbFolder", @"..\..\json.txt");
            
            this.contact = ContactHelper.AddNewContactViewModel();
            this.contactManager.AddContact(this.contact);
        }

        [OneTimeTearDown]
        public void Clear()
        {
           ContactHelper.CleanDb();
        }

        /// <summary>
        /// Тест на добавление контакта.
        /// </summary>
        [Test]
        public void AddContactTest()
        {
            var model = this.contactRepository
                .GetContacts()
                ?.Where(x => x.Name == this.contact.Name)
                .ToList();
            
            Assert.NotNull(model);
            Assert.AreEqual(model.Count, 1);
            Assert.AreEqual(model.FirstOrDefault()?.Name, this.contact.Name);
        }
        
        /// <summary>
        /// Тест на изменение контакта.
        /// </summary>
        [Test]
        public void EditContactTest()
        {
            var modal = this.contactRepository
                .GetContacts()
                ?.FirstOrDefault(x => x.Name == contact.Name);
            
            Assert.NotNull(modal);

            var newContact = new ContactViewModel
            {
                Id = modal.Id,
                Name = modal.Name,
                Surname = "NewSurname" + Guid.NewGuid().ToString(),
                Email = modal.Email,
                Phone = modal.Phone,
                Vk = modal.Vk,
                Birthday = modal.Birthday
            };
            
            this.contactManager.EditContact(newContact);
            var newModal = this.contactRepository.GetContacts()?.FirstOrDefault(x => x.Id == newContact.Id);

            Assert.NotNull(newModal);
            Assert.AreEqual(newModal.Surname, newContact.Surname);
        }
        
        /// <summary>
        /// Тест на изменение контакта с несуществующим Id.
        /// </summary>
        [Test]
        public void NotFoundContactIdEditTest()
        {
            var modal = this.contactRepository
                .GetContacts()
                ?.LastOrDefault(x => x.Name == contact.Name);
            
            Assert.NotNull(modal);

            var newContact = new ContactViewModel
            {
                Id = modal.Id + 100,
                Name = modal.Name,
                Surname = "NewSurname" + Guid.NewGuid().ToString(),
                Email = modal.Email,
                Phone = modal.Phone,
                Vk = modal.Vk,
                Birthday = modal.Birthday
            };

            try
            {
                this.contactManager.EditContact(newContact);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Contact not found", e.Message);
            }
        }
        
        /// <summary>
        /// Тест на изменение несуществующего контакта.
        /// </summary>
        [Test]
        public void NotFoundContactEditTest()
        {
            ContactHelper.CleanDb();
            var newContact = new ContactViewModel
            {
                Id = 1,
                Name = Guid.NewGuid().ToString(),
                Surname = "NewSurname" + Guid.NewGuid().ToString(),
                Email = "test@gmail.com",
                Phone = "89999999999",
                Vk = "test",
                Birthday = DateTime.Now.Date,
            };

            try
            {
                this.contactManager.EditContact(newContact);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Contacts not found", e.Message);
            }
        }
        
        /// <summary>
        /// Тест на удаление контакта.
        /// </summary>
        [Test]
        public void DeleteContactTest()
        {
            var modal = this.contactRepository
                .GetContacts()
                ?.FirstOrDefault(x => x.Name == contact.Name);
            
            Assert.NotNull(modal);
            
            this.contactManager.DeleteContact(modal.Id);
            
            var deletedModel = this.contactRepository
                .GetContacts()
                ?.FirstOrDefault(x => x.Id == modal.Id);
            
            Assert.IsNull(deletedModel);
        }
        
        /// <summary>
        /// Тест на изменение контакта с несуществующим Id.
        /// </summary>
        [Test]
        public void NotFoundContactIdDeleteTest()
        {
            var modal = this.contactRepository
                .GetContacts()
                .OrderBy(x => x.Id)
                ?.Last(x => x.Name == contact.Name);
            
            Assert.NotNull(modal);

            try
            {
                this.contactManager.DeleteContact(modal.Id + 100);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Contact not found", e.Message);
            }
        }
        
        /// <summary>
        /// Тест на изменение несуществующего контакта.
        /// </summary>
        [Test]
        public void NotFoundContactDeleteTest()
        {
            ContactHelper.CleanDb();
            try
            {
                this.contactManager.DeleteContact(1);
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Contacts not found", e.Message);
            }
        }
        
        /// <summary>
        /// Тест на получение списка контактов.
        /// </summary>
        [Test]
        public void GetContactsTest()
        {
            var repositoryModels = contactRepository.GetContacts().ToList();
            var managerModels = contactManager.GetContacts().ToList();
            Assert.NotNull(managerModels);
            Assert.AreEqual(repositoryModels.Count, managerModels.Count);

            var ids = managerModels.Select(x => x.Id).ToList();
            foreach (var model in repositoryModels)
            {
                Assert.IsTrue(ids.Contains(model.Id));
            }
        }
        
        /// <summary>
        /// Тест на поиск контакта по Id.
        /// </summary>
        [Test]
        public void GetContactByIdTest()
        {
            var modal = this.contactRepository
                .GetContacts()
                ?.FirstOrDefault(x => x.Name == contact.Name);

            var result = this.contactManager.GetContactById(modal.Id);
            Assert.NotNull(result);
            
            Assert.AreEqual(modal.Id, result.Id);
            Assert.AreEqual(modal.Name, result.Name);
            Assert.AreEqual(modal.Surname, result.Surname);
            Assert.AreEqual(modal.Phone, result.Phone);
            Assert.AreEqual(modal.Email, result.Email);
            Assert.AreEqual(modal.Birthday, result.Birthday);
            Assert.AreEqual(modal.Vk, result.Vk);
        }
    }
}