using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGP_Game.States
{
    class Options
    {
        // Create and define all buttons

        private static Button Fullscreen = new Button("Fullscreen: NaN", Color.Cyan, new Vector2(0, -200));
        private static Button Resolution = new Button("Resolution: NaN", Color.Cyan, new Vector2(0, -150));
        private static Button Volume = new Button("Volume: NaN", Color.Cyan, new Vector2(0, -100));
        private static Button Difficulty = new Button("Difficulty: NaN", Color.Cyan, new Vector2(0, 20));
        private static Button Annotations = new Button("Annotations: NaN", Color.Cyan, new Vector2(0, 70));
        private static Button Return = new Button("Return", Color.Cyan, new Vector2(0, 180));

        // Resolution parameters

        private static int CurrentResolutionIndex;
        private static int MaxResolutionIndex = 6;

        // Curent volume (increments of 10%), set to 100% by default

        public static int CurrentVolume = 10;

        private static void ToggleVolume()
        {
            // Toggle Volume

            CurrentVolume = (CurrentVolume == 0) ? 10 : CurrentVolume - 1;

            // Set volume of media player and sound effects

            MediaPlayer.Volume = (float)CurrentVolume / 10;
            SoundEffect.MasterVolume = (float)CurrentVolume / 10;
        }

        private static void SetResolution(bool toggle)
        {
            // Toggle the index if needed

            if (toggle)
            {
                CurrentResolutionIndex = (CurrentResolutionIndex == MaxResolutionIndex) ? 1 : CurrentResolutionIndex + 1;
            }

            // Set resolution according to the index

            switch (CurrentResolutionIndex)
            {
                case 1:
                    Main.Graphics.PreferredBackBufferWidth = 800;
                    Main.Graphics.PreferredBackBufferHeight = 600;
                    break;
                case 2:
                    Main.Graphics.PreferredBackBufferWidth = 1024;
                    Main.Graphics.PreferredBackBufferHeight = 768;
                    break;
                case 3:
                    Main.Graphics.PreferredBackBufferWidth = 1280;
                    Main.Graphics.PreferredBackBufferHeight = 800;
                    break;
                case 4:
                    Main.Graphics.PreferredBackBufferWidth = 1280;
                    Main.Graphics.PreferredBackBufferHeight = 1024;
                    break;
                case 5:
                    Main.Graphics.PreferredBackBufferWidth = 1366;
                    Main.Graphics.PreferredBackBufferHeight = 768;
                    break;
                case 6:
                    Main.Graphics.PreferredBackBufferWidth = 1920;
                    Main.Graphics.PreferredBackBufferHeight = 1080;
                    break;
            }

            Main.Graphics.ApplyChanges();
        }

        public static void Initialize()
        {
            // Detect the most popular screen resolutions

            switch (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)
            {
                // 800 x 600
                case 480000:
                    MaxResolutionIndex = 1;
                    break;
                // 1024 x 768
                case 786432:
                    MaxResolutionIndex = 2;
                    break;
                // 1280 x 800
                case 1024000:
                    MaxResolutionIndex = 3;
                    break;
                // 1280 x 1024
                case 1310720:
                    MaxResolutionIndex = 4;
                    break;
                // 1366 x 768
                case 1049088:
                    MaxResolutionIndex = 5;
                    break;
                // 1920 x 1080
                case 2073600:
                    MaxResolutionIndex = 6;
                    break;
            }

            // Set current resolution to maximum resolution

            CurrentResolutionIndex = MaxResolutionIndex;

            // Apply chosen resolution

            SetResolution(false);
        }

        public static void Update()
        {
            // Update text of buttons

            Fullscreen.Text = "Fullscreen: " + ((Main.Graphics.IsFullScreen) ? "Yes" : "No");
            Resolution.Text = "Resolution: " + Main.Graphics.PreferredBackBufferWidth + " x " + Main.Graphics.PreferredBackBufferHeight;
            Volume.Text = "Volume: " + CurrentVolume * 10 + "%";

            // Check buttons and act accordingly

            if (Fullscreen.Check())
            {
                Main.Graphics.ToggleFullScreen();
            }

            if (Resolution.Check())
            {
                SetResolution(true);
            }

            if (Volume.Check())
            {
                ToggleVolume();
            }

            // WIP
            Difficulty.Check();
            Annotations.Check();

            if (Return.Check())
            {
                Manager.SetNewState(Manager.State.Menu);
            }
        }

        public static void Draw()
        {
            // Draw background

            Main.SpriteBatch.Draw(Main.Menu, new Rectangle(0, 0, Main.Graphics.PreferredBackBufferWidth, Main.Graphics.PreferredBackBufferHeight), Color.White);

            // Draw all buttons

            Fullscreen.Draw();
            Resolution.Draw();
            Volume.Draw();
            Difficulty.Draw();
            Annotations.Draw();
            Return.Draw();
        }
    }
}
