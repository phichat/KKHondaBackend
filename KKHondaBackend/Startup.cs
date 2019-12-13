using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using KKHondaBackend.Data;
using KKHondaBackend.Services;
using KKHondaBackend.Services.Ris;
using Microsoft.OpenApi.Models;

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

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "KK Honda", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var SwaggerEndpoint = "";
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
                SwaggerEndpoint = "/swagger/v1/swagger.json";
            } 
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(builder =>
                {
                    builder.WithOrigins("http://203.154.126.61/KK-Honda-Web")
                           .AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
                SwaggerEndpoint = "../swagger/v1/swagger.json";
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint(SwaggerEndpoint, "KK Honda V1");
            });
        }
    }
}
