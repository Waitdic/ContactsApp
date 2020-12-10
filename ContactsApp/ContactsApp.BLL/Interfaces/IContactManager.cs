using System.Collections.Generic;
using ContactsApp.BLL.Models;
using ContactsApp.DAL.Models;

namespace ContactsApp.BLL.Interfaces
{
    public interface IContactManager
    {
        /// <summary>
        /// Добавить контакт.
        /// </summary>
        /// <param name="contact">Contact.</param>
        void AddContact(Contact contact);

        /// <summary>
        /// Изменить контакт.
        /// </summary>
        /// <param name="contact">Contact.</param>
        void EditContact(Contact contact);

        /// <summary>
        /// Удалить контакт.
        /// </summary>
        /// <param name="contact">
        /// Contact.
        /// </param>
        void DeleteContact(Contact contact);

        /// <summary>
        /// Получить список контактов.
        /// </summary>
        List<Contact> GetContacts();

        /// <summary>
        /// Получить контакт по Id.
        /// </summary>
        Contact GetContactById(int id);
    }
}
