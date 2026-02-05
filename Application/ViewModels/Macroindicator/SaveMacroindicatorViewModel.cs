using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Macroindicator
{
    public class SaveMacroindicatorViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must enter the name of the macrodindicator.")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "You must enter the weight of the country.")]
        public required decimal Weight { get; set; }
        [Required(ErrorMessage = "You must enter if the macroindicator is better high or not.")]
        public required bool BetterHigh { get; set; }
    }
}
