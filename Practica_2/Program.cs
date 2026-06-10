using System;

class Program
{
    static void CambiarValor (int  x)
    {
        x = 100;
        Console.WriteLine("Dentro de CambiarValor: " + x);
    }

    static void CambiarReferencia (int[] arr)
    {
        arr[0] = 100;
        Console.WriteLine("Dentro de CambiarReferencia: arr[0]" + arr[0]);
    }

    static void Main()
    {
        int numero = 5;
        int[] arreglo = {1, 2, 3};

        Console.WriteLine("Antes de llamar a las funciones:");
        Console.WriteLine("numero = " + numero);
        Console.WriteLine("arreglo[0] = " + arreglo[0]);

        CambiarValor(numero);
        CambiarReferencia(arreglo);

        Console.WriteLine("\nDespués de llamar a las funciones:");
        Console.WriteLine("numero = " + numero);
        Console.WriteLine("arreglo[0] = " + arreglo[0]);
    }
}