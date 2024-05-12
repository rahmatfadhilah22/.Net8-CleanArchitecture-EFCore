using Domain.Interface.Repositories;
using Domain.Interface.Services;
using Domain.Models;

namespace Application.Services
{
    public class TerritoriesService : ITerritoriesService
    {
        private readonly ITerritoriesRepository _repository;
        public TerritoriesService(ITerritoriesRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Territories>> GetRecords(string? keyword, int? page, int? pageSize)
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

        public async Task<Territories> GetRecord(string? id)
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

        public async Task<string?> Insert(Territories entity)
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

        public async Task<int> Update(Territories id)
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

        public Task<Territories> GetRecord(int? id)
        {
            throw new NotImplementedException();
        }

        Task<int?> IBaseService<Territories>.Insert(Territories entity)
        {
            throw new NotImplementedException();
        }
    }
}
