using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Lab3.App.DataSources;
using Lab3.App.Exporters;
using Lab3.App.Interfaces;
using Lab3.App.Models;
using Lab3.App.Services;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;

Console.WriteLine("Лабораторная работа №3");
Console.WriteLine("Задание №1 -> вариант №3: Генератор отчётов");

var demoSales = new[]
{
	new SalesRecord(new DateOnly(2026, 5, 1), "Coffee", 2, 3.50m),
	new SalesRecord(new DateOnly(2026, 5, 1), "Tea", 1, 2.00m),
	new SalesRecord(new DateOnly(2026, 5, 2), "Coffee", 1, 3.50m),
	new SalesRecord(new DateOnly(2026, 5, 2), "Cookie", 5, 1.20m),
	new SalesRecord(new DateOnly(2026, 5, 3), "Tea", 3, 2.00m),
};

IReportDataSource dataSource = new InMemorySalesDataSource(demoSales);
IReportBuilder reportBuilder = new SalesSummaryReportBuilder("Отчёт по продажам");
var app = new ReportApplication(dataSource, reportBuilder);

var report = app.CreateReport();

IReportExporter[] exporters =
{
	new CsvReportExporter(),
	new JsonReportExporter(),
};

var requestedFormat = GetRequestedFormat(args);
var selectedExporters = SelectExporters(exporters, requestedFormat);

Console.WriteLine();
Console.WriteLine($"Отчёт сформирован: \"{report.Title}\"");
Console.WriteLine($"Сгенерирован: {report.GeneratedAt:O}");
Console.WriteLine($"Строк: {report.Rows.Count}");
Console.WriteLine(
	$"Итого: {report.TotalQuantity} шт., сумма {report.TotalRevenue.ToString("0.00", CultureInfo.InvariantCulture)}");

foreach (var exporter in selectedExporters)
{
	Console.WriteLine();
	Console.WriteLine($"--- {exporter.Format.ToUpperInvariant()} ---");
	Console.WriteLine(exporter.Export(report));
}

Console.WriteLine();

static string? GetRequestedFormat(string[] args)
{
	if (args.Length == 0)
	{
		return null;
	}

	for (var i = 0; i < args.Length; i++)
	{
		var arg = args[i];

		if (arg.Equals("--format", StringComparison.OrdinalIgnoreCase) ||
			arg.Equals("-f", StringComparison.OrdinalIgnoreCase))
		{
			return (i + 1) < args.Length ? args[i + 1] : null;
		}

		const string longPrefix = "--format=";
		const string shortPrefix = "-f=";

		if (arg.StartsWith(longPrefix, StringComparison.OrdinalIgnoreCase))
		{
			return arg[longPrefix.Length..];
		}

		if (arg.StartsWith(shortPrefix, StringComparison.OrdinalIgnoreCase))
		{
			return arg[shortPrefix.Length..];
		}
	}

	var first = args[0];
	return first.StartsWith('-') ? null : first;
}

static IReportExporter[] SelectExporters(IReportExporter[] exporters, string? requestedFormat)
{
	ArgumentNullException.ThrowIfNull(exporters);

	if (exporters.Length == 0)
	{
		throw new ArgumentException("At least one exporter must be provided.", nameof(exporters));
	}

	if (string.IsNullOrWhiteSpace(requestedFormat))
	{
		return exporters;
	}

	var selected = exporters.FirstOrDefault(exporter =>
		string.Equals(exporter.Format, requestedFormat, StringComparison.OrdinalIgnoreCase));

	if (selected is null)
	{
		Console.WriteLine(
			$"Неизвестный формат '{requestedFormat}'. Доступно: {string.Join(", ", exporters.Select(e => e.Format))}.");
		return exporters;
	}

	return new[] { selected };
}
