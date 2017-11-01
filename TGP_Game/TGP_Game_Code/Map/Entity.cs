using Microsoft.Xna.Framework;
using System;

namespace TGP_Game_Code.Map
{
    public class Entity
    {
        public bool Active = true;

        // Animation

        public int EntityTypeIndex = 0;
        private float AnimationIndex = 0f;

        // Position

        public Point StartingPosition;
        public Rectangle Position = new Rectangle(0, 0, 48, 48);
        private Rectangle TexturePosition = new Rectangle(0, 0, 32, 32);

        // Velocity and acceleration

        private float VerticalAcceleration = 0.09f;
        private float MaximumVerticalVelocity = 10f;

        public float HorizontalAcceleration = 0.035f;
        public float MaximumHorizontalVelocity = 0f;

        public Vector2 Velocity = Vector2.Zero;
        public Rectangle VelocityPosition;

        protected bool MovedDownDuringLastUpdate;

        // Movement

        public bool MoveUp, MoveDown, MoveLeft, MoveRight, Jump;

        // Jumping

        protected bool IsJumping;
        protected float JumpingHeight;
        public float MaximumJumpingHeight = 0f;
        public float MinimumJumpingHeight = 1.5f;

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
            // Calculate acceleration of chosen axis

            if (axis == 'X') return HorizontalAcceleration * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (axis == 'Y') return VerticalAcceleration * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            return 0;
        }

        public virtual void WallCollision() { }

        public virtual void FloorCollision() { }
        
        public Entity(Point startingPosition)
        {
            StartingPosition = startingPosition;
            Position.Location = startingPosition;

            Active = true;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!Active) return;

            // Calculate horizontal entity velocity

            if (MoveRight != MoveLeft)
            {
                // Accelerate entity if there is horizontal movement and velocity is not maximum

                if (MoveRight && Velocity.X < MaximumHorizontalVelocity) Velocity.X += AccelerationDelta(gameTime, 'X');
                else if (Velocity.X > -MaximumHorizontalVelocity) Velocity.X -= AccelerationDelta(gameTime, 'X');
            }
            else
            {
                // Decelerate entity if there is no horizontal movement and velocity is not close to 0 (if it is, set it to 0)

                if (Velocity.X < HorizontalAcceleration && Velocity.X > -HorizontalAcceleration) Velocity.X = 0f;
                else if (Velocity.X < 0f) Velocity.X += AccelerationDelta(gameTime, 'X');
                else if (Velocity.X > 0f) Velocity.X -= AccelerationDelta(gameTime, 'X');
            }

            // Check for collision on horizontal axis
            // Progress entity position if there is no collision

            if (CheckCollision('X'))
            {
                WallCollision();
                Velocity.X = 0;
            }
            else Position.X += (int)Velocity.X;

            // Calculate vertical velocity and jumping mechanics

            MovedDownDuringLastUpdate = false;

            if (!IsJumping)
            {
                // Accelerate entity's fall if it is not jumping and velocity is not maximum

                if (Velocity.Y < MaximumVerticalVelocity) Velocity.Y += AccelerationDelta(gameTime, 'Y') / 2;

                // Check for collision on vertical axis

                if (CheckCollision('Y'))
                {
                    FloorCollision();
                    Velocity.Y = 0f;

                    // Update jumping state if there is floor underneath

                    if (Jump)
                    {
                        IsJumping = true;

                        Main.JumpSound.Play();
                    }
                }
                else if ((int)Velocity.Y > 0)
                {
                    // Progress entity position if there is no collision

                    MovedDownDuringLastUpdate = true;
                    Position.Y += (int)Velocity.Y;
                }
            }
            else
            {
                // Accelerate entity's jump if it is jumping and velocity is not maximum

                if (Velocity.Y > -MaximumVerticalVelocity) Velocity.Y -= AccelerationDelta(gameTime, 'Y');

                // Check for collision on vertical axis

                if (CheckCollision('Y') || (!Jump && -JumpingHeight >= MinimumJumpingHeight * Map.TileDestinationRectangle.Height) || -JumpingHeight >= MaximumJumpingHeight * Map.TileDestinationRectangle.Height)
                {
                    // Reset entity jumping if there is collision

                    Velocity.Y = 0f;
                    JumpingHeight = 0f;
                    IsJumping = false;
                }
                else
                {
                    // Progress entity position and jump height

                    Position.Y += (int)Velocity.Y;
                    JumpingHeight += Velocity.Y;
                }
            }
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
