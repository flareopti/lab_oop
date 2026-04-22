using System;
using System.Collections.Generic;

namespace Lab2.App.Task1;

public static class RangePartitioner
{
    public static IEnumerable<(int Start, int End)> SplitInclusiveRange(int start, int end, int parts)
    {
        if (parts <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(parts), parts, "Parts must be positive.");
        }

        if (start > end)
        {
            throw new ArgumentOutOfRangeException(nameof(start), start, "Start must be <= end.");
        }

        var total = end - start + 1;
        var baseSize = total / parts;
        var remainder = total % parts;

        var current = start;
        for (var i = 0; i < parts; i++)
        {
            var size = baseSize + (i < remainder ? 1 : 0);
            var rangeStart = current;
            var rangeEnd = current + size - 1;

            yield return (rangeStart, rangeEnd);

            current = rangeEnd + 1;
        }
    }
}
