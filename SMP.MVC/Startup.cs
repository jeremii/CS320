﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

//using SMP.MVC.Authentication;
using SMP.MVC.Configuration;
using SMP.MVC.Filters;
using SMP.MVC.WebServiceAccess;
using SMP.MVC.WebServiceAccess.Base;
using SMP.Models.Entities;
using SMP.DAL.Initializers;
using SMP.Models;
using SMP.Service;
using SMP.Service.Controllers;
using SMP.DAL.EF;

namespace SMP.MVC
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
                //.AddUserSecrets<>();
            Configuration = builder.Build();
            Environment = env;
        }

        public IHostingEnvironment Environment { get; }
        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            // Add framework services.
            services.AddSingleton(_ => Configuration);
            services.AddSingleton<IWebServiceLocator, WebServiceLocator>();
            services.AddSingleton<IWebApiCalls, WebApiCalls>();
            //services.AddScoped<SignInManager<User>, SignInManager<User>>();

            // Add framework services.
            if (Environment.IsDevelopment())
            {
                //services.AddDbContext<Context>(options =>
                    //options.UseSqlServer(Configuration.GetConnectionString("SMP")));
            }
            services.AddDbContext<Context>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SMP")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<Context>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            // Add framework services.
            services.AddMvc();

            //services.AddSingleton<IAuthHelper, AuthHelper>();
            //services.AddMvc(config => {
            //    config.Filters.Add(
            //        new AuthActionFilter(services.BuildServiceProvider().GetService<IAuthHelper>()));
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
