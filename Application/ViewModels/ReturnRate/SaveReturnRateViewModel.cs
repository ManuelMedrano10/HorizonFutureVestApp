using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.ReturnRate
{
    public class SaveReturnRateViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must enter a minimum rate.")]
        public required decimal MinimumRate { get; set; }
        [Required(ErrorMessage = "You must enter a maximum rate.")]
        public required decimal MaximumRate { get; set; }
    }
}
