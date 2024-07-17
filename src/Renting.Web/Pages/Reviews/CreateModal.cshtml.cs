using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using Renting.Reviews;

using System;
using System.Threading.Tasks;

namespace Renting.Web.Pages.Reviews
{
    public class CreateModalModel : RentingPageModel
    {
        [BindProperty]
        public CreateOrUpdateReviewDto Review { get; set; }

        private readonly IReviewAppService _reviewAppService;

        public CreateModalModel(IReviewAppService reviewAppService)
        {
            _reviewAppService = reviewAppService;
        }

        public void OnGet()
        {
            Review = new CreateOrUpdateReviewDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _reviewAppService.CreateAsync(Review);
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
