using System;
using System.Collections.Generic;
using ContactsApp.DAL.Models;
using Newtonsoft.Json;
using System.Configuration;
using System.Data.SqlTypes;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.DAL.Repository
{
    /// <summary>
    /// Класс репозитория для работы с данными бд.
    /// </summary>
    public class ContactRepository : IContactRepository
    {
        private readonly Context db;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepository"/> class.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public ContactRepository(Context context)
        {
            this.db = context;
        }
        
        /// <inheritdoc cref="IContactRepository"/>
        public void AddContact(Contact contact)
        {
            this.db.Contacts.Add(contact);
            this.SaveChange();
        }

        /// <inheritdoc cref="IContactRepository"/>
        public List<Contact> GetContacts()
        {
            return this.db.Contacts.ToList();
        }

        /// <inheritdoc cref="IContactRepository"/>
        public Contact GetContact(int id)
        {
            return this.db.Contacts.AsQueryable().FirstOrDefault(x => x.Id == id);
        }

        /// <inheritdoc cref="IContactRepository"/>
        public void DeleteContact(int id)
        {
            var contact = this.db.Contacts.AsQueryable().FirstOrDefault(x => x.Id == id);
            db.Contacts.Remove(contact);
            this.SaveChange();
        }

        /// <inheritdoc cref="IContactRepository"/>
        public void EditContact(Contact contact)
        {
            db.Entry(contact).State = EntityState.Modified;
            this.SaveChange();
        }

        /// <inheritdoc cref="IContactRepository"/>
        public bool CheckAvailability(int id)
        {
            return this.db.Contacts.Any(x => x.Id == id);
        }
        
        private void SaveChange()
        {
            try
            {
                db.SaveChangesAsync();
            }
            catch (SqlException e)
            {
                throw new SqlTypeException("Ошибка при сохранение данных. Проблема: " + e.Message) ;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /*static string FilePart = GetFilePath();

        /// <inheritdoc cref="IContactManager"/>
        public void AddContacts(List<Contact> contacts)
        {
            this.CheckFile();
            this.Serializer(contacts);
        }

        /// <inheritdoc cref="IContactManager"/>
        public List<Contact> GetContacts()
        {
            this.CheckFile();
            var contacts = this.Deserializer();
            return contacts?.Count != 0 ? contacts : null;
        }

        /// <summary>
        /// Метод сериализации списка контактов, загружаемых в файл.
        /// </summary>
        /// <param name="contacts">Список контактов.</param>
        /// <exception cref="Exception"></exception>
        private void Serializer(List<Contact> contacts)
        {
            try
            {
                var serializer = new JsonSerializer();
                using StreamWriter sw = new StreamWriter(FilePart);
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, contacts);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Метод десериализации контактов, хранящихся в файле.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Метод для проверки налиция файла бд (файла .txt)
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        private void CheckFile()
        {
            if (File.Exists(FilePart)) return;

            try
            {
                File.Create(FilePart).Dispose();
            }
            catch (Exception e)
            {
                throw new ArgumentException("Проблемы с созданием базы");
            }
        }

        /// <summary>
        /// Метод для постройки пути до файла бд (файл .txt)
        /// </summary>
        /// <returns>Полный путь до файла.</returns>
        private static string GetFilePath()
        {
            var currentConfig = ConfigurationManager.AppSettings.Get("DbFolder");
            return Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, currentConfig);
        }*/
    }
}
