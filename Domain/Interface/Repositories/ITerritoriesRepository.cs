using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Repositories
{
    public interface ITerritoriesRepository : IBaseRepository<Territories>                                      
    {
        Task<Territories> GetRecord(string? id);
        new Task<string?> Insert(Territories entity);
    }
}
