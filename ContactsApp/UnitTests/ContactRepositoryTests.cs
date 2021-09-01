 // Потом переделаю тесты
 /*using System;
 using System.Linq;
 using ContactsApp.DAL.Repository;
 using Moq;
 using NUnit.Framework;

namespace UnitTests
{
    /// <summary>
    /// Тесты на проверку ContactRepository
    /// </summary>
    [TestFixture]
    public class ContactRepositoryTests
    {
        private IContactRepository contactRepository;

        private void Initial()
        {
            var moq = new Mock<IFakeContext>();
            moq.Setup(cx => cx.Contacts.ToList())
                .Returns(ContactHelper.GetTestContacts());
            moq
                .Setup(cx => cx.Contacts.AsQueryable().FirstOrDefault(x => x.Id == 0))
                .Returns(ContactHelper.AddNewContact);

            contactRepository = new ContactRepository(moq.Object);
        }
        
        [Test, Description("Тест на добавление контакта в бд. Позитивный тест.")]
        public void AddContact_Contact_CorrectResult()
        {
            // SetUp
            Initial();  
            var contact = ContactHelper.AddNewContact();
            
            // Act
            contactRepository.AddContact(contact);
            var result =this.contactRepository?.GetContacts()?.Where(x => x.Name == contact.Name);
            
            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(result.Count(), 1);
        }
        
        [Test, Description("Тест на получение контакта по Id. Позитивный тест.")]
        public void GetContact_Id_CorrectResult()
        {
            // SetUp
            Initial();
            var contact = ContactHelper.AddNewContact();
            contactRepository.AddContact(contact);
            var model =this.contactRepository?.GetContacts()?.FirstOrDefault(x => x.Name == contact.Name);
            Assert.NotNull(model);
            
            // Act
            var result = this.contactRepository.GetContact(model.Id);
           
            // Assert
            Assert.NotNull(result);
        }
        
        [Test, Description("Тест на удаление контакта в бд. Позитивный тест.")]
        public void DeleteContact_Id_CorrectResult()
        {
            // SetUp
            Initial();
           
            // Act
            contactRepository.DeleteContact(1);
        }
        
        [Test, Description("Тест на изменение контакта в бд. Позитивный тест.")]
        public void EditContact_Contact_CorrectResult()
        {
            // SetUp
            Initial();
            var contact = ContactHelper.AddNewContact();
            contactRepository.AddContact(contact);
            var model =this.contactRepository?.GetContacts()?.FirstOrDefault(x => x.Name == contact.Name);
            Assert.NotNull(model);
            
            // Act
            model.Surname = "editSurname" + Guid.NewGuid().ToString();
            contactRepository.EditContact(model);
        }
    }
}*/