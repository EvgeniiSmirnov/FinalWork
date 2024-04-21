using System.Reflection;
using Microsoft.Extensions.Configuration;
using FinalWork.Models;
using FinalWork.Models.Enums;

namespace FinalWork.Helpers.Configuration;

public static class Configurator
{
    private static readonly Lazy<IConfiguration> s_configuration;
    public static IConfiguration Configuration => s_configuration.Value;

    static Configurator()
    {
        s_configuration = new Lazy<IConfiguration>(BuildConfiguration);
    }

    private static IConfiguration BuildConfiguration()
    {
        var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var builder = new ConfigurationBuilder()
            .SetBasePath(basePath ?? throw new InvalidOperationException())
            .AddJsonFile("appsettings.json");

        var appSettingFiles = Directory.EnumerateFiles(basePath ?? string.Empty, "appsettings.*.json");

        foreach (var appSettingFile in appSettingFiles)
        {
            builder.AddJsonFile(appSettingFile);
        }

        return builder.Build();
    }

    public static AppSettings AppSettings
    {
        get
        {
            var appSettings = new AppSettings();
            var child = Configuration.GetSection("AppSettings");

            appSettings.UI_URL = child["UI_URL"];
            appSettings.API_URL = child["API_URL"];

            return appSettings;
        }
    }

    public static List<User?> Users
    {
        get
        {
            List<User?> users = new List<User?>();
            var child = Configuration.GetSection("Users");
            foreach (var section in child.GetChildren())
            {
                var user = new User
                {
                    Password = section["Password"],
                    Username = section["Username"]
                };

                user.UserType = section["UserType"].ToLower() switch
                {
                    "admin" => UserType.Admin,
                    "user" => UserType.User,
                    _ => user.UserType
                };

                users.Add(user);
            }

            return users;
        }
    }

    public static User? Admin => Users.Find(x => x?.UserType == UserType.Admin);

    public static string? BrowserType => Configuration[nameof(BrowserType)];

    public static double WaitsTimeout => double.Parse(Configuration[nameof(WaitsTimeout)]);

    public static string? Token => Configuration[nameof(Token)];
}