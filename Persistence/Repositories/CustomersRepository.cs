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
    public class CustomersRepository : ICustomersRepository
    {
        private readonly DatabaseContext _context;
        public CustomersRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Customers>> GetRecords(string? keyword, int? page, int? pageSize)
        {
            try
            {
                if (_context.Customers == null)
                    return Enumerable.Empty<Customers>();

                int pageResult = pageSize ?? 10; 
                int pageNumber = page ?? 1;

                var result = _context.Customers.AsQueryable();

                if (keyword != null)
                {
                    result = result.Where(x =>
                                    x.CompanyName.Contains(keyword)
                                    || x.ContactName.Contains(keyword)
                                    || x.City.Contains(keyword));
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

        public async Task<Customers> GetRecord(string? id)
        {
            try
            {
                var result = await _context.Customers.Where(e => e.CustomerID == id).FirstOrDefaultAsync();
                if (result == null)
                    return new Customers();
                
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string?> Insert(Customers entity)
        {
            try
            {
                var newEntity = await _context.Customers.AddAsync(entity);
                int result = await _context.SaveChangesAsync();
                return newEntity.Entity.CustomerID;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<int> Update(Customers entity)
        {
            try
            {
                var Customers = await _context.Customers.FindAsync(entity.CustomerID);

                if (Customers == null)
                    throw new Exception("No data can Update");

                Customers.CompanyName = entity.CompanyName;
                Customers.ContactName = entity.ContactName;

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
                var Customers = await _context.Customers.FindAsync(id);

                if (Customers == null)
                    throw new Exception("No data can delete");

                _context.Customers.Remove(Customers);
                int result = await _context.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Customers> GetRecord(int? id)
        {
            throw new NotImplementedException();
        }

        Task<int?> IBaseRepository<Customers>.Insert(Customers entity)
        {
            throw new NotImplementedException();
        }
    }
}
