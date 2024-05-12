using Domain.Interface.Repositories;
using Domain.Interface.Services;
using Domain.Models;

namespace Application.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _repository;
        public CustomersService(ICustomersRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Customers>> GetRecords(string? keyword, int? page, int? pageSize)
        {
            try
            {
                var result = await _repository.GetRecords(keyword, page, pageSize);
                return result;
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
                var result = await _repository.GetRecord(id);
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
                var result = await _repository.Insert(entity);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Update(Customers id)
        {
            try
            {
                var result = await _repository.Update(id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> Delete(List<int> entities)
        {
            try
            {
                int results = 0;
                foreach (var entity in entities)
                {
                    results += await _repository.Delete(entity);
                }
                return results;
            }
            catch(Exception) 
            {
                throw;
            }
        }

        public Task<Customers> GetRecord(int? id)
        {
            throw new NotImplementedException();
        }

        Task<int?> IBaseService<Customers>.Insert(Customers entity)
        {
            throw new NotImplementedException();
        }
    }
}
