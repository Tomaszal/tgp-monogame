using Microsoft.Xna.Framework;
using System.Reflection;

namespace TGP.States
{
	class About : State
	{
		public About()
		{
			// Add texts

			Texts.Add(new Text(Color.White, new Vector2(0, -200), "Version " + Assembly.GetExecutingAssembly().GetName().Version + " (reworked)"));
			Texts.Add(new Text(Color.White, new Vector2(0, -120), "Developed by:"));
			Texts.Add(new Text(Color.White, new Vector2(0, -90), "Tomas Zaluckij"));
			Texts.Add(new Text(Color.White, new Vector2(0, -60), "(@Tomaszal)"));
			Texts.Add(new Text(Color.White, new Vector2(0, 0), "Music: Torrey Desmond Rogers"));
			Texts.Add(new Text(Color.White, new Vector2(0, 30), "Sound effects: SoundBible.com"));
			Texts.Add(new Text(Color.White, new Vector2(0, 100), "Background: Wyatt S. Miles (flashpotatoes)"));

			// Add buttons

			Buttons.Add(new Button("Return", new Vector2(0, 180), Color.LightSlateGray, 0));
		}

		public override void Draw(GameTime gameTime)
		{
			// Draw background

			DrawMenuBackground();

			base.Draw(gameTime);
		}
	}
}
