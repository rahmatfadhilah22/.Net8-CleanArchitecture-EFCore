using Domain.Models;

namespace Domain.Interface.Services
{
    public interface ICustomersService : IBaseService<Customers>
    {
        Task<Customers> GetRecord(string? id);
        new Task<string?> Insert(Customers entity);
    }
}
