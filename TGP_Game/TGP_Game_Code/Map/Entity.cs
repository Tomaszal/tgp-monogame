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

        public char MovementDirection = 'N';

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
        }

        public virtual void Draw(GameTime gameTime)
        {
            // Escape method if entity is not active

            if (!IsActive())
            {
                return;
            }

            // Progress the animation if movement direction is not 'N'one

            if (MovementDirection != 'N')
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

            TexturePosition.Y = 0;

            switch (MovementDirection)
            {
                case 'L':
                    TexturePosition.Y = 1;
                    break;
                case 'R':
                    TexturePosition.Y = 2;
                    break;
                case 'B':
                    TexturePosition.Y = 3;
                    break;
            }

            TexturePosition.Y *= 32;

            // Draw entity

            Main.SpriteBatch.Draw(Main.Entities, Position, TexturePosition, Color.White);

        }
    }
}
