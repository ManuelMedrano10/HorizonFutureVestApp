using Application.Services;
using Application.ViewModels.RankingSimulator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HorizonFutureVestApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly RankingService _rankingService;
        private readonly MacroindicatorService _macroindicatorService;
        private readonly IndicatorByCountryService _indicatorByCountryService;
        public HomeController(RankingService rankingService, MacroindicatorService macroindicatorService, IndicatorByCountryService indicatorByCountryService)
        {
            _macroindicatorService = macroindicatorService;
            _rankingService = rankingService;
            _indicatorByCountryService = indicatorByCountryService;
        }
        public async Task<IActionResult> Index(int? year)
        {
            var listIndicators = await _indicatorByCountryService.GetAll();
            var years = listIndicators.Select(i => i.Year).Distinct().OrderByDescending(y => y).ToList();

            ViewBag.Years = new SelectList(years);
            ViewBag.SelectedYear = year;
            List<RankingResultViewModel> ranking = new();

            if(year.HasValue && year.Value > 0)
            {
                var listMacroindicators = await _macroindicatorService.GetAll();
                ranking = await _rankingService.CalculateRanking(listMacroindicators, year.Value);
            }
            
            return View(ranking);
        }   
    }
}
