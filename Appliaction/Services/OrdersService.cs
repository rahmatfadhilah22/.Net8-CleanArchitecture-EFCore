using Domain.Interface.Repositories;
using Domain.Interface.Services;
using Domain.Models;

namespace Application.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _repository;
        public OrdersService(IOrdersRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Orders>> GetRecords(string? keyword, int? page, int? pageSize)
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

        public async Task<Orders> GetRecord(int? id)
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

        public async Task<int?> Insert(Orders entity)
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

        public async Task<int> Update(Orders id)
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

    }
}
