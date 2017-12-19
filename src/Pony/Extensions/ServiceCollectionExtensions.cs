using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Pony.Reporting.Mazes;
using Pony.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Pony.Middleware;

namespace Pony.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var hostingEnvironment = services.BuildServiceProvider().GetService<IHostingEnvironment>();

            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainAutoMapperProfile());
            });

            services.AddSingleton(sp => autoMapperConfig.CreateMapper());

            return services;
        }


    }
}