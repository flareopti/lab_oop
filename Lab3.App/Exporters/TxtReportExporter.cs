using System;
using System.Globalization;
using System.Text;
using Lab3.App.Interfaces;
using Lab3.App.Models;

namespace Lab3.App.Exporters;

public sealed class TxtReportExporter : IReportExporter
{
    public string Format => "txt";

    public string Export(Report report)
    {
        ArgumentNullException.ThrowIfNull(report);

        var sb = new StringBuilder();

        sb.AppendLine(report.Title);
        sb.AppendLine($"GeneratedAt: {report.GeneratedAt.ToString("O", CultureInfo.InvariantCulture)}");
        sb.AppendLine();

        foreach (var row in report.Rows)
        {
            sb.Append("- ")
                .Append(row.ProductName)
                .Append(": ")
                .Append(row.TotalQuantity.ToString(CultureInfo.InvariantCulture))
                .Append(" pcs, ")
                .Append(row.TotalRevenue.ToString("0.00", CultureInfo.InvariantCulture))
                .AppendLine();
        }

        sb.AppendLine();
        sb.Append("TOTAL: ")
            .Append(report.TotalQuantity.ToString(CultureInfo.InvariantCulture))
            .Append(" pcs, ")
            .Append(report.TotalRevenue.ToString("0.00", CultureInfo.InvariantCulture))
            .AppendLine();

        return sb.ToString();
    }
}
