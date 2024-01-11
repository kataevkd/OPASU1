using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using RESTfull.Infrastructure;
using RESTfull.Infrastructure.Repository;
using RESTfull.Domain;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TestProjectFORRESTfull
{
    public class testAuthorRep
    {
        //r
        //Тест, проверяющий, что база данных создалась
        public void VoidTest()
        {
            var testHelper = new TestHelper();
            var authorRepository = testHelper.AuthorRepository;

            Assert.Equals(1, 1);
        }


        public async void TestAdd()
        {
            var testHelper = new TestHelper();
            var authorRepository = testHelper.AuthorRepository;
            var person = new Author { Name = "Dimas" };
            person.Id = Guid.NewGuid();

            var perons = await authorRepository.AddAsync(person);
            //Запрещаем отслеживание сущностей (разрываем связи с БД)
            authorRepository.ChangeTrackerClear();

            Assert.IsTrue(authorRepository.GetAllAsync().Result.Count == 2);
            Assert.Equals("Dimas", authorRepository.GetByIdAsync(person.Id).Result.Name);
            Assert.Equals("Andrik", authorRepository.GetByNameAsync("Andrik").Result.Name);
            Assert.Equals("Dimas", authorRepository.GetByNameAsync("Dimas").Result.Name);
            Assert.Equals("Andrik", authorRepository.GetByIdAsync(person.Id).Result.Name);
        }


        public void TestUpdateAdd()
        {
            var testHelper = new TestHelper();
            var authorRepository = testHelper.AuthorRepository;
            var person = authorRepository.GetByNameAsync("Andrik").Result;
            //Запрещаем отслеживание сущностей (разрываем связи с БД)
            authorRepository.ChangeTrackerClear();
            person.Name = "Kostik Latyshev";
            var phoneNumber = new Publication { PublicationName = "NAT", PublicationTheme = "Network" };
            person.AddPublication(phoneNumber);

            authorRepository.UpdateAsync(person).Wait();

            Assert.Equals("Kostik Latyshe", authorRepository.GetByNameAsync("Kostik Latyshe").Result.Name);
            Assert.Equals(3, authorRepository.GetByNameAsync("Kostik Latyshe").Result.PublicationCount);
        }


        public void TestUpdateDelete()
        {
            var testHelper = new TestHelper();
            var authorRepository = testHelper.AuthorRepository;
            var author = authorRepository.GetByNameAsync("Andrik").Result;
            //Запрещаем отслеживание сущностей (разрываем связи с БД)
            authorRepository.ChangeTrackerClear();
            author.RemovePublication(0);

            authorRepository.UpdateAsync(author).Wait();

            Assert.Equals(1, authorRepository.GetByNameAsync("Andrik").Result.PublicationCount);
        }
    }
}
