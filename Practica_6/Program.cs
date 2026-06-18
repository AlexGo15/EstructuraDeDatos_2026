using System;

class Program
{
    static void Main()
    {
        // 1. Intercambio usando ref
        int a = 10;
        int b = 20;

        Console.WriteLine($"Antes: a = {a}, b = {b}");
        Intercambiar(ref a, ref b);
        Console.WriteLine($"Después: a = {a}, b = {b}");
        Console.WriteLine();

        // 2. Uso de out para devolver múltiples resultados
        int dividendo = 17;
        int divisor = 5;
        int cociente = Dividir(dividendo, divisor, out int residuo);

        Console.WriteLine($"{dividendo} / {divisor}");
        Console.WriteLine($"Cociente: {cociente}");
        Console.WriteLine($"Residuo: {residuo}");
        Console.WriteLine();

        // 3. Referencias de objetos
        Alumno alumno1 = new Alumno { Nombre = "Dany" };
        Alumno alumno2 = alumno1;

        alumno2.Nombre = "3Treum";

        Console.WriteLine($"alumno1.Nombre = {alumno1.Nombre}");
        Console.WriteLine($"alumno2.Nombre = {alumno2.Nombre}");
    }

    static void Intercambiar(ref int x, ref int y)
    {
        int temp = x;
        x = y;
        y = temp;
    }

    static int Dividir(int dividendo, int divisor, out int residuo)
    {
        residuo = dividendo % divisor;
        return dividendo / divisor;
    }
}

class Alumno
{
    public string Nombre { get; set; } = string.Empty;
}