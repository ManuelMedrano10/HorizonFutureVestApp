namespace Application.ViewModels.Macroindicator
{
    public class MacroindicatorViewModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Weight { get; set; }
        public required bool BetterHigh { get; set; }
    }
}
