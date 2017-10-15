using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGP_Game.States
{
    static class Character
    {
        private static int CharacterIndex = 3;

        // Define each button

        private static Button Next = new Button(">", Color.PaleVioletRed, new Vector2(50, 0));
        private static Button Previous = new Button("<", Color.PaleVioletRed, new Vector2(-50, 0));
        private static Button Select = new Button("Select", Color.PaleVioletRed, new Vector2(0, 100));
        private static Button Return = new Button("Return", Color.Cyan, new Vector2(0, 180));

        private static void DrawCharacterDescription(Color color, string line1, string line2, string line3)
        {
            // Draw provided character description

            Manager.DrawText(color, new Vector2(0, -160), line1);
            Manager.DrawText(Color.LightSlateGray, new Vector2(0, -130), line2);
            Manager.DrawText(Color.LightSlateGray, new Vector2(0, -100), line3);
        }

        public static void Update()
        {
            // Check each button and act accordingly

            if (Next.Check())
            {
                CharacterIndex = (CharacterIndex == 4) ? 1 : CharacterIndex + 1;
            }

            if (Previous.Check())
            {
                CharacterIndex = (CharacterIndex == 1) ? 4 : CharacterIndex - 1;
            }

            // WIP, create logic for character selection
            Select.Check();

            if (Return.Check())
            {
                Manager.SetNewState(Manager.State.Menu);
            }
        }

        public static void Draw()
        {
            // Draw background

            Manager.DrawMenuBackground();

            // WIP, draw character preview

            // Draw text

            Manager.DrawText(Color.White, new Vector2(0, -280), "Choose your character:");

            // Draw character specific description text

            switch (CharacterIndex)
            {
                case 1:
                    DrawCharacterDescription(Color.GreenYellow, "Leaper", "Jumps very high, but runs very slow.", "Has 3 lifes.");
                    break;
                case 2:
                    DrawCharacterDescription(Color.MediumSpringGreen, "Muller", "Runs very fast, but very jumps low.", "Has 3 lifes.");
                    break;
                case 3:
                    DrawCharacterDescription(Color.MediumVioletRed, "Survivalist", "Jumps low and runs slow.", "Has 5 lifes.");
                    break;
                case 4:
                    DrawCharacterDescription(Color.DodgerBlue, "Tryhard", "Jumps low and runs slow.", "Has 1 life.");
                    break;
            }

            // Draw each button

            Next.Draw();
            Previous.Draw();
            Select.Draw();
            Return.Draw();
        }
    }
}
