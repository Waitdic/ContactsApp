using System;
using System.Linq;
using ContactsApp.BLL.Models;
using Xunit;

namespace UnitTests
{
    public class AccountControllerTests
    {
        [Fact]
        public void AddNewContactTest()
        {
            // Arrange
            var manager = new ContactManager(new ContactRepository());

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
            Assert.Equal(contact.Email, "danis161616@yandex.ru");
            Assert.Equal(contact.Phone, "8-111-111-11-11");
            Assert.Equal(contact.Vk, "daniska1616");
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
