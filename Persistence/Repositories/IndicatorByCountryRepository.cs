using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Entities;

namespace Persistence.Repositories
{
    public class IndicatorByCountryRepository
    {
        private readonly HorizonDbContext _context;
        public IndicatorByCountryRepository(HorizonDbContext context)
        {
            _context = context;
        }

        public async Task<IndicatorByCountry?> AddAsync(IndicatorByCountry indicatorBC)
        {
            await _context.Set<IndicatorByCountry>().AddAsync(indicatorBC);
            await _context.SaveChangesAsync();

            return indicatorBC;
        }

        public async Task<IndicatorByCountry?> UpdateAsync(int id, IndicatorByCountry indicatorBC)
        {
            var entry = await _context.Set<IndicatorByCountry>().FindAsync(id);

            if(entry != null)
            {
                _context.Entry(entry).CurrentValues.SetValues(indicatorBC);
                await _context.SaveChangesAsync();
                return entry;
            }

            return null;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<IndicatorByCountry>().FindAsync(id);
            if(entity != null)
            {
                _context.Set<IndicatorByCountry>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<IndicatorByCountry>> GetAllList()
        {
            return await _context.Set<IndicatorByCountry>().ToListAsync();
        }

        public async Task<IndicatorByCountry?> GetById(int id)
        {
            return await _context.Set<IndicatorByCountry>().FindAsync(id);
        }

        public IQueryable<IndicatorByCountry> GetAllQuery()
        {
            return _context.Set<IndicatorByCountry>().AsQueryable();
        }
    }
}
