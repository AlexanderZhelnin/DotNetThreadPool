using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadPool_Test;

public class MyThreadPool
{
    private readonly ConcurrentQueue<WaitCallback> _waitCallbacks = new();
    private readonly ConcurrentBag<Thread> _threads = new();
    private int _countRun = 0;

    public MyThreadPool()
    {
        for (var i = 0; i < 10; i++)
            CreateThread();
    }

    private void CreateThread()
    {
        var thread = new Thread(() =>
        {
            while (true)
            {
                Interlocked.Increment(ref _countRun);

                while (_waitCallbacks.TryDequeue(out var callback))
                    callback?.Invoke(null);

                Interlocked.Decrement(ref _countRun);

                Thread.Sleep(100);
            }

        });
        thread.Start();

        _threads.Add(thread);
    }

    public void QueueUserWorkItem(WaitCallback callBack)
    {
        _waitCallbacks.Enqueue(callBack);

        if (_countRun >= _threads.Count)
            CreateThread();

    }
}
