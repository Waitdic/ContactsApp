using System;
using System.Configuration;
using System.Linq;
using ContactsApp.BLL.Models;
using ContactsApp.DAL.Repository;
using NUnit.Framework;

namespace UnitTests
{
    /// <summary>
    /// Тесты на проверку ContactManager
    /// </summary>
    [TestFixture]
    public class ContactManagerTests
    {
        private readonly ContactManager contactManager = new ContactManager(new ContactRepository());
        private readonly ContactRepository contactRepository = new ContactRepository();

        private ContactVM contact;
        
        private void Initialize()
        {
            ConfigurationManager.AppSettings.Set("DbFolder", @"..\..\json.txt");
            
            this.contact = ContactHelper.AddNewContactViewModel();
            this.contactManager.AddContact(this.contact);
        }
        
        private static void Clean()
        {
           ContactHelper.CleanDb();
        }

        /// <summary>
        /// Тест на добавление контакта.
        /// </summary>
        [Test]
        public void AddContact_CorrectResult()
        {
            // SetUp
            Initialize();
            var model = this.contactRepository
                .GetContacts()
                ?.Where(x => x.Name == this.contact.Name)
                .ToList();
            
            // Assert
            Assert.NotNull(model);
            Assert.AreEqual(model.Count, 1);
            Assert.AreEqual(model.FirstOrDefault()?.Name, this.contact.Name);
            Clean();
        }
        
        /// <summary>
        /// Тест на валидацию добавления контакта.
        /// </summary>
        [Test]
        public void AddContact_NullObject_ThrowException()
        {
            // SetUp
            Initialize();
            
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                this.contactManager.AddContact(null);
            });
            Clean();
        }
        
        /// <summary>
        /// Тест на изменение контакта.
        /// </summary>
        [Test]
        public void EditContact_Contact_CorrectResult()
        {
            // SetUp
            Initialize();
            var modal = this.contactRepository
                .GetContacts()
                ?.FirstOrDefault(x => x.Name == contact.Name);
            
            Assert.NotNull(modal);

            var newContact = new ContactVM
            {
                Id = modal.Id,
                Name = modal.Name,
                Surname = "NewSurname" + Guid.NewGuid().ToString(),
                Email = modal.Email,
                Phone = modal.Phone,
                Vk = modal.Vk,
                Birthday = modal.Birthday
            };
            
            // Act
            this.contactManager.EditContact(newContact);
            var newModal = this.contactRepository.GetContacts()?.FirstOrDefault(x => x.Id == newContact.Id);

            // Assert
            Assert.NotNull(newModal);
            Assert.AreEqual(newModal.Surname, newContact.Surname);
            Clean();
        }
        
        /// <summary>
        /// Тест на изменение контакта с несуществующим Id.
        /// </summary>
        [Test]
        public void EditContact_NonExistentId_ThrowException()
        {
            // SetUp
            Initialize();
            var modal = this.contactRepository
                .GetContacts()
                ?.LastOrDefault(x => x.Name == contact.Name);
            
            Assert.NotNull(modal);

            var newContact = new ContactVM
            {
                Id = modal.Id + 100,
                Name = modal.Name,
                Surname = "NewSurname" + Guid.NewGuid(),
                Email = modal.Email,
                Phone = modal.Phone,
                Vk = modal.Vk,
                Birthday = modal.Birthday
            };

            // Assert
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                // Act
                this.contactManager.EditContact(newContact);
            });
            Assert.AreEqual("Contact not found", ex.Message);
            Clean();
        }
        
        /// <summary>
        /// Тест на изменение несуществующего контакта.
        /// </summary>
        [Test]
        public void EditContact_NonExistentContact_ThrowException()
        {
            // SetUp
            Initialize();
            ContactHelper.CleanDb();
            var newContact = new ContactVM
            {
                Id = 1,
                Name = Guid.NewGuid().ToString(),
                Surname = "NewSurname" + Guid.NewGuid().ToString(),
                Email = "test@gmail.com",
                Phone = "89999999999",
                Vk = "test",
                Birthday = DateTime.Now.Date,
            };

            // Assert
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                // Act
                this.contactManager.EditContact(newContact);
            });
            Assert.AreEqual("Contacts not found", ex.Message);
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
            var modal = this.contactRepository
                .GetContacts()
                ?.FirstOrDefault(x => x.Name == contact.Name);
            
            Assert.NotNull(modal);
            
            // Act
            this.contactManager.DeleteContact(modal.Id);
            
            var deletedModel = this.contactRepository
                .GetContacts()
                ?.FirstOrDefault(x => x.Id == modal.Id);
            
            // Assert
            Assert.IsNull(deletedModel);
            Clean();
        }
        
        /// <summary>
        /// Тест на изменение контакта с несуществующим Id.
        /// </summary>
        [Test]
        public void DeleteContact_NonExistentId_ThrowException()
        {
            // SetUp
            Initialize();
            var modal = this.contactRepository
                .GetContacts()
                .OrderBy(x => x.Id)
                ?.Last(x => x.Name == contact.Name);
            
            Assert.NotNull(modal);

            // Assert
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                // Act
                this.contactManager.DeleteContact(modal.Id + 100);
            });
            Assert.AreEqual("Contact not found", ex.Message);
            Clean();
        }
        
        /// <summary>
        /// Тест на изменение несуществующего контакта.
        /// </summary>
        [Test]
        public void DeleteContact_NonExistentContact_ThrowException()
        {
            // SetUp
            Initialize();
            ContactHelper.CleanDb();
            
            // Assert
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                // Act
                this.contactManager.DeleteContact(1);
            });
            Assert.AreEqual("Contacts not found", ex.Message);
            Clean();
        }
        
        /// <summary>
        /// Тест на получение списка контактов.
        /// </summary>
        [Test]
        public void GetContacts_CorrectResult()
        {
            // SetUp
            Initialize();
            var repositoryModels = contactRepository.GetContacts().ToList();
            var managerModels = contactManager.GetContacts().ToList();
            
            // Assert
            Assert.NotNull(managerModels);
            Assert.AreEqual(repositoryModels.Count, managerModels.Count);

            // Act
            var ids = managerModels.Select(x => x.Id).ToList();
            foreach (var model in repositoryModels)
            {
                //Assert
                Assert.IsTrue(ids.Contains(model.Id));
            }
            Clean();
        }
        
        /// <summary>
        /// Тест на поиск контакта по Id.
        /// </summary>
        [Test]
        public void GetContact_Id_CorrectResult()
        {
            // SetUp
            Initialize();
            var modal = this.contactRepository
                .GetContacts()
                ?.FirstOrDefault(x => x.Name == contact.Name);

            // Act
            var result = this.contactManager.GetContactById(modal.Id);
            
            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(modal.Id, result.Id);
            Assert.AreEqual(modal.Name, result.Name);
            Assert.AreEqual(modal.Surname, result.Surname);
            Assert.AreEqual(modal.Phone, result.Phone);
            Assert.AreEqual(modal.Email, result.Email);
            Assert.AreEqual(modal.Birthday, result.Birthday);
            Assert.AreEqual(modal.Vk, result.Vk);
            Clean();
        }
    }
}