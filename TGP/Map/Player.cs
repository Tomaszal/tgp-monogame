using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace TGP.Map
{
	public class Player : Entity
	{
		private Vector2 MapPosition;

		public int Lifes;

		public int ScoreWalked;
		public int ScoreKilled;

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

		public void Respawn()
		{
			Active = true;

			Position.Location = StartingPosition;

			IsJumping = false;
			JumpingHeight = 0f;

			Velocity = Vector2.Zero;

			Map.GameOn = true;
		}

		public void Die()
		{
			if (!Active) return;

			Active = false;

			Lifes--;

			Map.GameOn = false;

			Map.Player.ScoreKilled -= (int)System.Math.Pow(Map.Difficulty, 2) * 10;

			Main.NewStateIndex = 5;
		}

		public override void FloorCollision()
		{
			if (!Active) return;

			// Calculate player's position on map

			MapPosition.Y = (int)Math.Floor((float)Position.Center.Y / Map.TileDestinationRectangle.Height);
			MapPosition.X = (int)Math.Floor((float)Position.Center.X / Map.TileDestinationRectangle.Width);

			// Die if player is in water

			if (Map.TileMap[(int)MapPosition.Y][(int)MapPosition.X].TexturePosition.X / 3 == Map.TileSourceRectangle.X)
			{
				Die();

				Main.DrownSound.Play();
			}

			// Win if player is in winning position

			if (Math.Abs(Map.WinPos.X - MapPosition.X) <= 1 && Math.Abs(Map.WinPos.Y - MapPosition.Y) <= 1)
			{
				Active = false;

				Map.GameOn = false;

				Main.NewStateIndex = 6;
			}
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

			// Update walk score

			if (ScoreWalked < Position.X / 5) ScoreWalked = Position.X / 5;

			// Check for collisions with enemies

			foreach (Enemy enemy in Map.Enemies)
			{
				if (!Active) break;

				if (!enemy.Active) continue;

				if (MovedDownDuringLastUpdate && TouchBottomOf(enemy.Position, Position))
				{
					enemy.Active = false;

					ScoreKilled += (int)System.Math.Pow(Map.Difficulty, 2) * 10;

					Main.EnemyDeathSound.Play();
				}
				else if (TouchRightOf(Position, enemy.Position) || TouchRightOf(enemy.Position, Position))
				{
					Die();

					Main.PlayerDeathSound.Play();
				}
			}
		}
	}
}
