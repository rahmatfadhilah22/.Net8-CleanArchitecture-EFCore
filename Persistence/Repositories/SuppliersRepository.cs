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
    public class SuppliersRepository : ISuppliersRepository
    {
        private readonly DatabaseContext _context;
        public SuppliersRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Suppliers>> GetRecords(string? keyword, int? page, int? pageSize)
        {
            try
            {
                if (_context.Suppliers == null)
                    return Enumerable.Empty<Suppliers>();

                int pageResult = pageSize ?? 10; 
                int pageNumber = page ?? 1;

                var result = _context.Suppliers.AsQueryable();

                if (keyword != null)
                {
                    result = result.Where(x =>
                                    x.CompanyName.Contains(keyword)
                                    || x.ContactName.Contains(keyword)
                                    || x.City.Contains(keyword)
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

        public async Task<Suppliers> GetRecord(int? id)
        {
            try
            {
                var result = await _context.Suppliers.Where(e => e.SupplierID == id).FirstOrDefaultAsync();
                if (result == null)
                    return new Suppliers();
                
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int?> Insert(Suppliers entity)
        {
            try
            {
                var newEntity = await _context.Suppliers.AddAsync(entity);
                int result = await _context.SaveChangesAsync();
                return newEntity.Entity.SupplierID;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<int> Update(Suppliers entity)
        {
            try
            {
                var Suppliers = await _context.Suppliers.FindAsync(entity.SupplierID);

                if (Suppliers == null)
                    throw new Exception("No data can Update");

                Suppliers.CompanyName = entity.CompanyName;

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
                var Suppliers = await _context.Suppliers.FindAsync(id);

                if (Suppliers == null)
                    throw new Exception("No data can delete");

                _context.Suppliers.Remove(Suppliers);
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
