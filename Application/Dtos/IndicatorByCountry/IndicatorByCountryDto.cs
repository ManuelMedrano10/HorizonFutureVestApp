using Application.Dtos.Country;
using Application.Dtos.Macroindicator;

namespace Application.Dtos.IndicatorByCountry
{
    public class IndicatorByCountryDto
    {
        public required int Id { get; set; }
        public required decimal IndicatorValue { get; set; }
        public required int Year { get; set; }
        public required int CountryId { get; set; }
        public CountryDto? Country { get; set; }
        public required int MacroindicatorId { get; set; }
        public MacroindicatorDto? Macroindicator { get; set; }
    }
}
