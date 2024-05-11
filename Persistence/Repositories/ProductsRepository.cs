using Domain.Interface.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly DatabaseContext _context;
        public ProductsRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Products>> GetRecords(string? keyword, int? page, int? pageSize)
        {
            try
            {
                if (_context.Products == null)
                    return Enumerable.Empty<Products>();

                int pageResult = pageSize ?? 10; 
                int pageNumber = page ?? 1;

                var result = _context.Products.AsQueryable();

                if (keyword != null)
                {
                    result = result.Where(x =>
                                    x.ProductName.Contains(keyword));
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

        public async Task<Products> GetRecord(int? id)
        {
            try
            {
                var result = await _context.Products.Where(e => e.CategoryID == id).FirstOrDefaultAsync();
                if (result == null)
                    return new Products();
                
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int?> Insert(Products entity)
        {
            try
            {
                var newEntity = await _context.Products.AddAsync(entity);
                int result = await _context.SaveChangesAsync();
                return newEntity.Entity.CategoryID;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<int> Update(Products entity)
        {
            try
            {
                var Products = await _context.Products.FindAsync(entity.CategoryID);

                if (Products == null)
                    throw new Exception("No data can Update");

                Products.ProductName = entity.ProductName;

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
                var Products = await _context.Products.FindAsync(id);

                if (Products == null)
                    throw new Exception("No data can delete");

                _context.Products.Remove(Products);
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
