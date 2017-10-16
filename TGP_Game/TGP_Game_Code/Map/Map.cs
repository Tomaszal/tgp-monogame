using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TGP_Game_Code.Map
{
    public static class Map
    {
        // Define tile sizes

        public static int TileSizeDestination = 32;
        public static int TileSizeSource = 16;

        // Create a static player

        public static Entity Player;

        // Create a static map

        public static List<List<Tile>> TileMap = new List<List<Tile>>();

        // Create and define all tile types

        public static Tile AirTile = new AirTile();
        public static Tile DirtTile = new DirtTile();
        public static Tile GrassTile = new GrassTile();
        public static Tile WaterTopTile = new WaterTopTile();
        public static Tile WaterBottomTile = new WaterBottomTile();
        public static Tile LightStoneTile = new LightStoneTile();
        public static Tile DarkStoneTile = new DarkStoneTile();
        public static Tile WinTile = new WinTile();

        // Variables for counting foreach cycles

        private static int X, Y;

        public static void GenerateTestMap()
        {
            int x, y;

            for (y = 0; y < 10; y++)
            {
                TileMap.Add(new List<Tile>());

                for (x = 0; x < 10; x++)
                {
                    TileMap[y].Add(DirtTile);
                    //if (y < 10)
                    //{
                    //    TileMap[y].Add(AirTile);
                    //}
                    //else
                    //{
                    //    TileMap[y].Add(DirtTile);
                    //}
                }
            }
        }

        public static void Initialize(int playerTypeIndex, Vector2 startingPlayerPosition)
        {
            // Define a static player

            Player = new Player(playerTypeIndex, startingPlayerPosition);

            GenerateTestMap();
        }

        public static void Update(GameTime gameTime)
        {
            Player.Update(gameTime);
        }

        public static void Draw(GameTime gameTime)
        {
            Y = 0;

            foreach (List<Tile> TileRow in TileMap)
            {
                X = 0;

                foreach (Tile Tile in TileRow)
                {
                    //System.Diagnostics.Debug.WriteLine(X + ":" + Y);

                    Main.SpriteBatch.Draw(Main.Tiles, new Rectangle(X * TileSizeDestination, Y * TileSizeDestination, (X + 1) * TileSizeDestination, (Y + 1) * TileSizeDestination), Tile.TexturePosition, Color.White);

                    X++;
                }

                Y++;
            }

            Player.Draw(gameTime);
        }
    }
}
