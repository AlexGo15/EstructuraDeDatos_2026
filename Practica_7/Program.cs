using System;
namespace RecursividadDemo
{
    internal class Program
    {
        static void Main()
        {
            // En esta "sección", va almacenda toda la parte técinca
            try
            {
                // 1. Primera parte del ejercicio: Cuenta Regresiva
                // En esta primera parte, únicamente guarda el valor del que comenzará la cuenta regresiva
                Console.WriteLine("=== DEMOSTRACIÓN DE PILA DE LLAMADAS (LIFO) ===\n");
                ImprimirCuentaRegresiva(3);

                // 2. Segunda parte: Suma Acumulativa Recursiva
                // Le otorgamos un número válido a la consola para que pueda hacer la operación
                Console.WriteLine("\n=== SUMA ACUMULATIVA RECURSIVA ===\n");
                Console.Write("Ingrese un número entero positivo: ");
                string? entrada = Console.ReadLine();

                // Estos dos "if" son en caso de haber ingresado un valor inválido (negativos, decimáles...)
                if (!int.TryParse(entrada, out int numero))
                {
                    Console.WriteLine("Error: Debe ingresar un número entero válido.");
                    return;
                }
                if (numero < 1)
                {
                    Console.WriteLine("Error: El número debe ser mayor o igual a 1.");
                    return;
                }
                // Con este, imprime el resultado de la suma acumulativa
                int resultado = SumarHasta(numero);
                Console.WriteLine($"\nLa suma de 1 hasta {numero} es: {resultado}");
            }

            // Comandos para excpeciones
            catch (OverflowException ex)
            {
                Console.WriteLine($"Error de desbordamiento numérico: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se produjo un error inesperado: {ex.Message}");
            }
        }

        // 1. Este códgio es el que imprime la cuenta regresiva, pero simulando una liberación de memoria
        static void ImprimirCuentaRegresiva(int numero)
        {
            Console.WriteLine($"Apilando -> {numero}");
            if (numero <= 0)
            {
                Console.WriteLine("<_Caso base alcanzado_>");
                return;
            }
            ImprimirCuentaRegresiva(numero - 1);
            Console.WriteLine($"Liberando -> {numero}");
            if (numero >= 3)
            {
                Console.WriteLine("<_DESPEGUE_>");
            }
        }

        // 2. Y por último, la suma acumulativa con caso base para que se detenga en '1'
        static int SumarHasta(int n)
        {
            checked
            {
                if (n == 1)
                {
                    return 1;
                }

                return n + SumarHasta(n - 1);
            }
        }
    }
}