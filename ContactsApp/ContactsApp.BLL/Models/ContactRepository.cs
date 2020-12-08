using System.Collections.Generic;
using System.IO;
using ContactsApp.BLL.Interfaces;
using Newtonsoft.Json;

namespace ContactsApp.BLL.Models
{
    public class ContactRepository : IContactRepository
    {
        public void AddContact(Contact contact)
        {
            this.Serializer(contact);
        }

        public void EditContact(Contact contact)
        {

        }

        public void DeleteContact(Contact contact)
        {

        }

        public List<Contact> GetContacts()
        {
            return this.Deserializer();
        }

        private void Serializer(Contact contact)
        {
            var serializer = new JsonSerializer();
            using StreamWriter sw = new StreamWriter(@"E:\учеба\Git\ContactsApp\DB\json.txt");
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, contact);
            }
        }

        private List<Contact> Deserializer()
        {
            var deserializer = new JsonSerializer();
            using var sr = new StreamReader(@"E:\учеба\Git\ContactsApp\DB\json.txt");
            using (var reader = new JsonTextReader(sr))
            {
                return deserializer.Deserialize<List<Contact>>(reader);
            }
        }
    }
}
