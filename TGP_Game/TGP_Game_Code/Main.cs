using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

// Debug command:
// System.Diagnostics.Debug.WriteLine();

namespace TGP_Game_Code
{
    public class Main : Game
    {
        public static GraphicsDeviceManager Graphics;
        public static SpriteBatch SpriteBatch;

        // States

        private List<States.State> States = new List<States.State>();

        private static int CurrentStateIndex = 0;
        public static int NewStateIndex { private get; set; } = 0;

        // Alpha value to fade the screen when transitioning states

        private static float TransitionAlpha = 0f;

        // Content

        public static SpriteFont DefaultFont;
        public static SpriteFont SmallFont;

        public static Texture2D Blank;
        public static Texture2D Logo;
        public static Texture2D Menu;

        public static Texture2D Entities;
        public static Texture2D Tiles;

        public static SoundEffect ButtonSound;

        // Boolean to check if left mouse button has been pressed in the previous cycle

        public static bool PreviousLeftMouseButtonState;

        private static void TransitionState(GameTime gameTime)
        {
            // If CurrentState matches NewState progress fading in and escape method or escape method if fading in is already done

            if (CurrentStateIndex == NewStateIndex)
            {
                if (TransitionAlpha > 0f)
                {
                    TransitionAlpha -= 0.005f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    return;
                }

                return;
            }

            // If fading out is not done progress it and escape method

            if (TransitionAlpha < 1f)
            {
                TransitionAlpha += 0.005f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                return;
            }

            // Set CurrentState to NewState

            CurrentStateIndex = NewStateIndex;
        }

        public Main() : base()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            // Add states

            // 0 - Main menu
            // 1 - Options menu
            // 2 - About menu
            // 3 - Character selection menu
            // 4 - Map handler (game state)

            States.Add(new States.Menu(this));
            States.Add(new States.Options());
            States.Add(new States.About());
            States.Add(new States.Character());
            States.Add(new States.GameHandler());

            // Make mouse visible and toggle full screen

            IsMouseVisible = true;
            //Graphics.ToggleFullScreen();

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

            Entities = Content.Load<Texture2D>("Textures/Entities");
            Tiles = Content.Load<Texture2D>("Textures/Tiles");

            // Load sounds

            ButtonSound = Content.Load<SoundEffect>("Sounds/Button");
            
            // Load background music and set it to play it on repeat

            Song BackgroundMusic = Content.Load<Song>("Sounds/Background");
            MediaPlayer.Play(BackgroundMusic);
            MediaPlayer.IsRepeating = true;

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            // Only update game if the window is focused

            if (IsActive)
            {
                // Transition to the new state if it is different from the current one

                TransitionState(gameTime);

                // Update current state

                States[CurrentStateIndex].Update(gameTime);

                // Update left mouse button state

                PreviousLeftMouseButtonState = (Mouse.GetState().LeftButton == ButtonState.Pressed);
            }

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

            // Draw current state

            States[CurrentStateIndex].Draw(gameTime);

            // Draw black rectangle over all screen with TransitionAlpha for state transition

            SpriteBatch.Draw(Blank, new Rectangle(0, 0, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight), Color.Black * TransitionAlpha);

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}