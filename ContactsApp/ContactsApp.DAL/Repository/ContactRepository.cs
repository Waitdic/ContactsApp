using System;
using System.Collections.Generic;
using System.IO;
using ContactsApp.DAL.Models;
using Newtonsoft.Json;
using System.Configuration;

namespace ContactsApp.DAL.Repository
{
    /// <summary>
    /// Класс репозитория для работы с данными бд.
    /// </summary>
    public class ContactRepository : IContactRepository
    {
        static string FilePart = GetFilePath();

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
        }
    }
}
