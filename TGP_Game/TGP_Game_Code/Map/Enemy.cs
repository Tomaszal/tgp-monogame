using Microsoft.Xna.Framework;
using System;

namespace TGP_Game_Code.Map
{
    public class Enemy : Entity
    {
        private bool RightMovement;

        private int BottomTile, CornerTile;

        private float DeadAlpha = 1f;

        public Enemy(Point startingPosition) : base(startingPosition)
        {
            // Set enemy entity type and maximum velocity

            EntityTypeIndex = 4;
            MaximumHorizontalVelocity = Map.EnemyVelocity;
        }

        public override void WallCollision()
        {
            // Flip movement direction if enemy collides with a wall

            RightMovement = !RightMovement;
        }

        public override void FloorCollision()
        {
            // Get bottom tile coordinates

            BottomTile = (int)Math.Floor((float)VelocityPosition.Bottom / Map.TileDestinationRectangle.Height);

            // Get coordinates of right or left corner tile (depending on movement direction)

            if (MoveRight) CornerTile = (int)Math.Floor((float)VelocityPosition.Right / Map.TileDestinationRectangle.Width);
            else CornerTile = (int)Math.Floor((float)VelocityPosition.Left / Map.TileDestinationRectangle.Width);

            // Flip movement direction if the bottom corner tile is dark stone (bad implementation)

            if (Map.TileMap[BottomTile][CornerTile].TexturePosition.X / 7 == Map.TileSourceRectangle.X)
            {
                RightMovement = !RightMovement;
                Velocity.X = 0;
            }
        }

        public override void Update(GameTime gameTime)
        {
            // Update movement directions

            MoveRight = RightMovement;
            MoveLeft = !RightMovement;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Fade away the enemy if it's dead

            if (!Active)
            {
                DeadAlpha -= 0.025f;

                Main.SpriteBatch.Draw(Main.Entities, Position, new Rectangle(EntityTypeIndex * 96, 0, 32, 32), Color.White * DeadAlpha);

                return;
            }

            base.Draw(gameTime);
        }
    }
}
