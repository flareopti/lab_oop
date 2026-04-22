using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Lab1.App.Task3;

public sealed class TreeNode
{
    private readonly List<TreeNode> _children = new();

    public string Value { get; }
    public IReadOnlyList<TreeNode> Children => new ReadOnlyCollection<TreeNode>(_children);

    public TreeNode(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public void AddChild(TreeNode child)
    {
        ArgumentNullException.ThrowIfNull(child);

        if (ReferenceEquals(child, this))
        {
            throw new ArgumentException("A node cannot be a child of itself.", nameof(child));
        }

        _children.Add(child);
    }

    public void TraverseDepthFirst(Action<TreeNode, int> action, int depth = 0)
    {
        ArgumentNullException.ThrowIfNull(action);

        action(this, depth);

        foreach (var child in _children)
        {
            child.TraverseDepthFirst(action, depth + 1);
        }
    }

    public int CountSubtreeNodes()
    {
        var total = 1; // include this node

        foreach (var child in _children)
        {
            total += child.CountSubtreeNodes();
        }

        return total;
    }

    public int CountDescendants() => CountSubtreeNodes() - 1;
}
