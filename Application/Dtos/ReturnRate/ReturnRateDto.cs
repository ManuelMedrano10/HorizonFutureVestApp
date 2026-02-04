namespace Application.Dtos.ReturnRate
{
    public class ReturnRateDto
    {
        public required int Id { get; set; }
        public required decimal MinimumRate { get; set; }
        public required decimal MaximumRate { get; set; }
    }
}
