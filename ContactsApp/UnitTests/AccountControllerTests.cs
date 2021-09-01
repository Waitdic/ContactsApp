using System.Linq;
using ContactsApp.BLL.Interfaces;
using ContactsApp.BLL.Models;
using ContactsApp.WEB.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace UnitTests
{
    /// <summary>
    /// Тесты на проверку AccountController
    /// </summary>
    [TestFixture]
    public class AccountControllerTests
    {
        private static AccountController accountController;

        private static void Initialize()
        {
            var moq = new Mock<IContactManager>();
            moq.Setup(x => x.GetContacts())
                .Returns(
                    ContactHelper
                    .GetTestContacts()
                    .Select(ContactVM.FromModelToView)
                    .ToList);
            
            moq.Setup(x => x.GetContactById(0))
                .Returns(ContactVM
                    .FromModelToView(ContactHelper.AddNewContact()));

            accountController = new AccountController(moq.Object);

        }
        
        [Test, Description("Тест на создание добавления нового контакта. Позитивный тест.")]
        public void AddContact_NewContact_CorrectResult()    
        {
            // SetUp
            Initialize();
            var newContact = ContactHelper.AddNewContactViewModel();

            // Act
            accountController.AddContact(newContact);
            var model = accountController.GetContact(0);
       
            // Assert
            Assert.NotNull(model);
        }
        
        [Test, Description("Тест на получения списка контактов. Позитивный тест.")]
        public void GetContacts_CorrectResult()
        {
            // SetUp
            Initialize();
            
            // Act
            var contact = accountController.GetContacts();

            // Assert    
            Assert.NotNull(contact);
        }
        
        [Test, Description("Тест на редактирование контакта. Позитивный тест.")]
        public void EditContact_Contact_CorrectResult()
        {
            // SetUp
            Initialize();
            var newContact = ContactHelper.AddNewContactViewModel();

            // Act
            var result = accountController.EditContact(newContact);
            var contactList = accountController.GetContacts();

            // Assert    
            Assert.AreEqual(typeof(OkResult), result.GetType());
            Assert.NotNull(contactList.Value);
        }
        
        [Test, Description("Тест на получение контакта по id. Позитивный тест.")]
        public void GetContact_Id_CorrectResult()
        {
            // SetUp
            Initialize();
            
            // Act
            var contact = accountController.GetContact(0);

            // Assert    
            Assert.NotNull(contact);
        }
        
        [Test, Description("Тест на удаление контакта. Позитивный тест.")]
        public void DeleteContact_Id_CorrectResult()
        {
            // SetUp
            Initialize();
            var contact = ContactHelper.AddNewContact();
            
            // Act
            accountController.DeleteContact(contact.Id);
        }
    }
}
