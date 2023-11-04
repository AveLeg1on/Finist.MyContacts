using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Finist.MyContacts.Model
{
   class MyDBContext: DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<FavoritesContact> FavoritesContacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MyContactsDB.db");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavoritesContact>()
                .HasOne(fc => fc.Contacts)
                .WithMany(c => c.FavoritesContacts)
                .HasForeignKey(fc => fc.ContactID);
        }
    }
}
