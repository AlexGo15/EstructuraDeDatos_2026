# EstructuraDeDatos_2026

Cuestionario de Evaluación, Clase/Práctica 7.

1. La "recursividad" es cuando una función se llama a sí misma las veces que se indiquen para dividir un problema en subproblemas, y así resolver el problema principal; necesita de un "caso base" (en donde se detiene y no vuelve a llamarse) y un "paso recursivo" (cada vez que la función se llama a sí misma). Mientras que ciclos "while" y/o "for" son muy eficientes para repeticiones simples, la recursividad puede expresar mejor los problemas jerárquicos; otra diferencia es que, si en while o for no se cumple su condición el ciclo se repite de manera indefinida, una recursividad produce diréctamente un StackOverflowException si no cuenta con su caso base. Por lo general, suele ser más natural utilizar una recursividad al revisar un árbol de directorios: recorre cada carpeta y regresa para revisar sus subcarpetas.

2. El código no cuenta con un Caso Base, lo que hace que la función no pare de llamarse a así misma; esto genera un StackOverflowException. Podemos resolver esto de la siguiente manera:
static int Factorial(int n)
{
    if (n < 0)
        throw new ArgumentException("El factorial no está definido para números negativos.");

    if (n == 0 || n == 1)
        return 1;

    return n * Factorial(n - 1);
}

3. Primero, SumarHasta(4) llama a SumarHasta(3), luego este llama a SumarHasta(2) y este último llama a SumarHasta (1), dejando 4 marcos en memoria. Al liberarse lo hacen al revés, de manera ascendente justo como un comportamiento LIFO (Last In, First Out); esto es, el último valor en entrar será el primero en salir.

4. Diré que depende... Un 'for' puede ser bastante útil para funciones simples (además de que consume poca memoria), mientras que una recursividad sirve para ejercicios más complejos; el árbol de directorios que mencioné anteriormente es un ejemplo de que la recursividad es más eficiente en estos aspectos que un for.

5. Siguiendo el código de la práctica, la salida sería la siguiente:
    Apilando -> 3
    Apilando -> 2
    Apilando -> 1
    Apilando -> 0

    <_Caso base alcanzado_>

    Liberando -> 1
    Liberando -> 2
    Liberando -> 3

    <_DESPEGUE_>