using System;
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
            var manager = new ContactManager();

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

        private Contact AddNewContact()
        {
            return new Contact
            {
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
