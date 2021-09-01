using System;
using System.Linq;
using ContactsApp.BLL.Models;
using ContactsApp.DAL.Repository;
using Moq;
using NUnit.Framework;

namespace UnitTests
{
    /// <summary>
    /// Тесты на проверку ContactManager
    /// </summary>
    [TestFixture]
    public class ContactManagerTests
    {
        private ContactManager contactManager;

        private void Initialize()
        {
            var mock = new Mock<IContactRepository>();
            mock.Setup(repo => repo.GetContacts())
                .Returns(ContactHelper.GetTestContacts());
            mock.Setup(repo => repo.GetContact(0))
                .Returns(ContactHelper.AddNewContact());
            mock.Setup(repo => repo.CheckAvailability(100))
                .Returns(false);
            mock.Setup(repo => repo.CheckAvailability(0))
                .Returns(true);
            
            contactManager = new ContactManager(mock.Object);
        }

        [Test, Description("Тест на добавление контакта. Позитивный тест.")]
        public void AddContact_CorrectResult()
        {
            // SetUp
            Initialize();

            var contact = new ContactVM()
            {
                Name = "Name",
                Surname = "Surname",
                Birthday = DateTime.Now.Date,
                Phone = "81111111111",
                Vk = "daniska1616",
                Email = "danis161616@yandex.ru",
            };

            contactManager.AddContact(contact);
        }

        [Test, Description("Тест на валидацию добавления контакта. Негативный тест.")]
        public void AddContact_NullObject_ThrowException()
        {
            // SetUp
            Initialize();

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                contactManager.AddContact(null);
            });
        }
   
        [Test, Description("Тест на изменение контакта. Позитивный тест.")]
        public void EditContact_Contact_CorrectResult()
        {
            // SetUp
            Initialize();
            var modal = this.contactManager.GetContactById(0);
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
            contactManager.EditContact(newContact);
        }
        
       
        [Test, Description("Тест на изменение контакта с несуществующим Id. Негативный тест.")]
        public void EditContact_NonExistentId_ThrowException()
        {
            // SetUp
            Initialize();
            var modal = this.contactManager.GetContactById(0);
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
                contactManager.EditContact(newContact);
            });

            Assert.AreEqual("Contact not found", ex.Message);
        }
       
        [Test, Description("Тест на изменение несуществующего контакта. Негативный тест.")]
        public void EditContact_NonExistentContact_ThrowException()
        {
            // SetUp
            Initialize();
            var newContact = new ContactVM
            {
                Id = 100,
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
                contactManager.EditContact(newContact);
            });

            Assert.AreEqual("Contact not found", ex.Message);
        }
        
        [Test, Description("Тест на удаление контакта. Позитивный тест.")]
        public void DeleteContact_Id_CorrectResult()
        {
            // SetUp
            Initialize();
            var modal = this.contactManager.GetContactById(0);
            Assert.NotNull(modal);

            // Act
            contactManager.DeleteContact(modal.Id.Value);
        }
        
       
        [Test, Description("Тест на изменение контакта с несуществующим Id. Негативный тест.")]
        public void DeleteContact_NonExistentId_ThrowException()
        {
            // SetUp
            Initialize();
          
            // Assert
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                // Act
                contactManager.DeleteContact(100);
            });
            Assert.AreEqual("Contact not found", ex.Message);
        }
       
        [Test, Description("Тест на получение списка контактов. Позитивный тест.")]
        public void GetContacts_CorrectResult()
        {
            // SetUp
            Initialize();
            var managerModels = contactManager.GetContacts().ToList();
            
            // Assert
            Assert.NotNull(managerModels);
        }
        
      
        [Test, Description("Тест на поиск контакта по Id. Позитивный тест.")]
        public void GetContact_Id_CorrectResult()
        {
            // SetUp
            Initialize();
            var modal = this.contactManager.GetContactById(0);
            Assert.NotNull(modal);

            // Act
            var result = this.contactManager.GetContactById(modal.Id.Value);
            
            // Assert
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