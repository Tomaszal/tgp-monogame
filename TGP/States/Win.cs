using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TGP.States
{
	class Win : State
	{
		int HearthIndex;
		Rectangle HearthRectangle = new Rectangle(0, 0, 80, 80);

		public Win()
		{
			// Add texts

			Texts.Add(new Text(Color.MediumVioletRed, new Vector2(0, -240), "You have won the game!"));
			Texts.Add(new Text(Color.White, new Vector2(0, -120), "Lifes left:"));
			Texts.Add(new Text(Color.White, new Vector2(0, 90), "Your score:"));

			// Add button

			Buttons.Add(new Button("Press R to return to main menu.", new Vector2(0, 180), Color.LightSlateGray, 0));
		}

		public override void Update(GameTime gameTime)
		{
			// Update score text

			Texts[2].TextString = "Your score: " + (Map.Map.Player.ScoreKilled + Map.Map.Player.ScoreWalked);

			// Check for R key press

			if (Keyboard.GetState().IsKeyDown(Keys.R))
			{
				Buttons[0].Action();
			}

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			// Draw background

			DrawMenuBackground();

			// Draw hearths

			HearthRectangle.Y = Main.Graphics.PreferredBackBufferHeight / 2 - 40;

			for (HearthIndex = 1; HearthIndex <= Map.Map.Player.Lifes; HearthIndex++)
			{
				HearthRectangle.X = Main.Graphics.PreferredBackBufferWidth / 2 + 40 * Map.Map.Player.Lifes - 80 * HearthIndex;

				Main.SpriteBatch.Draw(Main.Hearth, HearthRectangle, Color.White);
			}

			base.Draw(gameTime);
		}
	}
}
