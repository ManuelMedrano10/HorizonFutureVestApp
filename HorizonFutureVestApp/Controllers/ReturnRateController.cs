using Application.Dtos.ReturnRate;
using Application.Services;
using Application.ViewModels.ReturnRate;
using Microsoft.AspNetCore.Mvc;
using Persistence.Contexts;
using Persistence.Entities;

namespace HorizonFutureVestApp.Controllers
{
    public class ReturnRateController : Controller
    {
        private readonly ReturnRateService _returnRateService;
        
        public ReturnRateController(HorizonDbContext context)
        {
            _returnRateService = new ReturnRateService(context);
        }

        public async Task<IActionResult> Index()
        {
            var dtos = await _returnRateService.GetAll();

            var listEntityVms = dtos.Select(rr =>
                new ReturnRateViewModel()
                {
                    Id = rr.Id,
                    MaximumRate = rr.MaximumRate,
                    MinimumRate = rr.MinimumRate
                }).ToList();

            return View(listEntityVms);
        }

        public async Task<IActionResult> Configure(int id)
        {
            ViewBag.EditMode = true;
            var dto = await _returnRateService.GetById(id);
            if(dto == null)
            {
                SaveReturnRateViewModel newVm = new()
                {
                    Id = 0,
                    MaximumRate = 0,
                    MinimumRate = 0
                };

                return View("Save", newVm);
            }
            SaveReturnRateViewModel vm = new()
            {
                Id = dto.Id,
                MaximumRate = dto.MaximumRate,
                MinimumRate = dto.MinimumRate
            };
            return View("Save", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Configure(SaveReturnRateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EditMode = true;
                return View("Save", vm);
            }

            ReturnRateDto dto = new()
            {
                Id = vm.Id,
                MaximumRate = vm.MaximumRate,
                MinimumRate = vm.MinimumRate
            };
            await _returnRateService.UpdateAsync(dto);
            return RedirectToRoute(new { controller = "ReturnRate", action = "Index" });
        }
    }
}
