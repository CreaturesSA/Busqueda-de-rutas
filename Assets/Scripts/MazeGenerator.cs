using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public static MazeGenerator mazeGen;
    public int width = 10; // Ancho del laberinto
    public int height = 10;
    public int large = 10;// Altura del laberinto
    public int seed, detail;
    public GameObject piece, wall;
    public static int[,] maze; // Representación del laberinto en un array bidimensional
    public static GameObject[,] map;
    void Start()
    {
        GenerateMaze();
        WallsGenerator wallsGenerator = new WallsGenerator();
        wallsGenerator.Generate(wall, width, height, large, detail, seed);
    }
    void GenerateMaze()
    {
        maze = new int[width, height];
        map = new GameObject[width, height];
        // Inicializar el laberinto
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                maze[i, j] = 1; // 1 representa un muro, 0 representa un camino
            }
        }
        // Llamar al método de generación recursiva

        GeneratePath(1, 1);
        PrintMaze();
    }
    void GeneratePath(int x, int y)
    {
        maze[x, y] = 0; // Marcar la posición actual como parte del camino
        int[] directions = { 0, 1, 2, 3 };
        Shuffle(directions);
        for (int i = 0; i < directions.Length; i++)
        {
            int nextX = x + 2 * (directions[i] == 1 ? 1 : (directions[i] == 3 ? -1 : 0));
            int nextY = y + 2 * (directions[i] == 2 ? 1 : (directions[i] == 0 ? -1 : 0));
            // Verificar si la próxima posición está dentro de los límites y aún no ha sido visitada
            if (nextX > 0 && nextX < width - 1 && nextY > 0 && nextY < height - 1 && maze[nextX, nextY] == 1)
            {
                maze[x + (nextX - x) / 2, y + (nextY - y) / 2] = 0; // Romper el muro entre las posiciones
                GeneratePath(nextX, nextY);
            }
        }
    }
    void Shuffle(int[] array)
    {
        // Método para barajar un array
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
    void PrintMaze()
    {
        for (int j = height - 1; j >= 0; j--)
        {
            for (int i = 0; i < width; i++)
            {
                Vector3 position = new Vector3(i * 1f, 0, j * 1f);
                if (maze[i, j] == 1)
                {
                    map[i, j] = Instantiate(wall, position, Quaternion.identity);
                }
                else
                {
                    map[i, j] = Instantiate(piece, position, Quaternion.identity);
                }

            }

        }
    }

}
