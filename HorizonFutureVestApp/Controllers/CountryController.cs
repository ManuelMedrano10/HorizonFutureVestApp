using Application.Dtos.Country;
using Application.Services;
using Application.ViewModels.Country;
using Microsoft.AspNetCore.Mvc;
using Persistence.Contexts;

namespace HorizonFutureVestApp.Controllers
{
    public class CountryController : Controller
    {
        private readonly CountryService _countryService;
        public CountryController(HorizonDbContext horizonDbContext)
        {
            _countryService = new CountryService(horizonDbContext);
        }
        public async Task<IActionResult> Index()
        {
            var dtos = await _countryService.GetAll();

            var listEntityVms = dtos.Select(c =>
                new CountryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsoCode = c.IsoCode
                }).ToList();

            return View(listEntityVms);
        }

        public IActionResult Create()
        {
            return View("Save", new SaveCountryViewModel() { Name = "", IsoCode = "" });
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveCountryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Save", vm);
            }

            CountryDto dto = new()
            {
                Id = 0,
                Name = vm.Name,
                IsoCode = vm.IsoCode
            };
            await _countryService.AddAsync(dto);
            return RedirectToRoute(new { controller = "Country", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.EditMode = true;
            var dto = await _countryService.GetById(id);

            if(dto == null)
            {
                return RedirectToRoute(new { controller = "Country", action = "Index" });
            }

            SaveCountryViewModel vm = new()
            {
                Id = dto.Id,
                Name = dto.Name,
                IsoCode = dto.IsoCode
            };
            return View("Save", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveCountryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EditMode = true;
                return View("Save", vm);
            }

            CountryDto dto = new()
            {
                Id = vm.Id,
                Name = vm.Name,
                IsoCode = vm.IsoCode
            };

            await _countryService.UpdateAsync(dto);
            return RedirectToRoute(new { controller = "Country", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _countryService.GetById(id);
            if(dto == null)
            {
                return RedirectToRoute(new { controller = "Country", action = "Index" });
            }

            DeleteCountryViewModel vm = new()
            {
                Id = dto.Id,
                Name = dto.Name
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteCountryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _countryService.DeleteAsync(vm.Id);
            return RedirectToRoute(new { controller = "Country", action = "Index" });
        }
    }
}
