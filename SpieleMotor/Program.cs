namespace SpieleMotor
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Get.Initialize();
            Game.Get.Render();
            while (Game.Get.IsRunning)
            {
                Game.Get.Update();
                Game.Get.Render();
            }
            Game.Get.Cleanup();
        }
    }
}
