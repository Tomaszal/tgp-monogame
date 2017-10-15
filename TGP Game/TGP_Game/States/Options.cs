﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Linq;

namespace TGP_Game.States
{
    class Options : State
    {
        // Resolution parameters

        private static int CurrentResolutionIndex;
        private static int MaxResolutionIndex = 6;

        // Curent volume (increments of 10%), set to 100% by default

        private static int CurrentVolume = 10;

        private class FullScreen : Button
        {
            public FullScreen(string text, Vector2 position) : base(text, position, Color.White, -1) { }

            public override void Action()
            {
                // Toggle full screen

                Main.Graphics.ToggleFullScreen();

                base.Action();
            }
        }

        private class Resolution : Button
        {
            public Resolution(string text, Vector2 position) : base(text, position, Color.White, -1) { }

            public override void Action()
            {
                // Toggle resolution and apply new one

                CurrentResolutionIndex = (CurrentResolutionIndex == MaxResolutionIndex) ? 1 : CurrentResolutionIndex + 1;

                ApplyResolution();

                base.Action();
            }
        }

        private class Volume : Button
        {
            public Volume(string text, Vector2 position) : base(text, position, Color.White, -1) { }

            public override void Action()
            {
                // Toggle volume

                CurrentVolume = (CurrentVolume == 0) ? 10 : CurrentVolume - 1;

                // Set volume of media player and sound effects

                MediaPlayer.Volume = (float)CurrentVolume / 10;
                SoundEffect.MasterVolume = (float)CurrentVolume / 10;

                base.Action();
            }
        }

        private class Difficulty : Button
        {
            public Difficulty(string text, Vector2 position) : base(text, position, Color.White, -1) { }

            public override void Action()
            {
                // Toggle difficulty

                base.Action();
            }
        }

        private class Annotations : Button
        {
            public Annotations(string text, Vector2 position) : base(text, position, Color.White, -1) { }

            public override void Action()
            {
                // Toggle annotations

                base.Action();
            }
        }

        private static void ApplyResolution()
        {
            // Apply resolution according to the index

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
        
        public Options()
        {
            // Add buttons

            Buttons.Add(new FullScreen("Full screen: NaN", new Vector2(0, -200)));
            Buttons.Add(new Resolution("Resolution: NaN", new Vector2(0, -150)));
            Buttons.Add(new Volume("Volume: NaN", new Vector2(0, -100)));
            Buttons.Add(new Difficulty("Difficulty: NaN", new Vector2(0, 20)));
            Buttons.Add(new Annotations("Annotations: NaN", new Vector2(0, 70)));
            Buttons.Add(new Button("Return", new Vector2(0, 180), Color.White, 0));

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

            ApplyResolution();
        }

        public override void Update(GameTime gameTime)
        {
            // Update text of each button that needs it

            Buttons.OfType<FullScreen>().FirstOrDefault().Text = "Fullscreen: " + ((Main.Graphics.IsFullScreen) ? "Yes" : "No");
            Buttons.OfType<Resolution>().FirstOrDefault().Text = "Resolution: " + Main.Graphics.PreferredBackBufferWidth + " x " + Main.Graphics.PreferredBackBufferHeight;
            Buttons.OfType<Volume>().FirstOrDefault().Text = "Volume: " + CurrentVolume * 10 + "%";

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Draw background

            DrawMenuBackground();

            base.Draw(gameTime);
        }
    }
}