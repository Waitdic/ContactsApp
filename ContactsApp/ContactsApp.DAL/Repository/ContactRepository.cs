using System;
using System.Collections.Generic;
using System.IO;
using ContactsApp.DAL.Models;
using Newtonsoft.Json;

namespace ContactsApp.DAL.Repository
{
    public class ContactRepository : IContactRepository
    {
        private static readonly string FileParth = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName + "\\ContactsApp.DAL\\DB\\json.txt";

        public void AddContact(List<Contact> contact)
        {
            this.CheckFile();
            this.Serializer(contact);
        }

        public List<Contact> GetContacts()
        {
            this.CheckFile();
            return this.Deserializer();
        }

        private void Serializer(List<Contact> contact)
        {
            try
            {
                var serializer = new JsonSerializer();
                using StreamWriter sw = new StreamWriter(FileParth);
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, contact);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private List<Contact> Deserializer()
        {
            try
            {
                var deserializer = new JsonSerializer();
                using var sr = new StreamReader(FileParth);
                using (var reader = new JsonTextReader(sr))
                {
                    return deserializer.Deserialize<List<Contact>>(reader);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void CheckFile()
        {
            if (!File.Exists(FileParth))
            {
                File.Create(FileParth);
            }
        }
    }
}
