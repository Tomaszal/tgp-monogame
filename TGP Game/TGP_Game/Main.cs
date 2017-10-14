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
        public static GraphicsDeviceManager Graphics;
        public static SpriteBatch SpriteBatch;

        // Content

        public static SpriteFont DefaultFont;
        public static SpriteFont SmallFont;

        public static Texture2D Blank;
        public static Texture2D Logo;
        public static Texture2D Menu;

        public static SoundEffect ButtonSound;

        // Volume variable (increments of 10%), set to 100% by default

        public static int Volume = 10;                              

        // Public variables for changing things from static members

        public static bool SetMouseVisibility;
        public static bool ExitGame;

        public Main()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch for drawing things

            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // Load fonts

            DefaultFont = Content.Load<SpriteFont>("Fonts/Default");
            SmallFont = Content.Load<SpriteFont>("Fonts/Small");

            // Load textures

            Blank = Content.Load<Texture2D>("Textures/Blank");
            Logo = Content.Load<Texture2D>("Textures/Logo");
            Menu = Content.Load<Texture2D>("Textures/Menu");

            // Load sounds

            ButtonSound = Content.Load<SoundEffect>("Sounds/Button");
            
            // Load background music and set it to play it on repeat

            Song BackgroundMusic = Content.Load<Song>("Sounds/Background");
            MediaPlayer.Play(BackgroundMusic);
            MediaPlayer.IsRepeating = true;
        }

        protected override void Update(GameTime gameTime)
        {
            // Only update game if the window is focused

            if (this.IsActive)
            {
                States.Manager.Update();
            }

            IsMouseVisible = SetMouseVisibility;

            if (ExitGame) Exit();

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            States.Manager.Draw();

            base.Draw(gameTime);
        }
    }
}
