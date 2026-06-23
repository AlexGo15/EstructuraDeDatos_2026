using System;
using System.Collections.Generic;

namespace SimulacroEstructuraDatos
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== SISTEMA ACADÉMICO =====");

            Student student = new Student();

            student.Name = "Juan";
            student.Age = 20;

            student.Grades.Add(90);
            student.Grades.Add(85);
            student.Grades.Add(70);

            StudentService service = new StudentService();

            service.RegisterStudent(student);

            Console.WriteLine();

            Course course = new Course();

            course.Name = "Estructura de Datos";
            course.Students.Add(student);

            Console.WriteLine("Listado de alumnos:");

            foreach (var s in course.Students)
            {
                s.ShowInformation();
            }

            Console.WriteLine();

            Teacher teacher = new Teacher();

            teacher.Name = "Carlos";
            teacher.Age = 40;
            teacher.Subject = "Programación";

            teacher.ShowInformation();

            Console.WriteLine();

            DebugHelper.PrintCollection(
                new List<string>()
                {
                    "Elemento A",
                    "Elemento B",
                    "Elemento C"
                });

            Console.WriteLine();
            Console.WriteLine("Fin del programa.");

            Console.ReadKey();
        }
    }

    // =====================================================
    // INTERFAZ
    // =====================================================

    public interface IPrintable
    {
        void Print();
    }

    // =====================================================
    // CLASE ABSTRACTA
    // =====================================================

    public abstract class ReportGenerator
    {
        public abstract void Generate();
    }

    // =====================================================
    // CLASE BASE
    // =====================================================

    public class Person
    {
        // CORRECCIÓN:
        // Se reemplazaron los campos públicos por propiedades
        // para aplicar encapsulamiento y controlar mejor
        // el acceso a los datos del objeto.

        public string Name { get; set; } = "";
        public int Age { get; set; }

        public virtual void ShowInformation()
        {
            Console.WriteLine($"Nombre: {Name}");
            Console.WriteLine($"Edad: {Age}");
        }
    }

    // =====================================================
    // STUDENT
    // =====================================================

    public class Student : Person, IPrintable
    {
        // CORRECCIÓN:
        // La colección ahora se maneja mediante una propiedad
        // para mejorar el encapsulamiento.

        public List<int> Grades { get; set; } = new();

        public double CalculateAverage()
        {
            int total = 0;

            foreach (var grade in Grades)
            {
                total += grade;
            }

            // CORRECCIÓN:
            // Se valida que existan calificaciones antes de
            // realizar la división para evitar errores.

            if (Grades.Count == 0)
            {
                return 0;
            }

            // CORRECCIÓN:
            // Conversión explícita a double para conservar
            // los decimales del promedio.

            return (double)total / Grades.Count;
        }

        public void Print()
        {
            Console.WriteLine($"Imprimiendo estudiante: {Name}");
        }

        public override void ShowInformation()
        {
            Console.WriteLine($"Alumno: {Name}");
            Console.WriteLine($"Edad: {Age}");
        }
    }

    // =====================================================
    // TEACHER
    // =====================================================

    public class Teacher : Person
    {
        // CORRECCIÓN:
        // Teacher hereda directamente de Person porque
        // un profesor no es un estudiante.

        public string Subject { get; set; } = "";

        public override void ShowInformation()
        {
            Console.WriteLine($"Profesor: {Name}");
            Console.WriteLine($"Materia: {Subject}");
        }
    }

    // =====================================================
    // COURSE
    // =====================================================

    public class Course
    {
        // CORRECCIÓN:
        // Se reemplazaron los campos públicos por propiedades.

        public string Name { get; set; } = "";

        public List<Student> Students { get; set; }
            = new List<Student>();
    }

    // =====================================================
    // STUDENT SERVICE
    // =====================================================

    public class StudentService
    {
        // CORRECCIÓN:
        // La dependencia se declara como readonly para
        // evitar modificaciones posteriores.
        // Como mejora futura podría utilizarse una interfaz
        // e inyección de dependencias.

        private readonly FileManager _fileManager;

        public StudentService()
        {
            _fileManager = new FileManager();
        }

        public void RegisterStudent(Student student)
        {
            Console.WriteLine("Registrando alumno...");

            try
            {
                double average =
                    student.CalculateAverage();

                Console.WriteLine(
                    $"Promedio: {average:F2}");

                _fileManager.Save(student.Name);

                if (average < 60)
                {
                    Console.WriteLine("Reprobado");
                }
                else
                {
                    Console.WriteLine("Aprobado");
                }
            }
            catch (Exception ex)
            {
                // CORRECCIÓN:
                // La excepción ya no se ignora.
                // Se informa al usuario qué ocurrió.

                Console.WriteLine(
                    $"Error durante el registro: {ex.Message}");
            }
        }
    }

    // =====================================================
    // FILE MANAGER
    // =====================================================

    public class FileManager
    {
        public void Save(string text)
        {
            Console.WriteLine(
                "Guardando información...");

            if (string.IsNullOrWhiteSpace(text))
            {
                // CORRECCIÓN:
                // Se utiliza una excepción específica para
                // indicar que el argumento recibido es inválido.

                throw new ArgumentException(
                    "El texto está vacío o es inválido.");
            }

            Console.WriteLine(text);
        }
    }

    // =====================================================
    // DEBUG HELPER
    // =====================================================

    public static class DebugHelper
    {
        public static void PrintCollection(
            List<string> values)
        {
            for (int i = 0; i < values.Count; i++)
            {
                Console.WriteLine(values[i]);
            }
        }
    }
}