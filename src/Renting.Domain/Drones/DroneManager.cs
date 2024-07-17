using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace Renting.Drones
{
    public class DroneManager : DomainService
    {
        private readonly IDroneRepository _droneRepository;
        public DroneManager(IDroneRepository droneRepository)
        {
            _droneRepository = droneRepository;
        }
        public async Task<Drone> CreateAsync(string name,
            string? model,
            DroneUtility? utility,
            DroneCategory? category,
            DateTime productionYear,
            decimal pricePerDay)
        {
            var drone = new Drone(GuidGenerator.Create(),
                name,
                model,
                utility,
                category,
                productionYear,
                pricePerDay);
            return drone;
        }

        public async Task<Drone> UpdateAsync(Drone drone,
            string name,
            string? model,
            DroneUtility? utility,
            DroneCategory? category,
            DateTime productionYear,
            decimal pricePerDay
            )
        {
            drone.Name = name;
            drone.Model = model;
            drone.Utility = utility;
            drone.Category = category;
            drone.ProductionYear = productionYear;
            drone.PricePerDay = pricePerDay;
            return drone;
        }
    }
}
