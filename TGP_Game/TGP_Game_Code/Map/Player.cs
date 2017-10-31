using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TGP_Game_Code.Map
{
    public class Player : Entity
    {
        public Player(Point startingPosition) : base(startingPosition) { }

        public override void Update(GameTime gameTime)
        {
            // Update movement directions

            MoveRight = Keyboard.GetState().IsKeyDown(Keys.D);
            MoveLeft = Keyboard.GetState().IsKeyDown(Keys.A);
            MoveUp = Keyboard.GetState().IsKeyDown(Keys.W);
            MoveDown = Keyboard.GetState().IsKeyDown(Keys.S);

            // Update jump state

            Jump = Keyboard.GetState().IsKeyDown(Keys.Space);

            base.Update(gameTime);
        }
    }
}
