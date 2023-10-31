using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models;

namespace WebApi.Repositories.Config
{
    public class ApartmentConfig : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.HasData(

                new Apartment { Id=1,No=3,Status="Boş",Floor=2,Type="3+1"},
                new Apartment { Id = 2, No = 6, Status = "Dolu", Floor = 4, Type = "3+1" }
            );
        }
    }
}
