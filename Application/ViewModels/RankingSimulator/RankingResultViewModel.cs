namespace Application.ViewModels.RankingSimulator
{
    public class RankingResultViewModel
    {
        public required string CountryName { get; set; }
        public required string IsoCode { get; set; }
        public required decimal Score { get; set; }
        public required decimal ReturnRate { get; set; }
    }
}
