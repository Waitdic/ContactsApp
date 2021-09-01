using System;
using System.Collections.Generic;
using ContactsApp.BLL.Models;
using ContactsApp.DAL.Models;

namespace UnitTests
{
    public static class ContactHelper
    {
        /// <summary>
        /// Создает заполненный объект класса ContactViewModel.
        /// </summary>
        /// <returns>Объект класса ContactViewModel.</returns>
        /// <remarks>Без ID.</remarks>
        public static ContactVM AddNewContactViewModel()
        {
            return new ContactVM()
            {
                Name = "Name" + Guid.NewGuid(),
                Surname = "Surname" + Guid.NewGuid(),
                Birthday = DateTime.Now.Date,
                Phone = "89999999999",
                Vk = "daniska1616",
                Email = "danis161616@yandex.ru",
            };
        }
        
        /// <summary>
        /// Создает заполненный объект класса Contact.
        /// </summary>
        /// <returns>Объект класса Contact.</returns>
        public static Contact AddNewContact()
        {
           return new Contact()
            {
                Id = 0,
                Name = "Name" + 0,
                Surname = "Surname" + Guid.NewGuid(),
                Birthday = DateTime.Now.Date,
                Phone = "81111111111",
                Vk = "daniska1616",
                Email = "danis161616@yandex.ru",
            };
        }

        public static List<Contact> GetTestContacts()
        {
            var contacts = new List<Contact>(3);
            for(var i = 0; i < 3; i++)
            {
                contacts.Add(new Contact
                {
                    Id = i,
                    Name = "Name" + i,
                    Surname = "Surname" + i,
                    Birthday = DateTime.Now.Date,
                    Phone = "81111111111",
                    Vk = "daniska1616",
                    Email = "danis161616@yandex.ru",
                });   
            }

            return contacts;
        }
    }
}