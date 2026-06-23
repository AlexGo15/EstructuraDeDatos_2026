using System;

// 1. Definición del struct inmutable para coordenadas GPS
readonly struct CoordenadaGPS
{
    public double Latitud { get; }
    public double Longitud { get; }

    public CoordenadaGPS(double lat, double lon)
    {
        // 3. Validación de rangos geográficos válidos
        if (lat < -90 || lat > 90)
            throw new ArgumentOutOfRangeException(
                nameof(lat),
                "Latitud fuera de rango [-90, 90]");

        if (lon < -180 || lon > 180)
            throw new ArgumentOutOfRangeException(
                nameof(lon),
                "Longitud fuera de rango [-180, 180]");

        Latitud = lat;
        Longitud = lon;
    }

    // Muestra las coordenadas almacenadas
    public void ImprimirUbicacion()
    {
        Console.WriteLine(
            $"Latitud: {Latitud}, Longitud: {Longitud}");
    }
}

class Program
{
    static void Main()
    {
        // 2. Demostración de copia por valor en un struct

        // Ciudad de México
        CoordenadaGPS c1 =
            new CoordenadaGPS(19.4326, -99.1332);

        // Copia por valor en el Stack
        CoordenadaGPS c2 = c1;

        // Reasignamos c2 → Berlín
        c2 = new CoordenadaGPS(52.5200, 13.4050);

        // Imprimimos ambas
        Console.WriteLine("--- c1 ---");
        c1.ImprimirUbicacion();

        Console.WriteLine("--- c2 ---");
        c2.ImprimirUbicacion();

        Console.WriteLine();


        // 3. Prueba de validación y manejo de excepciones
        try
        {
            Console.Write("Latitud: ");
            string? latInput = Console.ReadLine();
            double lat = double.Parse(latInput ?? throw new FormatException());

            Console.Write("Longitud: ");
            string? lonInput = Console.ReadLine();
            double lon = double.Parse(lonInput ?? throw new FormatException());

            var coord =
                new CoordenadaGPS(lat, lon);

            coord.ImprimirUbicacion();
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine(
                $"Error de rango: {ex.Message}");
        }
        catch (FormatException)
        {
            Console.WriteLine(
                "Error: Debe ingresar valores numéricos válidos.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Error inesperado: {ex.Message}");
        }
    }
}