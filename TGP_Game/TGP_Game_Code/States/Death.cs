using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TGP_Game_Code.States
{
    class Death : State
    {
        int HearthIndex;
        Rectangle HearthRectangle = new Rectangle(0, 0, 80, 80);

        private class RespawnButton : Button
        {
            public RespawnButton(string text, Vector2 position, Color color, int index) : base(text, position, color, index) { }

            public override void Action()
            {
                Map.Map.Player.Respawn();

                base.Action();
            }
        }

        public Death()
        {
            // Add texts

            Texts.Add(new Text(Color.IndianRed, new Vector2(0, -240), "You have died!"));
            Texts.Add(new Text(Color.White, new Vector2(0, -120), "Lifes left:"));
            Texts.Add(new Text(Color.White, new Vector2(0, 90), "Your score:"));

            // Add button

            Buttons.Add(new RespawnButton("Press R to respawn.", new Vector2(0, 180), Color.LightSlateGray, 4));
        }

        public override void Update(GameTime gameTime)
        {
            // Update texts and button

            if (Map.Map.Player.Lifes == 0)
            {
                Texts[0].TextString = "You have lost!";
                Texts[1].TextString = "";

                Buttons[0].StateIndex = 0;
                Buttons[0].Text = "Press R to return to main menu.";

                Map.Map.GameOn = false;
            }
            else
            {
                Texts[0].TextString = "You have died!";
                Texts[1].TextString = "Lifes left:";

                Buttons[0].StateIndex = 4;
                Buttons[0].Text = "Press R to respawn.";
            }

            Texts[2].TextString = "Your score: " + (Map.Map.Player.ScoreKilled + Map.Map.Player.ScoreWalked);

            // Check for R key press

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                if (Map.Map.Player.Lifes != 0) Map.Map.Player.Respawn();

                Main.NewStateIndex = Buttons[0].StateIndex;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Draw background

            DrawMenuBackground();

            // Draw hearths

            HearthRectangle.Y = Main.Graphics.PreferredBackBufferHeight / 2 - 40;

            for (HearthIndex = 1; HearthIndex <= Map.Map.Player.Lifes; HearthIndex++)
            {
                HearthRectangle.X = Main.Graphics.PreferredBackBufferWidth / 2 + 40 * Map.Map.Player.Lifes - 80 * HearthIndex;

                Main.SpriteBatch.Draw(Main.Hearth, HearthRectangle, Color.White);
            }

            base.Draw(gameTime);
        }
    }
}
