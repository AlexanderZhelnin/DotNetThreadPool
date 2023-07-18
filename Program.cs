
using ThreadPool_Test;

async Task Test1()
{
    //
    // тут какой-то код
    //

    await Test2();

    //
    // тут какой-то код
    //

    await Test2();

    //
    // тут какой-то код
    //
}

async Task Test2()
{
    //
    // тут какой-то код
    //
}

//new Task(() =>
//{
//    //
//    // тут какой-то код
//    //
//}).Start();


var thPool = new MyThreadPool();
for (var i = 0; i < 100; i++)
{
    var ii = i;
    thPool.QueueUserWorkItem(_ =>
    {
        for (var j = 0; j < 10; j++)
            Console.WriteLine($"{ii}_{j}");
    });
}

MyTask.Run(_ =>
    {
        Thread.Sleep(1000);
        return 1;
    })
    .ContinueWith(r =>
    {
        Console.WriteLine(r);
        return 2;
    });

//Test1();
//Test1();

//Console.ReadLine();

