using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Lab3.App.Models;

public sealed class Report
{
    private readonly IReadOnlyList<ReportRow> _rows;

    public string Title { get; }
    public DateTimeOffset GeneratedAt { get; }
    public IReadOnlyList<ReportRow> Rows => _rows;

    public int TotalQuantity { get; }
    public decimal TotalRevenue { get; }

    public Report(string title, DateTimeOffset generatedAt, IEnumerable<ReportRow> rows)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title must not be empty.", nameof(title));
        }

        if (generatedAt == default)
        {
            throw new ArgumentOutOfRangeException(nameof(generatedAt), generatedAt, "GeneratedAt must be specified.");
        }

        ArgumentNullException.ThrowIfNull(rows);

        var rowList = rows.ToList();
        if (rowList.Any(row => row is null))
        {
            throw new ArgumentException("Rows collection must not contain null values.", nameof(rows));
        }

        _rows = new ReadOnlyCollection<ReportRow>(rowList);

        Title = title;
        GeneratedAt = generatedAt;
        TotalQuantity = rowList.Sum(row => row.TotalQuantity);
        TotalRevenue = rowList.Sum(row => row.TotalRevenue);
    }
}
