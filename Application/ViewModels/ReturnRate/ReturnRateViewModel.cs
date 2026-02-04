namespace Application.ViewModels.ReturnRate
{
    public class ReturnRateViewModel
    {
        public required int Id { get; set; }
        public required decimal MinimumRate { get; set; }
        public required decimal MaximumRate { get; set; }
    }
}
