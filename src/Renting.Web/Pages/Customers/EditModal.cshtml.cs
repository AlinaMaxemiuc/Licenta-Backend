using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Renting.Customers;

using System;
using System.Threading.Tasks;

namespace Renting.Web.Pages.Customers
{
    public class EditModalModel : RentingPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateOrUpdateCustomerDto Customer { get; set; }

        private readonly ICustomerAppService _customerAppService;

        public EditModalModel(ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }

        public async Task OnGetAsync()
        {
            var customerDto = await _customerAppService.GetAsync(Id);
            Customer = ObjectMapper.Map<CustomerDetailsDto, CreateOrUpdateCustomerDto>(customerDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _customerAppService.UpdateAsync(Id, Customer);
            return NoContent();
        }
    }
}
