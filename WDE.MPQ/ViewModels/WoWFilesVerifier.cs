using System.IO;
using WDE.Module.Attributes;

namespace WDE.MPQ.ViewModels
{
    [UniqueProvider]
    [AutoRegister]
    internal class WoWFilesVerifier : IWoWFilesVerifier
    {
        private string[] wrathMpqs => new string[]{"common.MPQ", "common-2.MPQ", "expansion.MPQ", "lichking.MPQ"};
        private string[] mopMpqs => new string[]
            { "art.MPQ", "expansion1.MPQ", "expansion2.MPQ", "expansion3.MPQ", "expansion4.MPQ", "world.MPQ", "world2.MPQ" };
        private string[] cataMpqs => new string[]
            { "art.MPQ", "expansion1.MPQ", "expansion2.MPQ", "expansion3.MPQ", "world.MPQ", "world2.MPQ" };
        
        // retail clients (8.x+) are 64 bit only and use Wow.exe, Wow-64.exe was used by 6.x-7.x
        private static readonly string[] executableNames = { "wow.exe", "Wow.exe", "WoW.exe", "Wow-64.exe", "WowB.exe", "WowT.exe" };
        private static readonly string[] retailSubfolders = { "_retail_", "_ptr_", "_beta_" };

        private static bool HasGameExecutable(string folder)
        {
            if (Directory.Exists(Path.Join(folder, "World of Warcraft.app")))
                return true;
            foreach (var exe in executableNames)
                if (File.Exists(Path.Join(folder, exe)))
                    return true;
            return false;
        }

        public string? ResolveGameFolder(string? wowFolder)
        {
            if (string.IsNullOrEmpty(wowFolder) || !Directory.Exists(wowFolder))
                return null;

            var hasData = Directory.Exists(Path.Join(wowFolder, "Data"));

            // classic layout: wow.exe and Data in the same folder
            if (hasData && HasGameExecutable(wowFolder))
                return wowFolder;

            // retail layout, root selected: World of Warcraft/_retail_/Wow.exe + World of Warcraft/Data
            if (hasData)
            {
                foreach (var sub in retailSubfolders)
                    if (HasGameExecutable(Path.Join(wowFolder, sub)))
                        return wowFolder;
            }

            // retail layout, _retail_ selected
            if (HasGameExecutable(wowFolder))
            {
                var parent = Directory.GetParent(Path.TrimEndingDirectorySeparator(wowFolder))?.FullName;
                if (parent != null && Directory.Exists(Path.Join(parent, "Data")))
                    return parent;
            }

            return null;
        }

        public WoWFilesType VerifyFolder(string? selectedFolder)
        {
            var wowFolder = ResolveGameFolder(selectedFolder);
            if (wowFolder == null)
                return WoWFilesType.Invalid;

            if (File.Exists(Path.Join(wowFolder, "Data", "config", "3b", "05", "3b0517b51edbe0b96f6ac5ea7eaaed38")))
                return WoWFilesType.Legion;

            if (IsRetailCasc(wowFolder))
                return WoWFilesType.Retail;
            
            if (ContainsAllFiles(wowFolder, wrathMpqs))
                return WoWFilesType.Wrath;
            
            if (ContainsAllFiles(wowFolder, mopMpqs))
                return WoWFilesType.Mop;
            
            if (ContainsAllFiles(wowFolder, cataMpqs))
                return WoWFilesType.Cata;

            return WoWFilesType.Invalid;
        }

        private bool IsRetailCasc(string wowFolder)
        {
            return Directory.Exists(Path.Join(wowFolder, "Data", "config"));
        }

        private bool ContainsAllFiles(string wowFolder, string[] files)
        {
            foreach (var mpq in files)
            {
                var path = new FileInfo(Path.Join(wowFolder, "Data", mpq));
                if (!path.Exists)
                    return false;
            }

            return true;
        }
    }
}