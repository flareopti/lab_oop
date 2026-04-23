using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Lab2.App.Task1;

public static class PrimeCounting
{
    public static void RunAllVersions(int threadCount)
    {
        Console.WriteLine("=== ЛР2 / Задание 1.1: Подсчёт простых чисел (1..10000) ===");
        Console.WriteLine($"Потоков: {threadCount}");

        var ranges = RangePartitioner.SplitInclusiveRange(1, 10_000, threadCount).ToArray();
        Console.WriteLine("Диапазоны для потоков:");
        for (var i = 0; i < ranges.Length; i++)        {
            Console.WriteLine($"Поток {i + 1}: {ranges[i].Start}..{ranges[i].End}");
        }   

        RunVersion("Версия 1: Monitor/lock", ranges, () => new LockCounter());
        RunVersion("Версия 2: Mutex", ranges, () => new MutexCounter());
        RunVersion("Версия 3: SemaphoreSlim(1)", ranges, () => new SemaphoreCounter());
    }

    private static void RunVersion(string title, (int Start, int End)[] ranges, Func<ISharedCounter> counterFactory)
    {
        Console.WriteLine();
        Console.WriteLine(title);

        using var counter = counterFactory();

        var stopwatch = Stopwatch.StartNew();

        var threads = new Thread[ranges.Length];

        for (var i = 0; i < ranges.Length; i++)
        {
            var threadIndex = i + 1;
            var localRange = ranges[i];

            threads[i] = new Thread(() => Worker(threadIndex, localRange.Start, localRange.End, counter))
            {
                IsBackground = false,
            };
        }

        foreach (var thread in threads)
        {
            thread.Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        stopwatch.Stop();

        Console.WriteLine($"Итого простых чисел: {counter.Value}");
        Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} ms");
    }

    private static void Worker(int threadIndex, int start, int end, ISharedCounter counter)
    {
        for (var number = start; number <= end; number++)
        {
            Console.WriteLine($"Поток {threadIndex}: {number}");

            if (PrimeMath.IsPrime(number))
            {
                counter.Increment();
            }
        }
    }
}
