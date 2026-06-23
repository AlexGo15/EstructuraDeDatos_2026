1. PERSON
Clase afectada: Person
Error detectado: Campos públicos Name y Age
Corrección aplicada: Se reemplazaron por propiedades (get; set;)
Principio violado: Encapsulamiento
Severidad: Alta

2. STUDENT
Clase afectada: Student
Error detectado: Campo público Grades
Corrección aplicada: Se convirtió en propiedad para controlar el acceso a los datos
Principio violado: Encapsulamiento
Severidad: Alta

3. COURSE
Clase afectada: Course
Error detectado: Campos públicos Name y Students
Corrección aplicada: Se reemplazaron por propiedades
Principio violado: Encapsulamiento
Severidad: Alta

4. STUDENT - CALCULATEAVERAGE
Clase afectada: Student
Error detectado: Posible división por cero en CalculateAverage()
Corrección aplicada: Se agregó validación para evitar dividir cuando no hay elementos
Principio violado: Robustez / Manejo de errores
Severidad: Alta

5. STUDENT - DIVISIÓN ENTERA
Clase afectada: Student
Error detectado: División entera que elimina decimales
Corrección aplicada: Conversión explícita a double
Principio violado: Correctitud de datos
Severidad: Media

6. TEACHER - HERENCIA INCORRECTA
Clase afectada: Teacher
Error detectado: Teacher heredaba de Student
Corrección aplicada: Ahora hereda de Person
Principio violado: Herencia incorrecta (relación “is-a”)
Severidad: Alta

7. TEACHER - SUBJECT
Clase afectada: Teacher
Error detectado: Campo público Subject
Corrección aplicada: Se convirtió en propiedad
Principio violado: Encapsulamiento
Severidad: Media

8. STUDENTSERVICE - EXCEPCIONES
Clase afectada: StudentService
Error detectado: catch vacío que ignora errores
Corrección aplicada: Se captura la excepción y se muestra mensaje descriptivo
Principio violado: Manejo de excepciones
Severidad: Alta

9. FILEMANAGER
Clase afectada: FileManager
Error detectado: Uso de Exception genérica
Corrección aplicada: Se reemplazó por ArgumentException
Principio violado: Calidad de software / Excepciones específicas
Severidad: Media

10. STUDENTSERVICE - DEPENDENCIA
Clase afectada: StudentService
Error detectado: Dependencia directa de FileManager
Corrección aplicada: Se marcó como readonly y se documentó mejora con interfaces
Principio violado: Buenas prácticas de diseño
Severidad: Baja

11. STUDENTSERVICE - DIP
Clase afectada: StudentService
Error detectado: Violación del principio DIP
Corrección aplicada: Se recomienda uso de interfaz e inyección de dependencias
Principio violado: SOLID - Dependency Inversion Principle
Severidad: Media

12. STUDENTSERVICE - SRP
Clase afectada: StudentService
Error detectado: Demasiadas responsabilidades en un solo método
Corrección aplicada: Se identifica necesidad de refactorización en varias clases
Principio violado: SOLID - Single Responsibility Principle
Severidad: Media