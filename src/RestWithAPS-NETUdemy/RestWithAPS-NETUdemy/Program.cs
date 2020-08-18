using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;

namespace RestWithAPS_NETUdemy
{
    public class Program
    {
        private static readonly string EnvironmentName;
        private static readonly IConfiguration Configuration;

        static Program()
        {
            EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()                
                .WriteTo.Console()
                .CreateLogger();
        }

        public static void Main(string[] args)
        {
            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();

            }
            catch (Exception exception)
            {
                Log.Fatal(exception, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            MigrateDatabase();

            return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddConfiguration(Configuration);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseSerilog();
        }


        private static void MigrateDatabase()
        {
            // exclude db/datasets from production and staging environments
            string location = EnvironmentName == Environments.Production || EnvironmentName == Environments.Staging
                ? "db/migrations"
                : "db";

            try
            {
                var cnx = new MySqlConnection(Configuration["MySqlConnection:MySqlConnectionString"]);
                var evolve = new Evolve.Evolve(cnx, msg => Log.Information(msg))
                {
                    Locations = new[] { location },
                    IsEraseDisabled = true,
                    Placeholders = new Dictionary<string, string>
                    {
                        ["${table4}"] = "table4"
                    }
                };

                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed.", ex);
                throw;
            }
        }
    }
}
