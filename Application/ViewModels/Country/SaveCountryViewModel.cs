using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Country
{
    public class SaveCountryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter the name of the country.")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "You must enter the Iso Code of the country.")]
        public required string IsoCode { get; set; }
    }
}
