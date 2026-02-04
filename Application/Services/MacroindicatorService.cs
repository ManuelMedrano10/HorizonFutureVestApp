using System.Diagnostics.Contracts;
using Application.Dtos.Macroindicator;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Entities;
using Persistence.Repositories;

namespace Application.Services
{
    public class MacroindicatorService
    {
        private readonly MacroindicatorRepository _macroindicatorRepository;
        public MacroindicatorService(HorizonDbContext horizonDbContext)
        {
            _macroindicatorRepository = new MacroindicatorRepository(horizonDbContext);
        }

        public async Task<bool> AddAsync(MacroindicatorDto dto)
        {
            try
            {
                var macroindicatorsWeightSum = await _macroindicatorRepository.GetAllQuery().SumAsync(m => m.Weight);

                if ((macroindicatorsWeightSum + dto.Weight) > 1)
                    return false;

                Macroindicator entity = new()
                {
                    Id = 0,
                    Name = dto.Name,
                    Weight = dto.Weight,
                    BetterHigh = dto.BetterHigh
                };

                Macroindicator? returnEntity = await _macroindicatorRepository.AddAsync(entity);
                if (returnEntity == null)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(MacroindicatorDto dto)
        {
            try
            {
                var macroindicatorsWeightSum = await _macroindicatorRepository.GetAllQuery().SumAsync(m => m.Weight);

                if ((macroindicatorsWeightSum + dto.Weight) > 1)
                    return false;

                Macroindicator entity = new()
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Weight = dto.Weight,
                    BetterHigh = dto.BetterHigh
                };

                Macroindicator? returnEntity = await _macroindicatorRepository.UpdateAsync(dto.Id,entity);
                if (returnEntity == null)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _macroindicatorRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<MacroindicatorDto?> GetById(int id)
        {
            try
            {
                var entity = await _macroindicatorRepository.GetById(id);

                if (entity == null)
                    return null;

                MacroindicatorDto dto = new()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Weight = entity.Weight,
                    BetterHigh = entity.BetterHigh
                };

                return dto;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<MacroindicatorDto>> GetAll()
        {
            try
            {
                var listEntities = await _macroindicatorRepository.GetAllList();

                var listEntityDtos = listEntities.Select(m =>
                    new MacroindicatorDto
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Weight = m.Weight,
                        BetterHigh = m.BetterHigh
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
