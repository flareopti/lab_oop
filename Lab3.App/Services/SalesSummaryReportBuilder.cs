using System;
using System.Collections.Generic;
using System.Linq;
using Lab3.App.Interfaces;
using Lab3.App.Models;

namespace Lab3.App.Services;

public sealed class SalesSummaryReportBuilder : IReportBuilder
{
    private readonly string _title;

    public SalesSummaryReportBuilder(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title must not be empty.", nameof(title));
        }

        _title = title;
    }

    public Report Build(IReadOnlyCollection<SalesRecord> records)
    {
        ArgumentNullException.ThrowIfNull(records);

        foreach (var record in records)
        {
            ArgumentNullException.ThrowIfNull(record);
        }

        var rows = records
            .GroupBy(record => record.ProductName, StringComparer.OrdinalIgnoreCase)
            .Select(group =>
            {
                var totalQuantity = group.Sum(record => record.Quantity);
                var totalRevenue = group.Sum(record => record.Revenue);

                var displayName = group.First().ProductName;
                return new ReportRow(displayName, totalQuantity, totalRevenue);
            })
            .OrderByDescending(row => row.TotalRevenue)
            .ThenBy(row => row.ProductName, StringComparer.OrdinalIgnoreCase)
            .ToList();

        return new Report(_title, DateTimeOffset.UtcNow, rows);
    }
}
