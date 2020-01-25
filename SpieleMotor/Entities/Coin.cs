using Console = System.Console;
using ConsoleColor = System.ConsoleColor;

namespace SpieleMotor.Entities
{
    class Coin : AEntity
    {
        public Coin(Vector2 _position) : base(_position, ConsoleColor.Yellow, 'Θ')
        {

        }

        public override void Destroy()
        {
            Console.Beep(700, 250);

            base.Destroy();
        }
    }
}
