using System;
using System.Threading;

namespace Lab2.App.Task1;

public interface ISharedCounter : IDisposable
{
    void Increment();
    int Value { get; }
}

public sealed class LockCounter : ISharedCounter
{
    private readonly object _locker = new();
    private int _value;

    public void Increment()
    {
        lock (_locker)
        {
            _value++;
        }
    }

    public int Value
    {
        get
        {
            lock (_locker)
            {
                return _value;
            }
        }
    }

    public void Dispose()
    {
    }
}

public sealed class MutexCounter : ISharedCounter
{
    private readonly Mutex _mutex = new();
    private int _value;

    public void Increment()
    {
        _mutex.WaitOne();
        try
        {
            _value++;
        }
        finally
        {
            _mutex.ReleaseMutex();
        }
    }

    public int Value
    {
        get
        {
            _mutex.WaitOne();
            try
            {
                return _value;
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }
    }

    public void Dispose() => _mutex.Dispose();
}

public sealed class SemaphoreCounter : ISharedCounter
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private int _value;

    public void Increment()
    {
        _semaphore.Wait();
        try
        {
            _value++;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public int Value
    {
        get
        {
            _semaphore.Wait();
            try
            {
                return _value;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }

    public void Dispose() => _semaphore.Dispose();
}
