﻿using ChessMaster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedisService;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
#if DEBUG
            var redisService = new RedisService.RedisService("192.168.32.29");
            services.AddSingleton<IRedisService>(sp => redisService);
#else
            services.AddSingleton<IRedisService>(sp => new RedisService.RedisService(Configuration["RedisHost"]));
#endif
            services.AddSingleton<IMaster, Master>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute("default", "{controller}/{action}/{id?}");
            //});
        }
    }
}
