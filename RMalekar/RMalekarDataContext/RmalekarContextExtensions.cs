using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace RMalekarEntityModels;
public static class RmalekarContextExtensions
{
    public static IServiceCollection AddRMalekarDataContext(
        this IServiceCollection services, string? connectionString = null)
    {
        if (connectionString is null)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
            {
                Server = Environment.GetEnvironmentVariable("RMALEKAR_WEBAPP_DB_SERVER"),
                Database = Environment.GetEnvironmentVariable("RMALEKAR_WEBAPP_DB_NAME"),
                UserID = Environment.GetEnvironmentVariable("MYSQL_USER"),
                Password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD"),
                SslMode = MySqlSslMode.None,
                AllowUserVariables = true,
                ConnectionIdleTimeout = 3
            };
            connectionString = builder.ConnectionString;
        }
        services.AddDbContext<RmalekarDataContext>(options =>
        {
            try
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), option=>option.EnableRetryOnFailure());
                // options.LogTo(RMalekarContextLogger.WriteLineAsync,
                //   new[] { Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting });
               // return StatusCode(500, "Internal server error");
            }
            catch (MySqlException ex)
            {
               // RMalekarContextLogger.WrieMySqlException(ex.StackTrace);
            }
        },
               contextLifetime: ServiceLifetime.Scoped,
               optionsLifetime: ServiceLifetime.Singleton
           );


        return services;
    }
}
