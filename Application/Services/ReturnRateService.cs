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
                    return false;

                ReturnRate entity = new()
                {
                    Id = dto.Id,
                    MaximumRate = dto.MaximumRate,
                    MinimumRate = dto.MinimumRate
                };

                if(entity.Id == 0)
                {
                    ReturnRate? newEntity = await _returnRateRepository.AddAsync(entity);
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

        public async Task<ReturnRateDto?> GetById(int id)
        {
            try
            {
                var entity = await _returnRateRepository.GetById(id);

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

        public async Task<List<ReturnRateDto>> GetAll()
        {
            try
            {
                var listEntity = await _returnRateRepository.GetAllList();

                var listEntityDtos = listEntity.Select(rr =>
                    new ReturnRateDto()
                    {
                        Id = rr.Id,
                        MinimumRate = rr.MinimumRate,
                        MaximumRate = rr.MaximumRate
                    }).ToList();

                return listEntityDtos;
            }
            catch (Exception)
            {
                return [];
            }
        }
    }
}
