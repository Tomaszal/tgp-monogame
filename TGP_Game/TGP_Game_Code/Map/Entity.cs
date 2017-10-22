using Microsoft.Xna.Framework;
using System;

namespace TGP_Game_Code.Map
{
    public class Entity
    {
        // Animation

        public int EntityTypeIndex = 0;
        private float AnimationIndex = 0f;

        // Position

        public Rectangle Position = new Rectangle(0, 0, 48, 48);
        private Rectangle TexturePosition = new Rectangle(0, 0, 32, 32);

        // Velocity and acceleration

        private float VerticalAcceleration = 0.09f;
        private float MaximumVerticalVelocity = 10f;

        public float HorizontalAcceleration = 0.035f;
        public float MaximumHorizontalVelocity = 0f;

        public Vector2 Velocity = Vector2.Zero;
        private Rectangle VelocityPosition;

        // Movement

        public bool MoveUp, MoveDown, MoveLeft, MoveRight, Jump;

        // Jumping

        private bool IsJumping;
        private float JumpingHeight;
        public float MaximumJumpingHeight = 0f;

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

        private float AccelerationDelta(GameTime gameTime, char axis)
        {
            if (axis == 'X') return HorizontalAcceleration * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (axis == 'Y') return VerticalAcceleration * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            return 0;
        }
        
        public virtual void Update(GameTime gameTime)
        {
            // Calculate horizontal entity velocity

            if (MoveRight != MoveLeft)
            {
                if (MoveRight && Velocity.X < MaximumHorizontalVelocity) Velocity.X += AccelerationDelta(gameTime, 'X');
                else if (Velocity.X > -MaximumHorizontalVelocity) Velocity.X -= AccelerationDelta(gameTime, 'X');
            }
            else
            {
                if (Velocity.X < HorizontalAcceleration && Velocity.X > -HorizontalAcceleration) Velocity.X = 0f;
                else if (Velocity.X < 0f) Velocity.X += AccelerationDelta(gameTime, 'X');
                else if (Velocity.X > 0f) Velocity.X -= AccelerationDelta(gameTime, 'X');
            }

            if (CheckCollision('X')) Velocity.X = 0;
            else Position.X += (int)Velocity.X;

            // Calculate vertical velocity (temporary)

            if (!IsJumping)
            {
                if (Velocity.Y < MaximumVerticalVelocity) Velocity.Y += AccelerationDelta(gameTime, 'Y') / 2;
                
                if (CheckCollision('Y'))
                {
                    Velocity.Y = 0f;
                    if (Jump) IsJumping = true;
                }
                else Position.Y += (int)Velocity.Y;
            }
            else
            {
                if (Velocity.Y > -MaximumVerticalVelocity) Velocity.Y -= AccelerationDelta(gameTime, 'Y');

                if (CheckCollision('Y') || !Jump || -JumpingHeight >= MaximumJumpingHeight * Map.TileDestinationRectangle.Height)
                {
                    Velocity.Y = 0f;
                    JumpingHeight = 0f;
                    IsJumping = false;
                }
                else
                {
                    Position.Y += (int)Velocity.Y;
                    JumpingHeight += Velocity.Y;
                }
            }

            //if (MoveDown != MoveUp)
            //{
            //    if (MoveDown && Velocity.Y < MaximumVerticalVelocity) Velocity.Y += AccelerationDelta(gameTime, 'Y');
            //    else if (Velocity.Y > -MaximumVerticalVelocity) Velocity.Y -= AccelerationDelta(gameTime, 'Y');
            //}
            //else
            //{
            //    if (Velocity.Y < VerticalAcceleration && Velocity.Y > -VerticalAcceleration) Velocity.Y = 0f;
            //    else if (Velocity.Y < 0) Velocity.Y += AccelerationDelta(gameTime, 'Y');
            //    else if (Velocity.Y > 0) Velocity.Y -= AccelerationDelta(gameTime, 'Y');
            //}

            //System.Diagnostics.Debug.WriteLine(Velocity.Y);

            // Check for collision and act accordingly
        }

        public virtual void Draw(GameTime gameTime)
        {
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
