using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Services
{
    public interface IEmployeeService : IBaseService<Employee>,
                                 IGetDataService<Employee>
    {

    }
}
