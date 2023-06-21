using crud.Models;
using Microsoft.EntityFrameworkCore;

namespace crud.Data
{
    public class crudDbContent : DbContext
    {
        public crudDbContent(DbContextOptions options)  : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}
