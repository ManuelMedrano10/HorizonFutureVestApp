using Application.Dtos.ReturnRate;
using Application.Services;
using Application.ViewModels.ReturnRate;
using Microsoft.AspNetCore.Mvc;
using Persistence.Contexts;

namespace HorizonFutureVestApp.Controllers
{
    public class ReturnRateController : Controller
    {
        private readonly ReturnRateService _returnRateService;
        
        public ReturnRateController(HorizonDbContext context)
        {
            _returnRateService = new ReturnRateService(context);
        }

        public async Task<IActionResult> Index(string? message = null, string? messageType = null)
        {
            var dto = await _returnRateService.GetAsync();

            SaveReturnRateViewModel vm;

            if (dto != null)
            {
                ViewBag.EditMode = true;
                vm = new()
                {
                    Id = dto.Id,
                    MaximumRate = dto.MaximumRate,
                    MinimumRate = dto.MinimumRate
                };

                ViewBag.Message = message;
                ViewBag.MessageType = messageType;
                return View(vm);
            }

            ViewBag.Message = message;
            ViewBag.MessageType = messageType;

            return View(new SaveReturnRateViewModel() { Id = 0, MaximumRate = 0, MinimumRate = 0 });
        }

        [HttpPost]
        public async Task<IActionResult> Index(SaveReturnRateViewModel vm)
        {
            try
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
                return RedirectToRoute(new { controller = "ReturnRate", action = "Index", message = "Product created successfully", messageType = "alert-success" });
            } 
            catch(Exception)
            {
                return RedirectToRoute(new { controller = "ReturnRate", action = "Index" });
            }
        }
    }
}
