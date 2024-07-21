using System.Drawing;
using System.Runtime.Intrinsics.Arm;

namespace ConsoleProject1
{
    internal class Program
    {
        // 맵 내 무작위 좌표에 위치한 보물을 찾을 시 종료되게 구현하려고 했습니다.
        // 맵 밖으로 나갈 시 종료되는 문제를 해결하지 못했습니다.
        public struct GameData
        {
            public bool running;

            public char[,] map;
            public ConsoleKey inputKey;
            public Point playerPos;
            public Point goalPos;
        }

        public struct Point
        {
            public int x;
            public int y;
        }

        static GameData data;


        static void Start()
        {
            Console.CursorVisible = false;

            data = new GameData();
            Random random = new Random();
            data.running = true;
            data.map = new char[5, 5];
            data.playerPos = new Point()
            {
                x = random.Next(1, data.map.GetLength(0)),
                y = random.Next(1, data.map.GetLength(1))
            };
            data.goalPos = new Point()
            {
                x = random.Next(1, data.map.GetLength(0)),
                y = random.Next(1, data.map.GetLength(1))
            };

            Console.Clear();
            Console.WriteLine("보물 찾기를 시작합니다.");
            Console.WriteLine("플레이어를 이동해 숨겨진 보물을 찾으세요.");
            Console.WriteLine("시작하려면 아무키나 입력하세요");
            Console.ReadKey();
        }
        static void End()
        {
            Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("=                                  =");
            Console.WriteLine("=           게임 클리어!           =");
            Console.WriteLine("=                                  =");
            Console.WriteLine("====================================");
            Console.WriteLine();
        }

        static void Render()
        {
            Console.Clear();

            PrintMap();
            PrintPlayer();
            PrintGoal();
        }
        static void Input()
        {
            data.inputKey = Console.ReadKey(true).Key;
        }

        static void Update()
        {
            Move();
            CheckGameClear();
        }

        static void PrintMap()
        {
            for (int y = 0; y < data.map.GetLength(0); y++)
            {
                for (int x = 0; x < data.map.GetLength(1); x++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("#");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
        static void PrintPlayer()
        {
            Console.SetCursorPosition(data.playerPos.x, data.playerPos.y);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("P");
            Console.ResetColor();
        }
        static void PrintGoal()
        {
            Console.SetCursorPosition(data.goalPos.x, data.goalPos.y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("#");
            Console.ResetColor();
        }
        static void Move()
        {
            switch (data.inputKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    MoveUp();
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    MoveDown();
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    MoveLeft();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    MoveRight();
                    break;
            }
        }
        static void CheckGameClear()
        {
            if (data.playerPos.x == data.goalPos.x &&
                data.playerPos.y == data.goalPos.y)
            {
                data.running = false;
            }
        }

        static void MoveUp()
        {
            Point next = new Point() { x = data.playerPos.x, y = data.playerPos.y - 1 };
            if (data.playerPos.y < data.map.GetLength(0))
            {
                data.playerPos = next;
            }
        }

        static void MoveDown()
        {
            Point next = new Point() { x = data.playerPos.x, y = data.playerPos.y + 1 };
            if (data.playerPos.y < data.map.GetLength(0))
            {
                data.playerPos = next;
            }
        }
        static void MoveLeft()
        {
            Point next = new Point() { x = data.playerPos.x - 1, y = data.playerPos.y };
            if (data.playerPos.x < data.map.GetLength(1))
            {
                data.playerPos = next;
            }
        }

        static void MoveRight()
        {
            Point next = new Point() { x = data.playerPos.x + 1, y = data.playerPos.y };
            if (data.playerPos.x < data.map.GetLength(1))
            {
                data.playerPos = next;
            }
        }

        static void Main(string[] args)
        {

            Start();

            while (data.running)
            {
                Render();
                Input();
                Update();
            }
            End();
        }
    }
}
