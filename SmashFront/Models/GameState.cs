using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmashFront.Models
{
    public class GameState
    {
        private string _characterID;
        private string _stageID;

        private const string _codesPath = @"C:\Users\Takaji\Documents\GitHub\SmashFront\SmashFront\Resources\CurrentGeckoCode.txt";

        public string DolphinPath { get; set; }
        public string ISOPath { get; set; }
        public string INIPath { get; set; }

        public GameState()
        {
            DolphinPath = @"C:\Users\Takaji\Downloads\SmashLadder-FM-v4.4--\Dolphin.exe";
            ISOPath = @"C:\Users\Takaji\Downloads\dolphin-master-4.0-7840-x64\Super Smash Bros. Melee (USA) (En,Ja) (v1.02).iso";
            INIPath = @"C:\Users\Takaji\Downloads\SmashLadder-FM-v4.4--\User\GameSettings\GALE01.ini";
        }

        public void LaunchGame()
        {
            string arguments = "-e \"" + ISOPath + "\" -b -c \"false\"";

            BackupPreviousINI();
            SaveNewINI();

            var processStartInfo = new ProcessStartInfo(DolphinPath, arguments);
            processStartInfo.WorkingDirectory = Path.GetDirectoryName(DolphinPath);
            processStartInfo.WindowStyle = ProcessWindowStyle.Minimized;

            Process.Start(processStartInfo).WaitForExit();
            RestoreINI();
        }

        public void BackupPreviousINI()
        {
            string backupPath = Path.GetDirectoryName(INIPath) + "\\" + Path.GetFileNameWithoutExtension(INIPath) + "BACKUP" + Path.GetExtension(INIPath);
            File.Copy(INIPath, backupPath, true);
        }

        public void SaveNewINI()
        {
            List<string> iniLines = File.ReadAllLines(INIPath).ToList();
            List<string> customCodeLines = File.ReadAllLines(_codesPath).ToList();

            int startIndex, endIndex;

            startIndex = iniLines.IndexOf("[Gecko_Enabled]");
            endIndex = iniLines.FindIndex(startIndex + 1, l => l.StartsWith("[") && l.EndsWith("]"));
            endIndex = (endIndex >= 0) ? endIndex : iniLines.Count;
            
            // Remove ALL other codes
            iniLines.RemoveRange(startIndex + 1, (endIndex - startIndex) + 1);

            // Insert all custom codes (for now)
            iniLines.InsertRange(startIndex + 1, customCodeLines.FindAll(l => l.StartsWith("$")));

            startIndex = iniLines.IndexOf("[Gecko]");
            endIndex = iniLines.FindIndex(startIndex + 1, l => l.StartsWith("[") && l.EndsWith("]"));
            endIndex = (endIndex >= 0) ? endIndex : iniLines.Count;
            iniLines.InsertRange(endIndex, customCodeLines);

            File.WriteAllLines(INIPath, iniLines);
        }

        public void RestoreINI()
        {
            string backupPath = Path.GetDirectoryName(INIPath) + "\\" + Path.GetFileNameWithoutExtension(INIPath) + "BACKUP" + Path.GetExtension(INIPath);
            File.Copy(backupPath, INIPath, true);
        }
    }
}
