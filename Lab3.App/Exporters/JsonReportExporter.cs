using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using Lab3.App.Interfaces;
using Lab3.App.Models;

namespace Lab3.App.Exporters;

public sealed class JsonReportExporter : IReportExporter
{
    public string Format => "json";

    public string Export(Report report)
    {
        ArgumentNullException.ThrowIfNull(report);

        return JsonSerializer.Serialize(
            report,
            new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            });
    }
}
