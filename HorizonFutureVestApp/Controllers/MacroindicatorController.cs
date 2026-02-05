using Application.Dtos.Macroindicator;
using Application.Services;
using Application.ViewModels.Macroindicator;
using Microsoft.AspNetCore.Mvc;
using Persistence.Contexts;

namespace HorizonFutureVestApp.Controllers
{
    public class MacroindicatorController : Controller
    {
        private readonly MacroindicatorService _macroindicatorService;
        public MacroindicatorController(HorizonDbContext horizonDbContext)
        {
            _macroindicatorService = new MacroindicatorService(horizonDbContext);
        }
        public async Task<IActionResult> Index()
        {
            var dtos = await _macroindicatorService.GetAll();

            var listEntityVms = dtos.Select(m =>
                new MacroindicatorViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    Weight = m.Weight,
                    BetterHigh = m.BetterHigh
                }).ToList();

            return View(listEntityVms);
        }

        public IActionResult Create()
        {
            return View("Save", new SaveMacroindicatorViewModel() { Name = "", Weight = 0, BetterHigh = false });
        }
        [HttpPost]
        public async Task<IActionResult> Create(SaveMacroindicatorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Save", vm);
            }

            MacroindicatorDto dto = new()
            {
                Id = 0,
                Name = vm.Name,
                Weight = vm.Weight,
                BetterHigh = vm.BetterHigh
            };
            await _macroindicatorService.AddAsync(dto);
            return RedirectToRoute(new { controller = "Macroindicator", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.EditMode = true;
            var dto = await _macroindicatorService.GetById(id);

            if(dto == null)
            {
                return RedirectToRoute(new { controller = "Macroindicator", action = "Index" });
            }

            SaveMacroindicatorViewModel vm = new()
            {
                Id = dto.Id,
                Name = dto.Name,
                Weight = dto.Weight,
                BetterHigh = dto.BetterHigh
            };
            return View("Save", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveMacroindicatorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EditMode = true;
                return View("Save", vm);
            }

            MacroindicatorDto dto = new()
            {
                Id = vm.Id,
                Name = vm.Name,
                Weight = vm.Weight,
                BetterHigh = vm.BetterHigh
            };
            await _macroindicatorService.UpdateAsync(dto);
            return RedirectToRoute(new { controller = "Macroindicator", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _macroindicatorService.GetById(id);

            if (dto == null)
            {
                return RedirectToRoute(new { controller = "Macroindicator", action = "Index" });
            }

            DeleteMacroindicatorViewModel vm = new()
            {
                Id = dto.Id,
                Name = dto.Name
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteMacroindicatorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _macroindicatorService.DeleteAsync(vm.Id);
            return RedirectToRoute(new { controller = "Macroindicator", action = "Index" });
        }
    }
}