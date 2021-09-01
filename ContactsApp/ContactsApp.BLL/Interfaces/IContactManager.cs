using System.Collections.Generic;
using ContactsApp.BLL.Models;

namespace ContactsApp.BLL.Interfaces
{
    /// <summary>
    /// Интерфейс менеджера, реализующего бизнесс логику обработки данным Contact
    /// </summary>
    public interface IContactManager
    {
        /// <summary>
        /// Добавить контакт.
        /// </summary>
        /// <param name="contact">Contact.</param>
        void AddContact(ContactVM contact);

        /// <summary>
        /// Изменить контакт.
        /// </summary>
        /// <param name="contact">Contact.</param>
        void EditContact(ContactVM contact);

        /// <summary>
        /// Удалить контакт.
        /// </summary><param name="id">Id контакта.</param>
        void DeleteContact(int id);

        /// <summary>
        /// Получить список контактов.
        /// </summary>
        List<ContactVM> GetContacts();

        /// <summary>
        /// Получить контакт по Id.
        /// </summary>
        /// <param name="id">Id контакта.</param>
        ContactVM GetContactById(int id);
    }
}
