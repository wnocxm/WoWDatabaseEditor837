namespace WDE.MPQ.ViewModels
{
    public enum WoWFilesType
    {
        Invalid,
        Wrath,
        Mop,
        Cata,
        Legion,
        Retail
    }
    
    public interface IWoWFilesVerifier
    {
        WoWFilesType VerifyFolder(string? wowFolder);

        /// <summary>
        /// Returns the game root folder (the one with the Data folder) for the selected folder.
        /// Since 8.x the game executable is in World of Warcraft/_retail_ while the data is in World of Warcraft/Data,
        /// so both folders can be selected.
        /// </summary>
        string? ResolveGameFolder(string? wowFolder);
    }
}