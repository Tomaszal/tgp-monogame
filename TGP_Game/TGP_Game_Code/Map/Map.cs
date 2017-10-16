using Microsoft.Xna.Framework;

namespace TGP_Game_Code.Map
{
    public static class Map
    {
        public static Entity Player;

        public static void Initialize(int playerTypeIndex, Vector2 startingPlayerPosition)
        {
            Player = new Player(playerTypeIndex, startingPlayerPosition);
        }
    }
}
