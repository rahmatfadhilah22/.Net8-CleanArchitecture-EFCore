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
    public class OrdersRepository : IOrdersRepository
    {
        private readonly DatabaseContext _context;
        public OrdersRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Orders>> GetRecords(string? keyword, int? page, int? pageSize)
        {
            try
            {
                if (_context.Orders == null)
                    return Enumerable.Empty<Orders>();

                int pageResult = pageSize ?? 10; 
                int pageNumber = page ?? 1;

                var result = _context.Orders.AsQueryable();

                if (keyword != null)
                {
                    result = result.Where(x =>
                                    x.ShipName.Contains(keyword)
                                    || x.ShipAddress.Contains(keyword));
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

        public async Task<Orders> GetRecord(int? id)
        {
            try
            {
                var result = await _context.Orders.Where(e => e.OrderID == id).FirstOrDefaultAsync();
                if (result == null)
                    return new Orders();
                
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int?> Insert(Orders entity)
        {
            try
            {
                var newEntity = await _context.Orders.AddAsync(entity);
                int result = await _context.SaveChangesAsync();
                return newEntity.Entity.OrderID;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<int> Update(Orders entity)
        {
            try
            {
                var Orders = await _context.Orders.FindAsync(entity.OrderID);

                if (Orders == null)
                    throw new Exception("No data can Update");

                Orders.ShipName = entity.ShipName;
                Orders.ShipAddress = entity.ShipAddress;

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
                var Orders = await _context.Orders.FindAsync(id);

                if (Orders == null)
                    throw new Exception("No data can delete");

                _context.Orders.Remove(Orders);
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
