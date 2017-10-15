using Microsoft.Xna.Framework;

namespace TGP_Game.States
{
    class Character : State
    {
        private static int CharacterIndex = 3;

        private class Next : Button
        {
            public Next(string text, Vector2 position, Color color) : base(text, position, color, -1) { }

            public override void Action()
            {
                // Increace or reset CharacterIndex

                CharacterIndex = (CharacterIndex == 4) ? 1 : CharacterIndex + 1;

                base.Action();
            }
        }

        private class Previous : Button
        {
            public Previous(string text, Vector2 position, Color color) : base(text, position, color, -1) { }

            public override void Action()
            {
                // Decreace or reset CharacterIndex

                CharacterIndex = (CharacterIndex == 1) ? 4 : CharacterIndex - 1;

                base.Action();
            }
        }

        private class Select : Button
        {
            public Select(string text, Vector2 position, Color color) : base(text, position, color, -1) { }

            public override void Action()
            {
                // Logic for selection

                base.Action();
            }
        }

        private void UpdateCharacterDescription(Color color, string line1, string line2, string line3)
        {
            // Update texts with provided character description and color

            Texts[1].TextString = line1;
            Texts[1].Color = color;
            Texts[2].TextString = line2;
            Texts[3].TextString = line3;
        }

        public Character()
        {
            // Add texts

            Texts.Add(new Text(Color.White, new Vector2(0, -280), "Choose your character:"));
            Texts.Add(new Text(Color.White, new Vector2(0, -160), "NaN"));
            Texts.Add(new Text(Color.LightSlateGray, new Vector2(0, -130), "NaN"));
            Texts.Add(new Text(Color.LightSlateGray, new Vector2(0, -100), "NaN"));

            // Add buttons

            Buttons.Add(new Next(">", new Vector2(50, 0), Color.PaleVioletRed));
            Buttons.Add(new Previous("<", new Vector2(-50, 0), Color.PaleVioletRed));
            Buttons.Add(new Select("Select", new Vector2(0, 100), Color.PaleVioletRed));
            Buttons.Add(new Button("Return", new Vector2(0, 180), Color.White, 0));
        }

        public override void Update(GameTime gameTime)
        {
            // Draw character specific description text

            switch (CharacterIndex)
            {
                case 1:
                    UpdateCharacterDescription(Color.GreenYellow, "Leaper", "Jumps very high, but runs very slow.", "Has 3 lifes.");
                    break;
                case 2:
                    UpdateCharacterDescription(Color.MediumSpringGreen, "Muller", "Runs very fast, but very jumps low.", "Has 3 lifes.");
                    break;
                case 3:
                    UpdateCharacterDescription(Color.MediumVioletRed, "Survivalist", "Jumps low and runs slow.", "Has 5 lifes.");
                    break;
                case 4:
                    UpdateCharacterDescription(Color.DodgerBlue, "Tryhard", "Jumps low and runs slow.", "Has 1 life.");
                    break;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Draw background

            DrawMenuBackground();

            // WIP, draw character preview

            base.Draw(gameTime);
        }
    }
}