using System.Collections.Generic;
using ContactsApp.DAL.Models;

namespace ContactsApp.DAL.Repository
{
    /// <summary>
    /// Интерфейс репозитория для работы с данными бд.
    /// </summary>
    public interface IContactRepository
    {
        /// <summary>
        /// Добавить контакты.
        /// </summary>
        /// <param name="contacts">Contact.</param>
        void AddContacts(List<Contact> contacts);

        /// <summary>
        /// Получить список контактов.
        /// </summary>
        List<Contact> GetContacts();
    }
}
