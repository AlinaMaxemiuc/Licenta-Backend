using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Renting.Reviews;

using System;
using System.Threading.Tasks;

namespace Renting.Web.Pages.Reviews
{
    public class EditModalModel : RentingPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateOrUpdateReviewDto Review { get; set; }

        private readonly IReviewAppService _reviewAppService;

        public EditModalModel(IReviewAppService reviewAppService)
        {
            _reviewAppService = reviewAppService;
        }

        public async Task OnGetAsync()
        {
            var reviewDto = await _reviewAppService.GetAsync(Id);
            Review = ObjectMapper.Map<ReviewDetailsDto, CreateOrUpdateReviewDto>(reviewDto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _reviewAppService.UpdateAsync(Id, Review);
            return NoContent();
        }
    }
}
