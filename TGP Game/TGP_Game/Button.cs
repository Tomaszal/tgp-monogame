using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TGP_Game
{
    class Button
    {
        public string Text;

        private Color Current;
        private Color Normal;
        private Color Hover;

        private Vector2 Position;

        private Rectangle ButtonRectangle;

        public Button(string text, Color color, Vector2 position)
        {
            // Assign color value & generate hover color
            
            Current = color;
            Normal = color;
            Hover = new Color(color.R + 150, color.G - 100, color.B + 50);

            // Assign position relative to the middle of the screen and text

            Position = position;
            Text = text;
        }

        public bool Check()
        {
            // Calculate button position relative to the middle of the screen

            ButtonRectangle = new Rectangle((int)(Main.Graphics.PreferredBackBufferWidth / 2 - Main.DefaultFont.MeasureString(Text).X / 2 + Position.X), (int)(Main.Graphics.PreferredBackBufferHeight / 2 + Position.Y), (int)(Main.DefaultFont.MeasureString(Text).X), (int)(Main.DefaultFont.MeasureString(Text).Y));

            // If button is not pointed to set Current color to Normal and return false

            if (!ButtonRectangle.Contains(Mouse.GetState().X, Mouse.GetState().Y))
            {
                Current = Normal;
                return false;
            }

            // Set Current color to Hover

            Current = Hover;

            // If left mouse button is not pressed or has already been pressed return false

            if (!(Mouse.GetState().LeftButton == ButtonState.Pressed) || States.Manager.PreviousMouse1State)
            {
                return false;
            }
            
            // Play ButtonSound and return true;

            Main.ButtonSound.Play();
            return true;
        }

        public void Draw()
        {
            // Draw button with DefaultFont

            Main.SpriteBatch.DrawString(Main.DefaultFont, Text, new Vector2(ButtonRectangle.X, ButtonRectangle.Y), Current);
        }
    }
}
