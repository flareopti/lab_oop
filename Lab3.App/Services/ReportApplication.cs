using System;
using Lab3.App.Interfaces;
using Lab3.App.Models;

namespace Lab3.App.Services;

public sealed class ReportApplication
{
    private readonly IReportDataSource _dataSource;
    private readonly IReportBuilder _builder;

    public ReportApplication(IReportDataSource dataSource, IReportBuilder builder)
    {
        _dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        _builder = builder ?? throw new ArgumentNullException(nameof(builder));
    }

    public Report CreateReport()
    {
        var records = _dataSource.GetSalesRecords();
        return _builder.Build(records);
    }
}
