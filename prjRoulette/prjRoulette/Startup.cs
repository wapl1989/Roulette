using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using prjRoulette.Data;
using prjRoulette.Interfaces;
using prjRoulette.Repositories;
using StackExchange.Redis;

namespace prjRoulette
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IConnectionMultiplexer>(x =>
                        ConnectionMultiplexer.Connect(Configuration.GetSection("ConnectionRedis").Value));
            services.AddSingleton<IRoulette, RouletteRepository>();
            services.AddSingleton<IBet, BetRepository>();
            services.AddSingleton<IOperationsRoulette, OperationsRedisRoulette>();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
