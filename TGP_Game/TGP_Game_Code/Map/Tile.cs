using Microsoft.Xna.Framework;

namespace TGP_Game_Code.Map
{
    public class Tile
    {
        public static Rectangle TexturePosition;
        public static bool Collide = true;
    }

    public class AirTile : Tile
    {
        public AirTile()
        {
            TexturePosition = new Rectangle(0 * 32, 0, Map.TileSizeSource, Map.TileSizeSource);
            Collide = false;
        }
    }

    public class DirtTile : Tile
    {
        public DirtTile()
        {
            TexturePosition = new Rectangle(1 * 32, 0, Map.TileSizeSource, Map.TileSizeSource);
        }
    }

    public class GrassTile : Tile
    {
        public GrassTile()
        {
            TexturePosition = new Rectangle(2 * 32, 0, Map.TileSizeSource, Map.TileSizeSource);
        }
    }

    public class WaterTopTile : Tile
    {
        public WaterTopTile()
        {
            TexturePosition = new Rectangle(3 * 32, 0, Map.TileSizeSource, Map.TileSizeSource);
        }
    }

    public class WaterBottomTile : Tile
    {
        public WaterBottomTile()
        {
            TexturePosition = new Rectangle(4 * 32, 0, Map.TileSizeSource, Map.TileSizeSource);
        }
    }

    public class LightStoneTile : Tile
    {
        public LightStoneTile()
        {
            TexturePosition = new Rectangle(5 * 32, 0, Map.TileSizeSource, Map.TileSizeSource);
        }
    }

    public class WinTile : Tile
    {
        public WinTile()
        {
            TexturePosition = new Rectangle(9 * 32, 0, Map.TileSizeSource, Map.TileSizeSource);
        }
    }

    public class DarkStoneTile : Tile
    {
        public DarkStoneTile()
        {
            TexturePosition = new Rectangle(10 * 32, 0, Map.TileSizeSource, Map.TileSizeSource);
        }
    }
}
