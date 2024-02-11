using Microsoft.EntityFrameworkCore;
using MVCCrud.Models.Domain;

namespace MVCCrud.Data
{
    public class MVCCrudDBContext : DbContext
    {
        public MVCCrudDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
