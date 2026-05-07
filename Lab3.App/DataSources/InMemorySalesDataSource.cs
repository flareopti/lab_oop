using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Lab3.App.Interfaces;
using Lab3.App.Models;

namespace Lab3.App.DataSources;

public sealed class InMemorySalesDataSource : IReportDataSource
{
    private readonly IReadOnlyCollection<SalesRecord> _records;

    public InMemorySalesDataSource(IEnumerable<SalesRecord> records)
    {
        ArgumentNullException.ThrowIfNull(records);

        var list = records.ToList();
        if (list.Any(record => record is null))
        {
            throw new ArgumentException("Records collection must not contain null values.", nameof(records));
        }

        _records = new ReadOnlyCollection<SalesRecord>(list);
    }

    public IReadOnlyCollection<SalesRecord> GetSalesRecords() => _records;
}
