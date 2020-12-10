using System;
using System.Linq;
using ContactsApp.BLL.Models;
using ContactsApp.DAL.Models;
using ContactsApp.DAL.Repository;
using NUnit.Framework;
using Xunit;
using Assert = Xunit.Assert;
//using Contact = ContactsApp.DAL.Models.Contact;

namespace UnitTests
{
    public class AccountControllerTests
    {
        [Fact]
        public void AddNewContactTest()
        {
            // Arrange
            var manager = new ContactsApp.BLL.Models.ContactManager(new ContactRepository());

            // Act
            bool check;
            try
            {
                manager.AddContact(AddNewContact());
                check = true;
            }
            catch (Exception)
            {
                check = false;
            }

            Assert.True(check);
        }

        [Fact]
        public void GetContactTest()
        {
            // Arrange
            var manager = new ContactManager(new ContactRepository());

            // Act
            var contact = manager.GetContacts().LastOrDefault();
            
            Assert.NotNull(contact);
            Assert.Equal("danis161616@yandex.ru", contact.Email);
            Assert.Equal("8-111-111-11-11", contact.Phone);
            Assert.Equal("daniska1616", contact.Vk);
        }

        private Contact AddNewContact()
        {
            var repository = new ContactRepository();
            return new Contact
            {
                Id = repository.GetContacts().Count + 1,
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
