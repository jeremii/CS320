﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Microsoft.EntityFrameworkCore;
using SMP.DAL.Initializers;
using SMP.DAL.EF;
using SMP.DAL.Repos;
using SMP.DAL.Repos.Interfaces;
using SMP.Models.Entities;
using SMP.Service.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace SMP.Service
{
    public class Startup
    {
        private IHostingEnvironment _env;
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            _env = env;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //services.AddMvc();
            services.AddMvcCore(config =>
                config.Filters.Add(new SMPExceptionFilter(_env.IsDevelopment())))
                .AddJsonFormatters(j =>
                {
                    j.ContractResolver = new DefaultContractResolver();
                    j.Formatting = Formatting.Indented;
                })
                    .AddJsonOptions(
                        options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                       );
            // http://docs.asp.net/en/latest/security/cors.html
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials();
                });
            });
            //NOTE: Did not disable mixed mode running here
            services.AddDbContext<StoreContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("PRS")));



            services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            services.AddScoped<IAddressRepo, AddressRepo>();
            services.AddScoped<IApproverRepo, ApproverRepo>();
            services.AddScoped<IBudgetRepo, BudgetRepo>();
            services.AddScoped<ICampusRepo, CampusRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<ICollegeRepo, CollegeRepo>();
            services.AddScoped<IEmployeeGroupRepo, EmployeeGroupRepo>();
            //services.AddScoped<IFileAttachmentRepo, FileAttachmentRepo>();
            services.AddScoped<IItemRepo, ItemRepo>();
            services.AddScoped<IOrderItemRepo, OrderItemRepo>();
            services.AddScoped<IRequisitionRepo, RequisitionRepo>();
            services.AddScoped<IStatusCodeRepo, StatusCodeRepo>();
            //services.AddScoped<IUserPrivilegeRepo, UserPrivilegeRepo>();
            services.AddScoped<IVendorRepo, VendorRepo>();
            services.AddScoped<ILoginRepo, LoginRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
