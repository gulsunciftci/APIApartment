using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore.Config;

namespace Repositories.EFCore
{
    public class RepositoryContext:DbContext //context veriye erişim noktasında yardımcı olur
    {
        public RepositoryContext(DbContextOptions options)
            :base(options)
        {
           
        }

        public DbSet<Apartment> Apartments { get; set; }

        //Config için ekledik
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApartmentConfig());
        }
    }
}
