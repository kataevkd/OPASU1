using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System;
using RESTfull.Domain;
namespace RESTfull.Infrastructure
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; } 
        public DbSet<Publication> Publications { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
