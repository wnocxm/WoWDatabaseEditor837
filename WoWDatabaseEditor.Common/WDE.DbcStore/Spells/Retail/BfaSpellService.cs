namespace WDE.DbcStore.Spells.Retail;

/// <summary>
/// Battle for Azeroth 8.3.7 (35662). Spell DB2 files and columns used by <see cref="RetailSpellService"/>
/// are present in 8.3.7 (verified against WoWDBDefs).
/// </summary>
public class BfaSpellService : RetailSpellService
{
    public BfaSpellService(DBCD.DBCD dbcd) : base(dbcd)
    {
    }

    public override DBCVersions Version => DBCVersions.BFA_35662;
}
