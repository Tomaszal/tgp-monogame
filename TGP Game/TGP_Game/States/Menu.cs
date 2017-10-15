using Microsoft.Xna.Framework;
using System.Linq;

namespace TGP_Game.States
{
    class Menu : State
    {
        private class Play : Button
        {
            public Play(string text, Vector2 position) : base(text, position, Color.White, -1) { }

            public override void Action()
            {
                // Set new state to character selection screen if no game is in progress, otherwise set new state back to game screen

                if (Text == "Play")
                {
                    Main.NewStateIndex = 3;
                }
                else
                {
                    Main.NewStateIndex = 4;
                }

                base.Action();
            }
        }
        
        private class Restart : Button
        {
            Button Play;

            public Restart(string text, Vector2 position, int stateIndex, Button play) : base(text, position, Color.White, stateIndex)
            {
                Play = play;
            }

            public override bool Check()
            {
                // Only check restart button if game is in progress (if it can be resumed)

                if (Play.Text == "Resume")
                {
                    return base.Check();
                }

                return false;
            }

            public override void Draw()
            {
                // Only check restart button if game is in progress (if it can be resumed)

                if (Play.Text == "Resume")
                {
                    base.Draw();
                }
            }
        }

        private class Exit : Button
        {
            private Game Instance;

            public Exit(string text, Vector2 position, Game instance) : base(text, position, Color.White, -1)
            {
                Instance = instance;
            }

            public override void Action()
            {
                // Exit the game
                
                Instance.Exit();

                base.Action();
            }
        }

        public Menu(Game instance)
        {
            // Add buttons

            Buttons.Add(new Play("Play", new Vector2(0, -50)));
            Buttons.Add(new Restart("Restart", new Vector2(0, -100), 1, Buttons.OfType<Play>().FirstOrDefault()));
            Buttons.Add(new Button("Options", new Vector2(0, 0), Color.White, 1));
            Buttons.Add(new Button("About", new Vector2(0, 50), Color.White, 2));
            Buttons.Add(new Exit("Exit", new Vector2(0, 100), instance));
        }

        public override void Draw(GameTime gameTime)
        {
            // Draw background and logo

            DrawMenuBackground();

            Main.SpriteBatch.Draw(Main.Logo, new Rectangle(Main.Graphics.PreferredBackBufferWidth / 2 - 135, 20, 300, 120), Color.White);

            base.Draw(gameTime);
        }
    }
}