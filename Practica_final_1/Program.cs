using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

class Program
{
    static void Main()
    {
        const int cantidad = 100000;

        Console.WriteLine("=== Motor de búsqueda de matrículas ===\n");

        // Generar matrículas
        List<string> matriculas = GenerarMatriculas(cantidad);

        Console.WriteLine($"Se generaron {matriculas.Count:N0} matrículas.");

        // Ordenar para búsqueda binaria
        matriculas.Sort();

        Console.Write("\nIngrese la matrícula que desea buscar (ejemplo: ABC-1234): ");
        string? objetivo = Console.ReadLine()?.Trim().ToUpper();

        if (string.IsNullOrWhiteSpace(objetivo))
        {
            Console.WriteLine("Entrada no válida.");
            return;
        }

        // Búsqueda lineal
        Stopwatch cronometro = Stopwatch.StartNew();
        bool encontradaLineal = BusquedaLineal(matriculas, objetivo);
        cronometro.Stop();

        Console.WriteLine("\n--- Búsqueda Lineal ---");
        Console.WriteLine(encontradaLineal
            ? "Matrícula encontrada."
            : "Matrícula no encontrada.");

        Console.WriteLine($"Tiempo: {cronometro.ElapsedMilliseconds} ms");

        // Búsqueda binaria
        cronometro.Restart();
        bool encontradaBinaria = BusquedaBinaria(matriculas, objetivo);
        cronometro.Stop();

        Console.WriteLine("\n--- Búsqueda Binaria ---");
        Console.WriteLine(encontradaBinaria
            ? "Matrícula encontrada."
            : "Matrícula no encontrada.");

        Console.WriteLine($"Tiempo: {cronometro.ElapsedMilliseconds} ms");
    }

    static List<string> GenerarMatriculas(int cantidad)
    {
        HashSet<string> conjunto = new();

        while (conjunto.Count < cantidad)
        {
            conjunto.Add(CrearMatricula());
        }

        return conjunto.ToList();
    }

    static string CrearMatricula()
    {
        string letras = new(
            Enumerable.Range(0, 3)
                      .Select(_ => (char)Random.Shared.Next('A', 'Z' + 1))
                      .ToArray());

        int numeros = Random.Shared.Next(1000, 10000);

        return $"{letras}-{numeros}";
    }

    static bool BusquedaLineal(List<string> lista, string objetivo)
    {
        foreach (string matricula in lista)
        {
            if (matricula == objetivo)
                return true;
        }

        return false;
    }

    static bool BusquedaBinaria(List<string> lista, string objetivo)
    {
        int izquierda = 0;
        int derecha = lista.Count - 1;

        while (izquierda <= derecha)
        {
            int centro = (izquierda + derecha) / 2;

            int comparacion = string.Compare(
                lista[centro],
                objetivo,
                StringComparison.Ordinal);

            if (comparacion == 0)
                return true;

            if (comparacion < 0)
                izquierda = centro + 1;
            else
                derecha = centro - 1;
        }

        return false;
    }
}