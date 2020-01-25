using System.Collections.Generic;

using Console = System.Console;
using ConsoleKeyInfo = System.ConsoleKeyInfo;
using ConsoleKey = System.ConsoleKey;

namespace SpieleMotor.Entities
{
    class Player : AEntity
    {
        public int Lives { get; private set; } = 3;
        public int Coins { get; private set; }

        public Player(Vector2 _position) : base(_position, System.ConsoleColor.Magenta, '¤')
        {

        }

        public override void Update()
        {
            int xMove = 0;
            int yMove = 0;

            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape)
            {
                Game.Get.Shutdown();
            }
            if (key.Key == ConsoleKey.LeftArrow)
            {
                xMove--;
            }
            if (key.Key == ConsoleKey.RightArrow)
            {
                xMove++;
            }
            if (key.Key == ConsoleKey.UpArrow)
            {
                yMove--;
            }
            if (key.Key == ConsoleKey.DownArrow)
            {
                yMove++;
            }
            m_Position.m_XPos += xMove;
            m_Position.m_YPos += yMove;

            List<AEntity> calls = Game.Get.CollisionWith(this);

            foreach(AEntity entity in calls)
            {
                if (entity is Coin)
                {
                    Game.Get.MyPlayer.Coins++;
                    entity.Destroy();
                }
                else if(entity is Ghost)
                {
                    Destroy();

                }
                else if(entity is Wall)
                {
                    m_Position.m_XPos -= xMove;
                    m_Position.m_YPos -= yMove;
                }
            }

            base.Update();
        }
        public override void Destroy()
        {
            if (Coins >= 40)
            {
                base.Destroy();

                Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2 - 1);
                Console.Write("Congratulations");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 3, Console.WindowHeight / 2);
                Console.Write("You won");
                Console.Beep(600, 600);
                Console.Beep(650, 600);
                Console.Beep(700, 700);
                Console.Beep(750, 1000);
                Console.ReadKey();

                Game.Get.Shutdown();
            }
            else if (Lives > 1)
            {
                Lives--;
                Game.Get.MyPlayer.m_Position = Game.Get.GetRandomPos();
                Console.Beep(600, 200);
                Console.Beep(600, 200);
            }
            else
            {
                base.Destroy();

                Console.SetCursorPosition(Console.WindowWidth / 2 - 3, Console.WindowHeight / 2 - 1);
                Console.Write("You Lost");
                Console.Beep(650, 600);
                Console.Beep(600, 600);
                Console.Beep(550, 700);
                Console.Beep(500, 1000);
                Console.ReadKey();

                Game.Get.Shutdown();
            }
        }
    }
}
