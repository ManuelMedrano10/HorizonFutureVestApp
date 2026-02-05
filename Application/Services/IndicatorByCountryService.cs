using Application.Dtos.Country;
using Application.Dtos.IndicatorByCountry;
using Application.Dtos.Macroindicator;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Entities;
using Persistence.Repositories;

namespace Application.Services
{
    public class IndicatorByCountryService
    {
        private readonly IndicatorByCountryRepository _indicatorByCountryRepository;
        public IndicatorByCountryService(HorizonDbContext horizonDbContext)
        {
            _indicatorByCountryRepository = new IndicatorByCountryRepository(horizonDbContext);
        }

        public async Task<bool> AddAsync(IndicatorByCountryDto dto)
        {
            try
            {
                var listIndicatorBC = _indicatorByCountryRepository.GetAllQuery()
                    .Where(i => i.MacroindicatorId == dto.MacroindicatorId &&
                            i.CountryId == dto.CountryId).ToList();

                foreach(var i in listIndicatorBC)
                {
                    if (dto.Year == i.Year)
                        return false;
                }

                IndicatorByCountry entity = new()
                {
                    Id = 0,
                    IndicatorValue = dto.IndicatorValue,
                    Year = dto.Year,
                    CountryId = dto.CountryId,
                    MacroindicatorId = dto.MacroindicatorId
                };

                IndicatorByCountry? returnEntity = await _indicatorByCountryRepository.AddAsync(entity);
                if (returnEntity == null)
                    return false;

                return true;                
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(IndicatorByCountryDto dto)
        {
            try
            {
                IndicatorByCountry entity = new()
                {
                    Id = dto.Id,
                    IndicatorValue = dto.IndicatorValue,
                    Year = dto.Year,
                    CountryId = dto.CountryId,
                    MacroindicatorId = dto.MacroindicatorId
                };

                IndicatorByCountry? returnEntity = await _indicatorByCountryRepository.UpdateAsync(entity.Id, entity);
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
                await _indicatorByCountryRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IndicatorByCountryDto?> GetById(int id)
        {
            try
            {
                var entity = await _indicatorByCountryRepository.GetById(id);

                if (entity == null)
                    return null;

                IndicatorByCountryDto dto = new()
                {
                    Id = entity.Id,
                    IndicatorValue = entity.IndicatorValue,
                    Year = entity.Year,
                    CountryId = entity.CountryId,
                    Country = entity.Country == null ? null : new CountryDto()
                    {
                        Id = entity.Country.Id,
                        Name = entity.Country.Name,
                        IsoCode = entity.Country.IsoCode
                    },
                    MacroindicatorId = entity.MacroindicatorId,
                    Macroindicator = entity.Macroindicator == null ? null : new MacroindicatorDto()
                    {
                        Id = entity.Macroindicator.Id,
                        Name = entity.Macroindicator.Name,
                        Weight = entity.Macroindicator.Weight,
                        BetterHigh = entity.Macroindicator.BetterHigh
                    }
                };

                return dto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<IndicatorByCountryDto>> GetAll()
        {
            try
            {
                var listEntities = await _indicatorByCountryRepository.GetAllList();

                var listEntityDtos = listEntities.Select(i =>
                    new IndicatorByCountryDto()
                    {
                        Id = i.Id,
                        IndicatorValue = i.IndicatorValue,
                        Year = i.Year,
                        CountryId = i.CountryId,
                        MacroindicatorId = i.MacroindicatorId
                    }).ToList();

                return listEntityDtos;
            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<List<IndicatorByCountryDto>> GetAllInclude()
        {
            try
            {
                var listEntitiesQuery = _indicatorByCountryRepository.GetAllQuery();

                var listEntities = await listEntitiesQuery.Include(ibc => ibc.Country).ToListAsync();

                var listEntityDtos = listEntities.Select(i =>
                new IndicatorByCountryDto()
                {
                    Id = i.Id,
                    IndicatorValue = i.IndicatorValue,
                    Year = i.Year,
                    CountryId = i.CountryId,
                    Country = i.Country == null ? null : new CountryDto()
                    {
                        Id = i.Country.Id,
                        Name = i.Country.Name,
                        IsoCode = i.Country.IsoCode
                    },
                    MacroindicatorId = i.MacroindicatorId,
                    Macroindicator = i.Macroindicator == null ? null : new MacroindicatorDto()
                    {
                        Id = i.Macroindicator.Id,
                        Name = i.Macroindicator.Name,
                        Weight = i.Macroindicator.Weight,
                        BetterHigh = i.Macroindicator.BetterHigh
                    }
                }).ToList();

                return listEntityDtos;
            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<List<IndicatorByCountryDto>> GetByYear(int year)
        {
            try
            {
                var listEntitiesQuery = _indicatorByCountryRepository.GetAllQuery();

                var listEntities = await listEntitiesQuery.Where(ibc => ibc.Year == year).ToListAsync();

                var listEntityDtos = listEntities.Select(i =>
                new IndicatorByCountryDto()
                {
                    Id = i.Id,
                    IndicatorValue = i.IndicatorValue,
                    Year = i.Year,
                    CountryId = i.CountryId,
                    Country = i.Country == null ? null : new CountryDto()
                    {
                        Id = i.Country.Id,
                        Name = i.Country.Name,
                        IsoCode = i.Country.IsoCode
                    },
                    MacroindicatorId = i.MacroindicatorId,
                    Macroindicator = i.Macroindicator == null ? null : new MacroindicatorDto()
                    {
                        Id = i.Macroindicator.Id,
                        Name = i.Macroindicator.Name,
                        Weight = i.Macroindicator.Weight,
                        BetterHigh = i.Macroindicator.BetterHigh
                    }
                }).ToList();

                return listEntityDtos;
            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<List<IndicatorByCountryDto>> GetByCountry(int id)
        {
            try
            {
                var listEntitiesQuery = _indicatorByCountryRepository.GetAllQuery();

                var listEntities = await listEntitiesQuery.Where(ibc => ibc.CountryId == id).ToListAsync();

                var listEntityDtos = listEntities.Select(i =>
                new IndicatorByCountryDto()
                {
                    Id = i.Id,
                    IndicatorValue = i.IndicatorValue,
                    Year = i.Year,
                    CountryId = i.CountryId,
                    Country = i.Country == null ? null : new CountryDto()
                    {
                        Id = i.Country.Id,
                        Name = i.Country.Name,
                        IsoCode = i.Country.IsoCode
                    },
                    MacroindicatorId = i.MacroindicatorId,
                    Macroindicator = i.Macroindicator == null ? null : new MacroindicatorDto()
                    {
                        Id = i.Macroindicator.Id,
                        Name = i.Macroindicator.Name,
                        Weight = i.Macroindicator.Weight,
                        BetterHigh = i.Macroindicator.BetterHigh
                    }
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
