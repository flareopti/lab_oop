using System;

namespace Lab3.App.Models;

public sealed class ReportRow
{
    public string ProductName { get; }
    public int TotalQuantity { get; }
    public decimal TotalRevenue { get; }

    public ReportRow(string productName, int totalQuantity, decimal totalRevenue)
    {
        if (string.IsNullOrWhiteSpace(productName))
        {
            throw new ArgumentException("Product name must not be empty.", nameof(productName));
        }

        if (totalQuantity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalQuantity), totalQuantity, "Total quantity must be non-negative.");
        }

        if (totalRevenue < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalRevenue), totalRevenue, "Total revenue must be non-negative.");
        }

        ProductName = productName;
        TotalQuantity = totalQuantity;
        TotalRevenue = totalRevenue;
    }
}
