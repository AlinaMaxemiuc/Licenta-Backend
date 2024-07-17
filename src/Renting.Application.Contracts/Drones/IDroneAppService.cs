using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Renting.Drones
{
    [IntegrationService]
    public interface IDroneAppService : ICrudAppService<
      DroneDetailsDto,
      DroneDto,
      Guid,
      GetDronesInput,
      CreateOrUpdateDroneDto,
      CreateOrUpdateDroneDto>
    {
    }
}
