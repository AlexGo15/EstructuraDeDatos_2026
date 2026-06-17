#nullable enable
using System;

public class Nodo
{
    public int ID { get; set; }
    public string? Dato { get; set; }

    public Nodo? HijoIzquierdo { get; set; }
    public Nodo? HijoDerecho { get; set; }
}

public static class Arbol
{
    public static string? BuscarNodo(Nodo? raiz, int idTarget)
    {
        if (raiz == null)
            return null;

        if (idTarget == raiz.ID)
            return raiz.Dato;

        if (idTarget < raiz.ID)
            return BuscarNodo(raiz.HijoIzquierdo, idTarget);

        return BuscarNodo(raiz.HijoDerecho, idTarget);
    }
}

public class Program
{
    public static void Main()
    {
        Nodo raiz = new()
        {
            ID = 50,
            Dato = "Raíz",
            HijoIzquierdo = new Nodo
            {
                ID = 25,
                Dato = "Nodo 25",
                HijoIzquierdo = new Nodo
                {
                    ID = 10,
                    Dato = "Nodo 10"
                },
                HijoDerecho = new Nodo
                {
                    ID = 30,
                    Dato = "Nodo 30"
                }
            },
            HijoDerecho = new Nodo
            {
                ID = 75,
                Dato = "Nodo 75",
                HijoIzquierdo = new Nodo
                {
                    ID = 60,
                    Dato = "Nodo 60"
                },
                HijoDerecho = new Nodo
                {
                    ID = 90,
                    Dato = "Nodo 90"
                }
            }
        };

        int idBuscado = 60;

        string? resultado = Arbol.BuscarNodo(raiz, idBuscado);

        if (resultado != null)
            Console.WriteLine($"Nodo encontrado: {resultado}");
        else
            Console.WriteLine("Nodo no encontrado.");
    }
}