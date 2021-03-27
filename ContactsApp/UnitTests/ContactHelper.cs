using System;
using System.Linq;
using ContactsApp.BLL.Models;
using ContactsApp.DAL.Models;
using ContactsApp.DAL.Repository;

namespace UnitTests
{
    public static class ContactHelper
    {
        private static readonly ContactManager contactManager = new ContactManager(new ContactRepository());
        private static readonly ContactRepository contactRepository = new ContactRepository();
        
        /// <summary>
        /// Создает заполненный объект класса ContactViewModel.
        /// </summary>
        /// <returns>Объект класса ContactViewModel.</returns>
        /// <remarks>Без ID.</remarks>
        public static ContactVM AddNewContactViewModel()
        {
            return new ContactVM()
            {
                Name = "Name" + Guid.NewGuid().ToString(),
                Surname = "Surname" + Guid.NewGuid().ToString(),
                Birthday = DateTime.Now.Date,
                Phone = "8-111-111-11-11",
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
            var models = contactRepository.GetContacts();
            return new Contact()
            {
                Id = models?.OrderBy(x => x.Id).Last().Id + 1 ?? 0,
                Name = "Name" + Guid.NewGuid().ToString(),
                Surname = "Surname" + Guid.NewGuid().ToString(),
                Birthday = DateTime.Now.Date,
                Phone = "8-111-111-11-11",
                Vk = "daniska1616",
                Email = "danis161616@yandex.ru",
            };
        }

        /// <summary>
        /// Очищает базу данных (json.txt).
        /// </summary>
        /// <returns>Объект класса ContactViewModel.</returns>
        public static void CleanDb()
        {
            var allContacts = contactRepository.GetContacts()?.Select(x => x.Id).ToList();
            if (allContacts != null)
            {
                allContacts.ForEach(x => contactManager.DeleteContact(x));
            }  
        }
    }
}