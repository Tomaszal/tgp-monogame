namespace TGP_Game_Code
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