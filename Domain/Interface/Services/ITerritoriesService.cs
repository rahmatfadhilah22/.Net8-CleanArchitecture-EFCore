using Domain.Models;

namespace Domain.Interface.Services
{
    public interface ITerritoriesService : IBaseService<Territories>
    {
        Task<Territories> GetRecord(string? id);
        Task<string?> Insert(Territories entity);
    }
}
