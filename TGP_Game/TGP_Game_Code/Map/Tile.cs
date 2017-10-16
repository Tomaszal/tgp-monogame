using Microsoft.Xna.Framework;

namespace TGP_Game_Code.Map
{
    public class Tile
    {
        public Rectangle TexturePosition;
        public bool Collide = true;
    }

    public class AirTile : Tile
    {
        public AirTile()
        {
            TexturePosition = new Rectangle(0 * Map.TileSizeSource, 0, Map.TileSizeSource, Map.TileSizeSource);
            Collide = false;
        }
    }

    public class DirtTile : Tile
    {
        public DirtTile()
        {
            TexturePosition = new Rectangle(1 * Map.TileSizeSource, 0, Map.TileSizeSource, Map.TileSizeSource);
        }
    }

    public class GrassTile : Tile
    {
        public GrassTile()
        {
            TexturePosition = new Rectangle(2 * Map.TileSizeSource, 0, Map.TileSizeSource, Map.TileSizeSource);
        }
    }

    public class WaterTopTile : Tile
    {
        public WaterTopTile()
        {
            TexturePosition = new Rectangle(3 * Map.TileSizeSource, 0, Map.TileSizeSource, Map.TileSizeSource);
        }
    }

    public class WaterBottomTile : Tile
    {
        public WaterBottomTile()
        {
            TexturePosition = new Rectangle(4 * Map.TileSizeSource, 0, Map.TileSizeSource, Map.TileSizeSource);
        }
    }

    public class LightStoneTile : Tile
    {
        public LightStoneTile()
        {
            TexturePosition = new Rectangle(5 * Map.TileSizeSource, 0, Map.TileSizeSource, Map.TileSizeSource);
        }
    }

    public class WinTile : Tile
    {
        public WinTile()
        {
            TexturePosition = new Rectangle(9 * Map.TileSizeSource, 0, Map.TileSizeSource, Map.TileSizeSource);
        }
    }

    public class DarkStoneTile : Tile
    {
        public DarkStoneTile()
        {
            TexturePosition = new Rectangle(10 * Map.TileSizeSource, 0, Map.TileSizeSource, Map.TileSizeSource);
        }
    }
}
