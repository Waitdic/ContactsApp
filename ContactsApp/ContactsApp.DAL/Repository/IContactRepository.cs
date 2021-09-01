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
        /// <param name="contact">Contact.</param>
        void AddContact(Contact contact);
        
        /// <summary>
        /// Изменить контакт.
        /// </summary>
        /// <param name="contact">Contact.</param>
        void EditContact(Contact contact);

        /// <summary>
        /// Получить контакт.
        /// </summary>
        /// <param name="id">Id.</param>
        Contact GetContact(int id);
        
        /// <summary>
        /// Получить список контактов.
        /// </summary>
        List<Contact> GetContacts();
        
        /// <summary>
        /// Добавить контакты.
        /// </summary>
        /// <param name="id">Id контакта.</param>
        void DeleteContact(int id);

        /// <summary>
        /// Проверить наличие элемента в базе.
        /// </summary>
        /// <param name="id">Id контакта.</param>
        bool CheckAvailability(int id);
    }
}
