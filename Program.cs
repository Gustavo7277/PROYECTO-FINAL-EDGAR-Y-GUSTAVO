using System;

using System.IO;



class Matrices

{

    static double[,] ultimaMatrizResultado;

    static string ultimaOperacion;



    static void Main(string[] args)

    {

        int opcion;

        do

        {

            Console.WriteLine("\n--- Menú de Operaciones con Matrices ---");

            Console.WriteLine("1. Sumar Matrices");

            Console.WriteLine("2. Restar Matrices");

            Console.WriteLine("3. Multiplicar Matrices");

            Console.WriteLine("4. Mostrar Última Operación");

            Console.WriteLine("5. Salir");

            Console.Write("Seleccione una opción: ");



            if (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 1 || opcion > 5)

            {

                Console.WriteLine("Opción inválida. Intente nuevamente.");

                continue;

            }



            if (opcion == 5)

            {

                Console.WriteLine("Gracias por usar el programa. ¡Hasta luego!");

                break;

            }



            switch (opcion)

            {

                case 1:

                    RealizarOperacion(SumarMatrices, "Suma");

                    break;

                case 2:

                    RealizarOperacion(RestarMatrices, "Resta");

                    break;

                case 3:

                    RealizarOperacion(MultiplicarMatrices, "Multiplicación");

                    break;

                case 4:

                    MostrarUltimaOperacion();

                    break;

            }

        } while (true);

    }



    static void RealizarOperacion(Func<double[,], double[,], double[,]> operacion, string nombreOperacion)

    {

        Console.WriteLine($"\n--- {nombreOperacion} de Matrices ---");

        int filas, columnas;



        Console.Write("Ingrese el número de filas de las matrices: ");

        filas = LeerDimension();



        Console.Write("Ingrese el número de columnas de las matrices: ");

        columnas = LeerDimension();



        double[,] matriz1 = LeerMatriz(filas, columnas, 1);

        double[,] matriz2 = LeerMatriz(filas, columnas, 2);



        double[,] resultado = operacion(matriz1, matriz2);



        Console.WriteLine($"\nEl resultado de la {nombreOperacion} es:");

        MostrarMatriz(resultado);



        ultimaMatrizResultado = resultado;

        ultimaOperacion = nombreOperacion;



        GuardarMatrizEnArchivo(resultado, nombreOperacion);



        Console.WriteLine("¿Desea realizar otra operación? (s/n): ");

        if (Console.ReadLine().ToLower() != "s")

        {

            Console.WriteLine("Regresando al menú principal...");

        }

    }



    static int LeerDimension()

    {

        int dimension;

        while (!int.TryParse(Console.ReadLine(), out dimension) || dimension <= 0)

        {

            Console.WriteLine("Valor inválido. Por favor, ingrese un número entero positivo.");

        }

        return dimension;

    }



    static double[,] LeerMatriz(int filas, int columnas, int numeroMatriz)

    {

        double[,] matriz = new double[filas, columnas];

        Console.WriteLine($"Ingrese los valores de la matriz {numeroMatriz}:");

        for (int i = 0; i < filas; i++)

        {

            for (int j = 0; j < columnas; j++)

            {

                Console.Write($"Elemento [{i + 1},{j + 1}]: ");

                while (!double.TryParse(Console.ReadLine(), out matriz[i, j]))

                {

                    Console.WriteLine("Valor inválido. Por favor, ingrese un número.");

                }

            }

        }

        return matriz;

    }



    static void MostrarMatriz(double[,] matriz)

    {

        int filas = matriz.GetLength(0);

        int columnas = matriz.GetLength(1);



        for (int i = 0; i < filas; i++)

        {

            for (int j = 0; j < columnas; j++)

            {

                Console.Write($"{matriz[i, j]} ");

            }

            Console.WriteLine();

        }

    }



    static double[,] SumarMatrices(double[,] matriz1, double[,] matriz2)

    {

        int filas = matriz1.GetLength(0);

        int columnas = matriz1.GetLength(1);

        double[,] resultado = new double[filas, columnas];



        for (int i = 0; i < filas; i++)

        {

            for (int j = 0; j < columnas; j++)

            {

                resultado[i, j] = matriz1[i, j] + matriz2[i, j];

            }

        }



        return resultado;

    }



    static double[,] RestarMatrices(double[,] matriz1, double[,] matriz2)

    {

        int filas = matriz1.GetLength(0);

        int columnas = matriz1.GetLength(1);

        double[,] resultado = new double[filas, columnas];



        for (int i = 0; i < filas; i++)

        {

            for (int j = 0; j < columnas; j++)

            {

                resultado[i, j] = matriz1[i, j] - matriz2[i, j];

            }

        }



        return resultado;

    }



    static double[,] MultiplicarMatrices(double[,] matriz1, double[,] matriz2)

    {

        int filas = matriz1.GetLength(0);

        int columnas = matriz2.GetLength(1);

        int sumas = matriz1.GetLength(1);

        double[,] resultado = new double[filas, columnas];



        for (int i = 0; i < filas; i++)

        {

            for (int j = 0; j < columnas; j++)

            {

                for (int k = 0; k < sumas; k++)

                {

                    resultado[i, j] += matriz1[i, k] * matriz2[k, j];

                }

            }

        }



        return resultado;

    }



    static void GuardarMatrizEnArchivo(double[,] matriz, string nombreOperacion)

    {

        string fileName = $"{nombreOperacion}_Resultado.txt";

        using (StreamWriter writer = new StreamWriter(fileName))

        {

            int filas = matriz.GetLength(0);

            int columnas = matriz.GetLength(1);



            writer.WriteLine($"{nombreOperacion} de Matrices:");

            for (int i = 0; i < filas; i++)

            {

                for (int j = 0; j < columnas; j++)

                {

                    writer.Write($"{matriz[i, j]} ");

                }

                writer.WriteLine();

            }

        }



        Console.WriteLine($"La Matriz Resultante del Calculo Elegido fue almacenada en el archivo {fileName}");

    }



    static void MostrarUltimaOperacion()

    {

        if (ultimaMatrizResultado == null)

        {

            Console.WriteLine("No se ha realizado ninguna operación aún.");

            return;

        }



        Console.WriteLine($"\nÚltima Operación Realizada: {ultimaOperacion}");

        MostrarMatriz(ultimaMatrizResultado);

    }

}
