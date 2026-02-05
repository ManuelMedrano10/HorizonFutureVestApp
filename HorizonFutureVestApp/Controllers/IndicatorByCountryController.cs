using Application.Dtos.IndicatorByCountry;
using Application.Services;
using Application.ViewModels.Country;
using Application.ViewModels.IndicatorByCountry;
using Application.ViewModels.Macroindicator;
using Microsoft.AspNetCore.Mvc;
using Persistence.Contexts;

namespace HorizonFutureVestApp.Controllers
{
    public class IndicatorByCountryController : Controller
    {
        private readonly IndicatorByCountryService _indicatorByCountryService;
        private readonly CountryService _countryService;
        private readonly MacroindicatorService _macroindicatorService;

        public IndicatorByCountryController(HorizonDbContext horizonDbContext)
        {
            _indicatorByCountryService = new IndicatorByCountryService(horizonDbContext);
            _countryService = new CountryService(horizonDbContext);
            _macroindicatorService = new MacroindicatorService(horizonDbContext);
        }

        public async Task<IActionResult> Index(int? countryId, int? year)
        {
            ViewBag.Country = await _countryService.GetAll();
            ViewBag.CurrentCountryId = countryId;
            ViewBag.CurrentYear = year;

            var dtos = await _indicatorByCountryService.GetFiltered(countryId, year);

            var listEntityVms = dtos.Select(ibc =>
                new IndicatorByCountryViewModel()
                {
                    Id = ibc.Id,
                    IndicatorValue = ibc.IndicatorValue,
                    Year = ibc.Year,
                    CountryId = ibc.CountryId,
                    Country = ibc.Country == null ? null : new CountryViewModel()
                    {
                        Id = ibc.Country.Id,
                        Name = ibc.Country.Name,
                        IsoCode = ibc.Country.IsoCode
                    },
                    MacroindicatorId = ibc.MacroindicatorId,
                    Macroindicator = ibc.Macroindicator == null ? null : new MacroindicatorViewModel()
                    {
                        Id = ibc.Macroindicator.Id,
                        Name = ibc.Macroindicator.Name,
                        Weight = ibc.Macroindicator.Weight,
                        BetterHigh = ibc.Macroindicator.BetterHigh
                    }
                }).ToList();

            return View(listEntityVms);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Country = await _countryService.GetAll();
            ViewBag.Macroindicator = await _macroindicatorService.GetAll();

            return View("Save", new SaveIndicatorByCountryViewModel()
            {
                IndicatorValue = 0,
                Year = 0,
                CountryId = null,
                MacroindicatorId = null
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveIndicatorByCountryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Country = await _countryService.GetAll();
                ViewBag.Macroindicator = await _macroindicatorService.GetAll();

                return View("Save", vm);
            }

            IndicatorByCountryDto dto = new()
            {
                Id = 0,
                IndicatorValue = vm.IndicatorValue,
                Year = vm.Year,
                CountryId = vm.CountryId ?? 0,
                MacroindicatorId = vm.MacroindicatorId ?? 0
            };

            await _indicatorByCountryService.AddAsync(dto);
            return RedirectToRoute(new { controller = "IndicatorByCountry", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.EditMode = true;
            ViewBag.Country = await _countryService.GetAll();
            ViewBag.Macroindicator = await _macroindicatorService.GetAll();

            var dto = await _indicatorByCountryService.GetById(id);

            if(dto == null)
            {
                return RedirectToRoute(new { controller = "IndicatorByCountry", action = "Index" });
            }

            SaveIndicatorByCountryViewModel vm = new()
            {
                Id = dto.Id,
                IndicatorValue = dto.IndicatorValue,
                Year = dto.Year,
                CountryId = dto.CountryId,
                MacroindicatorId = dto.MacroindicatorId
            };

            return View("Save", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveIndicatorByCountryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EditMode = true;
                ViewBag.Country = await _countryService.GetAll();
                ViewBag.Macroindicator = await _macroindicatorService.GetAll();

                return View("Save", vm);
            }

            IndicatorByCountryDto dto = new()
            {
                Id = vm.Id,
                IndicatorValue = vm.IndicatorValue,
                Year = vm.Year,
                CountryId = vm.CountryId ?? 0,
                MacroindicatorId = vm.MacroindicatorId ?? 0
            };
            await _indicatorByCountryService.UpdateAsync(dto);
            return RedirectToRoute(new { controller = "IndicatorByCountry", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _indicatorByCountryService.GetById(id);

            if(dto == null)
            {
                return RedirectToRoute(new { controller = "IndicatorByCountry", action = "Index" });
            }

            DeleteIndicatorByCountryViewModel vm = new()
            {
                Id = dto.Id,
                Year = dto.Year
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteIndicatorByCountryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _indicatorByCountryService.DeleteAsync(vm.Id);
            return RedirectToRoute(new { controller = "IndicatorByCountry", action = "Index" });
        }
    }
}
