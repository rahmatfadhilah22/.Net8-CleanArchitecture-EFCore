﻿using Application.Services;
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
            services.AddScoped<ISuppliersService, SuppliersService>();
            services.AddScoped<ISuppliersRepository, SuppliersRepository>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<ICustomersService, CustomersService>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<IShippersService, ShippersService>();
            services.AddScoped<IShippersRepository, ShippersRepository>();
        }
    }

}
