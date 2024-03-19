using Microsoft.EntityFrameworkCore;
using RegLogin.Models;

namespace RegLogin.Entities
{
    public class RegDbContext:DbContext
    {
        public RegDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<RegModel> RegTable { get; set; }
    }
}
