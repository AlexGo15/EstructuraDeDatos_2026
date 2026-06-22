using System;
using System.Diagnostics;

public static class FibonacciAlgorithms
{
    // 1. Fibonacci recursivo tradicional
    public static long Recursive(int n)
    {
        if (n <= 1)
            return n;

        return Recursive(n - 1) + Recursive(n - 2);
    }

    // 2. Fibonacci con Memoization
    public static long Memoized(int n)
    {
        long[] cache = new long[n + 1];
        Array.Fill(cache, -1);

        return MemoizedInternal(n, cache);
    }

    private static long MemoizedInternal(int n, long[] cache)
    {
        if (n <= 1)
            return n;

        if (cache[n] != -1)
            return cache[n];

        cache[n] = MemoizedInternal(n - 1, cache)
                 + MemoizedInternal(n - 2, cache);

        return cache[n];
    }
}

    // 3. Banco de pruebas con Stopwatch
public static class Benchmark
{
    public static void Run(int n)
    {
        Stopwatch sw = new();

        sw.Start();
        long recursiveResult = FibonacciAlgorithms.Recursive(n);
        sw.Stop();

        Console.WriteLine("\n=== Fibonacci Recursivo ===");
        Console.WriteLine($"Resultado: {recursiveResult}");
        Console.WriteLine($"Tiempo: {sw.ElapsedMilliseconds} ms");

        sw.Restart();
        long memoizedResult = FibonacciAlgorithms.Memoized(n);
        sw.Stop();

        Console.WriteLine("\n=== Fibonacci Memoization ===");
        Console.WriteLine($"Resultado: {memoizedResult}");
        Console.WriteLine($"Tiempo: {sw.ElapsedMilliseconds} ms");
    }
}

// Impresión de resultados
// Otorgar un valor a n manulamente
public class Program
{
    public static void Main()
    {
        int n = ReadN();

        Console.WriteLine($"\nCalculando Fibonacci({n})...");
        Benchmark.Run(n);
    }

    private static int ReadN()
    {
        while (true)
        {
            Console.Write("Introduce el valor de n: ");

            string? input = Console.ReadLine();

            if (int.TryParse(input, out int n) && n >= 0)
            {
                return n;
            }

            Console.WriteLine("Error: introduce un número entero mayor o igual a 0.\n");
        }
    }
}