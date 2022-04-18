using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_2
{
    internal class Program
    {
        private static char player = '@';
        private static char finish = 'F';
        private static char wall = '#';
        private static char emptyCell = ' ';

        private static int width = 50;
        private static int height = 25;
        private static int mazeUpperLeftCorner = 0;
        private static int mazeUpperRightCorner = width - 1;
        private static int mazeLowerLeftCorner = height - 1;

        private static ConsoleKey regenerateButton = ConsoleKey.Spacebar;

        private static int playerX = 1;
        private static int playerY = 1;
        private static int finishX = width - 3;
        private static int finishY = height - 3;

        private static Random rnd = new Random();

        private static int blockFreq = 30;

        static void Main(string[] args)
        {
            char[,] field = MazeCreating();

            ConsoleKeyInfo key  = new ConsoleKeyInfo();

            while (!IsGameEnd())
            {
                if(key.Key == regenerateButton) field = MazeRegeneration();
                MazeGeneration(field);
                (int dx, int dy) = MovingInput();
                (int newX, int newY) = MoveLogic(dx, dy);
                TryMove(field, newX, newY);
            }

            Console.Clear();
            EndGameMessage();
            Console.ReadKey();
        }

        private static char[,] MazeCreating()
        {
            char[,] field = new char[width, height];
            char cell;

            for (int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    int value = rnd.Next(0, 100);

                    if (value < blockFreq || i == mazeUpperLeftCorner || j == mazeUpperLeftCorner || i == mazeLowerLeftCorner || j == mazeUpperRightCorner)
                    {
                        cell = wall;
                    }
                    else
                    {
                        cell = emptyCell;
                    }

                    field[j, i] = cell;
                }
            }
            return field;
        }

        private static void MazeGeneration(char[,] field)
        {
            Console.Clear();
            char cell;

            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    if(j == playerX && i == playerY)
                    {
                        cell = player;
                    }
                    else if(j == finishX && i == finishY)
                    {
                        cell = finish;
                        field[j, i] = cell;
                    }
                    else
                    {
                        cell = field[j, i];
                    }
                    Console.Write(cell);
                }
                Console.WriteLine();
            }
        }

        private static char[,] MazeRegeneration()
        {
            char[,] newField = new char[width, height];

            playerX = 1;
            playerY = 1;
            newField = MazeCreating();
            MazeGeneration(newField);
            return newField;
        }
        
        private static (int, int) MovingInput()
        {
            int dx = 0;
            int dy = 0;

            ConsoleKeyInfo moveKey = Console.ReadKey();

            if (moveKey.Key == ConsoleKey.UpArrow) dy = -1;
            else if (moveKey.Key == ConsoleKey.DownArrow) dy = 1;
            else if (moveKey.Key == ConsoleKey.LeftArrow) dx = -1;
            else if (moveKey.Key == ConsoleKey.RightArrow) dx = 1;

            return (dx, dy);
        }

        private static (int, int) MoveLogic(int dx, int dy)
        {
            int newX = playerX;
            int newY = playerY;

            newX += dx;
            newY += dy;

            return (newX, newY);
        }

        private static void TryMove(char[,] field, int newX, int newY)
        {
            if (CanMove(field, newX, newY)) Move(newX, newY);
        }

        private static bool CanMove(char[,] field, int newX, int newY)
        {
            if (field[newX, newY] == wall)
            {
                newX -= 1;
                newY -= 1;
                return false;
            }
            return true;
        }

        private static void Move(int newX, int newY)
        {
            playerX = newX;
            playerY = newY;
        }

        private static bool IsGameEnd()
        {
            if(playerX == finishX && playerY == finishY) return true;
            return false;
        }

        private static void EndGameMessage()
        {
            Console.WriteLine("Congratulations! You won!");
        }
    }
}
