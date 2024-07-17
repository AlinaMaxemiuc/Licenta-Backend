using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Renting.Drones;

using System;
using System.Threading.Tasks;

namespace Renting.Web.Pages.Drones
{
    public class EditModalModel : RentingPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateOrUpdateDroneDto Drone { get; set; }

        private readonly IDroneAppService _droneAppService;

        public EditModalModel(IDroneAppService droneAppService)
        {
            _droneAppService = droneAppService;
        }

        public async Task OnGetAsync()
        {
            var droneDto = await _droneAppService.GetAsync(Id);
            Drone = ObjectMapper.Map<DroneDetailsDto, CreateOrUpdateDroneDto>(droneDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _droneAppService.UpdateAsync(Id, Drone);
            return NoContent();
        }
    }
}
