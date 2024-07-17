using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using Renting.Drones;

using System;
using System.Threading.Tasks;

namespace Renting.Web.Pages.Drones
{
    public class CreateModalModel : RentingPageModel
    {
        [BindProperty]
        public CreateOrUpdateDroneDto Drone { get; set; }

        private readonly IDroneAppService _droneAppService;

        public CreateModalModel(IDroneAppService droneAppService)
        {
            _droneAppService = droneAppService;
        }

        public void OnGet()
        {
            Drone = new CreateOrUpdateDroneDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _droneAppService.CreateAsync(Drone);
            }
            catch (Exception ex)
            {
                // Log the exception and return a user-friendly message
                Logger.LogError(ex, "An error occurred while creating the drone.");
                ModelState.AddModelError(string.Empty, "An error occurred while creating the drone.");
                return Page();
            }

            return NoContent();
        }

    }
}
