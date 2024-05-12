using Domain.Interface.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly DatabaseContext _context;
        public RegionRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Region>> GetRecords(string? keyword, int? page, int? pageSize)
        {
            try
            {
                if (_context.Region == null)
                    return Enumerable.Empty<Region>();

                int pageResult = pageSize ?? 10; 
                int pageNumber = page ?? 1;

                var result = _context.Region.AsQueryable();

                if (keyword != null)
                {
                    result = result.Where(x =>
                                    x.RegionDescription.Contains(keyword));
                }

                int pageCount = (int)Math.Ceiling(await result.CountAsync() / (double)pageResult);

                result = result
                    .Skip((pageNumber - 1) * pageResult)
                    .Take(pageResult);

                return await result.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Region> GetRecord(int? id)
        {
            try
            {
                var result = await _context.Region
                    .Include(r => r.Territories)
                    .Where(e => e.RegionID == id).FirstOrDefaultAsync();
                if (result == null)
                    return new Region();
                
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int?> Insert(Region entity)
        {
            try
            {
                var newEntity = await _context.Region.AddAsync(entity);
                int result = await _context.SaveChangesAsync();
                return newEntity.Entity.RegionID;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<int> Update(Region entity)
        {
            try
            {
                var Region = await _context.Region.FindAsync(entity.RegionID);

                if (Region == null)
                    throw new Exception("No data can Update");

                Region.RegionDescription = entity.RegionDescription;

                int result = await _context.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> Delete(int? id)
        {
            try
            {
                var Region = await _context.Region.FindAsync(id);

                if (Region == null)
                    throw new Exception("No data can delete");

                _context.Region.Remove(Region);
                int result = await _context.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
