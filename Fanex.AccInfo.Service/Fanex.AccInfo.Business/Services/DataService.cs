using Fanex.AccInfo.Business.Models;
using Fanex.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.AccInfo.Business.Services;

public interface IDataService
{
    Tuple<KoTimeModel, List<Model41>> QueryID41Data(ScheduleTime scheduleTime);
}

public class DataServiceImp(
    IDateTimeProviderService dateTimeProvider,
    IDynamicRepository repository)
    : IDataService
{
    public Tuple<KoTimeModel, List<Model41>> QueryID41Data(ScheduleTime scheduleTime)
    {
        var dateTime = dateTimeProvider.DateTime(scheduleTime);
        var criteria = new Criteria.Criteria("Acc_Rpt_Letstalk_ConsecWinByProduct")
        {
            ExecTime4Gmt = dateTime.ExecTime4Gmt
        };

        var data = repository.FetchMultiple<KoTimeModel, Model41>(criteria);
        var item1 = data.Item1.FirstOrDefault();
        var item2 = data.Item2.ToList();
        var result = new Tuple<KoTimeModel, List<Model41>>(item1, item2);
        return result;
    }
}