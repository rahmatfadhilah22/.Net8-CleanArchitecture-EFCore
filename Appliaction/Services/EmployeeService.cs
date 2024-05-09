using Domain.Interface.Repositories;
using Domain.Interface.Services;
using Domain.Models;


namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Employee>> GetData()
        {
            return await _repository.GetData();
        }

        public Task<Employee> GetData(int id)
        {
            throw new NotImplementedException();
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
