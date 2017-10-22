using Microsoft.Xna.Framework;
using System;

namespace TGP_Game_Code.Map
{
    public class Entity
    {
        //public bool Active;

        // Animation

        public int EntityTypeIndex = 0;
        private float AnimationIndex = 0f;

        // Position

        public Rectangle Position = new Rectangle(0, 0, 48, 48);
        private Rectangle TexturePosition = new Rectangle(0, 0, 32, 32);
        
        // Velocity and acceleration

        public float Acceleration = 0.035f;
        public float MaximumVelocity = 10f;

        public Vector2 Velocity = Vector2.Zero;
        private Rectangle VelocityPosition;

        // Movement

        public bool MoveUp, MoveDown, MoveLeft, MoveRight;

        // Collision

        private int TopTile, BottomTile, LeftTile, RightTile;
        private int X, Y;

        private bool CheckCollision(char Axis)
        {
            // Function checks if there is a collision on certain axis after adding veolicty

            // Calculate position after adding velocity to certain axis

            VelocityPosition = Position;
            if (Axis == 'Y') VelocityPosition.Y += (int)Velocity.Y;
            if (Axis == 'X') VelocityPosition.X += (int)Velocity.X;

            // Calculate corner tiles that the entity is touching

            TopTile = (int)Math.Floor((float)VelocityPosition.Top / Map.TileDestinationRectangle.Height);
            BottomTile = (int)Math.Floor((float)VelocityPosition.Bottom / Map.TileDestinationRectangle.Height);
            LeftTile = (int)Math.Floor((float)VelocityPosition.Left / Map.TileDestinationRectangle.Width);
            RightTile = (int)Math.Floor((float)VelocityPosition.Right / Map.TileDestinationRectangle.Width);
            
            // Return true if the position is out of bounds of the tile map

            if (TopTile < 0) return true;
            if (BottomTile >= Map.TileMap.Count) return true;
            if (LeftTile < 0) return true;
            if (RightTile >= Map.TileMap[0].Count) return true;

            // Check for collision with surrounding tiles on tile map

            for (Y = TopTile; Y <= BottomTile; Y++)
            {
                for (X = LeftTile; X <= RightTile; X++)
                {
                    if (Map.TileMap[Y][X].Collide) return true;
                }
            }

            // Return false if there was no collision

            return false;
        }

        private float AccelerationDelta(GameTime gameTime)
        {
            return Acceleration * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        private bool IsActive()
        {
            // Escape method if entity is not active

            //if (!Active) return false;

            // Escape method if entity is out of the bounds of the screen

            //if (!States.Options.Screen.Contains(Position)) return false;

            return true;
        }
        
        public virtual void Update(GameTime gameTime)
        {
            // Escape method if entity is not active

            if (!IsActive()) return;

            // Calculate horizontal entity velocity

            if (MoveRight != MoveLeft)
            {
                if (MoveRight && Velocity.X < MaximumVelocity) Velocity.X += AccelerationDelta(gameTime);
                else if (Velocity.X > -MaximumVelocity) Velocity.X -= AccelerationDelta(gameTime);
            }
            else
            {
                if (Velocity.X < 0) Velocity.X += AccelerationDelta(gameTime);
                else if (Velocity.X > 0) Velocity.X -= AccelerationDelta(gameTime);
            }

            // Calculate vertical velocity (temporary)

            if (MoveDown != MoveUp)
            {
                if (MoveDown && Velocity.Y < MaximumVelocity) Velocity.Y += AccelerationDelta(gameTime);
                else if (Velocity.Y > -MaximumVelocity) Velocity.Y -= AccelerationDelta(gameTime);
            }
            else
            {
                if (Velocity.Y < 0) Velocity.Y += AccelerationDelta(gameTime);
                else if (Velocity.Y > 0) Velocity.Y -= AccelerationDelta(gameTime);
            }

            // Check for collision and act accordingly

            if (CheckCollision('Y')) Velocity.Y = 0;
            else Position.Y += (int)Velocity.Y;

            if (CheckCollision('X')) Velocity.X = 0;
            else Position.X += (int)Velocity.X;
        }

        public virtual void Draw(GameTime gameTime)
        {
            // Escape method if entity is not active

            if (!IsActive()) return;

            // Progress the animation if there is movement

            if (MoveUp != MoveDown || MoveLeft != MoveRight) AnimationIndex = AnimationIndex + 0.005f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // If animation is done reset it to 0f

            if (AnimationIndex >= 3f) AnimationIndex = 0f;

            // Calculate rectangle position in texture file

            TexturePosition.X = EntityTypeIndex * 96 + (int)Math.Floor(AnimationIndex) * 32;
            
            if (MoveLeft && !MoveRight) TexturePosition.Y = 1;
            else if (!MoveLeft && MoveRight) TexturePosition.Y = 2;
            else if (MoveUp && !MoveDown) TexturePosition.Y = 3;
            else TexturePosition.Y = 0;

            TexturePosition.Y *= 32;

            // Draw entity

            Main.SpriteBatch.Draw(Main.Entities, Position, TexturePosition, Color.White);
        }
    }
}
