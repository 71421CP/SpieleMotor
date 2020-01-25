using Console = System.Console;
using ConsoleColor = System.ConsoleColor;
using Math = System.Math;

namespace SpieleMotor
{
    // Hiervon sollen alle GameObjects erben
    abstract class AEntity
    {
        public Vector2 m_Position;
        public ConsoleColor m_Color;
        public char m_Char;

        public AEntity(Vector2 _position, ConsoleColor _color, char _char)
        {
            m_Position = _position;
            m_Color = _color;
            m_Char = _char;
        }

        public virtual void Update()
        {
            m_Position.m_XPos = Math.Min(Math.Max(m_Position.m_XPos, 0), Console.WindowWidth);
            m_Position.m_YPos = Math.Min(Math.Max(m_Position.m_YPos, 3), Console.WindowHeight - 1);

        }
        public virtual void Render()
        {
            Console.SetCursorPosition(m_Position.m_XPos, m_Position.m_YPos);
            Console.ForegroundColor = m_Color;
            Console.Write(m_Char);
        }
        public virtual void Destroy()
        {
            Game.Get.Destroy(this);
        }
    }
}
