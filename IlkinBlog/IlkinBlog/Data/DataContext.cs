
using IlkinBlog.Models;


namespace IlkinBlog.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Blog> blogs { get; set; }

        public DbSet<Category> categories { get; set;  }

        public DbSet<User> users { get; set; }

        public DbSet<Color> colors { get; set; }

        public DbSet<Question> questions { get; set; }

    }
}
