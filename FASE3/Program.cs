using System;

namespace DataCore
{
    /// <summary>
    /// Struct inmutable que representa el registro de datos básico (Value Type).
    /// </summary>
    public readonly struct RegistroDatos
    {
        public int Id { get; }
        public string Nombre { get; }
        public decimal Monto { get; }

        public RegistroDatos(int id, string nombre, decimal monto)
        {
            Id = id;
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Monto = monto;
        }

        public override string ToString()
        {
            return $"Id: {Id,-2} | Nombre: {Nombre,-15} | Monto: {Monto,10:C}";
        }
    }

    /// <summary>
    /// Clase NodoRegistro (Reference Type en Heap).
    /// Representa un eslabón individual dentro de la lista simplemente enlazada.
    /// </summary>
    public class NodoRegistro
    {
        // El dato que almacena este nodo
        public RegistroDatos Dato { get; set; }

        // Referencia gestionada al siguiente nodo (null si es el último eslabón)
        public NodoRegistro? Siguiente { get; set; }

        /// <summary>
        /// Constructor: Inicializa el nodo con su dato y marca Siguiente como null.
        /// </summary>
        public NodoRegistro(RegistroDatos dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }

    /// <summary>
    /// Clase TablaDinamica.
    /// Controlador principal de la lista simplemente enlazada en la Heap.
    /// </summary>
    public class TablaDinamica
    {
        private NodoRegistro? cabeza;
        private int contadorRegistros;

        public int ContadorRegistros => contadorRegistros;

        public TablaDinamica()
        {
            cabeza = null;
            contadorRegistros = 0;
        }

        /// <summary>
        /// Inserta un registro al inicio de la lista. Complejidad O(1).
        /// </summary>
        public void InsertarInicio(RegistroDatos nuevoRegistro)
        {
            NodoRegistro nuevoNodo = new NodoRegistro(nuevoRegistro);
            nuevoNodo.Siguiente = cabeza;
            cabeza = nuevoNodo;
            contadorRegistros++;
        }

        /// <summary>
        /// Inserta un registro al final de la lista. Complejidad O(n).
        /// </summary>
        public void InsertarFinal(RegistroDatos nuevoRegistro)
        {
            NodoRegistro nuevoNodo = new NodoRegistro(nuevoRegistro);

            if (cabeza == null)
            {
                cabeza = nuevoNodo;
            }
            else
            {
                NodoRegistro actual = cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevoNodo;
            }

            contadorRegistros++;
        }

        /// <summary>
        /// Busca un nodo por su Id y lo desvincula de la cadena actualizando referencias.
        /// Complejidad O(n).
        /// </summary>
        public void EliminarPorId(int idTarget)
        {
            if (cabeza == null) return;

            // Caso especial 1: Eliminar la Cabeza
            if (cabeza.Dato.Id == idTarget)
            {
                cabeza = cabeza.Siguiente;
                contadorRegistros--;
                return;
            }

            // Caso general 2: Eliminar nodo intermedio o final
            NodoRegistro anterior = cabeza;
            NodoRegistro? actual = cabeza.Siguiente;

            while (actual != null)
            {
                if (actual.Dato.Id == idTarget)
                {
                    // Reconecta la cadena saltando el nodo objetivo
                    anterior.Siguiente = actual.Siguiente;
                    contadorRegistros--;
                    return;
                }

                anterior = actual;
                actual = actual.Siguiente;
            }
        }

        /// <summary>
        /// Extrae todos los registros de la cadena y los devuelve en un arreglo estático.
        /// Sirve de puente de interoperabilidad con QuickSort y SelectionSort. Complejidad O(n).
        /// </summary>
        public RegistroDatos[] ObtenerComoArreglo()
        {
            RegistroDatos[] resultado = new RegistroDatos[contadorRegistros];
            NodoRegistro? actual = cabeza;
            int i = 0;

            while (actual != null && i < contadorRegistros)
            {
                resultado[i] = actual.Dato;
                actual = actual.Siguiente;
                i++;
            }

            return resultado;
        }
    }

    /// <summary>
    /// Orquestador Principal (Main)
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=================================================");
            Console.WriteLine(" DATACORE MOTOR - FASE 3: MEMORIA HEAP Y LISTAS ");
            Console.WriteLine("=================================================\n");

            // Instanciar la estructura dinámica
            TablaDinamica dataCore = new TablaDinamica();

            // Paso 1: Insertar 15 registros dinámicos
            for (int i = 1; i <= 15; i++)
            {
                RegistroDatos reg = new RegistroDatos(i, $"Transacción-{i}", i * 100.0m);
                dataCore.InsertarFinal(reg);
                Console.WriteLine($"[INSERT] Registro {i} añadido a la cadena.");
            }

            // Paso 2: Eliminar 2 registros específicos (ID 5 e ID 11)
            Console.WriteLine("\n--- Eliminando registros con Id 5 y Id 11 ---");
            dataCore.EliminarPorId(5);
            dataCore.EliminarPorId(11);
            Console.WriteLine("Cadena reestructurada exitosamente. Sin NullReferenceException.");

            // Paso 3: Convertir a arreglo y ordenar con QuickSort (Motor de Fase 2)
            RegistroDatos[] arreglo = dataCore.ObtenerComoArreglo();
            Console.WriteLine($"\nRegistros en arreglo: {arreglo.Length} (esperado: 13)");

            // Invocación del motor QuickSort heredado de Fase 2
            QuickSort(arreglo, 0, arreglo.Length - 1);

            Console.WriteLine("\n--- Arreglo ordenado por Id (QuickSort) ---");
            foreach (var r in arreglo)
            {
                Console.WriteLine(r.ToString());
            }

            Console.WriteLine("\n=================================================");
            Console.WriteLine(" EJECUCIÓN COMPLETADA SIN ERRORES EN LA CADENA ");
            Console.WriteLine("=================================================");
        }

        #region Algoritmo QuickSort (Motor Heredado de Fase 2)
        public static void QuickSort(RegistroDatos[] arr, int izquierda, int derecha)
        {
            if (izquierda < derecha)
            {
                int indicePivote = Particionar(arr, izquierda, derecha);
                QuickSort(arr, izquierda, indicePivote - 1);
                QuickSort(arr, indicePivote + 1, derecha);
            }
        }

        private static int Particionar(RegistroDatos[] arr, int izquierda, int derecha)
        {
            int pivote = arr[derecha].Id;
            int i = izquierda - 1;

            for (int j = izquierda; j < derecha; j++)
            {
                if (arr[j].Id <= pivote)
                {
                    i++;
                    Intercambiar(arr, i, j);
                }
            }

            Intercambiar(arr, i + 1, derecha);
            return i + 1;
        }

        private static void Intercambiar(RegistroDatos[] arr, int i, int j)
        {
            RegistroDatos temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
        #endregion
    }
}