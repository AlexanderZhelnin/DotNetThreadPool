namespace ThreadPool_Test;

public class MyTask
{
    protected readonly Func<object?, object?> _callback;

    private static readonly MyThreadPool _pool = new();
    
    private bool _isRun = false;
    private object? _result;
    private MyTask? _myTaskToContinue;

    public MyTask(Func<object?, object?> callback)
    {
        _callback = callback;
    }

    public static MyTask Run(Func<object?, object?> callback)
    {
        var result = new MyTask(callback);
        result.Run(null);
        return result;
    }

    protected void Run(object? arg)
    {
        if (_isRun) return;
        _isRun = true;

        _pool.QueueUserWorkItem(_ =>
        {
            _result = _callback(arg);
            _myTaskToContinue?.Run(_result);
        });
    }

    public MyTask ContinueWith(Func<object?, object?> callback)
    {
        _myTaskToContinue = new MyTask(callback);

        if (_result is not null)
            _myTaskToContinue.Run(_result);

        return _myTaskToContinue;
    }
}
