using System;
using System.Numerics;

class Program
{
    static void Main()
    {
        int n = 5;

        Console.WriteLine($"Factorial recursivo de {n}: {FactorialInt(n)}");
        Console.WriteLine($"Factorial iterativo de {n}: {FactorialIterativo(n)}");

        BigInteger nGrande = 100;
        Console.WriteLine($"\nFactorial profesional de {nGrande}:");
        Console.WriteLine(FactorialProfesional(nGrande));
    }

    /// Factorial recursivo usando int.
    /// Adecuado para valores pequeños.
    static int FactorialInt(int n)
    {
        if (n < 0)
            throw new ArgumentException("El número no puede ser negativo.");

        if (n == 0 || n == 1)
            return 1;

        // Punto de quiebre:
        // Coloca aquí un punto de quiebre para observar
        // cómo se generan las llamadas recursivas.
        return n * FactorialInt(n - 1);
    }

    /// Factorial iterativo usando int.
    static int FactorialIterativo(int n)
    {
        if (n < 0)
            throw new ArgumentException("El número no puede ser negativo.");

        int resultado = 1;

        for (int i = 2; i <= n; i++)
        {
            //  Punto de quiebre:
            // Coloca aquí un punto de quiebre para observar
            // cómo cambia el valor de resultado.
            resultado *= i;
        }

        return resultado;
    }

    /// Factorial para números muy grandes.
    /// Utiliza BigInteger.
    static BigInteger FactorialProfesional(BigInteger n)
    {
        if (n < 0)
            throw new ArgumentException("El número no puede ser negativo.");

        BigInteger resultado = 1;

        for (BigInteger i = 2; i <= n; i++)
        {
            resultado *= i;
        }

        return resultado;
    }
}