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

        public async Task<IEnumerable<Employee>> GetData()
        {
            return await _context.Employees.ToListAsync();
        }

        public Task<Employee?> GetData(int id)
        {
            return _context.Employees.FirstOrDefaultAsync(e => e.EmployeeID == id);
        }

        public Task<int?> Insert(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
