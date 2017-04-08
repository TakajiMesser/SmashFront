using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SmashFront.ViewModels
{
    public static class Character
    {
        public enum Characters
        {
            Bowser,
            DonkeyKong,
            DrMario,
            Falco,
            Falcon,
            Fox,
            GameAndWatch,
            Ganon,
            IceClimbers,
            Jigglypuff,
            Kirby,
            Link,
            Luigi,
            Mario,
            Marth,
            Mewtwo,
            Ness,
            Peach,
            Pichu,
            Pikachu,
            Roy,
            Samus,
            Yoshi,
            YoungLink,
            Zelda,
            Random
        };

        public static List<string> GetCharacterNames()
        {
            return new List<string>() { "Bowser", "DonkeyKong", "DrMario", "Falco", "Falcon",
                                        "Fox", "GameAndWatch", "Ganon", "IceClimbers", "Jigglypuff",
                                        "Kirby", "Link", "Luigi", "Mario", "Marth",
                                        "Mewtwo", "Ness", "Peach", "Pichu", "Pikachu",
                                        "Roy", "Samus", "Yoshi", "YoungLink", "Zelda",
                                        "Random" };
        }

        public static Characters GetCharacterFromName(string name)
        {
            switch (name)
            {
                case "Bowser":
                    return Characters.Bowser;
                case "DonkeyKong":
                    return Characters.DonkeyKong;
                default:
                    return Characters.Random;
            }
        }

        public static string GetNarratorNameFromCharacter(Characters character)
        {
            switch (character)
            {
                case Characters.Bowser:
                    return "Bowser";
                case Characters.DonkeyKong:
                    return "DonkeyKong";
                default:
                    return "";
            }
        }
    }
}
