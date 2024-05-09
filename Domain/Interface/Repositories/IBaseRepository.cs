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
        Task<int?> Insert(T entity);
        Task<int> Update(int? id);

    }
}
