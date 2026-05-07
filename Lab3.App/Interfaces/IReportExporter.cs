using Lab3.App.Models;

namespace Lab3.App.Interfaces;

public interface IReportExporter
{
    string Format { get; }

    string Export(Report report);
}
