using System.Collections.Generic;
using Lab3.App.Models;

namespace Lab3.App.Interfaces;

public interface IReportDataSource
{
    IReadOnlyCollection<SalesRecord> GetSalesRecords();
}
