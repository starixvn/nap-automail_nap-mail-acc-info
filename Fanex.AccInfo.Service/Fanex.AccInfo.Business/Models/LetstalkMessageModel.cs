using System.Collections.Generic;

namespace Fanex.AccInfo.Business.Models;

public class LetstalkMessageModel
{
    public string ReportId { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }

    public List<string> LetstalkGroup { get; set; }
}