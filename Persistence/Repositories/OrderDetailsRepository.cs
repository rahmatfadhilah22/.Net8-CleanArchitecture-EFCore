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
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly DatabaseContext _context;
        public OrderDetailsRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<OrderDetails>> GetRecords(string? keyword, int? page, int? pageSize)
        {
            try
            {
                if (_context.OrderDetails == null)
                    return Enumerable.Empty<OrderDetails>();

                int pageResult = pageSize ?? 10; 
                int pageNumber = page ?? 1;

                var result = _context.OrderDetails.AsQueryable();

                if (keyword != null)
                {
                    result = result.Where(x =>
                                    x.Quantity == Convert.ToInt16(keyword));
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

        public async Task<OrderDetails> GetRecord(int? id)
        {
            try
            {
                var result = await _context.OrderDetails.Where(e => e.OrderID == id).FirstOrDefaultAsync();
                if (result == null)
                    return new OrderDetails();
                
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int?> Insert(OrderDetails entity)
        {
            try
            {
                var newEntity = await _context.OrderDetails.AddAsync(entity);
                int result = await _context.SaveChangesAsync();
                return newEntity.Entity.OrderID;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<int> Update(OrderDetails entity)
        {
            try
            {
                var OrderDetails = await _context.OrderDetails.FindAsync(entity.OrderID);

                if (OrderDetails == null)
                    throw new Exception("No data can Update");

                OrderDetails.UnitPrice = entity.UnitPrice;
                OrderDetails.Quantity = entity.Quantity;

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
                var OrderDetails = await _context.OrderDetails.FindAsync(id);

                if (OrderDetails == null)
                    throw new Exception("No data can delete");

                _context.OrderDetails.Remove(OrderDetails);
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
