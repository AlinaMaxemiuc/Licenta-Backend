using Renting.Customers;
using Renting.Drones;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Renting.Reviews
{
    public class ReviewManager : DomainService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IRepository<Drone, Guid> _droneRepository;
        private readonly IRepository<Customer, Guid> _customerRepository;

        public ReviewManager(IReviewRepository reviewRepository, IRepository<Drone, Guid> droneRepository, IRepository<Customer, Guid> customerRepository)
        {
            _reviewRepository = reviewRepository;
            _droneRepository = droneRepository;
            _customerRepository = customerRepository;
        }

        public async Task<Review> CreateAsync(
            int? rating,
            string? content,
            DateTime? reviewDate,
            Guid droneId,
            Guid customerId)
        {
            await _droneRepository.GetAsync(droneId);
            await _customerRepository.GetAsync(customerId);

            var review = new Review(
                GuidGenerator.Create(),
                rating,
                content,
                reviewDate,
                droneId,
                customerId
            );

            await _reviewRepository.InsertAsync(review);
            return review;
        }

        public async Task<Review> UpdateAsync(
            Guid id,
            int? rating,
            string? content,
            DateTime? reviewDate,
            Guid droneId,
            Guid customerId)
        {
            var review = await _reviewRepository.GetAsync(id);
            await _droneRepository.GetAsync(droneId);
            await _customerRepository.GetAsync(customerId);

            review.Rating = rating;
            review.Content = content;
            review.ReviewDate = reviewDate;
            review.DroneId = droneId;
            review.CustomerId = customerId;

            await _reviewRepository.UpdateAsync(review);
            return review;
        }

    }
}
