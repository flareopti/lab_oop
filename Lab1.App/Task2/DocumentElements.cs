using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Lab1.App.Task2;

public interface IDocumentElement
{
    void Accept(IDocumentVisitor visitor);
}

public sealed class Paragraph : IDocumentElement
{
    public string Text { get; }

    public Paragraph(string text)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
    }

    public void Accept(IDocumentVisitor visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        visitor.VisitParagraph(this);
    }
}

public sealed class Image : IDocumentElement
{
    public string Url { get; }
    public string? AltText { get; }

    public Image(string url, string? altText = null)
    {
        Url = url ?? throw new ArgumentNullException(nameof(url));
        AltText = altText;
    }

    public void Accept(IDocumentVisitor visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        visitor.VisitImage(this);
    }
}

public sealed class Table : IDocumentElement
{
    public IReadOnlyList<IReadOnlyList<string>> Rows { get; }

    public Table(IEnumerable<IEnumerable<string>> rows)
    {
        ArgumentNullException.ThrowIfNull(rows);

        var materialized = rows
            .Select(r => r?.Select(cell => cell ?? string.Empty).ToList() ?? throw new ArgumentException("Row must not be null", nameof(rows)))
            .Select(r => (IReadOnlyList<string>)new ReadOnlyCollection<string>(r))
            .ToList();

        Rows = new ReadOnlyCollection<IReadOnlyList<string>>(materialized);
    }

    public void Accept(IDocumentVisitor visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        visitor.VisitTable(this);
    }
}
