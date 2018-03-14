using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace TGP.States
{
	class GameHandler : State
	{
		public override void Update(GameTime gameTime)
		{
			// Update map

			Map.Map.Update(gameTime);

			// Exit to main menu if escape key is pressed

			if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Main.NewStateIndex = 0;
		}

		public override void Draw(GameTime gameTime)
		{
			// Draw map

			Map.Map.Draw(gameTime);
		}
	}
}
