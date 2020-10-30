using ContactsApp.BLL.Models;

namespace ContactsApp.BLL.Interfaces
{
    public interface IContactManager
    {
        /// <summary>Добавить контакт.</summary>
        /// <param name="contact">Contact.</param>
        void AddContact(Contact contact);

        /// <summary>Изменить контакт.</summary>
        /// <param name="contact">Contact.</param>
        void EditContact(Contact contact);

        /// <summary>Удалить контакт.</summary>
        /// <param name="contact">Contact.</param>
        void DeleteContact(Contact contact);

        /// <summary>Добавить контакт.</summary>
        /// <remarks>Список контактов.</remarks>
        ///List<Contact> GetContacts();

        /// <summary>Добавить контакт.</summary>
        /// <remarks>Список контактов.</remarks>
        Contact GetContacts();
    }
}
