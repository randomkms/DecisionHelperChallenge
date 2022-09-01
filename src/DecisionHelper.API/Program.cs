using DecisionHelper.API.Extensions;
using DecisionHelper.Infrastructure;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace DecisionHelper.API
{
    public class Program
    {
        private static void Main(string[] args)
        {
            const string corsPolicyName = "AllowAll";

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCorsConfig(corsPolicyName);
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddInfrastructure(builder.Configuration.GetValue<bool>("UseRedis"));
            builder.Services.AddCustomServices();

            builder.Configuration.AddEnvironmentVariables();

            builder.Services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>((options) =>
            {
                return new[] { builder.Configuration.GetSection("Redis").Get<RedisConfiguration>() };
            });

            var app = builder.Build();

            app.Services.SeedInitialData();

            app.UseCors(corsPolicyName);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}