using System.Collections.Generic;
using ContactsApp.BLL.Models;

namespace ContactsApp.BLL.Interfaces
{
    public interface IContactManager
    {
        /// <summary>
        /// Добавить контакт.
        /// </summary>
        /// <param name="contact">Contact.</param>
        void AddContact(ContactViewModel contact);

        /// <summary>
        /// Изменить контакт.
        /// </summary>
        /// <param name="contact">Contact.</param>
        void EditContact(ContactViewModel contact);

        /// <summary>
        /// Удалить контакт.
        /// </summary>
        /// <param name="id">
        /// Id.
        /// </param>
        void DeleteContact(int id);

        /// <summary>
        /// Получить список контактов.
        /// </summary>
        List<ContactViewModel> GetContacts();

        /// <summary>
        /// Получить контакт по Id.
        /// </summary>
        ContactViewModel GetContactById(int id);
    }
}
