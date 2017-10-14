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

            // Update current state

            switch (CurrentState)
            {
                case State.Menu:
                    Menu.Update();
                    break;
                case State.About:
                    About.Update();
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
            }

            // Draw black rectangle over all screen with TransitionAlpha for state transition

            Main.SpriteBatch.Draw(Main.Blank, new Rectangle(0, 0, Main.Graphics.PreferredBackBufferWidth, Main.Graphics.PreferredBackBufferHeight), Color.Black * TransitionAlpha);

            Main.SpriteBatch.End();
        }
    }
}
