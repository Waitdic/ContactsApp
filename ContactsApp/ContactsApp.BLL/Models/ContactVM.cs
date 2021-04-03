using System;
using System.Text.RegularExpressions;
using ContactsApp.DAL.Models;

namespace ContactsApp.BLL.Models
{
    /// <summary>
    /// Класс ViewModel контакта, хранящий информацию о человеке 
    /// </summary>
    public class ContactVM
    {
        private int? id;
        private string name;
        private string surname;
        private DateTime birthday;
        private string phone;
        private string email;
        private string vk;

        /// <summary>
        /// Id.
        /// </summary>
        public int? Id
        {
            get => this.id;
            set => this.id = value;
        }

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name
        {
            get => this.name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Имя не было заполнено!");
                }

                this.name = value;
            }
        }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string Surname
        {
            get => this.surname;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Фамилия не была заполнена!");
                }

                this.surname = value;
            }
        }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime Birthday
        {
            get => this.birthday;
            set
            {
                if (!DateTime.TryParse(value.ToString(), out DateTime result))
                {
                    throw new ArgumentException("Дата рождения имеет неверный формат");
                }

                if (value > DateTime.Now.Date)
                {
                    throw new ArgumentException("Дата рождения не может быть больше настоящего времени");
                }

                this.birthday = value;
            }
        }

        /// <summary>
        /// Телефонный номер.
        /// </summary>
        public string Phone
        {
            get => this.phone;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Телефон не был не было вписан!");
                }
                
                if (!Regex.IsMatch(value, @"[0-9]{11}", RegexOptions.IgnoreCase))
                {
                    throw new ArgumentException("Номер телефона имеет неверный формат!");
                }

                this.phone = value;
            }
        }

        /// <summary>
        /// Электронная почта.
        /// </summary>
        public string Email
        {
            get => this.email;
            set
            {
                if (!Regex.IsMatch(value,
                    @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})",
                    RegexOptions.IgnoreCase))
                {
                    throw new ArgumentException("Email имеет неверный формат!");
                }

                this.email = value;
            }
        }

        /// <summary>
        /// Вк.
        /// </summary>
        public string Vk
        {
            get => this.vk;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Вк не был заполнен");
                }

                this.vk = value;
            }
        }
        
        /// <summary>
        /// Конвертировать из ViewModel в Contact.
        /// </summary>
        /// <param name="contact">Объект класса ContactVM.</param>
        /// <returns>Объект класса Contact.</returns>
        public Contact FromViewToModel()
        {
            return new Contact
            {
                Id = this.Id.GetValueOrDefault(),
                Name = this.Name,
                Surname = this.Surname,
                Birthday = this.Birthday,
                Phone = this.Phone,
                Email = this.Email,
                Vk = this.Vk,
            };
        }

        /// <summary>
        /// Конвертировать из Contact в ViewModel.
        /// </summary>
        /// <param name="contact">Объект класса Contact.</param>
        /// <returns>Объект класса ContactVM.</returns>
        public static ContactVM FromModelToView(Contact contact)
        {
            return new ContactVM
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
    }
}
