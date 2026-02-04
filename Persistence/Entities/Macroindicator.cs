using Microsoft.VisualBasic;

namespace Persistence.Entities
{
    public class Macroindicator
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Weight { get; set; }
        public required bool BetterHigh { get; set; }

        //Navigation property
        public ICollection<IndicatorByCountry>? IndicatorsByCountries { get; set; }
    }
}
