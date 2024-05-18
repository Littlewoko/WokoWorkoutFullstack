using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Workout_API.DBContexts;

namespace Workout_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureBuilder(builder);
            ConfigureDatabase(builder);

            var app = builder.Build();
            ConfigureApp(app);

            app.Run();
        }

        private static void ConfigureBuilder(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        private static void ConfigureDatabase(WebApplicationBuilder builder)
        {
            var configuration = Configuration.GetUserSecretsConfiguration();
            string connectionString = Configuration.GetConfigurationItem("ConnectionString");

            builder.Services.AddDbContext<DBContext>(
                options => options.UseSqlServer(connectionString));
        }

        private static void ConfigureApp(WebApplication app)
        {
            SetupSwagger(app);

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }

        private static void SetupSwagger(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}