using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SmashFront.ViewModels
{
    public enum MenuScreenHeaders
    {
        MainMenu,
        Singleplayer,
        Multiplayer,
        Stadium,
        Settings
    };

    public static class MenuScreen
    {
        public static String GetNameForHeader(MenuScreenHeaders header)
        {
            switch (header)
            {
                case MenuScreenHeaders.MainMenu:
                    return "MAIN MENU";
                case MenuScreenHeaders.Singleplayer:
                    return "SINGLEPLAYER";
                case MenuScreenHeaders.Multiplayer:
                    return "MULTIPLAYER";
                case MenuScreenHeaders.Stadium:
                    return "STADIUM";
                case MenuScreenHeaders.Settings:
                    return "SETTINGS";
                default:
                    return "";
            }
        }

        public static List<String> GetOptionsForHeader(MenuScreenHeaders header)
        {
            switch (header)
            {
                case MenuScreenHeaders.MainMenu:
                    return new List<string>() { "SINGLEPLAYER", "MULTIPLAYER", "STADIUM", "TRAINING", "SETTINGS", "QUIT" };
                case MenuScreenHeaders.Singleplayer:
                    return new List<string>() { "CLASSIC", "ADVENTURE", "ALL STAR" };
                case MenuScreenHeaders.Multiplayer:
                    return new List<string>() { "LOCAL", "MATCHMAKING", "CUSTOM GAMES" };
                case MenuScreenHeaders.Stadium:
                    return new List<string>() { "EVENT MATCHES", "TARGET TEST", "HOME-RUN CONTEST", "MULTI-MAN MELEE" };
                case MenuScreenHeaders.Settings:
                    return new List<string>() { "RUMBLE", "AUDIO" };
                default:
                    return new List<string>();
            }
        }

        public static string GetImageNameForHeader(MenuScreenHeaders header)
        {
            switch (header)
            {
                case MenuScreenHeaders.MainMenu:
                    return "BattlefieldBackground";
                default:
                    return "UpThrowBackground";
            }
        }

        public static string GetDescriptionForOption(String option)
        {
            switch (option)
            {
                case "SINGLEPLAYER":
                    return "Description for Singleplayer";
                case "MULTIPLAYER":
                    return "Description for Multiplayer";
                case "STADIUM":
                    return "Description for Stadium";
                case "TRAINING":
                    return "Description for Training";
                case "SETTINGS":
                    return "Description for Settings";
                case "LOCAL":
                    return "Description for Local";
                case "MATCHMAKING":
                    return "Description for Matchmaking";
                case "CUSTOM GAMES":
                    return "Description for Custom Games";
                case "QUIT":
                    return "Description for Quit";
                default:
                    return "";
            }
        }
    }
}
