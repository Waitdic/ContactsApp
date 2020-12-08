using System;
using System.Text.RegularExpressions;

namespace ContactsApp.BLL.Models
{
    public class Contact
    {
        private int id;
        private string name;
        private string surname;
        private DateTime birthday;
        private string phone;
        private string email;
        private string vk;

        /// <summary>Id.</summary>
        public int Id
        {
            get => this.id;
            set
            {
                if (!int.TryParse(value.ToString(), out value))
                {
                    throw new ArgumentException("Неправильный формат идентификатора!");
                }

                this.Id = value;
            }
        }

        /// <summary>Имя.</summary>
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

        /// <summary>Фамилия.</summary>
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

        /// <summary>Дата рождения.</summary>
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

        /// <summary>Телефонный номер.</summary>
        public string Phone
        {
            get => this.phone;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Телефон не был не было вписан!");
                }

                this.phone = value;
            }
        }

        /// <summary>Электронная почта.</summary>
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

        /// <summary>Вк.</summary>
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
    }
}
