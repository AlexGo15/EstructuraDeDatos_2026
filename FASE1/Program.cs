using System;

namespace ProyectoFinalEstructuraDatos
{
    public struct RegistroDatos
    {
        private int id;
        private string nombre;
        private double promedio;

        public int Id
        {
            get => id;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El ID debe ser mayor que cero.");
                id = value;
            }
        }

        public string Nombre
        {
            get => nombre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre no puede estar vacío.");
                nombre = value;
            }
        }

        public double Promedio
        {
            get => promedio;
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("El promedio debe estar entre 0 y 100.");
                promedio = value;
            }
        }

        public RegistroDatos(int id, string nombre, double promedio)
        {
            this.id = 0;
            this.nombre = "";
            this.promedio = 0;

            Id = id;
            Nombre = nombre;
            Promedio = promedio;
        }

        public override string ToString()
        {
            return $"{Id,2} | {Nombre,-12} | {Promedio,6:F2}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                const int TOTAL = 40;

                RegistroDatos[] registros = new RegistroDatos[TOTAL];

                Random rnd = new Random();

                for (int i = 0; i < TOTAL; i++)
                {
                    registros[i] = new RegistroDatos(
                        i + 1,
                        $"Alumno{i + 1}",
                        Math.Round(rnd.NextDouble() * 100, 2));
                }

                Console.WriteLine("==============================================");
                Console.WriteLine("REGISTROS ANTES DEL ORDENAMIENTO");
                Console.WriteLine("==============================================");

                Imprimir(registros);

                OrdenarPorSeleccion(
                    registros,
                    out int comparaciones,
                    out int intercambios);

                Console.WriteLine();
                Console.WriteLine("==============================================");
                Console.WriteLine("REGISTROS DESPUÉS DEL ORDENAMIENTO");
                Console.WriteLine("==============================================");

                Imprimir(registros);

                Console.WriteLine();
                Console.WriteLine("==============================================");
                Console.WriteLine($"Comparaciones : {comparaciones}");
                Console.WriteLine($"Intercambios  : {intercambios}");
                Console.WriteLine("==============================================");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error de validación:");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado:");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Presione una tecla para finalizar...");
            Console.ReadKey();
        }

        static void OrdenarPorSeleccion(
            RegistroDatos[] datos,
            out int comparaciones,
            out int intercambios)
        {
            comparaciones = 0;
            intercambios = 0;

            for (int i = 0; i < datos.Length - 1; i++)
            {
                int indiceMenor = i;

                for (int j = i + 1; j < datos.Length; j++)
                {
                    comparaciones++;

                    if (datos[j].Promedio < datos[indiceMenor].Promedio)
                    {
                        indiceMenor = j;
                    }
                }

                if (indiceMenor != i)
                {
                    // Intercambio usando tuplas modernas
                    (datos[i], datos[indiceMenor]) =
                        (datos[indiceMenor], datos[i]);

                    intercambios++;
                }
            }
        }

        static void Imprimir(RegistroDatos[] datos)
        {
            Console.WriteLine("ID | Nombre       | Prom.");
            Console.WriteLine("-------------------------------");

            foreach (RegistroDatos r in datos)
            {
                Console.WriteLine(r);
            }
        }
    }
}