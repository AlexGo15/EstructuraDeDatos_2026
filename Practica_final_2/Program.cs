using System;

class Program
{
    static void Main()
    {
        int[] arr = { 64, 34, 25, 12, 22, 11, 90 };

        Console.WriteLine("Arreglo original:");
        ImprimirArreglo(arr);

        BubbleSort(arr);

        Console.WriteLine("\nArreglo ordenado:");
        ImprimirArreglo(arr);
    }

    static void BubbleSort(int[] arr)
    {
        int n = arr.Length;

        for (int i = 0; i < n - 1; i++)
        {
            bool huboIntercambio = false;

            // j llega como máximo a n - 2 - i,
            // por lo que arr[j + 1] siempre es válido.
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    // Intercambio usando tuplas (C# moderno)
                    (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                    huboIntercambio = true;
                }
            }

            // Si no hubo intercambios, el arreglo ya está ordenado.
            if (!huboIntercambio)
                break;
        }
    }

    static void ImprimirArreglo(int[] arr)
    {
        Console.WriteLine(string.Join(", ", arr));
    }
}