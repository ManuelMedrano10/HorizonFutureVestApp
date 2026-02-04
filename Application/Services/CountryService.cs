using Application.Dtos.Country;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Persistence.Contexts;
using Persistence.Entities;
using Persistence.Repositories;

namespace Application.Services
{
    public class CountryService
    {
        private readonly CountryRepository _countryRepository;
        public CountryService(HorizonDbContext horizonDbContext)
        {
            _countryRepository = new CountryRepository(horizonDbContext);
        }

        public async Task<bool> AddAsync(CountryDto dto)
        {
            try
            {
                Country entity = new()
                { 
                    Id = 0,
                    Name = dto.Name,
                    IsoCode = dto.IsoCode
                };

                Country? returnEntity = await _countryRepository.AddAsync(entity);
                if (returnEntity == null)
                    return false;

                return true;
            } 
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(CountryDto dto)
        {
            try
            {
                Country entity = new()
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    IsoCode = dto.IsoCode
                };

                Country? returnEntity = await _countryRepository.UpdateAsync(entity.Id, entity);
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
                await _countryRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<CountryDto?> GetById(int id)
        {
            try
            {
                var entity = await _countryRepository.GetById(id);
                
                if (entity == null)
                    return null;

                CountryDto dto = new()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    IsoCode = entity.IsoCode
                };

                return dto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<CountryDto>> GetAll()
        {
            try
            {
                var listEntities = await _countryRepository.GetAllList();

                var listEntityDtos = listEntities.Select(c =>
                    new CountryDto()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        IsoCode = c.IsoCode
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