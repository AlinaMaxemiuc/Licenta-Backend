using Renting.Customers;
using Renting.Drones;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Renting.Reviews
{
    [IntegrationService]
    public class ReviewAppService : RentingAppService, IReviewAppService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ReviewManager _reviewManager;
        public ReviewAppService(IReviewRepository reviewRepository, ReviewManager reviewManager)
        {
            _reviewRepository = reviewRepository;
            _reviewManager = reviewManager;
        }
        public async Task<ReviewDetailsDto> GetAsync(Guid id)
        {
            var review = await _reviewRepository.GetAsync(id);

            var dto = ObjectMapper.Map<Review, ReviewDetailsDto>(review);
            return dto;
        }
        public async Task<PagedResultDto<ReviewDto>> GetListAsync(GetReviewsInput input)
        {
            var reviewDtos = new List<ReviewDto>();
            var reviews = await _reviewRepository.GetPagedListAsync(input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                true);

            foreach (var review in reviews)
            {
                var dto = ObjectMapper.Map<Review, ReviewDto>(review);
                reviewDtos.Add(dto);
            }

            return new PagedResultDto<ReviewDto>(reviews.Count, reviewDtos);
        }
        public async Task<ReviewDetailsDto> CreateAsync(CreateOrUpdateReviewDto input)
        {
            var review = await _reviewManager.CreateAsync(input.Rating,
                input.Content,
                input.ReviewDate,
                input.DroneId,
                input.CustomerId);

            review = await _reviewRepository.InsertAsync(review);
            return ObjectMapper.Map<Review, ReviewDetailsDto>(review);
        }

        public async Task<ReviewDetailsDto> UpdateAsync(Guid id, CreateOrUpdateReviewDto input)
        {
            var review = await _reviewRepository.GetAsync(id);

            review.Rating = input.Rating;
            review.Content = input.Content;
            review.ReviewDate = input.ReviewDate;
            review.DroneId = input.DroneId;
            review.CustomerId = input.CustomerId;

            review = await _reviewRepository.UpdateAsync(review);

            var dto = ObjectMapper.Map<Review, ReviewDetailsDto>(review);
            return dto;
        }
        public async Task DeleteAsync(Guid id)
        {
            await _reviewRepository.DeleteAsync(id);
        }
    }
}
