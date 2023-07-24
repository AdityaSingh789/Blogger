using Blogger.Models;
using Blogger.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext>options) : base(options)
        {

        }
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<User> Users{ get; set; }
    }
}
