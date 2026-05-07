using System;
using System.Globalization;
using System.Net;
using System.Text;
using Lab3.App.Interfaces;
using Lab3.App.Models;

namespace Lab3.App.Exporters;

public sealed class HtmlReportExporter : IReportExporter
{
    public string Format => "html";

    public string Export(Report report)
    {
        ArgumentNullException.ThrowIfNull(report);

        var sb = new StringBuilder();

        sb.AppendLine("<!doctype html>");
        sb.AppendLine("<html lang=\"ru\">");
        sb.AppendLine("<head>");
        sb.AppendLine("  <meta charset=\"utf-8\" />");
        sb.AppendLine($"  <title>{Html(report.Title)}</title>");
        sb.AppendLine("</head>");
        sb.AppendLine("<body>");

        sb.AppendLine($"  <h1>{Html(report.Title)}</h1>");
        sb.AppendLine(
            $"  <div>GeneratedAt: <time datetime=\"{Html(report.GeneratedAt.ToString("O", CultureInfo.InvariantCulture))}\">{Html(report.GeneratedAt.ToString("O", CultureInfo.InvariantCulture))}</time></div>");

        sb.AppendLine("  <hr />");

        sb.AppendLine("  <table>");
        sb.AppendLine("    <thead>");
        sb.AppendLine("      <tr><th>Product</th><th>Quantity</th><th>Revenue</th></tr>");
        sb.AppendLine("    </thead>");
        sb.AppendLine("    <tbody>");

        foreach (var row in report.Rows)
        {
            sb.Append("      <tr>")
                .Append($"<td>{Html(row.ProductName)}</td>")
                .Append($"<td>{row.TotalQuantity.ToString(CultureInfo.InvariantCulture)}</td>")
                .Append($"<td>{row.TotalRevenue.ToString("0.00", CultureInfo.InvariantCulture)}</td>")
                .AppendLine("</tr>");
        }

        sb.AppendLine("    </tbody>");
        sb.AppendLine("    <tfoot>");
        sb.Append("      <tr>")
            .Append("<td><strong>TOTAL</strong></td>")
            .Append($"<td><strong>{report.TotalQuantity.ToString(CultureInfo.InvariantCulture)}</strong></td>")
            .Append($"<td><strong>{report.TotalRevenue.ToString("0.00", CultureInfo.InvariantCulture)}</strong></td>")
            .AppendLine("</tr>");
        sb.AppendLine("    </tfoot>");
        sb.AppendLine("  </table>");

        sb.AppendLine("</body>");
        sb.AppendLine("</html>");

        return sb.ToString();
    }

    private static string Html(string value) => WebUtility.HtmlEncode(value);
}
