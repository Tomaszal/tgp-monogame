namespace TGP_Game
{
#if WINDOWS
    static class Program
    {
        static void Main(string[] args)
        {
            using (Main Game = new Main())
            {
                Game.Run();
            }
        }
    }
#endif
}