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
    }
}