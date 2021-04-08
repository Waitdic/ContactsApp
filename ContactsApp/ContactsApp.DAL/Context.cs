using ContactsApp.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.DAL
{
    public sealed class Context : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}