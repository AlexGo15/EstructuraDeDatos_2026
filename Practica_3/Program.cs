using System;
using System.Collections.Generic;
using System.Linq;

public class Producto
{
    public double Precio { get; set; }
    public int Cantidad { get; set; }
}

class Program
{
    static void Main()
    {
        List<Producto> productos = new()
        {
            new Producto {Precio = 75.0, Cantidad = 5 },
            new Producto {Precio = 50.0, Cantidad = 3 },
            new Producto {Precio = 15.0, Cantidad = 7 },
            new Producto {Precio = 25.5, Cantidad = 2 }
        };
        var productosFiltrados = productos
            .Where(p => p.Precio > 50.0)
            .OrderByDescending(p => p.Cantidad)
            .ToList();

        foreach (var producto in productosFiltrados)
        {
            Console.WriteLine($"Precio: {producto.Precio}, Cantidad: {producto.Cantidad}");
        }
    }
}