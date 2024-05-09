using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Services
{
    public interface IGetDataService<T> where T : BaseEntities
    {
        Task<IEnumerable<T>> GetData();
        Task<T> GetData(int id);
    }
}
