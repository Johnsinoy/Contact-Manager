using Microsoft.EntityFrameworkCore;

namespace ContactManager.Models
{
    public class ContactContext:DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options)
             : base(options)
        { }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Friend" },
            new Category { CategoryId = 2, Name = "Work" },
            new Category { CategoryId = 3, Name = "Family" }
            );
            modelBuilder.Entity<Contact>().HasData(
           new Contact
           {
               ContactId = 1,
               FirstName = "Dolores",
               LastName = "Del Rio",
               Phone = "555-987-6543",
               Email = "dolores@hotmail.com",
               CategoryId = 1,
               DateAdded = new DateTime(2024, 2, 10, 0, 0, 0, DateTimeKind.Utc) // Use a fixed date

           },
           new Contact
           {
               ContactId = 2,
               FirstName = "Efren",
               LastName = "Herrera",
               Phone = "555-456-7890",
               Email = "efren@aol.com",
               CategoryId = 2,
               DateAdded = new DateTime(2024, 2, 10, 0, 0, 0, DateTimeKind.Utc) // Use a fixed date

           },
           new Contact
           {
               ContactId = 3,
               FirstName = "Mary Ellen",
               LastName = "Walton",
               Phone = "555-123-4567",
               Email = "MaryEllen@yahoo.com",
               CategoryId = 3,
               DateAdded = new DateTime(2024, 2, 10, 0, 0, 0, DateTimeKind.Utc) // Use a fixed date

           }

           );
        }

    }
}
