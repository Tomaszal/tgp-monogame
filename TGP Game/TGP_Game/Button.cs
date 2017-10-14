using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TGP_Game
{
    class Button
    {
        private string Text;

        private Color Current;
        private Color Normal;
        private Color Hover;

        private Vector2 Position;

        private Rectangle ButtonRectangle;

        public string GetText()
        {
            return Text;
        }

        public void SetText(string text)
        {
            // Set text value and calculate button position relative to the middle of the screen

            Text = text;

            ButtonRectangle = new Rectangle((int)(Main.Graphics.PreferredBackBufferWidth / 2 - Main.DefaultFont.MeasureString(Text).X / 2 + Position.X), (int)(Main.Graphics.PreferredBackBufferHeight / 2 + Position.Y), (int)(Main.DefaultFont.MeasureString(Text).X), (int)(Main.DefaultFont.MeasureString(Text).Y));
        }

        public Button(string text, Color color, Vector2 position)
        {
            // Assign color value & generate hover color
            
            Current = color;
            Normal = color;
            Hover = new Color(color.R + 150, color.G - 100, color.B + 50);

            // Assign position relative to the middle of the screen and text

            Position = position;
            SetText(text);
        }

        public bool Check()
        {
            // Check if the button is pressed
            
            if (ButtonRectangle.Contains(Mouse.GetState().X, Mouse.GetState().Y))
            {
                // If yes, set current color to hover

                Current = Hover;

                // If left mouse button has just been clicked, return true

                if (Mouse.GetState().LeftButton == ButtonState.Pressed && !States.Manager.PreviousMouse1State)
                {
                    Main.ButtonSound.Play();
                    return true;
                }

                // Otherwise return false, this prevents unwanted activation when holding down left mouse button

                return false;
            }
            else
            {
                // If no, set Current color to Normal and return false

                Current = Normal;

                return false;
            }
        }

        public void Draw()
        {
            // Draw button with DefaultFont

            Main.SpriteBatch.DrawString(Main.DefaultFont, Text, new Vector2(ButtonRectangle.X, ButtonRectangle.Y), Current);
        }
    }
}
