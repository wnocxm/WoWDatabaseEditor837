using LinqToDB;
using LinqToDB.Data;

namespace WDE.TrinityMySqlDatabase.Models;

/// <summary>
/// TrinityCore BfA 8.3.7 hotfixes database (broadcast_text lives here since 8.x).
/// </summary>
public class TrinityBfaHotfixDatabase : DataConnection
{
    public TrinityBfaHotfixDatabase(string connectionString)
        : base(new DataOptions().UseConnectionString(ProviderName.MySqlConnector, connectionString))
    {
    }

    public ITable<MySqlBroadcastTextBfa> BroadcastTexts => this.GetTable<MySqlBroadcastTextBfa>();
    public ITable<MySqlBroadcastTextLocaleBfa> BroadcastTextLocale => this.GetTable<MySqlBroadcastTextLocaleBfa>();
}
