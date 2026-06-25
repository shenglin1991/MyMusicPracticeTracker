using MySql.Data.MySqlClient;

namespace MusicPracticeTracker.Api.Configuration;

public static class DatabaseConfiguration
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        var configuredConnectionString = configuration.GetConnectionString("DefaultConnection");
        if (!string.IsNullOrWhiteSpace(configuredConnectionString))
        {
            return configuredConnectionString;
        }

        var host = configuration["DB_HOST"] ?? "127.0.0.1";
        var port = configuration["DB_PORT"] ?? "3307";
        var username = configuration["DB_USERNAME"] ?? "root";
        var password = configuration["DB_PASSWORD"] ?? "root";
        var database = configuration["DB_DATABASE"] ?? "music_practice_tracker";
        var parsedPort = uint.TryParse(port, out var value) ? value : 3307;

        var connectionStringBuilder = new MySqlConnectionStringBuilder
        {
            Server = host,
            Port = parsedPort,
            Database = database,
            UserID = username,
            Password = password,
            SslMode = MySqlSslMode.Disabled,
            AllowPublicKeyRetrieval = true
        };

        return connectionStringBuilder.ConnectionString;
    }
}
