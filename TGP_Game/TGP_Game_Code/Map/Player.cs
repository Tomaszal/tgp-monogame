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
            MovementDirection = 'N';

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Position.X += 1;
                MovementDirection = 'R';
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Position.X -= 1;
                MovementDirection = 'L';
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D) && Keyboard.GetState().IsKeyDown(Keys.A))
            {
                MovementDirection = 'N';
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Position.Y += 1;
                MovementDirection = 'F';
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Position.Y -= 1;
                MovementDirection = 'B';
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.W))
            {
                MovementDirection = 'N';
            }

            base.Update(gameTime);
        }
    }
}
