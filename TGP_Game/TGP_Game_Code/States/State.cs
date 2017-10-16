using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TGP_Game_Code.States
{
    public class State
    {
        public List<Button> Buttons = new List<Button>();
        public List<Text> Texts = new List<Text>();

        protected void DrawMenuBackground()
        {
            // Method to draw menu background

            Main.SpriteBatch.Draw(Main.Menu, new Rectangle(0, 0, Main.Graphics.PreferredBackBufferWidth, Main.Graphics.PreferredBackBufferHeight), Color.White);
        }

        public State() { }

        public virtual void Update(GameTime gameTime)
        {
            // Check each button in list for a press and act accordingly

            foreach (Button button in Buttons)
            {
                if(button.Check())
                {
                    button.Action();
                }
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            // Draw each text in list

            foreach (Text text in Texts)
            {
                text.Draw();
            }

            // Draw each button in list

            foreach (Button button in Buttons)
            {
                button.Draw();
            }
        }
    }
}