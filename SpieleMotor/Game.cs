using System.Collections.Generic;
using SpieleMotor.Entities;

using Console = System.Console;

namespace SpieleMotor
{
    class Game
    {
        static Game s_game; // Singleton
        private List<AEntity> m_entities = new List<AEntity>();
        private List<AEntity> m_entitiesToRemove = new List<AEntity>();
        private int m_stepCounter;
        public System.Random m_rdm = new System.Random();

        public bool IsRunning { get; private set; }
        public Player MyPlayer { get; private set; }
        public static Game Get
        {
            get
            {
                if ( s_game == null)
                {
                    s_game = new Game();
                }

                return s_game;
             }
        }

        private Game()
        {

        }

        public void Initialize()
        {
            Console.CursorVisible = false;
            IsRunning = true;

            LoadLevel();
        }
        public void Update()
        {
            foreach(AEntity entity in m_entities)
            {
                entity.Update();
            }

            foreach (AEntity entity in m_entitiesToRemove)
            {
                m_entities.Remove(entity);
            }
            if (MyPlayer.Coins >= 40)
            {
                MyPlayer.Destroy();
            }
        }
        public void Render()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 1);
            Console.Write("Steps: {0}   Lives: {1}   Coins: {2}/40", m_stepCounter++, MyPlayer.Lives, MyPlayer.Coins);
            foreach (AEntity entity in m_entities)
            {
                entity.Render();
            }
            Console.ResetColor();
        }
        public void Cleanup()
        {
        }
        public void Shutdown()
        {
            IsRunning = false;
        }

        private void LoadLevel()
        {
            for(int i = 3; i <= Console.WindowHeight; i++)
            {
                m_entities.Add(new Wall(new Vector2(0, i)));
            }
            for (int i = 3; i <= Console.WindowHeight; i++)
            {
                m_entities.Add(new Wall(new Vector2(Console.WindowWidth - 1, i)));
            }
            for (int i = 1; i < Console.WindowWidth - 1; i++)
            {
                m_entities.Add(new Wall(new Vector2(i, 3)));
            }
            for (int i = 1; i < Console.WindowWidth - 1; i++)
            {
                m_entities.Add(new Wall(new Vector2(i, Console.WindowHeight)));
            }

            for (int i = 3 + Console.WindowHeight / 4; i < Console.WindowHeight / 1.3f; i++)
            {
                m_entities.Add(new Wall(new Vector2(Console.WindowWidth / 4, i)));
            }
            for (int i = 3 + Console.WindowHeight / 4; i < Console.WindowHeight / 1.3f; i++)
            {
                m_entities.Add(new Wall(new Vector2(Console.WindowWidth / 4 * 3, i)));
            }
            for (int i = Console.WindowWidth / 4 + 1; i < Console.WindowWidth / 4 * 3; i++)
            {
                m_entities.Add(new Wall(new Vector2(i, (int)(Console.WindowHeight / 1.3f))));
            }

            for (int i = 0; i < 40; i++)
            {
                m_entities.Add(new Coin(GetRandomPos()));
            }
            for (int i = 0; i < 4; i++)
            {
                m_entities.Add(new Ghost(GetRandomPos()));
            }
            MyPlayer = new Player(GetRandomPos());
            m_entities.Add(MyPlayer);

        }
        public List<AEntity> CollisionWith(AEntity _source)
        {
            List<AEntity> collisions = new List<AEntity>();

            foreach(AEntity entity in m_entities)
            {
                if(entity == _source)
                {
                    continue;
                }
                if(_source.m_Position.m_XPos == entity.m_Position.m_XPos
                    && _source.m_Position.m_YPos == entity.m_Position.m_YPos)
                {
                    collisions.Add(entity);
                }
            }

            return collisions;
        }
        public void Destroy(AEntity _entity)
        {
            m_entitiesToRemove.Add(_entity);
        }
        public Vector2 GetRandomPos()
        {
            Vector2 pos = new Vector2(m_rdm.Next(0, Console.WindowWidth - 1), m_rdm.Next(3, Console.WindowHeight - 1));

            foreach (AEntity entity in m_entities)
            {
                if (pos.m_XPos == entity.m_Position.m_XPos
                    && pos.m_YPos == entity.m_Position.m_YPos)
                {
                    return GetRandomPos();
                }
            }
            return pos;
        }
    }
}
