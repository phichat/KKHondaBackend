﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using KKHondaBackend.Data;
using KKHondaBackend.Services;
using KKHondaBackend.Services.Ris;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace KKHondaBackend
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
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowCredentials();
            }));

            var connection = Configuration.GetConnectionString("KKConnection");
            services.AddDbContext<dbwebContext>(options => options
                .EnableSensitiveDataLogging()
                .UseSqlServer(connection));
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "KK Honda API", Version = "v1" });
                // var basePath = AppContext.BaseDirectory;
                // var xmlPath = Path.Combine(basePath, "KKHondaBackend.xml"); 
                // c.IncludeXmlComments(xmlPath);
            });

            services.AddMvc();

            services.AddTransient<ICustomerServices, CustomerServices>();
            services.AddTransient<IBookingServices, BookingServices>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IBranchService, BranchService>();
            services.AddTransient<IContractGroupService, ContractGroupService>();
            services.AddTransient<IContractTypeService, ContractTypeService>();
            services.AddTransient<IRelationService, RelationService>();
            services.AddTransient<IZoneService, ZoneService>();
            services.AddTransient<ISysParameterService, SysParameterService>();
            services.AddTransient<IStatusService, StatusService>();
            services.AddTransient<IBankingService, BankingService>();
            services.AddTransient<IMSendbackService, MSendbackService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "KK Honda API V1");
            });
        }
    }
}
