using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.EntityConfigurations
{
    public class IndicatorByCountryEntityConfiguration : IEntityTypeConfiguration<IndicatorByCountry>
    {
        public void Configure(EntityTypeBuilder<IndicatorByCountry> builder)
        {
            #region Basic Configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("IndicatorsByCountries");
            #endregion

            #region Property Configurations
            builder.Property(ic => ic.IndicatorValue).IsRequired();
            #endregion

            #region Relationships
            builder.HasOne(ic => ic.Country)
                .WithMany(c => c.IndicatorsByCountries)
                .HasForeignKey(ic => ic.CountryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ic => ic.Macroindicator)
                .WithMany(m => m.IndicatorsByCountries)
                .HasForeignKey(ic => ic.MacroindicatorId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
