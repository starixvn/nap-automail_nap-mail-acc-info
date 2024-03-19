using Fanex.Data.Repository;
using System;

namespace Fanex.AccInfo.Business.Criteria;

public class Criteria : CriteriaBase
{
    private string SpName { get; set; }

    public Criteria(string spName)
    {
        SpName = spName;
    }

    public override string GetSettingKey() => SpName;

    public override bool IsValid() => true;

    public DateTime ExecTime4Gmt { get; set; }
}