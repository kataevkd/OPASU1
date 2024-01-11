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

namespace TestProjectFORRESTfull
{
    public class TestHelper
    {
        private readonly Context _context;
        public TestHelper()
        {
            //Используем базу обычную базу данных, не в памяти
            //Имя тестовой базы данных должно отличатсья от базы данных проекта
            var contextOptions = new DbContextOptionsBuilder<Context>()
                .UseNpgsql(@"Server=(localhost)\postgres;Database=Test")
                .Options;

            _context = new Context(contextOptions);


            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            //Значение идентификатора явно не задаем. Используем для идентификации уникальное в рамках теста имя
            var author1 = new Author
            {
                Name = "Andrik",
            };
            author1.AddPublication(new Publication { PublicationName = "First World War", PublicationTheme = "History", PublicationDate = "19.02.2011" });
            author1.AddPublication(new Publication { PublicationName = "WWW", PublicationTheme = "Network", PublicationDate = "19.03.2012" });

            _context.Authors.Add(author1);
            _context.SaveChanges();
            //Запрещаем отслеживание (разрываем связи с БД)
            _context.ChangeTracker.Clear();
        }

        public AuthorRepository AuthorRepository
        {
            get
            {
                return new AuthorRepository(_context);
            }
        }
    }
}
