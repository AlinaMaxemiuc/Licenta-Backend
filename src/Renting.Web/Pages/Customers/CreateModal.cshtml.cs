using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using Renting.Customers;

using System;
using System.Threading.Tasks;

namespace Renting.Web.Pages.Customers
{
    public class CreateModalModel : RentingPageModel
    {
        [BindProperty]
        public CreateOrUpdateCustomerDto Customer { get; set; }

        private readonly ICustomerAppService _customerAppService;

        public CreateModalModel(ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }

        public void OnGet()
        {
            Customer = new CreateOrUpdateCustomerDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _customerAppService.CreateAsync(Customer);
            }
            catch (Exception ex)
            {
                // Log the exception and return a user-friendly message
                Logger.LogError(ex, "An error occurred while creating the customer.");
                ModelState.AddModelError(string.Empty, "An error occurred while creating the customer.");
                return Page();
            }

            return NoContent();
        }

    }
}
