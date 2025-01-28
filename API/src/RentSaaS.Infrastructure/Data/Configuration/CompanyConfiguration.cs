using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentSaaS.Domain.Entities;
using RentSaaS.Domain.Enums;

namespace RentSaaS.Infrastructure.Data.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(x => x.type)
                   .HasConversion(
                       v => v.ToString(), // Convert enum to string for storage
                       v => (CompanyType)Enum.Parse(typeof(CompanyType), v) // Convert string back to enum
                   );
                 
        }
    }
}
