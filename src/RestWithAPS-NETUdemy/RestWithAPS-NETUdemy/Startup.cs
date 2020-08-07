//extern alias MySqlConnectorAlias;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestWithAPS_NETUdemy.Business;
using RestWithAPS_NETUdemy.Model.Context;
using RestWithAPS_NETUdemy.Repository;
using RestWithAPS_NETUdemy.Repository.Generic;

namespace RestWithAPS_NETUdemy
{
    public class Startup
    {
        public IConfiguration Configuration { get; }        
        private IHostEnvironment Enviroment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment enviroment)
        {
            Configuration = configuration;            
            Enviroment = enviroment;            
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["MySqlConnection:MySqlConnectionString"];

            services.AddDbContext<MySQLContext>(opt => opt.UseMySql(connectionString));            

            if (Enviroment.IsDevelopment())
            {
                //try
                //{
                //    var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                //    var evolve = new Evolve.Evolve(evolveConnection, msg => Debug.WriteLine(msg))
                //    {
                //        Locations = new List<string> { "db/migrations" },
                //        IsEraseDisabled = true,                        
                //    };                    

                //    evolve.Migrate();
                //}
                //catch (Exception ex)
                //{
                //   // _logger.LogCritical("Database migration failed", ex);
                //     throw;
                //}
            }

            services.AddControllers();

            services.AddApiVersioning();

            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddScoped<IPersonBusiness, PersonBusiness>();
            services.AddScoped<IBookBusiness, BookBusiness>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
