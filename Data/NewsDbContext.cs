using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsWebsite.Models;

namespace NewsWebsite.Data
{
    public class NewsDbContext: DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options)
         : base(options) {
        }

        public DbSet<News> News { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TeamMember> teamMembers { get; set; }
        public DbSet<ContactUs> contacts { get; set; }
    }
}
