using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Entities;

namespace Persistence.Repositories
{
    public class MacroindicatorRepository
    {
        private readonly HorizonDbContext _context;
        public MacroindicatorRepository(HorizonDbContext context)
        {
            _context = context;
        }

        public async Task<Macroindicator?> AddAsync(Macroindicator macroindicator)
        {
            await _context.Set<Macroindicator>().AddAsync(macroindicator);
            await _context.SaveChangesAsync();

            return macroindicator;
        }

        public async Task<Macroindicator?> UpdateAsync(int id, Macroindicator macroindicator)
        {
            var entry = await _context.Set<Macroindicator>().FindAsync(id);

            if(entry != null)
            {
                _context.Entry(entry).CurrentValues.SetValues(macroindicator);
                await _context.SaveChangesAsync();
                return entry;
            }

            return null;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<Macroindicator>().FindAsync(id);
            if(entity != null)
            {
                _context.Set<Macroindicator>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Macroindicator>> GetAllList()
        {
            return await _context.Set<Macroindicator>().ToListAsync();
        }

        public async Task<Macroindicator?> GetById(int id)
        {
            return await _context.Set<Macroindicator>().FindAsync(id);
        }

        public IQueryable<Macroindicator> GetAllQuery()
        {
            return _context.Set<Macroindicator>().AsQueryable();
        }
    }
}
