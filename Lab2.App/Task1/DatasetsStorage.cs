using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Lab2.App.Task1;

public static class DatasetsStorage
{
    public static IReadOnlyList<int[]> LoadOrCreate(string filePath, int setCount, int numbersPerSet, int minValue, int maxValue, int seed)
    {
        if (File.Exists(filePath))
        {
            return Load(filePath, setCount, numbersPerSet, minValue, maxValue);
        }

        var created = Generate(setCount, numbersPerSet, minValue, maxValue, seed);
        Save(filePath, created);
        return created;
    }

    private static IReadOnlyList<int[]> Load(string filePath, int setCount, int numbersPerSet, int minValue, int maxValue)
    {
        var lines = File.ReadAllLines(filePath);

        if (lines.Length != setCount)
        {
            throw new InvalidDataException($"Expected {setCount} lines (datasets), but found {lines.Length}.");
        }

        var result = new List<int[]>(setCount);

        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i].Trim();
            if (line.Length == 0)
            {
                throw new InvalidDataException($"Line {i + 1} is empty.");
            }

            var parts = line.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length != numbersPerSet)
            {
                throw new InvalidDataException($"Line {i + 1}: expected {numbersPerSet} numbers, but found {parts.Length}.");
            }

            var numbers = new int[numbersPerSet];
            for (var j = 0; j < parts.Length; j++)
            {
                if (!int.TryParse(parts[j], NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
                {
                    throw new InvalidDataException($"Line {i + 1}: invalid integer '{parts[j]}'.");
                }

                if (value < minValue || value > maxValue)
                {
                    throw new InvalidDataException($"Line {i + 1}: value {value} is out of range [{minValue}..{maxValue}].");
                }

                numbers[j] = value;
            }

            result.Add(numbers);
        }

        return result;
    }

    private static IReadOnlyList<int[]> Generate(int setCount, int numbersPerSet, int minValue, int maxValue, int seed)
    {
        var random = new Random(seed);
        var result = new List<int[]>(setCount);

        for (var i = 0; i < setCount; i++)
        {
            var set = new int[numbersPerSet];
            for (var j = 0; j < numbersPerSet; j++)
            {
                set[j] = random.Next(minValue, maxValue + 1);
            }

            result.Add(set);
        }

        return result;
    }

    private static void Save(string filePath, IReadOnlyList<int[]> datasets)
    {
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var lines = datasets
            .Select(set => string.Join(',', set.Select(n => n.ToString(CultureInfo.InvariantCulture))))
            .ToArray();

        File.WriteAllLines(filePath, lines);
    }
}
