using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using ContactsApp.DAL.Models;
using ContactsApp.DAL.Repository;
using NUnit.Framework;

namespace UnitTests
{
    /// <summary>
    /// Тесты на проверку ContactRepository
    /// </summary>
    [TestFixture]
    public class ContactRepositoryTests
    {
        private readonly ContactRepository contactRepository = new ContactRepository();

        private Contact contact;

        private void Initialize()
        {
            ConfigurationManager.AppSettings.Set("DbFolder", @"..\..\json.txt");
            this.contact = ContactHelper.AddNewContact();
        }

        /// <summary>
        /// Тест на добавление контакта в бд.
        /// </summary>
        [Test]
        public void GetContacts_Contacts_CorrectResult()
        {
            // SetUp
            Initialize();
            
            // Act
            this.contactRepository.AddContacts(new List<Contact>{ this.contact });
            var result =this.contactRepository.GetContacts();
            
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.FirstOrDefault(x => x.Id == this.contact.Id));
            ContactHelper.CleanDb();
        }
        
    }
}