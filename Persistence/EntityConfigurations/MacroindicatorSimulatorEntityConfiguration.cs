using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.EntityConfigurations
{
    public class MacroindicatorSimulatorEntityConfiguration : IEntityTypeConfiguration<MacroindicatorSimulator>
    {
        public void Configure(EntityTypeBuilder<MacroindicatorSimulator> builder)
        {
            #region Basic Configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("MacroindicatorsSimulaaror");
            #endregion

            #region Property Configurations
            builder.Property(mi => mi.Name).IsRequired().HasMaxLength(100);
            builder.Property(mi => mi.Weight).IsRequired();
            builder.Property(mi => mi.BetterHigh).IsRequired();
            #endregion

            #region Relatioships
            builder.
            #endregion
        }
    }
}
