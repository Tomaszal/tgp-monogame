using Microsoft.Xna.Framework;

namespace TGP_Game.States
{
    class About
    {
        // Create and define Return button

        private static Button Return = new Button("Return", Color.Cyan, new Vector2(0, 170));

        private static void DrawText(Vector2 position, string text)
        {
            // Draw text at position relative to the middle of the screen with DefaultFont

            Main.SpriteBatch.DrawString(Main.DefaultFont, text, new Vector2(Main.Graphics.PreferredBackBufferWidth / 2 - Main.DefaultFont.MeasureString(text).X / 2 + position.X, Main.Graphics.PreferredBackBufferHeight / 2 + position.Y), Color.White);
        }

        public static void Update()
        {
            // Check return button for a press and set state to Menu

            if (Return.Check()) Manager.SetNewState(Manager.State.Menu);
        }

        public static void Draw()
        {
            // Draw background

            Main.SpriteBatch.Draw(Main.Menu, new Rectangle(0, 0, Main.Graphics.PreferredBackBufferWidth, Main.Graphics.PreferredBackBufferHeight), Color.White);

            // Draw 'about' text

            DrawText(new Vector2(0, -200), "Version 0.01 (rework)");
            DrawText(new Vector2(0, -120), "Developed by:");
            DrawText(new Vector2(0, -90), "Tomas Zaluckij");
            DrawText(new Vector2(0, -60), "(@Tomaszal)");
            DrawText(new Vector2(0, 0), "Music: Torrey Desmond Rogers");
            DrawText(new Vector2(0, 30), "Sound effects: SoundBible.com");
            DrawText(new Vector2(0, 100), "Background: Wyatt S. Miles (flashpotatoes)");

            // Draw Return button

            Return.Draw();
        }
    }
}
