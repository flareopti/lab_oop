using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Lab1.App.Task2;

public sealed class Document
{
    private readonly List<IDocumentElement> _elements = new();

    public IReadOnlyList<IDocumentElement> Elements => new ReadOnlyCollection<IDocumentElement>(_elements);

    public Document()
    {
    }

    public Document(IEnumerable<IDocumentElement> elements)
    {
        ArgumentNullException.ThrowIfNull(elements);

        foreach (var element in elements)
        {
            Add(element);
        }
    }

    public void Add(IDocumentElement element)
    {
        ArgumentNullException.ThrowIfNull(element);
        _elements.Add(element);
    }

    public void Export(IDocumentVisitor visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);

        foreach (var element in _elements)
        {
            element.Accept(visitor);
        }
    }
}
