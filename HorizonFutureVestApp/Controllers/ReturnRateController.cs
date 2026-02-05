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

        public /*async Task<*/IActionResult/*>*/ Index(/*int id*/)
        {
            /*ViewBag.EditMode = true;
            var dto = await _returnRateService.GetById(id);
            if(dto == null)
            {
                return View(newVm);
            }
            SaveReturnRateViewModel vm = new()
            {
                Id = dto.Id,
                MaximumRate = dto.MaximumRate,
                MinimumRate = dto.MinimumRate
            };*/
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SaveReturnRateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EditMode = true;
                return View(vm);
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
