//extern alias MySqlConnectorAlias;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using RestWithAPS_NETUdemy.Business;
using RestWithAPS_NETUdemy.Business.Implementations;
using RestWithAPS_NETUdemy.Hypermedia;
using RestWithAPS_NETUdemy.Model.Context;
using RestWithAPS_NETUdemy.Repository;
using RestWithAPS_NETUdemy.Repository.Generic;
using RestWithAPS_NETUdemy.Repository.Implementattions;
using RestWithAPS_NETUdemy.Security.Config;
using System;
using Tapioca.HATEOAS;

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

            ExecuteMigrations();

            var signingConfigurations = new SigningConfiguration();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                Configuration.GetSection("TokenConfigurations")
            )
            .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);


            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Validates the signing of a received token
                paramsValidation.ValidateIssuerSigningKey = true;

                // Checks if a received token is still valid
                paramsValidation.ValidateLifetime = true;

                // Tolerance time for the expiration of a token (used in case
                // of time synchronization problems between different
                // computers involved in the communication process)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Enables the use of the token as a means of
            // authorizing access to this project's resources
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddControllers();
            services.AddApiVersioning();

            //Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RestFul Api with Asp net Core", Version = "v1" });
            });

            //Incluindo outros content type
            services.AddMvc(opt =>
            {
                opt.RespectBrowserAcceptHeader = true;
                opt.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
                opt.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            }).AddXmlSerializerFormatters();

            //HATEOAS
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add(new PersonEnricher());
            services.AddSingleton(filterOptions);

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddScoped<IPersonBusiness, PersonBusiness>();
            services.AddScoped<IBookBusiness, BookBusiness>();
            services.AddScoped<ILoginBusiness, LoginBusiness>();
            services.AddScoped<IFileBusiness, FileBusiness>();
        }

        private void ExecuteMigrations()
        {
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

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1"));

            var opt = new RewriteOptions();
            opt.AddRedirect("^$", "swagger");
            app.UseRewriter(opt);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=Values}/{id?}");
            });
        }
    }
}
