using Microsoft.Xna.Framework;

namespace TGP_Game_Code.Map
{
    // Tile and derived tile type classes

    public class Tile
    {
        public Rectangle TexturePosition = Map.TileSourceRectangle;
        public bool Collide = true;
        public Color ColorCode = new Color(0, 0, 0);
    }

    public class AirTile : Tile
    {
        public AirTile()
        {
            TexturePosition.X *= 0;
            Collide = false;
            ColorCode = new Color(255, 255, 255);
        }
    }

    public class DirtTile : Tile
    {
        public DirtTile()
        {
            TexturePosition.X *= 1;
            ColorCode = new Color(150, 120, 90);
        }
    }

    public class GrassTile : Tile
    {
        public GrassTile()
        {
            TexturePosition.X *= 2;
            ColorCode = new Color(0, 120, 0);
        }
    }

    public class WaterTopTile : Tile
    {
        public WaterTopTile()
        {
            TexturePosition.X *= 3;
            ColorCode = new Color(70, 120, 180);
        }
    }

    public class WaterBottomTile : Tile
    {
        public WaterBottomTile()
        {
            TexturePosition.X *= 4;
            ColorCode = new Color(70, 120, 120);
        }
    }

    public class LightStoneTile : Tile
    {
        public LightStoneTile()
        {
            TexturePosition.X *= 5;
            ColorCode = new Color(140, 140, 140);
        }
    }

    public class WinTile : Tile
    {
        public WinTile()
        {
            TexturePosition.X *= 6;
            ColorCode = new Color(255, 255, 0);
        }
    }

    public class DarkStoneTile : Tile
    {
        public DarkStoneTile()
        {
            TexturePosition.X *= 7;
            ColorCode = new Color(60, 30, 40);
        }
    }

    public class NullTile : Tile
    {
        public NullTile()
        {
            TexturePosition.X *= 8;
        }
    }
}
