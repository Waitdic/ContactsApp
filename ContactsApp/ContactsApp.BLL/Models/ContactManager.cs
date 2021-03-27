using System;
using System.Collections.Generic;
using System.Linq;
using ContactsApp.BLL.Interfaces;
using ContactsApp.DAL.Models;
using ContactsApp.DAL.Repository;

namespace ContactsApp.BLL.Models
{
    public class ContactManager : IContactManager
    {
        private readonly IContactRepository contactRepository;

        public ContactManager(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public void AddContact(ContactViewModel contact)
        {
            var models = this.contactRepository.GetContacts();
            if (models == null)
            {
                contact.Id = 0;
                var entity = this.FromViewToModel(contact);
                this.contactRepository.AddContact(new List<Contact> { entity });
            }
            else
            {
                contact.Id = models.OrderBy(x => x.Id).Last().Id + 1;
                var entity = this.FromViewToModel(contact);
                models.Add(entity);

                this.contactRepository.AddContact(models);
            }
        }

        public void EditContact(ContactViewModel contact)
        {
            var models = this.contactRepository.GetContacts();
            this.ValidationModels(models, contact.Id.GetValueOrDefault());

            var entity = this.FromViewToModel(contact);
            entity.Id = contact.Id.Value;

            var deletedModel = models.AsEnumerable().FirstOrDefault(x => x.Id == contact.Id.Value);
            models.Remove(deletedModel);
            models.Add(entity);

            this.contactRepository.AddContact(models);
        }

        public void DeleteContact(int id)
        {
            var models = this.contactRepository.GetContacts();
            this.ValidationModels(models, id);

            var contact = models.FirstOrDefault(i => i.Id == id);
            models.Remove(contact);

            this.contactRepository.AddContact(models);
        }

        public List<ContactViewModel> GetContacts()
        {
            return this.contactRepository.GetContacts()
                ?.Select(this.FromModelToViewModel)
                .ToList();
        }

        public ContactViewModel GetContactById(int id)
        {
            return this.FromModelToViewModel
            (this.contactRepository
                .GetContacts()
                ?.FirstOrDefault(x => x.Id == id)
                );
        }

        private Contact FromViewToModel(ContactViewModel contact)
        {
            return new Contact
            {
                Id = contact.Id.Value,
                Name = contact.Name,
                Surname = contact.Surname,
                Birthday = contact.Birthday,
                Phone = contact.Phone,
                Email = contact.Email,
                Vk = contact.Vk,
            };
        }

        private ContactViewModel FromModelToViewModel(Contact contact)
        {
            return new ContactViewModel
            {
                Id = contact.Id,
                Name = contact.Name,
                Surname = contact.Surname,
                Birthday = contact.Birthday,
                Phone = contact.Phone,
                Email = contact.Email,
                Vk = contact.Vk,
            };
        }

        private void ValidationModels(List<Contact> models, int? id)
        {
            if (models == null)
            {
                throw new ArgumentException("Contacts not found");
            }

            if (id == null || models.All(x => x.Id != id.GetValueOrDefault()))
            {
                throw new ArgumentException("Contact not found");
            }
        }
    }
}
