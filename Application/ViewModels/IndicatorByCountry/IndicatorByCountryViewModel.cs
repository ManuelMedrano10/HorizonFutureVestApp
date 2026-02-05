using Application.ViewModels.Country;
using Application.ViewModels.Macroindicator;

namespace Application.ViewModels.IndicatorByCountry
{
    public class IndicatorByCountryViewModel
    {
        public required int Id { get; set; }
        public required decimal IndicatorValue { get; set; }
        public required int Year { get; set; }
        public required int CountryId { get; set; }
        public CountryViewModel? Country { get; set; }
        public required int MacroindicatorId { get; set; }
        public MacroindicatorViewModel? Macroindicator { get; set; }
    }
}
