using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TGP_Game_Code.States
{
    public class Button
    {
        public string Text = "NaN";

        protected Color Normal = Color.Cyan;
        private Color Current;
        private Color Hover;

        protected Vector2 Position = Vector2.Zero;
        private Rectangle ButtonRectangle;

        protected int StateIndex = -1;

        public Button() { }

        public Button(string text, Vector2 position, Color color, int stateIndex)
        {
            // Assign text, position, color and state index to witch to switch to

            Text = text;
            Position = position;
            Normal = color;
            StateIndex = stateIndex;

            // Assign current color and generate Hover color

            Current = Normal;
            Hover = new Color(Normal.R + 150, Normal.G - 100, Normal.B + 50);
        }

        public virtual void Action()
        {
            // Escape method if StateIndex is -1

            if (StateIndex == -1)
            {
                return;
            }

            // Otherwise set new state to StateIndex

            Main.NewStateIndex = StateIndex;
        }

        public virtual bool Check()
        {
            // Calculate button position relative to the middle of the screen

            ButtonRectangle = new Rectangle((int)(Main.Graphics.PreferredBackBufferWidth / 2 - Main.DefaultFont.MeasureString(Text).X / 2 + Position.X), (int)(Main.Graphics.PreferredBackBufferHeight / 2 + Position.Y), (int)(Main.DefaultFont.MeasureString(Text).X), (int)(Main.DefaultFont.MeasureString(Text).Y));

            // If button is not pointed to set current color to normal and return false

            if (!ButtonRectangle.Contains(Mouse.GetState().X, Mouse.GetState().Y))
            {
                Current = Normal;
                return false;
            }

            // Set current color to hover

            Current = Hover;

            // If left mouse button is not pressed or has already been pressed return false

            if (!(Mouse.GetState().LeftButton == ButtonState.Pressed) || Main.PreviousLeftMouseButtonState)
            {
                return false;
            }
            
            // Play button sound and return true;

            Main.ButtonSound.Play();

            return true;
        }

        public virtual void Draw()
        {
            // Draw button with default font

            Main.SpriteBatch.DrawString(Main.DefaultFont, Text, new Vector2(ButtonRectangle.X, ButtonRectangle.Y), Current);
        }
    }
}