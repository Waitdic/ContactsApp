using System;
using ContactsApp.DAL.Models;
using NUnit.Framework;

namespace UnitTests
{
    public class ContactTest
    {
        /// <summary>
        /// Тест на присваивание значений полям класса Contact.
        /// </summary>
        /// <param name="field">Поле класса.</param>
        /// <param name="value">Значение.</param>
        [Test]
        [TestCase("Name", "Name")]
        [TestCase("Surname", "Surname")]
        [TestCase("Phone", "8-111-111-11-11")]
        [TestCase("Email", "danis161616@yandex.ru")]
        [TestCase("Vk", "daniska1616")]
        [TestCase("Birthday", "")]
        public void AssignmenValuesInFieldsTest (string field, string value)
        {
            var contact = new Contact();
            switch (field)
            {
                case "Name":
                    contact.Name = value;
                    Assert.AreEqual(contact.Name, value);
                    break;
                case "Surname":
                    contact.Surname = value;
                    Assert.AreEqual(contact.Surname, value);
                    break;
                case "Phone":
                    contact.Phone = value;
                    Assert.AreEqual(contact.Phone, value);
                    break;
                case "Email":
                    contact.Email = value;
                    Assert.AreEqual(contact.Email, value);
                    break;
                case "Vk":
                    contact.Vk = value;
                    Assert.AreEqual(contact.Vk, value);
                    break;
                case "Birthday":
                    var date = DateTime.Now.Date;
                    contact.Birthday = date;
                    Assert.AreEqual(contact.Birthday, date);
                    break;
                default:
                    throw new Exception("Поле отсутсвует");
            }
        }

        /// <summary>
        /// Тест на валидацию присвоение имени.
        /// </summary>
        /// <param name="value">Присваиваемая переменная.</param>
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ValidateNameTest(string value)
        {
            try
            {
                var contact = new Contact();
                contact.Name = value;
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual(e.Message, "Имя не было заполнено!");
            }
        }
        
        /// <summary>
        /// Тест на валидацию присвоение фамилии.
        /// </summary>
        /// <param name="value">Присваиваемая переменная.</param>
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ValidateSurnameTest(string value)
        {
            try
            {
                var contact = new Contact();
                contact.Surname = value;
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual(e.Message, "Фамилия не была заполнена!");
            }
        }
        
        /// <summary>
        /// Тест на валидацию присвоение даты.
        /// </summary>
        /// <param name="value">Присваиваемая переменная.</param>
        /// <param name="message">Ошибка.</param>
        [Test]
        // [TestCase("фыв", "Дата рождения имеет неверный формат")]
        [TestCase(null, "Дата рождения не может быть больше настоящего времени")]
        public void ValidateBirthdayTest(string value, string message)
        {
            try
            {
                var contact = new Contact();
                var date = DateTime.Now.AddDays(1).Date;
                contact.Birthday = date;
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual(e.Message, message);
            }
        }
        
        /// <summary>
        /// Тест на валидацию присвоение номера.
        /// </summary>
        /// <param name="value">Присваиваемая переменная.</param>
        /// <param name="message">Ошибка.</param>
        [Test]
        [TestCase("", "Телефон не был не было вписан!")]
        [TestCase(null, "Телефон не был не было вписан!")]
        [TestCase("AD", "Номер телефона имеет неверный формат!")]
        [TestCase("123ad", "Номер телефона имеет неверный формат!")]
        [TestCase("ЯБ", "Номер телефона имеет неверный формат!")]
        [TestCase("123ая", "Номер телефона имеет неверный формат!")]
        public void ValidatePhoneTest(string value, string message)
        {
            try
            {
                var contact = new Contact();
                contact.Phone = value;
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual(e.Message, message);
            }
        }

        /// <summary>
        /// Тест на валидацию присвоение Email.
        /// </summary>
        /// <param name="value">Присваиваемая переменная.</param>
        [Test]
        [TestCase("asd@gmail")]
        [TestCase("@gmail")]
        [TestCase("#as@gmail")]
        [TestCase("@gmail.com")]
        public void ValidateEmailTest(string value)
        {
            try
            {
                var contact = new Contact();
                contact.Email = value;
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual(e.Message, "Email имеет неверный формат!");
            }
        }
        
        /// <summary>
        /// Тест на валидацию присвоение Vk.
        /// </summary>
        /// <param name="value">Присваиваемая переменная.</param>
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void ValidateVkTest(string value)
        {
            try
            {
                var contact = new Contact();
                contact.Vk = value;
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual(e.Message,"Вк не был заполнен" );
            }
        }
    }
}
