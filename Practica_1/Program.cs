using System;
class Program
{
    static void Main()
    {
        double numero = LeerDoublePositivo();
        Console.WriteLine($"Número válido ingresado: {numero}");
    }

    static double LeerDoublePositivo()
    {
        double valor;
        do
        {
            Console.Write("Ingrese un número decimal positivo: ");
            string? entrada = Console.ReadLine();

            if(double.TryParse(entrada, out valor) && valor >0)
            {
                return valor;
            }
            Console.WriteLine("Error: favor de ingresar un número decimal positivo válido.\n");
        }
        while (true);
    }
}