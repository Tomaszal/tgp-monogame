using Microsoft.Xna.Framework;

namespace TGP_Game.States
{
    class About
    {
        // Create and define Return button

        private static Button Return = new Button("Return", Color.Cyan, new Vector2(0, 180));

        public static void Update()
        {
            // Check Return button for a press and set state to Menu

            if (Return.Check())
            {
                Manager.SetNewState(Manager.State.Menu);
            }
        }

        public static void Draw()
        {
            // Draw background

            Main.SpriteBatch.Draw(Main.Menu, new Rectangle(0, 0, Main.Graphics.PreferredBackBufferWidth, Main.Graphics.PreferredBackBufferHeight), Color.White);

            // Draw 'about' text

            Manager.DrawText(Color.White, new Vector2(0, -200), "Version 0.02 (rework)");
            Manager.DrawText(Color.White, new Vector2(0, -120), "Developed by:");
            Manager.DrawText(Color.White, new Vector2(0, -90), "Tomas Zaluckij");
            Manager.DrawText(Color.White, new Vector2(0, -60), "(@Tomaszal)");
            Manager.DrawText(Color.White, new Vector2(0, 0), "Music: Torrey Desmond Rogers");
            Manager.DrawText(Color.White, new Vector2(0, 30), "Sound effects: SoundBible.com");
            Manager.DrawText(Color.White, new Vector2(0, 100), "Background: Wyatt S. Miles (flashpotatoes)");

            // Draw Return button

            Return.Draw();
        }
    }
}
