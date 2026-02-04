namespace Application.Dtos.Macroindicator
{
    public class MacroindicatorDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Weight { get; set; }
        public required bool BetterHigh { get; set; }
    }
}
