using System;

struct Estudiante
{
    public string Nombre;
    public int Edad;

    public Estudiante(string nombre, int edad)
    {
        Nombre = nombre;
        Edad = edad;
    }
}

class Program
{
    static void Main()
    {
        Estudiante[] estudiantes =
        {
            new Estudiante("Luis", 20),
            new Estudiante("Ana", 18),
            new Estudiante("Carlos", 22),
            new Estudiante("María", 19)
        };

        Console.WriteLine("Arreglo original:");
        Mostrar(estudiantes);

        InsertionSort(estudiantes);

        Console.WriteLine("\nArreglo ordenado:");
        Mostrar(estudiantes);
    }

    static void InsertionSort(Estudiante[] arreglo)
    {
        for (int i = 1; i < arreglo.Length; i++)
        {
            // Se guarda temporalmente el elemento que se desea insertar.
            Estudiante clave = arreglo[i];

            // j comienza una posición antes de la clave.
            int j = i - 1;

            // Mientras existan elementos a la izquierda y sean mayores,
            // se desplazan una posición hacia la derecha.
            while (j >= 0 && arreglo[j].Edad > clave.Edad)
            {
                arreglo[j + 1] = arreglo[j];
                j--;
            }

            /*
             * Al salir del while pueden ocurrir dos casos:
             *
             * 1. j == -1
             *    Significa que la clave es el elemento más pequeño.
             *    Debe colocarse en la posición 0.
             *
             * 2. arreglo[j].Edad <= clave.Edad
             *    Significa que j quedó apuntando al último elemento
             *    menor o igual que la clave.
             *
             * En ambos casos la posición correcta es j + 1.
             *
             * Si se usara arreglo[j] en lugar de arreglo[j + 1]:
             * - Cuando j == -1 se intentaría acceder a arreglo[-1],
             *   provocando una excepción.
             * - En los demás casos se sobrescribiría un elemento que
             *   debe permanecer en su posición.
             */

            arreglo[j + 1] = clave;
        }
    }

    static void Mostrar(Estudiante[] arreglo)
    {
        foreach (Estudiante estudiante in arreglo)
        {
            Console.WriteLine($"{estudiante.Nombre,-8} Edad: {estudiante.Edad}");
        }
    }
}