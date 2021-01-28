using System;
using System.Collections.Generic;
using System.IO;
using ContactsApp.DAL.Models;
using Newtonsoft.Json;
using System.Configuration;

namespace ContactsApp.DAL.Repository
{
    public class ContactRepository : IContactRepository
    {
        static string FilePart = GetFilePath();

        public void AddContact(List<Contact> contact)
        {
            this.CheckFile();
            this.Serializer(contact);
        }

        public List<Contact> GetContacts()
        {
            this.CheckFile();
            var contacts = this.Deserializer();
            return contacts.Count != 0 ? contacts : null;
        }

        private void Serializer(List<Contact> contact)
        {
            try
            {
                var serializer = new JsonSerializer();
                using StreamWriter sw = new StreamWriter(FilePart);
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
                using var sr = new StreamReader(FilePart);
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
            if (!File.Exists(FilePart))
            {
                File.Create(FilePart).Dispose();
            }
        }

        private static string GetFilePath()
        {
            var currentConfig = ConfigurationManager.AppSettings.Get("DbFolder");
            return Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, currentConfig);
        }
    }
}
