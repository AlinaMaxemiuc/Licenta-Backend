using AutoMapper.Internal.Mappers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Renting.Drones
{
    [IntegrationService]
    public class DroneAppService : RentingAppService, IDroneAppService
    {
        private readonly IDroneRepository _droneRepository;
        private readonly DroneManager _droneManager;

        public DroneAppService(IDroneRepository droneRepository, DroneManager droneManager)
        {
            _droneRepository = droneRepository;
            _droneManager = droneManager;
        }

        public async Task<DroneDetailsDto> GetAsync(Guid id)
        {
            var drone = await _droneRepository.GetAsync(id);

            var dto = ObjectMapper.Map<Drone, DroneDetailsDto>(drone);
            return dto;
        }
        public async Task<PagedResultDto<DroneDto>> GetListAsync(GetDronesInput input)
        {
            var droneDtos = new List<DroneDto>();
            var drones = await _droneRepository.GetPagedListAsync(input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                true);

            foreach (var drone in drones)
            {
                var dto = ObjectMapper.Map<Drone, DroneDto>(drone);
                droneDtos.Add(dto);
            }

            return new PagedResultDto<DroneDto>(drones.Count, droneDtos);
        }
        public async Task<DroneDetailsDto> CreateAsync(CreateOrUpdateDroneDto input)
        {
            var drone = await _droneManager.CreateAsync(input.Name,
                input.Model,
                input.Utility,
                input.Category,
                input.ProductionYear,
                input.PricePerDay
                );

            drone = await _droneRepository.InsertAsync(drone);
            return ObjectMapper.Map<Drone, DroneDetailsDto>(drone);
        }

        public async Task<DroneDetailsDto> UpdateAsync(Guid id, CreateOrUpdateDroneDto input)
        {
            var drone = await _droneRepository.GetAsync(id);

            drone.Name = input.Name;
            drone.Model = input.Model;
            drone.Utility = input.Utility;
            drone.Category = input.Category;
            drone.ProductionYear = input.ProductionYear;
            drone.PricePerDay = input.PricePerDay;

            drone = await _droneRepository.UpdateAsync(drone);
            var dto = ObjectMapper.Map<Drone, DroneDetailsDto>(drone);
            return dto;
        }
        public async Task DeleteAsync(Guid id)
        {
            await _droneRepository.DeleteAsync(id);
        }
        public async Task<List<DroneDetailsDto>> GetByParameterAsync(string parameter)
        {
            parameter = parameter.ToLower();
            var drones = await _droneRepository.GetListAsync(x => x.Name.ToLower().Contains(parameter)
            || x.Model.ToLower().Contains(parameter));
            if (drones == null || !drones.Any())
            {
                throw new EntityNotFoundException(typeof(Drone), parameter);
            }

            var dtos = ObjectMapper.Map<List<Drone>, List<DroneDetailsDto>>(drones);
            return dtos;
        }

        public async Task<PagedResultDto<DroneDto>> FilterAsync(string name = null, 
            string model = null,
            DroneUtility? utility = null, 
            DroneCategory? category = null,
            DateTime? productionYear = null, 
            decimal? minPricePerDay = null, 
            decimal? maxPricePerDay = null, 
            int skipCount = 0, 
            int maxResultCount = 10)
        {
            var drones = await _droneRepository.GetPagedListAsync(
                skipCount: skipCount,
                maxResultCount: maxResultCount,
                sorting: null,
                includeDetails: true
            );

            // Filtrare dupa criteriile specificate
            var filteredDrones = drones.Where(d =>
                (name == null || d.Name.Contains(name)) &&
                (model == null || d.Model.Contains(model)) &&
                (utility == null || d.Utility == utility) &&
                (category == null || d.Category == category) &&
                (!productionYear.HasValue || d.ProductionYear == productionYear) &&
                (!minPricePerDay.HasValue || d.PricePerDay >= minPricePerDay) &&
                (!maxPricePerDay.HasValue || d.PricePerDay <= maxPricePerDay)
            ).ToList();

            var droneDtos = ObjectMapper.Map<List<Drone>, List<DroneDto>>(filteredDrones);
            return new PagedResultDto<DroneDto>(filteredDrones.Count, droneDtos);
        }
    }
}
