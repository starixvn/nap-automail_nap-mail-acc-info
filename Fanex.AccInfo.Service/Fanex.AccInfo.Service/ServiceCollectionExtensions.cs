using Fanex.AccInfo.Business.Models;
using Fanex.AccInfo.Business.Services;
using Fanex.Logging;
using Fanex.Logging.Sentry.RavenClient;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SharpRaven;
using System;
using System.Collections.Generic;
using System.IO;

namespace Fanex.AccInfo.Service;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection Services { get; set; }

    public static void LoadConfiguration(this IServiceCollection services)
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        var text = File.ReadAllText("appsettings.json");
        AccInfoConfigService.Config = JsonConvert.DeserializeObject<ConfigSettingModel>(text);
        var json = File.ReadAllText(AccInfoConfigService.Config.AccFile);
        var accInfo = JsonConvert.DeserializeObject<Dictionary<string, AccInfoModel>>(json);
        AccInfoConfigService.Config.AccInfos = accInfo;
    }

    public static void SentryRegister(this IServiceCollection services)
    {
        var sentry = new SentryLogging(new SentryEngineOptions
        {
            Dsn = new Dsn(AccInfoConfigService.Config.SentryClientKey)
        });

        LogManager
            .SetDefaultLogCategory(AccInfoConfigService.Config.CategorySentryLog)
            .Use(new List<ILogEngine>
            {
                sentry
            });
    }

    public static void InternalServiceRegister(this IServiceCollection services)
    {
        Services = services;
    }
}