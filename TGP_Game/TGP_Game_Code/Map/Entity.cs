using Microsoft.Xna.Framework;
using System;

namespace TGP_Game_Code.Map
{
    public class Entity
    {
        public bool Active;

        public int EntityTypeIndex = 0;

        private float AnimationIndex = 0f;

        protected Rectangle Position;
        protected Rectangle TexturePosition = new Rectangle(0, 0, 32, 32);

        public Vector2 Velocity = Vector2.Zero;

        public float MaximumVelocity = 10f;
        public float Acceleration = 0.035f;

        public bool MoveUp, MoveDown, MoveLeft, MoveRight;
        
        private bool IsActive()
        {
            // Escape method if entity is not active

            if (!Active)
            {
                return false;
            }

            // Escape method if entity is out of the bounds of the screen

            if (!States.Options.Screen.Contains(Position))
            {
                return false;
            }

            return true;
        }

        public Entity(int entityTypeIndex, Vector2 position)
        {
            // Set all values

            EntityTypeIndex = entityTypeIndex;
            Position = new Rectangle((int)position.X, (int)position.Y, 48, 48);
            Active = true;
        }

        public virtual void Update(GameTime gameTime)
        {
            // Escape method if entity is not active

            if (!IsActive())
            {
                return;
            }

            if ((MoveRight && !MoveLeft && Velocity.X < MaximumVelocity) || ((!MoveLeft || (MoveRight && MoveLeft)) && Velocity.X < 0))
            {
                Velocity.X += Acceleration * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if ((MoveLeft && !MoveRight && Velocity.X > - MaximumVelocity) || ((!MoveRight || (MoveRight && MoveLeft)) && Velocity.X > 0))
            {
                Velocity.X -= Acceleration * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            Position.X += (int)Velocity.X;
        }

        public virtual void Draw(GameTime gameTime)
        {
            // Escape method if entity is not active

            if (!IsActive())
            {
                return;
            }

            // Progress the animation if there is movement

            if (MoveUp || MoveDown || MoveLeft || MoveRight)
            {
                AnimationIndex = AnimationIndex + 0.005f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            // If animation is done reset it to 0f

            if (AnimationIndex >= 3f)
            {
                AnimationIndex = 0f;
            }

            // Calculate rectangle position in texture file

            TexturePosition.X = EntityTypeIndex * 96 + (int)Math.Floor(AnimationIndex) * 32;

            if (MoveLeft && !MoveRight)
            {
                TexturePosition.Y = 1;
            }
            else if (!MoveLeft && MoveRight)
            {
                TexturePosition.Y = 2;
            }
            else if (MoveUp && !MoveDown)
            {
                TexturePosition.Y = 3;
            }
            else
            {
                TexturePosition.Y = 0;
            }

            TexturePosition.Y *= 32;

            // Draw entity

            Main.SpriteBatch.Draw(Main.Entities, Position, TexturePosition, Color.White);
        }
    }
}
