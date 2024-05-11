using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntities
    {
        Task<IEnumerable<T>> GetRecords(string? keyword, int? page, int? pageSize);
        Task<T> GetRecord(int? id);
        Task<int?> Insert(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(int? id);

    }
}
