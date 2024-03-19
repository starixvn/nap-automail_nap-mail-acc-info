using Fanex.Data;
using Fanex.Data.Repository;
using Fanex.Data.SqlServer;
using Fanex.LetstalkService.Client;
using Fanex.Logging;
using Fanex.Logging.Sentry.RavenClient;
using Fanex.AccInfo.Business.Models;
using Fanex.AccInfo.Business.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SharpRaven;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LetstalkConfig = Fanex.LetstalkService.Core.Services.AppConfigService;

namespace Fanex.AccInfo.Service;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection Services { get; set; }

    public static void LoadConfiguration(this IServiceCollection services)
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var text = File.ReadAllText("appsettings.json");
        AppConfigService.Config = JsonConvert.DeserializeObject<ConfigSettingModel>(text);
        LetstalkConfig.Config.LetstalkServiceApi = AppConfigService.Config.LetstalkServiceApi;
    }

    public static void SentryRegister(this IServiceCollection services)
    {
        var sentry = new SentryLogging(new SentryEngineOptions
        {
            Dsn = new Dsn(AppConfigService.Config.SentryClientKey)
        });

        LogManager
            .SetDefaultLogCategory(AppConfigService.Config.CategorySentryLog)
            .Use(new List<ILogEngine>
            {
                sentry
            });
    }

    public static void FanexDataRegister(this IServiceCollection services, IConfiguration config)
    {
        var connections = config.GetSection("ConnectionStrings")
            .GetChildren()
            .ToDictionary(
                x => x.Key,
                y => new ConnectionConfiguration(y.Key, y.Value)
            );

        DbSettingProviderManager
            .StartNewSession()
            .Use(connections)
            .WithSqlServer()
            .UseDefaultDbSettingProvider(config.GetSection("StoredProcedurePath").Value)
            .Run();
    }

    public static void InternalServiceRegister(this IServiceCollection services)
    {
        services.AddSingleton<ILog, LogImp>();
        services.AddSingleton<IDynamicRepository, DynamicRepository>();
        services.AddSingleton<IDateTimeProviderService, DateTimeProviderServiceImp>();
        services.AddSingleton<IDataService, DataServiceImp>();
        services.AddSingleton<IFormatService, FormatServiceImp>();
        services.AddSingleton<LetstalkClientService, LetstalkClientServiceImp>();
        services.AddSingleton<IMainService, MainServiceImp>();
        services.AddSingleton<IMessageProviderService, MessageProviderServiceImp>();

        Services = services;
    }
}