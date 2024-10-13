namespace BookCURD.Data
{
    using BookCURD.Model;
    using Microsoft.EntityFrameworkCore;

    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
    }

}
