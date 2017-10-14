using Microsoft.Xna.Framework;

namespace TGP_Game.States
{
    static class Menu
    {
        // Create and define every button

        static Button Restart = new Button("Restart", Color.OrangeRed, new Vector2(0, -100));
        static Button Play = new Button("Play", Color.Cyan, new Vector2(0, -50));
        static Button Options = new Button("Options", Color.Cyan, new Vector2(0, 0));
        static Button About = new Button("About", Color.Cyan, new Vector2(0, 50));
        static Button Exit = new Button("Exit", Color.Cyan, new Vector2(0, 100));

        public static void Update()
        {
            // Make mouse visible

            Main.SetMouseVisibility = true;

            // Check every button for a press and set states accordingly
            
            if (Options.Check()) Manager.SetNewState(Manager.State.Options);
            if (About.Check()) Manager.SetNewState(Manager.State.About);
            if (Exit.Check()) Main.ExitGame = true;

            // If game is not in progress load Character state, otherwise return to Game state

            if (Play.Check())
                if (Play.GetText() == "Play") Manager.SetNewState(Manager.State.Character);
                else Manager.SetNewState(Manager.State.Game);

            // Only check restart button if game is in progress

            if (Play.GetText() == "Resume")
                if (Restart.Check()) Manager.SetNewState(Manager.State.Game);
        }

        public static void Draw()
        {
            // Draw background and logo

            Main.SpriteBatch.Draw(Main.Menu, new Rectangle(0, 0, Main.Graphics.PreferredBackBufferWidth, Main.Graphics.PreferredBackBufferHeight), Color.White);

            Main.SpriteBatch.Draw(Main.Logo, new Rectangle(Main.Graphics.PreferredBackBufferWidth / 2 - 135, 20, 300, 120), Color.White);

            // Draw every button

            Play.Draw();
            Options.Draw();
            About.Draw();
            Exit.Draw();

            // Only draw Restart button if game is in progress

            if (Play.GetText() == "Resume") Restart.Draw();
        }
    }
}
