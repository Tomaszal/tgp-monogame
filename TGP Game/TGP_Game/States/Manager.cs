using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGP_Game.States
{
    public static class Manager
    {
        // Define different states

        public enum State
        {
            Menu,
            Character,
            Options,
            About,
            Game,
            Death,
            Win,
        }

        // Current and new states, set to Menu by default

        private static State CurrentState = State.Menu;
        private static State NewState = State.Menu;

        // Alpha value and blank Texture2D to fade the screen when switching states

        private static float TransitionAlpha = 0f;
        private static Texture2D BlankTexture;

        public static void SetNewState(State newState) => NewState = newState;

        public static void Initialize(ContentManager content)
        {
            BlankTexture = content.Load<Texture2D>("Images/BlankTexture");
        }

        public static void Update()
        {
            // Increace TransitionAlpha if NewState is different from CurrentState to fade screen out
            // When TransitionAlpha is 1f (black screen) set CurrentState to NewState
            // Decreace TransitionAlpha when done to fade screen back in

            if (NewState != CurrentState)
            {
                if (TransitionAlpha < 1f) TransitionAlpha += 0.05f;
                else CurrentState = NewState;
            }
            else if (TransitionAlpha > 0f) TransitionAlpha -= 0.05f;
        }

        public static void Draw(SpriteBatch spriteBatch, ContentManager content)
        {
            spriteBatch.Begin();

            // Draw black rectangle over all screen with TransitionAlpha for state transition

            spriteBatch.Draw(BlankTexture, new Rectangle(0, 0, Main.Graphics.PreferredBackBufferWidth, Main.Graphics.PreferredBackBufferHeight), Color.Black * TransitionAlpha);

            spriteBatch.End();
        }
    }
}
