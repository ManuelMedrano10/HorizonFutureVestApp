namespace Persistence.Entities
{
    public class ReturnRate
    {
        public required int Id { get; set; }
        public required decimal MinimumRate { get; set; }
        public required decimal MaximumRate { get; set; }
    }
}
