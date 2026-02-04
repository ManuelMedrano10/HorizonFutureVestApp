using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Entities;

namespace Persistence.Repositories
{
    public class CountryRepository 
    {
        private readonly HorizonDbContext _context;
        public CountryRepository(HorizonDbContext context)
        {
            _context = context;
        }

        public async Task<Country?> AddAsync(Country country)
        {
            await _context.Set<Country>().AddAsync(country);
            await _context.SaveChangesAsync();

            return country;
        }

        public async Task<Country?> UpdateAsync(int id, Country country)
        {
            var entry = await _context.Set<Country>().FindAsync(id);

            if(entry != null)
            {
                _context.Entry(entry).CurrentValues.SetValues(country);
                await _context.SaveChangesAsync();
                return entry;
            }

            return null;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<Country>().FindAsync(id);
            if(entity != null)
            {
                _context.Set<Country>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Country>> GetAllList()
        {
            return await _context.Set<Country>().ToListAsync();
        }

        public async Task<Country?> GetById(int id)
        {
            return await _context.Set<Country>().FindAsync(id);
        }

        public IQueryable<Country> GetAllQuery()
        {
            return _context.Set<Country>().AsQueryable();
        }
    }
}
