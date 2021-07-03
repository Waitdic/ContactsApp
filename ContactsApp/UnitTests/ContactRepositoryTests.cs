////using System;
////using System.Linq;
////using System.Threading.Tasks;
////using ContactsApp.DAL;
////using ContactsApp.DAL.Models;
////using ContactsApp.DAL.Repository;
////using Microsoft.EntityFrameworkCore;
////using Moq;
////using NUnit.Framework;

////namespace UnitTests
////{
////    /// <summary>
////    /// Тесты на проверку ContactRepository
////    /// </summary>
////    [TestFixture]
////    public class ContactRepositoryTests
////    {
////        private IContactRepository contactRepository;

////        /// <summary>
////        /// Тест на добавление контакта в бд.
////        /// </summary>
////        [Test, Description("Тест на добавление контакта в бд. Позитивный тест.")]
////        public void AddContact_Contact_CorrectResult()
////        {
////            // SetUp
////            var dbContextMock = new Mock<Context>();  
////            var dbSetMock = new Mock<DbSet<Contact>>();  
////            dbSetMock.Setup(s => s.Add(It.IsAny<Contact>()));
////            dbSetMock.Setup(s => s.Find()).Returns(Task.FromResult(new List<Contact>()));
////            dbContextMock.Setup(s => s.Set<Context>()).Returns(dbSetMock.Object);  
////            var contact = ContactHelper.AddNewContact();
            
////            // Act
////            this.contactRepository.AddContact(contact);
////            var result =this.contactRepository?.GetContacts()?.Where(x => x.Name == contact.Name);
            
////            // Assert
////            Assert.NotNull(result);
////            Assert.AreEqual(result.Count(), 1);
////        }

////        /// <summary>
////        /// Тест на получение контакта по Id.
////        /// </summary>
////        [Test, Description("Тест на получение контакта по Id. Позитивный тест.")]
////        public void GetContact_Id_CorrectResult()
////        {
////            // SetUp
////            var contact = ContactHelper.AddNewContact();
////            this.contactRepository.AddContact(contact);
////            var model =this.contactRepository?.GetContacts()?.FirstOrDefault(x => x.Name == contact.Name);
////            Assert.NotNull(model);
            
////            // Act
////            var result = this.contactRepository.GetContact(model.Id);
           
////            // Assert
////            Assert.NotNull(result);
////        }
        
////        /// <summary>
////        /// Тест на удаление контакта в бд.
////        /// </summary>
////        [Test, Description("Тест на удаление контакта в бд. Позитивный тест.")]
////        public void DeleteContact_Id_CorrectResult()
////        {
////            // SetUp
////            var contact = ContactHelper.AddNewContact();
////            this.contactRepository.AddContact(contact);
////            var model =this.contactRepository?.GetContacts()?.FirstOrDefault(x => x.Name == contact.Name);
////            Assert.NotNull(model);
            
////            // Act
////            this.contactRepository.DeleteContact(model.Id);
////            var result = this.contactRepository.GetContact(model.Id);

////            // Assert
////            Assert.IsNull(result);
////        }
        
////        /// <summary>
////        /// Тест на изменение контакта в бд.
////        /// </summary>
////        [Test, Description("Тест на изменение контакта в бд. Позитивный тест.")]
////        public void EditContact_Contact_CorrectResult()
////        {
////            // SetUp
////            var contact = ContactHelper.AddNewContact();
////            this.contactRepository.AddContact(contact);
////            var model =this.contactRepository?.GetContacts()?.FirstOrDefault(x => x.Name == contact.Name);
////            Assert.NotNull(model);
            
////            // Act
////            model.Surname = "editSurname" + Guid.NewGuid().ToString();
////            this.contactRepository.EditContact(model);
////            var result = this.contactRepository.GetContact(model.Id);
            
////            // Assert
////            Assert.NotNull(result);
////            Assert.AreEqual(result.Surname, model.Surname);
////        }
        
////        /// <summary>
////        /// Тест проверки наличия записи контакта в базе.
////        /// </summary>
////        [Test, Description("Тест проверки наличия записи контакта в базе . Позитивный тест.")]
////        public void CheckAvailability_CorrectResult()
////        {
////            // SetUp
////            var contact = ContactHelper.AddNewContact();
////            this.contactRepository.AddContact(contact);
////            var model = this.contactRepository?.GetContacts()?.FirstOrDefault(x => x.Name == contact.Name);
////            Assert.NotNull(model);
            
////            // Act
////            var trueResult = this.contactRepository.CheckAvailability(model.Id);
////            var falseResult = this.contactRepository.CheckAvailability(-10);
            
////            // Assert
////            Assert.IsTrue(trueResult);
////            Assert.IsFalse(falseResult);
////        }
////    }
////} 