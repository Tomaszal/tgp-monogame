using Microsoft.Xna.Framework;

namespace TGP_Game_Code.States
{
    class GameHandler : State
    {
        public override void Update(GameTime gameTime)
        {
            Map.Map.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Map.Map.Draw(gameTime);
        }
    }
}
