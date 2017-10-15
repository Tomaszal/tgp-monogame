using Microsoft.Xna.Framework;

namespace TGP_Game.States
{
    static class Menu
    {
        // Define each button

        static Button Restart = new Button("Restart", Color.OrangeRed, new Vector2(0, -100));
        static Button Play = new Button("Play", Color.Cyan, new Vector2(0, -50));
        static Button Options = new Button("Options", Color.Cyan, new Vector2(0, 0));
        static Button About = new Button("About", Color.Cyan, new Vector2(0, 50));
        static Button Exit = new Button("Exit", Color.Cyan, new Vector2(0, 100));

        public static void Update()
        {
            // Make mouse visible

            Main.SetMouseVisibility = true;

            // Check each button and set states accordingly

            if (Options.Check())
            {
                Manager.SetNewState(Manager.State.Options);
            }

            if (About.Check())
            {
                Manager.SetNewState(Manager.State.About);
            }

            if (Exit.Check())
            {
                Main.ExitGame = true;
            }

            // If game is not in progress load Character state, otherwise return to Game state

            if (Play.Check())
            {
                if (Play.Text == "Play")
                {
                    Manager.SetNewState(Manager.State.Character);
                }
                else
                {
                    Manager.SetNewState(Manager.State.Game);
                }
            }

            // Only check restart button if game is in progress

            if (Play.Text == "Resume")
            {
                if (Restart.Check()) Manager.SetNewState(Manager.State.Game);
            }
        }

        public static void Draw()
        {
            // Draw background and logo

            Manager.DrawMenuBackground();

            Main.SpriteBatch.Draw(Main.Logo, new Rectangle(Main.Graphics.PreferredBackBufferWidth / 2 - 135, 20, 300, 120), Color.White);

            // Draw all buttons

            Play.Draw();
            Options.Draw();
            About.Draw();
            Exit.Draw();

            // Only draw Restart button if game is in progress

            if (Play.Text == "Resume")
            {
                Restart.Draw();
            }
        }
    }
}
