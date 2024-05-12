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
    public class ShippersRepository : IShippersRepository
    {
        private readonly DatabaseContext _context;
        public ShippersRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Shippers>> GetRecords(string? keyword, int? page, int? pageSize)
        {
            try
            {
                if (_context.Shippers == null)
                    return Enumerable.Empty<Shippers>();

                int pageResult = pageSize ?? 10; 
                int pageNumber = page ?? 1;

                var result = _context.Shippers.AsQueryable();

                if (keyword != null)
                {
                    result = result.Where(x =>
                                    x.CompanyName.Contains(keyword)
                                    || x.Phone.Contains(keyword));
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

        public async Task<Shippers> GetRecord(int? id)
        {
            try
            {
                var result = await _context.Shippers.Where(e => e.ShipperID == id).FirstOrDefaultAsync();
                if (result == null)
                    return new Shippers();
                
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int?> Insert(Shippers entity)
        {
            try
            {
                var newEntity = await _context.Shippers.AddAsync(entity);
                int result = await _context.SaveChangesAsync();
                return newEntity.Entity.ShipperID;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<int> Update(Shippers entity)
        {
            try
            {
                var Shippers = await _context.Shippers.FindAsync(entity.ShipperID);

                if (Shippers == null)
                    throw new Exception("No data can Update");

                Shippers.CompanyName = entity.CompanyName;
                Shippers.Phone = entity.Phone;

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
                var Shippers = await _context.Shippers.FindAsync(id);

                if (Shippers == null)
                    throw new Exception("No data can delete");

                _context.Shippers.Remove(Shippers);
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
