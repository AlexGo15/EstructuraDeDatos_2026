using System;

namespace DataCore
{
    /// <summary>
    /// Representa el modelo de datos básico almacenado en el sistema DataCore.
    /// </summary>
    public class RegistroDatos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Valor { get; set; }

        public RegistroDatos(int id, string nombre, double valor)
        {
            Id = id;
            Nombre = nombre ?? string.Empty;
            Valor = valor;
        }

        public override string ToString()
        {
            return $"[ID: {Id,-5} | Nombre: {Nombre,-20} | Valor: {Valor,8:F2}]";
        }
    }

    /// <summary>
    /// Nodo para la estructura de Lista Enlazada Simple (Tabla Dinámica).
    /// </summary>
    public class Nodo
    {
        public RegistroDatos Dato { get; set; }
        public Nodo Siguiente { get; set; }

        public Nodo(RegistroDatos dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }

    /// <summary>
    /// Estructura de almacenamiento principal basada en una Lista Enlazada Simple.
    /// Contiene la lógica para inserción, eliminación, recorrido, extracción a arreglo y ordenamiento.
    /// </summary>
    public class TablaDinamica
    {
        public Nodo Cabeza { get; private set; }
        public int TotalRegistros { get; private set; }

        public TablaDinamica()
        {
            Cabeza = null;
            TotalRegistros = 0;
        }

        /// <summary>
        /// Inserta un nuevo registro al final de la lista enlazada.
        /// </summary>
        public void Insertar(int id, string nombre, double valor)
        {
            RegistroDatos nuevoDato = new RegistroDatos(id, nombre, valor);
            Nodo nuevoNodo = new Nodo(nuevoDato);

            if (Cabeza == null)
            {
                Cabeza = nuevoNodo;
            }
            else
            {
                Nodo actual = Cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevoNodo;
            }
            TotalRegistros++;
        }

        /// <summary>
        /// Elimina un nodo por su ID redirigiendo punteros.
        /// </summary>
        public bool Eliminar(int id)
        {
            if (Cabeza == null) return false;

            if (Cabeza.Dato.Id == id)
            {
                Cabeza = Cabeza.Siguiente;
                TotalRegistros--;
                return true;
            }

            Nodo actual = Cabeza;
            while (actual.Siguiente != null && actual.Siguiente.Dato.Id != id)
            {
                actual = actual.Siguiente;
            }

            if (actual.Siguiente != null)
            {
                actual.Siguiente = actual.Siguiente.Siguiente;
                TotalRegistros--;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Muestra todos los elementos recorriendo la lista de cabeza a cola.
        /// </summary>
        public void Mostrar()
        {
            if (Cabeza == null)
            {
                Console.WriteLine("  [!] La tabla dinámica está vacía. No hay registros que mostrar.");
                return;
            }

            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("                         REGISTROS EN MEMORIA                          ");
            Console.WriteLine("----------------------------------------------------------------------");
            Nodo actual = Cabeza;
            int posicion = 1;
            while (actual != null)
            {
                Console.WriteLine($"  #{posicion,-3} {actual.Dato}");
                actual = actual.Siguiente;
                posicion++;
            }
            Console.WriteLine("----------------------------------------------------------------------");
        }

        /// <summary>
        /// Extrae los nodos válidos de la Lista Enlazada a un arreglo estático auxiliar.
        /// </summary>
        public RegistroDatos[] ExtraerAArreglo()
        {
            if (TotalRegistros == 0 || Cabeza == null)
            {
                return new RegistroDatos[0];
            }

            int contador = 0;
            Nodo actual = Cabeza;
            while (actual != null)
            {
                if (actual.Dato != null) contador++;
                actual = actual.Siguiente;
            }

            RegistroDatos[] arreglo = new RegistroDatos[contador];
            actual = Cabeza;
            int i = 0;
            while (actual != null && i < contador)
            {
                if (actual.Dato != null)
                {
                    arreglo[i] = actual.Dato;
                    i++;
                }
                actual = actual.Siguiente;
            }

            return arreglo;
        }

        /// <summary>
        /// Ordena el arreglo auxiliar utilizando el algoritmo QuickSort (O(n log n) promedio).
        /// </summary>
        public static void OrdenarArregloQuickSort(RegistroDatos[] arreglo, int izquierda, int derecha)
        {
            if (arreglo == null || arreglo.Length <= 1 || izquierda >= derecha) return;

            int i = izquierda;
            int j = derecha;
            int pivote = arreglo[(izquierda + derecha) / 2].Id;

            while (i <= j)
            {
                while (arreglo[i].Id < pivote) i++;
                while (arreglo[j].Id > pivote) j--;

                if (i <= j)
                {
                    RegistroDatos temp = arreglo[i];
                    arreglo[i] = arreglo[j];
                    arreglo[j] = temp;
                    i++;
                    j--;
                }
            }

            if (izquierda < j) OrdenarArregloQuickSort(arreglo, izquierda, j);
            if (i < derecha) OrdenarArregloQuickSort(arreglo, i, derecha);
        }

        /// <summary>
        /// Ejecuta la Búsqueda Binaria Indexada O(log n) sobre el arreglo previamente ordenado.
        /// </summary>
        public static (RegistroDatos Registro, int Comparaciones) BuscarRegistroIndexado(RegistroDatos[] arreglo, int idBuscado)
        {
            int comparaciones = 0;

            if (arreglo == null || arreglo.Length == 0)
            {
                return (null, 0);
            }

            int izquierda = 0;
            int derecha = arreglo.Length - 1;

            while (izquierda <= derecha)
            {
                int medio = izquierda + (derecha - izquierda) / 2;
                comparaciones++;

                if (arreglo[medio].Id == idBuscado)
                {
                    return (arreglo[medio], comparaciones);
                }

                if (arreglo[medio].Id < idBuscado)
                {
                    izquierda = medio + 1;
                }
                else
                {
                    derecha = medio - 1;
                }
            }

            return (null, comparaciones);
        }
    }

    /// <summary>
    /// Clase principal que contiene la interfaz de usuario en consola (Menú Maestro CLI).
    /// </summary>
    public class Program
    {
        private static TablaDinamica baseDeDatos = new TablaDinamica();
        private static RegistroDatos[] indiceOrdenado = null;
        private static bool indiceActualizado = false;

        public static void Main(string[] args)
        {
            int opcion = -1;

            CargarDatosIniciales();

            do
            {
                try
                {
                    MostrarEncabezadoMenu();
                    Console.Write("  Seleccione una opción (1-6): ");
                    string entrada = Console.ReadLine() ?? string.Empty;

                    if (!int.TryParse(entrada, out opcion))
                    {
                        MostrarError("Por favor, ingrese un número entero válido correspondiente al menú.");
                        Pausar();
                        continue;
                    }

                    Console.WriteLine();

                    switch (opcion)
                    {
                        case 1:
                            EjecutarOp1_Insertar();
                            break;
                        case 2:
                            EjecutarOp2_Eliminar();
                            break;
                        case 3:
                            EjecutarOp3_Mostrar();
                            break;
                        case 4:
                            EjecutarOp4_IndexarYOrdenar();
                            break;
                        case 5:
                            EjecutarOp5_BusquedaBinaria();
                            break;
                        case 6:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("  [✓] Gracias por utilizar DataCore v4.0. ¡Cierre de sesión seguro realizado!");
                            Console.ResetColor();
                            break;
                        default:
                            MostrarError("Opción fuera de rango. Seleccione un número entre 1 y 6.");
                            break;
                    }
                }
                catch (FormatException ex)
                {
                    MostrarError($"Excepción de formato: {ex.Message}");
                }
                catch (InvalidOperationException ex)
                {
                    MostrarError($"Operación no válida: {ex.Message}");
                }
                catch (IndexOutOfRangeException ex)
                {
                    MostrarError($"Índice fuera de rango: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MostrarError($"Error no esperado: {ex.Message}");
                }

                if (opcion != 6)
                {
                    Pausar();
                }

            } while (opcion != 6);
        }

        private static void MostrarEncabezadoMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================----------------------");
            Console.WriteLine("                        DATACORE v4.0 - MENÚ MAESTRO                   ");
            Console.WriteLine("================================================----------------------");
            Console.ResetColor();
            Console.WriteLine($"  [Estado Actual: {baseDeDatos.TotalRegistros} registros en memoria | Índice: {(indiceActualizado ? "Sincronizado" : "Desactualizado/Pendiente")}]");
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("  1. Insertar Nuevo Registro");
            Console.WriteLine("  2. Eliminar Registro por ID");
            Console.WriteLine("  3. Mostrar Todos los Registros (Lista Enlazada)");
            Console.WriteLine("  4. Indexar y Ordenar Datos (Construir Índice Auxiliar)");
            Console.WriteLine("  5. Búsqueda Binaria Indexada - O(log n)");
            Console.WriteLine("  6. Salir del Sistema");
            Console.WriteLine("----------------------------------------------------------------------");
        }

        private static void EjecutarOp1_Insertar()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(">>> OPCIÓN 1: INSERTAR REGISTRO <<<");
            Console.ResetColor();

            Console.Write("  Ingrese ID (Entero positivo): ");
            if (!int.TryParse(Console.ReadLine(), out int id) || id < 0)
            {
                MostrarError("El ID debe ser un número entero positivo.");
                return;
            }

            Console.Write("  Ingrese Nombre/Descripción: ");
            string nombre = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MostrarError("El nombre no puede estar vacío.");
                return;
            }

            Console.Write("  Ingrese Valor Monetario / Numérico: ");
            if (!double.TryParse(Console.ReadLine(), out double valor))
            {
                MostrarError("El valor debe ser una cifra numérica válida.");
                return;
            }

            baseDeDatos.Insertar(id, nombre, valor);
            indiceActualizado = false;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  [✓] Registro [ID: {id}] insertado exitosamente en la Tabla Dinámica.");
            Console.ResetColor();
        }

        private static void EjecutarOp2_Eliminar()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(">>> OPCIÓN 2: ELIMINAR REGISTRO <<<");
            Console.ResetColor();

            if (baseDeDatos.TotalRegistros == 0)
            {
                MostrarError("La base de datos está vacía. No hay registros para eliminar.");
                return;
            }

            Console.Write("  Ingrese el ID del registro a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                MostrarError("Debe ingresar un ID numérico válido.");
                return;
            }

            bool eliminado = baseDeDatos.Eliminar(id);
            if (eliminado)
            {
                indiceActualizado = false;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  [✓] Registro con ID {id} eliminado correctamente.");
                Console.ResetColor();
            }
            else
            {
                MostrarError($"No se encontró ningún registro con ID {id} para eliminar.");
            }
        }

        private static void EjecutarOp3_Mostrar()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(">>> OPCIÓN 3: MOSTRAR REGISTROS (RECORRIDO SECUENCIAL) <<<");
            Console.ResetColor();
            baseDeDatos.Mostrar();
        }

        private static void EjecutarOp4_IndexarYOrdenar()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(">>> OPCIÓN 4: INDEXAR Y ORDENAR DATOS <<<");
            Console.ResetColor();

            if (baseDeDatos.TotalRegistros == 0)
            {
                MostrarError("No hay registros en la lista para construir el índice.");
                return;
            }

            indiceOrdenado = baseDeDatos.ExtraerAArreglo();
            TablaDinamica.OrdenarArregloQuickSort(indiceOrdenado, 0, indiceOrdenado.Length - 1);
            indiceActualizado = true;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  [✓] Índice construido y ordenado exitosamente en memoria.");
            Console.WriteLine($"      Total de elementos indexados: {indiceOrdenado.Length}");
            Console.WriteLine("      Algoritmo de ordenamiento aplicado: QuickSort - O(n log n)");
            Console.ResetColor();

            Console.WriteLine("\n  Muestra del Índice Ordenado (Arreglo Auxiliar):");
            for (int i = 0; i < indiceOrdenado.Length; i++)
            {
                Console.WriteLine($"   Posición [{i}]: {indiceOrdenado[i]}");
            }
        }

        private static void EjecutarOp5_BusquedaBinaria()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(">>> OPCIÓN 5: BÚSQUEDA BINARIA INDEXADA <<<");
            Console.ResetColor();

            if (baseDeDatos.TotalRegistros == 0)
            {
                MostrarError("La base de datos está vacía.");
                return;
            }

            if (!indiceActualizado || indiceOrdenado == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("  [!] ADVERTENCIA: El índice no está sincronizado con la lista.");
                Console.WriteLine("      Ejecutando ordenamiento y actualización automática del índice...");
                Console.ResetColor();
                EjecutarOp4_IndexarYOrdenar();
                Console.WriteLine();
            }

            Console.Write("  Ingrese el ID que desea buscar mediante Búsqueda Binaria: ");
            if (!int.TryParse(Console.ReadLine(), out int idBuscado))
            {
                MostrarError("El ID a buscar debe ser un número entero.");
                return;
            }

            var resultado = TablaDinamica.BuscarRegistroIndexado(indiceOrdenado, idBuscado);

            Console.WriteLine("\n  ---------------- RESULTADOS DE LA BÚSQUEDA ----------------");
            if (resultado.Registro != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  [✓] REGISTRO ENCONTRADO:");
                Console.WriteLine($"      {resultado.Registro}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  [X] REGISTRO NO ENCONTRADO: El ID {idBuscado} no existe en la base de datos.");
                Console.ResetColor();
            }

            // CORRECCIÓN APLICADA AQUÍ: Se usa Math.Log(x, 2) compatible con cualquier versión de .NET
            double maxComparacionesEsperadas = Math.Ceiling(Math.Log(indiceOrdenado.Length + 1, 2));

            Console.WriteLine($"  ----------------------------------------------------------");
            Console.WriteLine($"  * Número total de comparaciones realizadas: {resultado.Comparaciones}");
            Console.WriteLine($"  * Complejidad algorítmica alcanzada: O(log n)");
            Console.WriteLine($"  * Comparaciones máximas esperadas para {indiceOrdenado.Length} elementos: {maxComparacionesEsperadas}");
            Console.WriteLine($"  ----------------------------------------------------------");
        }

        private static void CargarDatosIniciales()
        {
            baseDeDatos.Insertar(105, "Servidor Blade R740", 4500.50);
            baseDeDatos.Insertar(102, "Switch Cisco 2960", 1200.00);
            baseDeDatos.Insertar(108, "Router Mikrotik Cloud", 850.75);
            baseDeDatos.Insertar(101, "Firewall Fortinet 60F", 2100.30);
            baseDeDatos.Insertar(104, "Unidad UPS APC 1500VA", 650.00);
            baseDeDatos.Insertar(107, "Disco SSD NVMe 2TB", 220.00);
            baseDeDatos.Insertar(103, "Memoria RAM DDR5 64GB", 310.00);
            baseDeDatos.Insertar(106, "Gabinete Rack 42U", 980.00);
        }

        private static void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  [X] ERROR: {mensaje}");
            Console.ResetColor();
        }

        private static void Pausar()
        {
            Console.WriteLine("\n  Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}