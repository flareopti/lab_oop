using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Lab1.App.Task2;

public sealed class Document
{
    public IReadOnlyList<IDocumentElement> Elements { get; }

    public Document(IEnumerable<IDocumentElement> elements)
    {
        ArgumentNullException.ThrowIfNull(elements);

        var list = elements.ToList();
        if (list.Any(e => e is null))
        {
            throw new ArgumentException("Document elements must not contain null", nameof(elements));
        }

        Elements = new ReadOnlyCollection<IDocumentElement>(list);
    }

    public string Export(IDocumentExportVisitor visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);

        foreach (var element in Elements)
        {
            element.Accept(visitor);
        }

        return visitor.GetOutput();
    }
}
