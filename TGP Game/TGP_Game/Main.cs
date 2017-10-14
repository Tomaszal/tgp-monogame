using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TGP_Game
{
    // Main class of the game

    public class Main : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager Graphics;               // Graphics device manager
        public static SpriteFont DefaultFont;                       // Default font
        public static SpriteFont SmallFont;                         // Small font
        public static int Volume = 10;                              // Volume variable (increments of 10%), set to 100% by default

        public static SoundEffect ButtonSound;                      // The sound a button makes when pressed
        
        private static SpriteBatch SpriteBatch;                     // Sprite batch
        
        public Main()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            States.Manager.Initialize(Content);

            // Load different media (fonts and sounds)

            DefaultFont = Content.Load<SpriteFont>("Fonts/Default");
            SmallFont = Content.Load<SpriteFont>("Fonts/Small");
            ButtonSound = Content.Load<SoundEffect>("Sounds/Button");
            
            // Load background music and set it to repeat endlessly

            Song BackgroundMusic = Content.Load<Song>("Sounds/Background");
            MediaPlayer.Play(BackgroundMusic);
            MediaPlayer.IsRepeating = true;
            
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            // Only update game if the window is focused

            if (this.IsActive)
            {
                States.Manager.Update();
            }

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            States.Manager.Draw(SpriteBatch, Content);

            base.Draw(gameTime);
        }

        public void ExitGame() => this.Exit();
    }
}
