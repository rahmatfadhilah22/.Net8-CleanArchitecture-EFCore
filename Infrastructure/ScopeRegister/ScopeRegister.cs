using Application.Services;
using Domain.Interface.Repositories;
using Domain.Interface.Services;
using Persistence.Repositories;

namespace Infrastructure.ScopeRegister
{
    public class ScopeRegister
    {
        public static void AddScopes(IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }
    }

}
