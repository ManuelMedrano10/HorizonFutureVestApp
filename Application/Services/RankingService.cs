using Application.Dtos.Macroindicator;
using Application.ViewModels.RankingSimulator;

namespace Application.Services
{
    public class RankingService
    {
        private readonly IndicatorByCountryService _indicatorByCountryService;
        private readonly CountryService _countryService;
        private readonly ReturnRateService _returnRateService;

        public RankingService(IndicatorByCountryService indicatorByCountryService, CountryService countryService, ReturnRateService returnRateService )
        {
            _indicatorByCountryService = indicatorByCountryService;
            _countryService = countryService;
            _returnRateService = returnRateService;
        }

        public async Task<List<RankingResultViewModel>> CalculateRanking(List<MacroindicatorDto> macroindicatorDtos, int year)
        {
            var countries = await _countryService.GetAll();
            var returnRates = await _returnRateService.GetAsync();
            var indicatorsByCountry = await _indicatorByCountryService.GetAll();
            var indicatorsByYear = await _indicatorByCountryService.GetByYear(year);

            decimal minimumRate = returnRates != null ? returnRates.MinimumRate : 2;
            decimal maximumRate = returnRates != null ? returnRates.MaximumRate : 15;

            var results = new List<RankingResultViewModel>();

            var ranges = new Dictionary<int, (decimal Min, decimal Max)>();

            foreach(var m in macroindicatorDtos)
            {
                var values = indicatorsByCountry.Where(i => i.MacroindicatorId == m.Id)
                                                .Select(i => i.IndicatorValue).ToList();

                if (values.Any())
                {
                    ranges[m.Id] = (values.Min(), values.Max());
                }
                else
                {
                    ranges[m.Id] = (0, 0);
                }
            }

            foreach (var c in countries)
            {
                decimal totalScoring = 0;
                bool IsValid = false;

                foreach (var m in macroindicatorDtos)
                {
                    var indicator = indicatorsByCountry.FirstOrDefault(i => i.CountryId == c.Id && i.MacroindicatorId == m.Id);

                    if (indicator != null && ranges.ContainsKey(m.Id))
                    {
                        IsValid = true;
                        var range = ranges[m.Id];
                        decimal rawValue = indicator.IndicatorValue;
                        decimal normalizedValue = 0;

                        if (range.Max - range.Min == 0)
                        {
                            normalizedValue = 1;
                        }
                        else
                        {
                            if (m.BetterHigh)
                            {
                                normalizedValue = (rawValue - range.Min) / (range.Max - range.Min);
                            }
                            else
                            {
                                normalizedValue = (range.Max - rawValue) / (range.Max - range.Min);
                            }
                        }

                        totalScoring += normalizedValue * m.Weight;
                    }
                }
                if (IsValid)
                {
                    decimal finalScoring = totalScoring * 100;
                    decimal roi = minimumRate + ((finalScoring / 100) * (maximumRate - minimumRate));

                    results.Add(new RankingResultViewModel()
                    {
                        CountryName = c.Name,
                        IsoCode = c.IsoCode,
                        Score = Math.Round(finalScoring, 2),
                        ReturnRate = Math.Round(roi, 2)
                    });
                }
            }

            results = results.OrderByDescending(x => x.Score).ToList();
            return results;
        }
    }
}
