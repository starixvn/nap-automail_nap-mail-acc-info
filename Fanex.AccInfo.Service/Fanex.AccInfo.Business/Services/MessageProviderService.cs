using Fanex.AccInfo.Business.Models;
using System.Collections.Generic;

namespace Fanex.AccInfo.Business.Services;

public interface IMessageProviderService
{
    IMessageService Execute(ReportModel reportModel);
}

public class MessageProviderServiceImp(
    IFormatService formatService,
    IDataService dataService,
    ILog log)
    : IMessageProviderService
{
    private readonly ILog log = log;

    private Dictionary<string, IMessageService> Service => new()
    {
        { "ID41", new MessageID41Service(formatService, dataService) }
    };

    public IMessageService Execute(ReportModel reportModel)
    {
        return Service[reportModel.Id];
    }
}