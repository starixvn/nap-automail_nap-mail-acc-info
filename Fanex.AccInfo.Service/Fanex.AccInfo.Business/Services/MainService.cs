using Fanex.LetstalkService.Client;
using Fanex.LetstalkService.Core.Models;
using Fanex.MailDelivery.Core.Models;
using Fanex.AccInfo.Business.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Fanex.AccInfo.Business.Services;

public interface IMainService
{
    JobMessage Execute(ScheduleTime scheduleTime);
}

public class MainServiceImp(
    ILog log,
    IDateTimeProviderService dateTimeService,
    IMessageProviderService messageService,
    LetstalkClientService letstalkClientService)
    : IMainService
{
    private readonly List<string> results = new();
    private const int DelayInMillisecond = 5000;

    public JobMessage Execute(ScheduleTime scheduleTime)
    {
        var result = new ExecuteResultModel();
        var jobMessage = new JobMessage();
        try
        {
            result = SendNotification(scheduleTime);
        }
        catch (Exception e)
        {
            result.Success = false;
            result.Message = e.Message;
            log.Log(e);
        }
        finally
        {
            jobMessage.Mail = result.Mail;
            jobMessage.JobStatus = result.Success ? JobStatus.SuccessWithoutSendmail : JobStatus.DataIsNotReady;
            jobMessage.TaskLog = new TaskLog
            {
                LogTime = DateTime.Now,
                Message = result.Message
            };
        }

        return jobMessage;
    }

    private ExecuteResultModel SendNotification(ScheduleTime scheduleTime)
    {
        LogInfo(scheduleTime);
        results.Clear();
        var reports = AppConfigService.Config.Reports;
        var messages = new List<LetstalkMessageModel>();
        reports.ForEach(report =>
        {
            var messageModel = messageService.Execute(report).GetMessageModel(report, scheduleTime);
            messages.Add(messageModel);
        });

        messages.ForEach(SendMessage);

        WriteFile(messages, scheduleTime);

        return new ExecuteResultModel
        {
            Mail = new Mail(),
            Success = results.Count == 0,
            Message = string.Join(", ", results)
        };
    }

    private void WriteFile(List<LetstalkMessageModel> messages, ScheduleTime scheduleTime)
    {
        var json = JsonConvert.SerializeObject(messages, Formatting.Indented);
        var time = dateTimeService.DateTime(scheduleTime).ExecTime4Gmt;
        var path = $"{AppConfigService.Config.Path}/{time:yyyy}/{time:MM}/{time:dd}";
        var name = $"ID-32-34-35-36-{time:yyyyMMdd-HHmm}.json";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        path = $@"{path}/{name}";
        File.WriteAllText(path, json);
    }

    private void SendMessage(LetstalkMessageModel messageModel)
    {
        var letstalkGroups = messageModel.LetstalkGroup;
        foreach (var group in letstalkGroups)
        {
            if (string.IsNullOrEmpty(group))
            {
                continue;
            }

            var letstalkModel = new LetstalkModel
            {
                TargetName = $"{AppConfigService.Config.TargetName}",
                Subject = messageModel.Subject,
                Content = messageModel.Body,
                Group = group
            };

            var result = letstalkClientService.SendMessage(letstalkModel);
            if (!result)
            {
                results.Add($"Report: {messageModel.ReportId}, Group: {group}");
            }

            Thread.Sleep(DelayInMillisecond);
        }
    }

    private void LogInfo(ScheduleTime scheduleTime)
    {
        var dateTime = dateTimeService.DateTime(scheduleTime);
        var start = dateTime.ExecTime4Gmt;

        var logs = new List<string>
        {
            "Fanex.NonSport.Service",
            $"Now: {DateTime.Now}",
            $"Start: {start}"
        };

        log.Log(string.Join(Environment.NewLine, logs));
    }
}