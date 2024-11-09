
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;



namespace Employee
{
    public class Startup
    {

        public Startup(IConfiguration configuration) {
        
            Configuration= configuration;
        }

        public  IConfiguration Configuration { get;}

        public void ConfigureServices(IServiceCollection services)
        {   
            //enable cors
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin",options=>options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });


            //json Serializer
            services.AddControllersWithViews().AddNewtonsoftJson(options=>options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options=>options.SerializerSettings.ContractResolver = new DefaultContractResolver());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //enable cors
            app.UseCors(options=>options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

        }


    }
}
