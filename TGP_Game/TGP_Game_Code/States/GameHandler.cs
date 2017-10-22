using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace TGP_Game_Code.States
{
    class GameHandler : State
    {
        public override void Update(GameTime gameTime)
        {
            Map.Map.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Main.NewStateIndex = 0;
        }

        public override void Draw(GameTime gameTime)
        {
            Map.Map.Draw(gameTime);
        }
    }
}
