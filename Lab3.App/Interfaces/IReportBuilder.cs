using System.Collections.Generic;
using Lab3.App.Models;

namespace Lab3.App.Interfaces;

public interface IReportBuilder
{
    Report Build(IReadOnlyCollection<SalesRecord> records);
}
