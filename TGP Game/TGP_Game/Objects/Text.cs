using Microsoft.Xna.Framework;

namespace TGP_Game.States
{
    public class Text
    {
        public string TextString;
        public Color Color;
        protected Vector2 Position;

        public Text(Color color, Vector2 position, string text)
        {
            // Assign color, position and text

            TextString = text;
            Position = position;
            Color = color;
        }

        public virtual void Draw()
        {
            // Draw text with default font

            Main.SpriteBatch.DrawString(Main.DefaultFont, TextString, new Vector2(Main.Graphics.PreferredBackBufferWidth / 2 - Main.DefaultFont.MeasureString(TextString).X / 2 + Position.X, Main.Graphics.PreferredBackBufferHeight / 2 + Position.Y), Color);
        }
    }
}