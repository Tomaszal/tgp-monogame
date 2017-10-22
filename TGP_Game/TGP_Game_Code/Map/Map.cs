using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace TGP_Game_Code.Map
{
    public static class Map
    {
        // Camera that follows the player

        public static Matrix CameraMatrix;
        public static Vector2 CameraCentre = Vector2.Zero;
        public static Vector2 CameraPosition = Vector2.Zero;

        // Define tile source and tile destination rectangles

        public static Rectangle TileSourceRectangle = new Rectangle(16, 0, 16, 16);
        public static Rectangle TileDestinationRectangle = new Rectangle(32, 32, 32, 32);

        // Create background assisting variables

        public static float BackgroundRatio;
        public static Rectangle BackgroundRectangle;

        // Create a static player

        public static Entity Player = new Player();

        // Create a static map

        public static List<List<Tile>> TileMap = new List<List<Tile>>();
        public static Vector2 MapSize;

        // Create and define all tile types

        public static Tile AirTile = new AirTile();
        public static Tile DirtTile = new DirtTile();
        public static Tile GrassTile = new GrassTile();
        public static Tile WaterTopTile = new WaterTopTile();
        public static Tile WaterBottomTile = new WaterBottomTile();
        public static Tile LightStoneTile = new LightStoneTile();
        public static Tile DarkStoneTile = new DarkStoneTile();
        public static Tile WinTile = new WinTile();
        public static Tile NullTile = new NullTile();

        private static List<Tile> TileTypes = new List<Tile> { AirTile, DirtTile, GrassTile, WaterTopTile, WaterBottomTile, LightStoneTile, DarkStoneTile, WinTile };

        // Others

        private static int X, Y;
        private static uint PackedValueDelta;

        public static void SaveMap(GraphicsDevice graphicsDevice, string mapName)
        {
            // Method to save the tile map to the specified .png file

            // Create a blank map image texture with the size of the tile map

            Texture2D MapImage = new Texture2D(graphicsDevice, (int)MapSize.X, (int)MapSize.Y);

            // Create a blank map data array with the size of the tile map

            Color[] MapData = new Color[(int)(MapSize.X * MapSize.Y)];

            // Convert tile map to map data array

            for (Y = 0; Y < (int)MapSize.Y; Y++)
            {
                for (X = 0; X < (int)MapSize.X; X++)
                {
                    // Set the map data array element to the color code of the tile element

                    MapData[Y * (int)MapSize.X + X] = TileMap[Y][X].ColorCode;
                }
            }

            // Set map image texture to map data array

            MapImage.SetData(MapData);

            // Open stream and save the specified .png file

            Stream Stream = File.Create(mapName + ".png");
            MapImage.SaveAsPng(Stream, (int)MapSize.X, (int)MapSize.Y);

            // Dispose of the stream and map image texture

            Stream.Dispose();
            MapImage.Dispose();
        }
        
        public static void LoadMap(ContentManager contentManager, string mapName)
        {
            // Method to load the tile map from the specified .png file

            // Load map image texture

            Texture2D MapImage = contentManager.Load<Texture2D>(mapName);

            // Set tile map size

            MapSize = new Vector2(MapImage.Width, MapImage.Height);

            // Get map data array from map image texture

            Color[] MapData = new Color[(int)(MapSize.X * MapSize.Y)];
            MapImage.GetData(MapData);

            // Convert map data into tile map

            for (Y = 0; Y < (int)MapSize.Y; Y++)
            {
                // Add a row of tiles to tile map

                TileMap.Add(new List<Tile>());

                for (X = 0; X < (int)MapSize.X; X++)
                {
                    // Add individual tiles

                    foreach (Tile Tile in TileTypes)
                    {
                        // Calculate packed value delta of colors in map data and different tile types

                        PackedValueDelta = Tile.ColorCode.PackedValue - MapData[Y * (int)MapSize.X + X].PackedValue;
                        
                        if (PackedValueDelta <= 10)
                        {
                            // Add tile of matched type

                            TileMap[Y].Add(Tile);

                            // Set player position if delta is 1

                            if (PackedValueDelta == 1) Player.Position.Location = new Point(X * TileDestinationRectangle.Width, Y * TileDestinationRectangle.Height);

                            break;
                        }
                    }

                    // Add a null tile if no tile type matches

                    if (TileMap[Y].Count != X + 1) TileMap[Y].Add(NullTile);
                }
            }

            // Dispose of map image texture

            MapImage.Dispose();

            // Calculate background rectangle based on tile map size

            CalculateBackgroundRectangle();
        }

        private static void CalculateBackgroundRectangle()
        {
            BackgroundRatio = MapSize.Y * TileDestinationRectangle.Height / Main.Background.Height;

            BackgroundRectangle = new Rectangle(0 , 0, (int)(Main.Background.Width * BackgroundRatio), (int)(Main.Background.Height * BackgroundRatio));
        }

        public static void Update(GameTime gameTime)
        {
            // Update the player

            Player.Update(gameTime);

            // Update camera matrix

            // Get screen centre

            CameraCentre.X = Main.Graphics.PreferredBackBufferWidth * 0.5f;
            CameraCentre.Y = Main.Graphics.PreferredBackBufferHeight * 0.5f;

            // Make camera position follow player on X axis if not near map borders, otherwise stick to theborders

            if (Player.Position.X > MapSize.X * TileDestinationRectangle.Width - CameraCentre.X) CameraPosition.X = CameraCentre.X * 2 - MapSize.X * TileDestinationRectangle.Width;
            else if (Player.Position.X < CameraCentre.X) CameraPosition.X = 0;
            else CameraPosition.X = CameraCentre.X - Player.Position.X;

            // Make camera position follow player on Y axis if not near map borders, otherwise stick to the borders

            if (Player.Position.Y > MapSize.Y * TileDestinationRectangle.Height - CameraCentre.Y) CameraPosition.Y = CameraCentre.Y * 2 - MapSize.Y * TileDestinationRectangle.Height;
            else if (Player.Position.Y < CameraCentre.Y) CameraPosition.Y = 0;
            else CameraPosition.Y = CameraCentre.Y - Player.Position.Y;
            
            // Create matrix translation for new camera position

            CameraMatrix = Matrix.CreateTranslation(CameraPosition.X, CameraPosition.Y, 0f);
        }

        public static void Draw(GameTime gameTime)
        {
            // Draw background with parallax effect
            
            for (X = 0; X < (int)(BackgroundRatio); X++)
            {
                BackgroundRectangle.X = X * BackgroundRectangle.Width + (int)(CameraPosition.X / 10);

                Main.SpriteBatch.Draw(Main.Background, BackgroundRectangle, Color.White);
            }
            
            // Draw every tile of the tile map

            for (Y = 0; Y < MapSize.Y; Y++)
            {
                for (X = 0; X < MapSize.X; X++)
                {
                    // Calculate destination rectangle

                    TileDestinationRectangle.X = TileDestinationRectangle.Width * X;
                    TileDestinationRectangle.Y = TileDestinationRectangle.Height * Y;

                    // Draw the tile with appropriate texture

                    Main.SpriteBatch.Draw(Main.Tiles, TileDestinationRectangle, TileMap[Y][X].TexturePosition, Color.White);
                }
            }

            // Draw player

            Player.Draw(gameTime);
        }
    }
}
