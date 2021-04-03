using System;
using System.Collections.Generic;
using System.Linq;
using ContactsApp.BLL.Interfaces;
using ContactsApp.DAL.Models;
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
            
            var models = this.contactRepository.GetContacts();

            if (models == null)
            {
                contact.Id = 0;
                var entity = contact.FromViewToModel();
                this.contactRepository.AddContacts(new List<Contact> { entity });
            }
            else
            {
                contact.Id = models.OrderBy(x => x.Id).Last().Id + 1;
                var entity = contact.FromViewToModel();
                models.Add(entity);

                this.contactRepository.AddContacts(models);
            }
        }

        /// <inheritdoc cref="IContactManager"/>
        public void EditContact(ContactVM contact)
        {
            var contacts = this.contactRepository.GetContacts();
            this.CheckContactsListForNull(contacts);
            this.CheckExistenceId(contact.Id.GetValueOrDefault(), contacts);

            var entity = contact.FromViewToModel();
            entity.Id = contact.Id.Value;

            var deletedModel = contacts.AsEnumerable().FirstOrDefault(x => x.Id == contact.Id.Value);
            contacts.Remove(deletedModel);
            contacts.Add(entity);

            this.contactRepository.AddContacts(contacts);
        }

        /// <inheritdoc cref="IContactManager"/>
        public void DeleteContact(int id)
        {
            var contacts = this.contactRepository.GetContacts();
            this.CheckContactsListForNull(contacts);
            this.CheckExistenceId(id, contacts);

            var contact = contacts.FirstOrDefault(i => i.Id == id);
            contacts.Remove(contact);

            this.contactRepository.AddContacts(contacts);
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
            return ContactVM.FromModelToView
            (this.contactRepository
                .GetContacts()
                ?.FirstOrDefault(x => x.Id == id)
            );
        }

        /// <summary>
        /// Метод для проверки списка контактов на null.
        /// </summary>
        /// <param name="contacts">Список контактов.</param>
        /// <exception cref="ArgumentException">Contacts not found.</exception>
        private void CheckContactsListForNull(List<Contact> contacts)
        {
            if (contacts == null)
            {
                throw new ArgumentException("Contacts not found");
            }
        }
        
        /// <summary>
        /// Метод для проверки Id на null и наличие его в общем списке контактов.
        /// </summary>
        /// <param name="id">Id контакта.</param>
        /// <param name="contacts">Список контактов.</param>
        /// <exception cref="ArgumentException">Contact not found.</exception>
        private void CheckExistenceId(int? id, List<Contact> contacts) 
        {
            if (id == null || contacts.All(x => x.Id != id.GetValueOrDefault()))
            {
                throw new ArgumentException("Contact not found");
            }
        }
    }
}
