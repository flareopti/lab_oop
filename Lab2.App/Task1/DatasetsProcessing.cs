using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace Lab2.App.Task1;

public static class DatasetsProcessing
{
    public static void Run(string datasetsFilePath, int maxConcurrentThreads)
    {
        Console.WriteLine();
        Console.WriteLine("=== ЛР2 / Задание 1.2: Обработка 15 наборов чисел с ограничением потоков ===");
        Console.WriteLine($"Файл наборов: {Path.GetFullPath(datasetsFilePath)}");
        Console.WriteLine($"Макс. одновременно работающих потоков: {maxConcurrentThreads}");

        const int setCount = 15;
        const int numbersPerSet = 100;
        const int minValue = 1;
        const int maxValue = 100;
        const int seed = 20260423;

        var datasets = DatasetsStorage.LoadOrCreate(datasetsFilePath, setCount, numbersPerSet, minValue, maxValue, seed);

        var semaphore = new SemaphoreSlim(maxConcurrentThreads, maxConcurrentThreads);
        using var mutex = new Mutex();

        var journalLock = new object();
        var journal = new List<DatasetResult>(setCount);

        long grandTotal = 0;

        var stopwatch = Stopwatch.StartNew();

        var threads = new Thread[setCount];

        for (var i = 0; i < setCount; i++)
        {
            var datasetIndex = i;
            threads[i] = new Thread(() => ProcessOneDataset(datasetIndex, datasets[datasetIndex], semaphore, mutex, journalLock, journal, ref grandTotal))
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
        semaphore.Dispose();

        Console.WriteLine();
        Console.WriteLine("Результаты по наборам:");

        foreach (var item in journal.OrderBy(r => r.DatasetNumber))
        {
            Console.WriteLine($"Набор {item.DatasetNumber}: сумма={item.Sum}, поток={item.ThreadId}");
        }

        Console.WriteLine();
        Console.WriteLine($"Общий итог по всем наборам: {grandTotal}");
        Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} ms");
    }

    private static void ProcessOneDataset(
        int datasetIndex,
        int[] numbers,
        SemaphoreSlim semaphore,
        Mutex mutex,
        object journalLock,
        List<DatasetResult> journal,
        ref long grandTotal)
    {
        semaphore.Wait();
        try
        {
            var sum = numbers.Sum(n => (long)n);
            var threadId = Thread.CurrentThread.ManagedThreadId;

            lock (journalLock)
            {
                journal.Add(new DatasetResult(datasetIndex + 1, sum, threadId));
            }

            mutex.WaitOne();
            try
            {
                grandTotal += sum;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
        finally
        {
            semaphore.Release();
        }
    }

    private readonly record struct DatasetResult(int DatasetNumber, long Sum, int ThreadId);
}
