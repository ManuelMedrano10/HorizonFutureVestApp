using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.EntityConfigurations
{
    public class CountryEntityConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            #region Basic Configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Countries");
            #endregion

            #region Property Configurations
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.IsoCode).IsRequired().HasMaxLength(2);
            #endregion
        }
    }
}
