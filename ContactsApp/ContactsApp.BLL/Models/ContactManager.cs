using System.Collections.Generic;

using ContactsApp.BLL.Interfaces;

namespace ContactsApp.BLL.Models
{
    public class ContactManager : IContactManager
    {
        private readonly IContactRepository contactRepository;

        public ContactManager(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public void AddContact(Contact contact)
        {
            contact.Id = 123;
            this.contactRepository.AddContact(contact);
        }

        public void EditContact(Contact contact)
        {
            
        }

        public void DeleteContact(Contact contact)
        {

        }

        public List<Contact> GetContacts()
        {
            return this.contactRepository.GetContacts();
        }

        public Contact GetContactById(int id)
        {
            return null;
        }
    }
}
