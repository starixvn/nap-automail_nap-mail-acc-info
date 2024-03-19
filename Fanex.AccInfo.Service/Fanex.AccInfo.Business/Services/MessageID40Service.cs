using Fanex.AccInfo.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.AccInfo.Business.Services;

public class MessageID41Service(
    IFormatService formatService,
    IDataService dataService)
    : IMessageService
{
    public LetstalkMessageModel GetMessageModel(ReportModel reportModel, ScheduleTime scheduleTime)
    {
        var data = dataService.QueryID41Data(scheduleTime);
        var letstalkGroups = reportModel.LetstalkGroup.Split(',').ToList();
        var content = new List<string>();
        foreach (var row in data.Item2)
        {
            content.Add($"{row.Product}: {row.UserName} {formatService.CutNumberOff(row.Winloss)}");
        }

        content.Add("");
        content.Add("(Report ID #41)");

        return new LetstalkMessageModel
        {
            ReportId = reportModel.Id,
            Subject = data.Item1.Title,
            Body = string.Join(Environment.NewLine, content),
            LetstalkGroup = letstalkGroups
        };
    }
}