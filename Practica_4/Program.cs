using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("___Algoritmos Recursivos___");

        Console.Write("Ingresa un número para calcular su factorial: ");
        if (int.TryParse(Console.ReadLine(), out int numFactorial))
        {
            try
            {
                long resultado = CalcularFactorial(numFactorial);
                Console.WriteLine($"{numFactorial}! = {resultado}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Entrada inválida.");
        }

        Console.Write("\nIngresa una posición de Fibonacci: ");
        if (int.TryParse(Console.ReadLine(), out int numFibonacci))
        {
            try
            {
                long fib = GenerarFibonacci(numFibonacci);
                Console.WriteLine($"Fibonacci({numFibonacci}) = {fib}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Entrada inválida.");
        }
    }

    static long CalcularFactorial(int n)
    {
        if (n < 0)
            throw new ArgumentException("No existe factorial para números negativos.");

        // Casos base
        if (n == 0 || n == 1)
            return 1;

        return n * CalcularFactorial(n - 1);
    }

    static long GenerarFibonacci(int n)
    {
        if (n < 0)
            throw new ArgumentException("La posición de Fibonacci no puede ser negativa.");

        // Casos base
        if (n == 0)
            return 0;

        if (n == 1)
            return 1;

        return GenerarFibonacci(n - 1) + GenerarFibonacci(n - 2);
    }
}