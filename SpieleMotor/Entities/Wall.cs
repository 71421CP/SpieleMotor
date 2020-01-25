using ConsoleColor = System.ConsoleColor;

namespace SpieleMotor.Entities
{
    class Wall :AEntity
    {
        public Wall(Vector2 _position) : base(_position, ConsoleColor.DarkBlue, '█')
        {

        }
    }
}
