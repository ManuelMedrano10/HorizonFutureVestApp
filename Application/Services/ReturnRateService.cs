using Application.Dtos.ReturnRate;
using Persistence.Contexts;
using Persistence.Entities;
using Persistence.Repositories;

namespace Application.Services
{
    public class ReturnRateService
    {
        private readonly ReturnRateRepository _returnRateRepository;
        public ReturnRateService(HorizonDbContext horizonDbContext)
        {
            _returnRateRepository = new ReturnRateRepository(horizonDbContext);
        }

        public async Task<bool> UpdateAsync(ReturnRateDto dto)
        {
            try
            {
                if (dto.MinimumRate > dto.MaximumRate)
                    throw new Exception("The minimum rate must be less than the maximum rate.");

                ReturnRate entity = new()
                {
                    Id = dto.Id,
                    MaximumRate = dto.MaximumRate,
                    MinimumRate = dto.MinimumRate
                };

                if(entity.Id == 0)
                {
                    ReturnRate? newEntity = await _returnRateRepository.AddAsync(entity);
                    return true;
                }

                ReturnRate? returnEntity = await _returnRateRepository.UpdateAsync(entity.Id, entity);
                if(returnEntity == null)
                    return false;
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ReturnRateDto?> GetAsync()
        {
            try
            {
                var listEntity = await _returnRateRepository.GetAllList();
                var entity = listEntity.FirstOrDefault();

                if (entity == null)
                    return null;

                ReturnRateDto dto = new()
                {
                    Id = entity.Id,
                    MaximumRate = entity.MaximumRate,
                    MinimumRate = entity.MinimumRate
                };

                return dto;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
