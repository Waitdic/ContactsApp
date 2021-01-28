using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using ContactsApp.DAL.Models;
using ContactsApp.DAL.Repository;
using NUnit.Framework;

namespace UnitTests
{
    public class ContactRepositoryTests
    {
        private readonly ContactRepository contactRepository = new ContactRepository();

        private Contact contact;

        [OneTimeSetUp]
        public void PreClearDb()
        {
            ConfigurationManager.AppSettings.Set("DbFolder", @"..\..\json.txt");
            ContactHelper.CleanDb();
        }
        
        [SetUp]
        public void Initialize()
        {
            ConfigurationManager.AppSettings.Set("DbFolder", @"..\..\json.txt");
            this.contact = ContactHelper.AddNewContact();
        }

        [OneTimeTearDown]
        public void Clean()
        {
            ContactHelper.CleanDb();
        }

        /// <summary>
        /// Тест на добавление контакта в бд.
        /// </summary>
        [Test]
        public void AddContactTest()
        {
            this.contactRepository.AddContact(new List<Contact>{ this.contact });
        }
        
        /// <summary>
        /// Тест на добавление контакта в бд.
        /// </summary>
        [Test]
        public void GetContactsTest()
        {
            this.contactRepository.AddContact(new List<Contact>{ this.contact });
            var result =this.contactRepository.GetContacts();
            
            Assert.NotNull(result);
            Assert.NotNull(result.FirstOrDefault(x => x.Id == this.contact.Id));
        }
        
    }
}