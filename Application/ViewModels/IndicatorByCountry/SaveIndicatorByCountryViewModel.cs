using System.ComponentModel.DataAnnotations;
using Application.ViewModels.Country;
using Application.ViewModels.Macroindicator;

namespace Application.ViewModels.IndicatorByCountry
{
    public class SaveIndicatorByCountryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter the value of the indicator.")]
        public required decimal IndicatorValue { get; set; }
        [Required(ErrorMessage = "You must enter the year of the indicator.")]
        public required int Year { get; set; }

        [Range(1,int.MaxValue,ErrorMessage = "You must enter the valid Country of the indicator.")]
        public int? CountryId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must enter the valid Macroindicator of the indicator.")]
        public int? MacroindicatorId { get; set; }
    }
}
