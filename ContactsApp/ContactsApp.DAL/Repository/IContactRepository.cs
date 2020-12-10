using System.Collections.Generic;
using ContactsApp.DAL.Models;

namespace ContactsApp.DAL.Repository
{
    public interface IContactRepository
    {
        /// <summary>
        /// Добавить контакт.
        /// </summary>
        /// <param name="contact">Contact.</param>
        void AddContact(List<Contact> contact);

        /// <summary>
        /// Получить список контактов.
        /// </summary>
        List<Contact> GetContacts();
    }
}
