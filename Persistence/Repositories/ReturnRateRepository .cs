using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Entities;

namespace Persistence.Repositories
{
    public class ReturnRateRepository
    {
        private readonly HorizonDbContext _context;
        public ReturnRateRepository(HorizonDbContext context)
        {
            _context = context;
        }

        public async Task<ReturnRate?> AddAsync(ReturnRate returnRate)
        {
            await _context.Set<ReturnRate>().AddAsync(returnRate);
            await _context.SaveChangesAsync();

            return returnRate;
        }

        public async Task<ReturnRate?> UpdateAsync(int id, ReturnRate returnRate)
        {
            var entry = await _context.Set<ReturnRate>().FindAsync(id);

            if(entry != null)
            {
                _context.Entry(entry).CurrentValues.SetValues(returnRate);
                await _context.SaveChangesAsync();
                return entry;
            }

            return null;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<ReturnRate>().FindAsync(id);
            if(entity != null)
            {
                _context.Set<ReturnRate>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ReturnRate>> GetAllList()
        {
            return await _context.Set<ReturnRate>().ToListAsync();
        }

        public async Task<ReturnRate?> GetById(int id)
        {
            return await _context.Set<ReturnRate>().FindAsync(id);
        }

        public IQueryable<ReturnRate> GetAllQuery()
        {
            return _context.Set<ReturnRate>().AsQueryable();
        }
    }
}
