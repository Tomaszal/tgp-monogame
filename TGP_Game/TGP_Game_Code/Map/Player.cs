using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TGP_Game_Code.Map
{
    public class Player : Entity
    {
        public Player(int playerTypeIndex, Vector2 position) : base(playerTypeIndex, position)
        {

        }

        public override void Update(GameTime gameTime)
        {
            //MovementDirection = 'N';

            MoveRight = Keyboard.GetState().IsKeyDown(Keys.D);
            MoveLeft = Keyboard.GetState().IsKeyDown(Keys.A);

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Position.Y += 5;
                MoveDown = true;
            }
            else
            {
                MoveDown = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Position.Y -= 5;
                MoveUp = true;
            }
            else
            {
                MoveUp = false;
            }

            base.Update(gameTime);
        }
    }
}
