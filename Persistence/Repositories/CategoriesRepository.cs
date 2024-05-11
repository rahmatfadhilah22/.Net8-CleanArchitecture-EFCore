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
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly DatabaseContext _context;
        public CategoriesRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Categories>> GetRecords(string? keyword, int? page, int? pageSize)
        {
            try
            {
                if (_context.Categories == null)
                    return Enumerable.Empty<Categories>();

                int pageResult = pageSize ?? 10; 
                int pageNumber = page ?? 1;

                var result = _context.Categories.AsQueryable();

                if (keyword != null)
                {
                    result = result.Where(x =>
                                    x.CategoryName.Contains(keyword)
                                    || x.Description.Contains(keyword));
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

        public async Task<Categories> GetRecord(int? id)
        {
            try
            {
                var result = await _context.Categories.Where(e => e.CategoryID == id).FirstOrDefaultAsync();
                if (result == null)
                    return new Categories();
                
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int?> Insert(Categories entity)
        {
            try
            {
                var newEntity = await _context.Categories.AddAsync(entity);
                int result = await _context.SaveChangesAsync();
                return newEntity.Entity.CategoryID;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<int> Update(Categories entity)
        {
            try
            {
                var Categories = await _context.Categories.FindAsync(entity.CategoryID);

                if (Categories == null)
                    throw new Exception("No data can Update");

                Categories.CategoryName = entity.CategoryName;
                Categories.Description = entity.Description;

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
                var Categories = await _context.Categories.FindAsync(id);

                if (Categories == null)
                    throw new Exception("No data can delete");

                _context.Categories.Remove(Categories);
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
