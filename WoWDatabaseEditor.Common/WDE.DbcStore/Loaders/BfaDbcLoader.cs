using WDE.Common.DBC;
using WDE.DbcStore.Providers;
using WDE.Module.Attributes;

namespace WDE.DbcStore.Loaders;

/// <summary>
/// Battle for Azeroth 8.3.7 (35662). The client files are WDC3 DB2s with the same file and column names
/// the retail loader uses (verified against WoWDBDefs), DBCD picks the right layout by layout hash.
/// </summary>
[AutoRegister]
internal class BfaDbcLoader : RetailDbcLoader
{
    public BfaDbcLoader(IDbcSettingsProvider dbcSettingsProvider,
        IDatabaseClientFileOpener opener,
        DBCD.DBCD dbcd) : base(dbcSettingsProvider, opener, dbcd)
    {
    }

    public override DBCVersions Version => DBCVersions.BFA_35662;
}
