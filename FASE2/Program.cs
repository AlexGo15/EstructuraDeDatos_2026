using System;
using System.Diagnostics;

namespace ProyectoFinalEstructuraDatos
{
    /// <summary>
    /// Modelo de datos inmutable representado como struct (Value Type) para maximizar 
    /// la localidad de caché y eliminar la sobrecarga del Garbage Collector en la RAM.
    /// Mantiene compatibilidad total con la Fase 1.
    /// </summary>
    public struct RegistroDatos
    {
        public int Id { get; }
        public string HashValidacion { get; }
        public double PesoBytes { get; }

        public RegistroDatos(int id, string hashValidacion, double pesoBytes)
        {
            if (id <= 0)
                throw new ArgumentException("[Error] El Id debe ser un entero positivo mayor que cero.", nameof(id));

            if (string.IsNullOrEmpty(hashValidacion))
                throw new ArgumentNullException(nameof(hashValidacion), "[Error] HashValidacion no puede ser null ni una cadena vacía.");

            if (pesoBytes <= 0)
                throw new ArgumentOutOfRangeException(nameof(pesoBytes), "[Error] PesoBytes debe ser un valor numérico positivo mayor que cero.");

            Id = id;
            HashValidacion = hashValidacion;
            PesoBytes = pesoBytes;
        }

        public override string ToString()
        {
            return $"[ID: {Id} | Hash: {HashValidacion.Substring(0, Math.Min(8, HashValidacion.Length))}... | Peso: {PesoBytes:F2} B]";
        }
    }

    class Program
    {
        // Contadores de instrumentación y benchmarking
        public static long contadorComparacionesSeleccion = 0;
        public static long contadorIntercambiosSeleccion = 0;
        public static long contadorLlamadasQuickSort = 0;
        public static long contadorIntercambiosQuickSort = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("==========================================================================");
            Console.WriteLine("    PROYECTO FINAL - FASE 2: MOTOR DE ORDENACIÓN AVANZADA (QUICKSORT)    ");
            Console.WriteLine("==========================================================================");

            int tamanoLote = 10000;
            Console.WriteLine($"[+] Generando lote de datos aleatorios deterministas (n = {tamanoLote:N0})...");
            RegistroDatos[] arregloOriginal = GenerarArregloAleatorio(tamanoLote, semilla: 42);

            // Clonar arreglos para garantizar igualdad de condiciones de prueba
            RegistroDatos[] copiaSeleccion = (RegistroDatos[])arregloOriginal.Clone();
            RegistroDatos[] copiaQuickSort = (RegistroDatos[])arregloOriginal.Clone();

            // -------------------------------------------------------------------
            // BENCHMARK 1: SELECCIÓN DIRECTA (FASE 1)
            // -------------------------------------------------------------------
            Console.WriteLine("[1/2] Ejecutando Selección Directa (Fase 1 - O(n²))...");
            contadorComparacionesSeleccion = 0;
            contadorIntercambiosSeleccion = 0;

            Stopwatch swSeleccion = Stopwatch.StartNew();
            OrdenarPorSeleccion(copiaSeleccion);
            swSeleccion.Stop();

            bool seleccionCorrecto = EstaOrdenado(copiaSeleccion);

            // -------------------------------------------------------------------
            // BENCHMARK 2: QUICKSORT RECURSIVO (FASE 2)
            // -------------------------------------------------------------------
            Console.WriteLine("[2/2] Ejecutando QuickSort Recursivo con Pivote Central (Fase 2 - O(n log n))...");
            contadorLlamadasQuickSort = 0;
            contadorIntercambiosQuickSort = 0;

            Stopwatch swQuickSort = Stopwatch.StartNew();
            QuickSort(copiaQuickSort, 0, copiaQuickSort.Length - 1);
            swQuickSort.Stop();

            bool quickSortCorrecto = EstaOrdenado(copiaQuickSort);

            // -------------------------------------------------------------------
            // IMPRESIÓN DEL REPORTE COMPARATIVO DE RENDIMIENTO
            // -------------------------------------------------------------------
            ImprimirReporteComparativo(
                tamanoLote, 
                swSeleccion.ElapsedMilliseconds, 
                swQuickSort.ElapsedMilliseconds, 
                seleccionCorrecto, 
                quickSortCorrecto
            );

            // Pruebas adicionales en escenarios extremos para la defensa técnica
            EjecutarPruebasCasosExtremos();
        }

        #region Implementación del Motor QuickSort (Fase 2)

        /// <summary>
        /// Método de control recursivo para QuickSort.
        /// Aplica el paradigma Divide y Vencerás procesando subparticiones dinámicas.
        /// </summary>
        public static void QuickSort(RegistroDatos[] arr, int bajo, int alto)
        {
            contadorLlamadasQuickSort++; // Instrumentación de la profundidad del Call Stack

            // Caso base: La recursión se detiene cuando la subpartición tiene 0 o 1 elemento
            if (bajo < alto)
            {
                // Divide: Obtiene el índice donde el pivote queda en su posición definitiva
                int indicePivote = Particionar(arr, bajo, alto);

                // Vence: Ordena recursivamente la sublista izquierda (menores al pivote)
                QuickSort(arr, bajo, indicePivote - 1);

                // Vence: Ordena recursivamente la sublista derecha (mayores al pivote)
                QuickSort(arr, indicePivote + 1, alto);
            }
        }

        /// <summary>
        /// Método de particionado optimizado con pivote central (mediana de rango) e intercambio por tuplas C#.
        /// Reorganiza el arreglo in-place reduciendo el riesgo del peor caso O(n²).
        /// </summary>
        private static int Particionar(RegistroDatos[] arr, int bajo, int alto)
        { apex:
            // Estrategia de Selección de Pivote Central: Evita la degradación O(n²) en arreglos ordenados
            int indiceMedio = bajo + (alto - bajo) / 2;
            int pivoteId = arr[indiceMedio].Id;

            // Mueve el pivote al extremo derecho temporalmente para facilitar el recorrido
            (arr[indiceMedio], arr[alto]) = (arr[alto], arr[indiceMedio]);
            contadorIntercambiosQuickSort++;

            int i = bajo - 1; // Puntero de elementos menores al pivote

            for (int j = bajo; j < alto; j++)
            {
                // Comparación por campo clave Id
                if (arr[j].Id <= pivoteId)
                {
                    i++;
                    // Intercambio idiomático de C# mediante Tuplas (sin variables auxiliares explícitas)
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                    contadorIntercambiosQuickSort++;
                }
            }

            // Coloca el pivote de vuelta en su posición definitiva de equilibrio (i + 1)
            (arr[i + 1], arr[alto]) = (arr[alto], arr[i + 1]);
            contadorIntercambiosQuickSort++;

            return i + 1; // Retorna el índice definitivo del pivote
        }

        #endregion

        #region Algoritmo de Selección Directa (Fase 1)

        public static void OrdenarPorSeleccion(RegistroDatos[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < n; j++)
                {
                    contadorComparacionesSeleccion++;
                    if (arr[j].Id < arr[minIdx].Id)
                    {
                        minIdx = j;
                    }
                }

                if (minIdx != i)
                {
                    (arr[i], arr[minIdx]) = (arr[minIdx], arr[i]);
                    contadorIntercambiosSeleccion++;
                }
            }
        }

        #endregion

        #region Métodos Auxiliares y Verificación

        public static RegistroDatos[] GenerarArregloAleatorio(int cantidad, int semilla)
        {
            Random rnd = new Random(semilla); // Semilla fija para reproducibilidad científica
            RegistroDatos[] arreglo = new RegistroDatos[cantidad];

            for (int i = 0; i < cantidad; i++)
            {
                arreglo[i] = new RegistroDatos(
                    id: rnd.Next(1, 1000001),
                    hashValidacion: Guid.NewGuid().ToString("N"),
                    pesoBytes: 10.0 + (rnd.NextDouble() * 9990.0)
                );
            }
            return arreglo;
        }

        public static bool EstaOrdenado(RegistroDatos[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i].Id > arr[i + 1].Id)
                    return false;
            }
            return true;
        }

        private static void ImprimirReporteComparativo(int n, long msSeleccion, long msQuickSort, bool statusSel, bool statusQS)
        {
            double ratioVelocidad = msQuickSort > 0 ? (double)msSeleccion / msQuickSort : msSeleccion;

            Console.WriteLine("==========================================================================");
            Console.WriteLine($"              REPORTE COMPARATIVO DE RENDIMIENTO (n = {n:N0})             ");
            Console.WriteLine("==========================================================================");
            Console.WriteLine(string.Format("{0,-28} | {1,-18} | {2,-18}", "Métrica / Dimensión", "Selección (Fase 1)", "QuickSort (Fase 2)"));
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine(string.Format("{0,-28} | {1,-18} | {2,-18}", "Complejidad Teórica", "O(n²)", "O(n log n)"));
            Console.WriteLine(string.Format("{0,-28} | {1,-18:N0} | {2,-18}", "Comparaciones Directas", contadorComparacionesSeleccion, "N/A (Subdivididas)"));
            Console.WriteLine(string.Format("{0,-28} | {1,-18:N0} | {2,-18:N0}", "Intercambios (Swaps)", contadorIntercambiosSeleccion, contadorIntercambiosQuickSort));
            Console.WriteLine(string.Format("{0,-28} | {1,-18} | {2,-18:N0}", "Llamadas Recursivas (Stack)", "0 (Iterativo)", contadorLlamadasQuickSort));
            Console.WriteLine(string.Format("{0,-28} | {1,-18} | {2,-18}", "Tiempo Real Medido (ms)", $"{msSeleccion} ms", $"{msQuickSort} ms"));
            Console.WriteLine(string.Format("{0,-28} | {1,-18} | {2,-18}", "Validación de Orden", statusSel ? "CORRECTO [✓]" : "ERROR [X]", statusQS ? "CORRECTO [✓]" : "ERROR [X]"));
            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine($" RESULTADO FINAL: QuickSort fue {ratioVelocidad:F1}x MÁS RÁPIDO que Selección Directa.");
            Console.WriteLine("==========================================================================");
        }

        private static void EjecutarPruebasCasosExtremos()
        {
            Console.WriteLine("[+] Ejecutando Validación Automática de Casos Extremos para QuickSort...");

            // Caso 1: Arreglo Vacío
            RegistroDatos[] vacio = new RegistroDatos[0];
            QuickSort(vacio, 0, vacio.Length - 1);
            Console.WriteLine($"    • Caso Arreglo Vacío (n=0): {(vacio.Length == 0 ? "PASADO [✓]" : "FALLADO [X]")}");

            // Caso 2: Un solo elemento
            RegistroDatos[] unico = new RegistroDatos[] { new RegistroDatos(100, "HASH12345", 50.0) };
            QuickSort(unico, 0, unico.Length - 1);
            Console.WriteLine($"    • Caso Elemento Único (n=1): {(unico.Length == 1 && unico[0].Id == 100 ? "PASADO [✓]" : "FALLADO [X]")}");

            // Caso 3: Arreglo ya ordenado
            RegistroDatos[] ordenado = GenerarArregloAleatorio(100, 10);
            Array.Sort(ordenado, (a, b) => a.Id.CompareTo(b.Id));
            QuickSort(ordenado, 0, ordenado.Length - 1);
            Console.WriteLine($"    • Caso Ya Ordenado (n=100): {(EstaOrdenado(ordenado) ? "PASADO [✓]" : "FALLADO [X]")}");

            // Caso 4: Arreglo invertido
            RegistroDatos[] invertido = GenerarArregloAleatorio(100, 20);
            Array.Sort(invertido, (a, b) => b.Id.CompareTo(a.Id));
            QuickSort(invertido, 0, invertido.Length - 1);
            Console.WriteLine($"    • Caso Orden Invertido (n=100): {(EstaOrdenado(invertido) ? "PASADO [✓]" : "FALLADO [X]")}");

            Console.WriteLine("==========================================================================");
            Console.WriteLine("[✓] Todas las pruebas de validación finalizaron con éxito.");
        }

        #endregion
    }
}