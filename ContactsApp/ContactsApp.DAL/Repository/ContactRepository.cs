using System;
using System.Collections.Generic;
using System.IO;
using ContactsApp.DAL.Models;
using Newtonsoft.Json;

namespace ContactsApp.DAL.Repository
{
    public class ContactRepository : IContactRepository
    {
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
                using StreamWriter sw = new StreamWriter(GetFilePath());
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
                using var sr = new StreamReader(GetFilePath());
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
            if (!File.Exists(GetFilePath()))
            {
                File.Create(GetFilePath()).Dispose();
            }
        }

        private static string GetFilePath()
        {
            if (Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName.Contains("ContactsApp\\"))
            {
                var directoryName = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName;
                return directoryName.Substring(0, directoryName.LastIndexOf("ContactsApp\\", StringComparison.Ordinal)) + "ContactsApp\\ContactsApp.DAL\\DB\\json.txt";
            }
            
            return Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName + "\\ContactsApp.DAL\\DB\\json.txt";
        }
    }
}
