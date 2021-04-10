using System;
using System.Collections.Generic;
using ContactsApp.DAL.Models;
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
            var test = this.db.Contacts.Add(contact);
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
                db.SaveChanges();
            }
            catch (SqlException e)
            {
                throw new SqlTypeException("Ошибка при сохранение данных. Проблема: " + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
