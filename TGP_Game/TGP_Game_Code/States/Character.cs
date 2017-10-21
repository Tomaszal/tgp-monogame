using Microsoft.Xna.Framework;

namespace TGP_Game_Code.States
{
    class Character : State
    {
        private static int CharacterIndex = 2;

        private Map.Entity Preview;

        private class Next : Button
        {
            public Next(string text, Vector2 position, Color color) : base(text, position, color, -1) { }

            public override void Action()
            {
                // Increace or reset CharacterIndex

                CharacterIndex = (CharacterIndex == 3) ? 0 : CharacterIndex + 1;

                base.Action();
            }
        }

        private class Previous : Button
        {
            public Previous(string text, Vector2 position, Color color) : base(text, position, color, -1) { }

            public override void Action()
            {
                // Decreace or reset CharacterIndex

                CharacterIndex = (CharacterIndex == 0) ? 3 : CharacterIndex - 1;

                base.Action();
            }
        }

        private class Select : Button
        {
            public Select(string text, Vector2 position, Color color) : base(text, position, color, 4) { }
            
            public override void Action()
            {
                // Logic for selection

                Map.Map.Initialize(CharacterIndex, new Vector2(20, 20));

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
            Buttons.Add(new Previous("<", new Vector2(-54, 0), Color.PaleVioletRed));
            Buttons.Add(new Select("Select", new Vector2(0, 100), Color.PaleVioletRed));
            Buttons.Add(new Button("Return", new Vector2(0, 180), Color.White, 0));

            // Add entity to preview the character

            Preview = new Map.Entity(CharacterIndex, new Vector2(Main.Graphics.PreferredBackBufferWidth / 2 - 24, Main.Graphics.PreferredBackBufferHeight / 2 - 8));
            //Preview.MovementDirection = 'F';
        }

        public override void Update(GameTime gameTime)
        {
            // Update character type index and set it to active

            //Preview.Active = true;
            Preview.EntityTypeIndex = CharacterIndex;
            Preview.MoveDown = true;

            // Draw character specific description text

            switch (CharacterIndex)
            {
                case 0:
                    UpdateCharacterDescription(Color.GreenYellow, "Leaper", "Jumps very high, but runs very slow.", "Has 3 lifes.");
                    break;
                case 1:
                    UpdateCharacterDescription(Color.MediumSpringGreen, "Muller", "Runs very fast, but very jumps low.", "Has 3 lifes.");
                    break;
                case 2:
                    UpdateCharacterDescription(Color.MediumVioletRed, "Survivalist", "Jumps low and runs slow.", "Has 5 lifes.");
                    break;
                case 3:
                    UpdateCharacterDescription(Color.DodgerBlue, "Tryhard", "Jumps low and runs slow.", "Has 1 life.");
                    break;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Draw background

            DrawMenuBackground();

            // Draw character preview

            Preview.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}