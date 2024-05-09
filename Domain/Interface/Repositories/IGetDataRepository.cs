using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Repositories
{
    public interface IGetDataRepository<T> where T : BaseEntities
    {
        Task<IEnumerable<T>> GetData();
        Task<T> GetData(int id);
    }
}
