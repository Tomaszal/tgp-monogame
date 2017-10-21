using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TGP_Game_Code.Map
{
    public class Player : Entity
    {
        public Player(int playerTypeIndex, Vector2 position) : base(playerTypeIndex, position) { }

        public override void Update(GameTime gameTime)
        {
            MoveRight = Keyboard.GetState().IsKeyDown(Keys.D);
            MoveLeft = Keyboard.GetState().IsKeyDown(Keys.A);
            MoveUp = Keyboard.GetState().IsKeyDown(Keys.W);
            MoveDown = Keyboard.GetState().IsKeyDown(Keys.S);

            base.Update(gameTime);
        }
    }
}
