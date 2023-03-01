using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    //need to pass a connection string to dbcontext
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AppUser> Users {get; set;} //this DbSet represents the table named Users
    }
}