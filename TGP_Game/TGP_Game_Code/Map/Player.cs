using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TGP_Game_Code.Map
{
    public class Player : Entity
    {
        public Player(Point startingPosition) : base(startingPosition) { }

        private bool TouchBottomOf(Rectangle r1, Rectangle r2)
        {
            return (r1.Top <= r2.Bottom + (r2.Height / 3) &&
                    r1.Top >= r2.Bottom - 1 &&
                    r1.Right >= r2.Left + (r2.Width / 5) &&
                    r1.Left <= r2.Right - (r2.Width / 5));
        }

        private bool TouchRightOf(Rectangle r1, Rectangle r2)
        {
            return (r1.Left >= r2.Left &&
                    r1.Left + r1.Width / 4 <= r2.Right + 5 &&
                    r1.Top <= r2.Bottom - (r2.Width / 4) &&
                    r1.Bottom >= r2.Top + (r2.Width / 4));
        }

        public override void Update(GameTime gameTime)
        {
            // Update movement directions

            MoveRight = Keyboard.GetState().IsKeyDown(Keys.D);
            MoveLeft = Keyboard.GetState().IsKeyDown(Keys.A);
            MoveUp = Keyboard.GetState().IsKeyDown(Keys.W);
            MoveDown = Keyboard.GetState().IsKeyDown(Keys.S);

            // Update jump state

            Jump = Keyboard.GetState().IsKeyDown(Keys.Space);

            // Update entity

            base.Update(gameTime);

            // Check for collisions with enemies

            foreach (Enemy enemy in Map.Enemies)
            {
                if (MovedDownDuringLastUpdate && TouchBottomOf(enemy.Position, Position)) enemy.Active = false;
                else if (enemy.Active && (TouchRightOf(Position, enemy.Position) || TouchRightOf(enemy.Position, Position))) Active = false;

                //if (MovedDownDuringLastUpdate && (enemy.Position.Contains(Position.Right, Position.Bottom) || enemy.Position.Contains(Position.Left, Position.Bottom))) enemy.Active = false;
            }
        }
    }
}
