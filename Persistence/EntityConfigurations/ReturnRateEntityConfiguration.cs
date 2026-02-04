using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.EntityConfigurations
{
    public class ReturnRateEntityConfiguration : IEntityTypeConfiguration<ReturnRate>
    {
        public void Configure(EntityTypeBuilder<ReturnRate> builder)
        {
            #region Basic Configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Rates");
            #endregion

            #region Property Configurations
            builder.Property(rr => rr.MinimumRate).IsRequired();
            builder.Property(rr => rr.MaximumRate).IsRequired();
            #endregion
        }
    }
}
