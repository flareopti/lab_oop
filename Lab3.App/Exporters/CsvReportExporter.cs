using System;
using System.Globalization;
using System.Text;
using Lab3.App.Interfaces;
using Lab3.App.Models;

namespace Lab3.App.Exporters;

public sealed class CsvReportExporter : IReportExporter
{
    public string Format => "csv";

    public string Export(Report report)
    {
        ArgumentNullException.ThrowIfNull(report);

        var sb = new StringBuilder();

        sb.AppendLine($"Title,{Escape(report.Title)}");
        sb.AppendLine($"GeneratedAt,{Escape(report.GeneratedAt.ToString("O", CultureInfo.InvariantCulture))}");
        sb.AppendLine();

        sb.AppendLine("Product,Quantity,Revenue");

        foreach (var row in report.Rows)
        {
            sb.Append(Escape(row.ProductName)).Append(',')
                .Append(row.TotalQuantity.ToString(CultureInfo.InvariantCulture)).Append(',')
                .Append(row.TotalRevenue.ToString("0.00", CultureInfo.InvariantCulture)).AppendLine();
        }

        sb.Append("TOTAL,")
            .Append(report.TotalQuantity.ToString(CultureInfo.InvariantCulture))
            .Append(',')
            .Append(report.TotalRevenue.ToString("0.00", CultureInfo.InvariantCulture))
            .AppendLine();

        return sb.ToString();
    }

    private static string Escape(string field)
    {
        if (field.Length == 0)
        {
            return field;
        }

        var needsQuotes =
            field.Contains(',') ||
            field.Contains('"') ||
            field.Contains('\n') ||
            field.Contains('\r');

        if (!needsQuotes)
        {
            return field;
        }

        return "\"" + field.Replace("\"", "\"\"") + "\"";
    }
}
