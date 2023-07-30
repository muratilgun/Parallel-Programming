using System.Diagnostics;

namespace StockAnalyzer;

internal class Program
{
    static object _syncRoot = new();
    static void Main(string[] args)
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();

        decimal total = 0;

        // It took : 2970ms to run
        //for (int i = 0; i < 100; i++)
        //{

        //    total += Compute(i);
        //}

        // It took : 500ms to run but the result is not correct
        //Parallel.For(0, 100, (i) =>
        //{
        //    total += Compute(i);
        //});

        // It took : 3270ms to run
        //Parallel.For(0, 100, (i) =>
        //{
        //    lock (_syncRoot)
        //    {
        //        total += Compute(i);

        //    }
        //});

        // It took : 750ms to run
        Parallel.For(0, 100, (i) =>
        {
            var result = Compute(i);
            lock (_syncRoot)
            {
                total += result;

            }
        });


        Console.WriteLine(total);
        Console.WriteLine($"It took: {stopwatch.ElapsedMilliseconds}ms to run");
        Console.ReadLine();
    }
    static Random random = new();
    static decimal Compute(int value)
    {
        var randOmMilliseconds = random.Next(10, 50);
        var end = DateTime.Now + TimeSpan.FromMilliseconds(randOmMilliseconds);

        while (DateTime.Now < end) { }

        return value + 0.5m;
    }
}