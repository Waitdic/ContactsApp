using System.Collections.Generic;
using ContactsApp.BLL.Models;

namespace ContactsApp.BLL.Interfaces
{
    public interface IContactRepository
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
    }
}
