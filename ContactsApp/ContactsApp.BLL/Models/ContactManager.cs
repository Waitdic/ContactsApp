using System;
using System.Collections.Generic;
using System.Linq;
using ContactsApp.BLL.Interfaces;
using ContactsApp.DAL.Repository;

namespace ContactsApp.BLL.Models
{
    /// <summary>
    /// Класс менеджера, реализующего бизнесс логику обработки данным Contact
    /// </summary>
    public class ContactManager : IContactManager
    {
        private readonly IContactRepository contactRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactManager"/> class.
        /// </summary>
        /// <param name="contactRepository">Репозиторий для класса Contact.</param>
        public ContactManager(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        /// <inheritdoc cref="IContactManager"/>
        public void AddContact(ContactVM contact)
        {
            if (contact == null)
            {
                throw new ArgumentException("Contact model is null!");
            }
            
            this.contactRepository.AddContact(contact.FromViewToModel());
        }

        /// <inheritdoc cref="IContactManager"/>
        public void EditContact(ContactVM contact)
        {
            this.CheckNullId(contact.Id);
            this.CheckExistenceId(contact.Id.GetValueOrDefault());

            this.contactRepository.EditContact(contact.FromViewToModel());
        }

        /// <inheritdoc cref="IContactManager"/>
        public void DeleteContact(int id)
        {
            this.CheckExistenceId(id);
            this.contactRepository.DeleteContact(id);
        }

        /// <inheritdoc cref="IContactManager"/>
        public List<ContactVM> GetContacts()
        {
            return this.contactRepository.GetContacts()
                ?.Select(ContactVM.FromModelToView)
                .ToList();
        }

        /// <inheritdoc cref="IContactManager"/>
        public ContactVM GetContactById(int id)
        {
            this.CheckExistenceId(id);
            return ContactVM.FromModelToView(this.contactRepository.GetContact(id));
        }

        /// <summary>
        /// Метод для проверки Id на null и .
        /// </summary>
        /// <param name="id">Id контакта.</param>
        /// <exception cref="ArgumentException">Contact Id not found.</exception>
        private void CheckNullId(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("Contact Id not found");
            }
        }
        
        /// <summary>
        /// Метод для проверки наличие его в общем списке контактов.
        /// </summary>
        /// <param name="id">Id контакта.</param>
        /// <exception cref="ArgumentException">Contact not found.</exception>
        private void CheckExistenceId(int id)
        {
            if (!this.contactRepository.CheckAvailability(id))
            {
                throw new ArgumentException("Contact not found");
            }
        }
    }
}
