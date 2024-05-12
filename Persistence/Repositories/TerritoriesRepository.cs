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
    public class TerritoriesRepository : ITerritoriesRepository
    {
        private readonly DatabaseContext _context;
        public TerritoriesRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Territories>> GetRecords(string? keyword, int? page, int? pageSize)
        {
            try
            {
                if (_context.Territories == null)
                    return Enumerable.Empty<Territories>();

                int pageResult = pageSize ?? 10; 
                int pageNumber = page ?? 1;

                var result = _context.Territories.AsQueryable();

                if (keyword != null)
                {
                    result = result.Where(x =>
                                    x.TerritoryDescription.Contains(keyword));
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

        public async Task<Territories> GetRecord(string? id)
        {
            try
            {
                var result = await _context.Territories.Where(e => e.TerritoryID == id).FirstOrDefaultAsync();
                if (result == null)
                    return new Territories();
                
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string?> Insert(Territories entity)
        {
            try
            {
                var newEntity = await _context.Territories.AddAsync(entity);
                int result = await _context.SaveChangesAsync();
                return newEntity.Entity.TerritoryID;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<int> Update(Territories entity)
        {
            try
            {
                var Territories = await _context.Territories.FindAsync(entity.TerritoryID);

                if (Territories == null)
                    throw new Exception("No data can Update");

                Territories.TerritoryDescription = entity.TerritoryDescription;

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
                var Territories = await _context.Territories.FindAsync(id);

                if (Territories == null)
                    throw new Exception("No data can delete");

                _context.Territories.Remove(Territories);
                int result = await _context.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Territories> GetRecord(int? id)
        {
            throw new NotImplementedException();
        }

        Task<int?> IBaseRepository<Territories>.Insert(Territories entity)
        {
            throw new NotImplementedException();
        }
    }
}
