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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DatabaseContext _context;
        public EmployeeRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Employee>> GetRecords(string? keyword, int? page, int? pageSize)
        {
            try
            {
                if (_context.Employees == null)
                    return Enumerable.Empty<Employee>();

                int pageResult = pageSize ?? 10; 
                int pageNumber = page ?? 1;

                var result = _context.Employees.AsQueryable();

                if (!string.IsNullOrEmpty(keyword))
                {
                    result = result.Where(e => e.FirstName.Contains(keyword));
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

        public async Task<Employee> GetRecord(int? id)
        {
            try
            {
                var result = await _context.Employees.Where(e => e.EmployeeID == id).FirstOrDefaultAsync();
                if (result == null)
                    return new Employee();
                
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int?> Insert(Employee entity)
        {
            try
            {
                var newEntity = await _context.Employees.AddAsync(entity);
                int result = await _context.SaveChangesAsync();
                return newEntity.Entity.EmployeeID;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<int> Update(Employee entity)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(entity.EmployeeID);

                if (employee == null)
                    throw new Exception("No data can Update");

                employee.FirstName = entity.FirstName;
                employee.LastName = entity.LastName;

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
                var employee = await _context.Employees.FindAsync(id);

                if (employee == null)
                    throw new Exception("No data can delete");

                _context.Employees.Remove(employee);
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
