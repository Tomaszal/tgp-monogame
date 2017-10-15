using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TGP_Game.States
{
    static class Manager
    {
        // Define different states

        public enum State
        {
            Menu,
            About,
            Options,
            Character,
            Game,
            Death,
            Win
        }

        // Current and new states, set to Menu by default

        private static State CurrentState = State.Menu;
        private static State NewState = State.Menu;

        public static void SetNewState(State newState) => NewState = newState;

        // Alpha value to fade the screen when switching states

        private static float TransitionAlpha = 0f;

        // Bool to check if left mouse button has previously been pressed

        public static bool PreviousMouse1State;

        private static void Transition()
        {
            // If CurrentState matches NewState progress fading in and exit method

            if (CurrentState == NewState && TransitionAlpha > 0f)
            {
                TransitionAlpha -= 0.05f;
                return;
            }

            // If fading out is not done progress it and exit method

            if (TransitionAlpha < 1f)
            {
                TransitionAlpha += 0.05f;
                return;
            }

            // Set CurrentState to NewState

            CurrentState = NewState;
        }

        public static void DrawText(Color color, Vector2 position, string text)
        {
            // Draw text at position relative to the middle of the screen with DefaultFont

            Main.SpriteBatch.DrawString(Main.DefaultFont, text, new Vector2(Main.Graphics.PreferredBackBufferWidth / 2 - Main.DefaultFont.MeasureString(text).X / 2 + position.X, Main.Graphics.PreferredBackBufferHeight / 2 + position.Y), color);
        }

        public static void Update()
        {
            // If there is a NewState fade screen out, set CurrentState to it and fade screen back in

            Transition();

            // Update current state

            switch (CurrentState)
            {
                case State.Menu:
                    Menu.Update();
                    break;
                case State.About:
                    About.Update();
                    break;
                case State.Options:
                    Options.Update();
                    break;
            }

            // Update left mouse button state

            PreviousMouse1State = (Mouse.GetState().LeftButton == ButtonState.Pressed);
        }

        public static void Draw()
        {
            Main.SpriteBatch.Begin();

            // Draw current state

            switch (CurrentState)
            {
                case State.Menu:
                    Menu.Draw();
                    break;
                case State.About:
                    About.Draw();
                    break;
                case State.Options:
                    Options.Draw();
                    break;
            }

            // Draw black rectangle over all screen with TransitionAlpha for state transition

            Main.SpriteBatch.Draw(Main.Blank, new Rectangle(0, 0, Main.Graphics.PreferredBackBufferWidth, Main.Graphics.PreferredBackBufferHeight), Color.Black * TransitionAlpha);

            Main.SpriteBatch.End();
        }
    }
}
