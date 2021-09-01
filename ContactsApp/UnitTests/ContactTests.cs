using System;
using ContactsApp.DAL.Models;
using NUnit.Framework;

namespace UnitTests
{
    /// <summary>
    /// Тест на валидацию класса Contact
    /// </summary>
    [TestFixture]
    public class ContactTests
    {
        private const string PropertyNameTest = "Тестирование присвоения {1} в Contact. {0}";
        
        /// <param name="field">Поле класса.</param>
        /// <param name="value">Значение.</param>
        [TestCase("Name", "Name", TestName = PropertyNameTest, Description = "Позитивный тест.")]
        public void Name_CorrectName(string field, string value)
        {
            // SetUp
            var contact = new Contact();
            
            // Act
            contact.Name = value;
            
            // Assert
            Assert.AreEqual(contact.Name, value);
        }
       
        /// <param name="field">Поле класса.</param>
        /// <param name="value">Значение.</param>
        [TestCase("Surname", "Surname", TestName = PropertyNameTest, Description = "Позитивный тест.")]
        public void Surname_CorrectSurname(string field, string value)
        {
            // SetUp
            var contact = new Contact();
            
            // Act
            contact.Surname = value;
            
            // Assert
            Assert.AreEqual(contact.Surname, value);
        }
        
        /// <param name="field">Поле класса.</param>
        /// <param name="value">Значение.</param>
        [TestCase("Phone", "81111111111", TestName = PropertyNameTest, Description = "Позитивный тест.")]
        public void Phone_CorrectPhone(string field, string value)
        {
            // SetUp
            var contact = new Contact();
            
            // Act
            contact.Phone = value;
            
            // Assert
            Assert.AreEqual(contact.Phone, value);
        }
        
        /// <param name="field">Поле класса.</param>
        /// <param name="value">Значение.</param>
        [TestCase("Email", "danis161616@yandex.ru", TestName = PropertyNameTest, Description = "Позитивный тест.")]
        public void Email_CorrectEmail(string field, string value)
        {
            // SetUp
            var contact = new Contact();
            
            // Act
            contact.Email = value;
            
            // Assert
            Assert.AreEqual(contact.Email, value);
        }
        
        /// <param name="field">Поле класса.</param>
        /// <param name="value">Значение.</param>
        [TestCase("", "daniska1616", TestName = PropertyNameTest, Description = "Позитивный тест.")]
        public void Vk_CorrectVk(string field, string value)
        {
            // SetUp
            var contact = new Contact();
            
            // Act
            contact.Vk = value;
            
            // Assert
            Assert.AreEqual(contact.Vk, value);
        }
        
        [Test, Description("Позитивный тест.")]
        public void Birthday_CorrectBirthday()
        {
            // SetUp
            var contact = new Contact();
            var date = DateTime.Now.Date;

            // Act
            contact.Birthday = date;
            
            // Assert
            Assert.AreEqual(contact.Birthday, date);
        }
        
        [Test, Description("Тест на валидацию присвоение Id. Негативный тест.")]
        public void Id_WrongId_ThrowsException()
        {
            // Setup
            var contact = new Contact();

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                contact.Id = -1;
            });
        }
        
        /// <param name="value">Присваиваемая переменная.</param>
        [TestCase("", TestName = "Ошибка присвоение {0} в Contact.Name,", Description = "Негативный тест.")]
        [TestCase(null, TestName = "Ошибка присвоение {0} в Contact.Name", Description = "Негативный тест.")]
        public void Name_WrongName_ThrowException(string value)
        {
            // SetUp
            var contact = new Contact();

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                contact.Name = value;
            });
        }
        
        /// <param name="value">Присваиваемая переменная.</param>
        [TestCase("", TestName = "Ошибка присвоение {0} в Contact.Surname", Description = "Негативный тест.")]
        [TestCase(null, TestName = "Ошибка присвоение {0} в Contact.Surname", Description = "Негативный тест.")]
        public void Surname_WrongSurname_ThrowException(string value)
        {
            // SetUp
            var contact = new Contact();
            
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                contact.Surname = value;
            });
        }
        
        [Test, Description("Тест на валидацию присвоение даты. Негативный тест.")]
        public void Birthday_WrongBirthday_ThrowException()
        {
            // SetUp
            var contact = new Contact();
            
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                var test = DateTime.Now.AddDays(1).Date;
                contact.Birthday = test;
            });
        }
        
        /// <param name="value">Присваиваемая переменная.</param>
        /// <param name="message">Ошибка.</param>
        [TestCase("", "Телефон не был не было вписан!", TestName = "Ошибка присвоение {0} в Contact.Phone с ошибкой {1}")]
        [TestCase(null, "Телефон не был не было вписан!", TestName = "Ошибка присвоение {0} в Contact.Phone с ошибкой {1}")]
        [TestCase("AD", "Номер телефона имеет неверный формат!", TestName = "Ошибка присвоение {0} в Contact.Phone с ошибкой {1}")]
        [TestCase("123ad", "Номер телефона имеет неверный формат!", TestName = "Ошибка присвоение {0} в Contact.Phone с ошибкой {1}")]
        [TestCase("ЯБ", "Номер телефона имеет неверный формат!", TestName = "Ошибка присвоение {0} в Contact.Phone с ошибкой {1}")]
        [TestCase("123ая", "Номер телефона имеет неверный формат!", TestName = "Ошибка присвоение {0} в Contact.Phone с ошибкой {1}")]
        public void Phone_WrongPhone_ThrowException(string value, string message)
        {
            // SetUp
            var contact = new Contact();
            
            // Assert
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                // Act
                contact.Phone = value;
            }).Message;
            Assert.AreEqual(ex, message);
        }
        
        /// <param name="value">Присваиваемая переменная.</param>
        [TestCase("asd@gmail", TestName = "Ошибка присвоение {0} в Contact.Email", Description = "Негативный тест.")]
        [TestCase("@gmail", TestName = "Ошибка присвоение {0} в Contact.Email", Description = "Негативный тест.")]
        [TestCase("#as@gmail", TestName = "Ошибка присвоение {0} в Contact.Email", Description = "Негативный тест.")]
        [TestCase("@gmail.com", TestName = "Ошибка присвоение {0} в Contact.Email", Description = "Негативный тест.")]
        public void Email_WrongEmail_ThrowException(string value)
        {
            // SetUp
            var contact = new Contact();
            
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                contact.Email = value;
            });
        }
        
        /// <param name="value">Присваиваемая переменная.</param>
        [TestCase("", TestName = "Ошибка присвоение {0} в Contact.Vk", Description = "Негативный тест.")]
        [TestCase(null, TestName = "Ошибка присвоение {0} в Contact.Vk", Description = "Негативный тест.")]
        public void Vk_WrongVk_ThrowException(string value)
        {
            // SetUp
            var contact = new Contact();
            
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                contact.Vk = value;
            });
        }
    }
}
