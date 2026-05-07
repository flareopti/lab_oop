using System;

namespace Lab3.App.Models;

public sealed class SalesRecord
{
    public DateOnly Date { get; }
    public string ProductName { get; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }

    public decimal Revenue => Quantity * UnitPrice;

    public SalesRecord(DateOnly date, string productName, int quantity, decimal unitPrice)
    {
        if (date == default)
        {
            throw new ArgumentOutOfRangeException(nameof(date), date, "Date must be specified.");
        }

        if (string.IsNullOrWhiteSpace(productName))
        {
            throw new ArgumentException("Product name must not be empty.", nameof(productName));
        }

        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), quantity, "Quantity must be a positive integer.");
        }

        if (unitPrice < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(unitPrice), unitPrice, "Unit price must be non-negative.");
        }

        Date = date;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}
