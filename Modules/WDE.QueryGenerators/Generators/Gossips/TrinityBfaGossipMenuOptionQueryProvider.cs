using System.Collections.Generic;
using System.Linq;
using WDE.Common.Database;
using WDE.Module.Attributes;
using WDE.QueryGenerators.Base;
using WDE.SqlQueryGenerator;

namespace WDE.QueryGenerators.Generators.Gossips;

/// <summary>
/// TrinityCore 8.3.7 splits a gossip option into three tables:
/// gossip_menu_option, gossip_menu_option_action and gossip_menu_option_box.
/// </summary>
[AutoRegister]
[SingleInstance]
[RequiresCore("TrinityBfa")]
public class TrinityBfaGossipMenuOptionQueryProvider : IInsertQueryProvider<IGossipMenuOption>, IDeleteQueryProvider<IGossipMenuOption>
{
    private static readonly DatabaseTable ActionTable = DatabaseTable.WorldTable("gossip_menu_option_action");
    private static readonly DatabaseTable BoxTable = DatabaseTable.WorldTable("gossip_menu_option_box");

    public DatabaseTable TableName => DatabaseTable.WorldTable("gossip_menu_option");

    public IQuery Insert(IGossipMenuOption t) => BulkInsert(new[] { t });

    public IQuery BulkInsert(IReadOnlyCollection<IGossipMenuOption> collection)
    {
        var trans = Queries.BeginTransaction(DataDatabaseType.World);

        trans.Table(TableName).BulkInsert(collection.Select(option => new
        {
            MenuId = option.MenuId,
            OptionIndex = option.OptionIndex,
            OptionIcon = (int)option.Icon,
            OptionText = option.Text,
            OptionBroadcastTextId = option.BroadcastTextId,
            OptionType = (uint)option.OptionType,
            OptionNpcFlag = option.NpcFlag,
            __comment = option.__comment,
            __ignored = option.__ignored
        }));

        var actions = collection.Where(option => option.ActionMenuId != 0 || option.ActionPoiId != 0).ToList();
        if (actions.Count > 0)
        {
            trans.Table(ActionTable).BulkInsert(actions.Select(option => new
            {
                MenuId = option.MenuId,
                OptionIndex = option.OptionIndex,
                ActionMenuId = option.ActionMenuId,
                ActionPoiId = option.ActionPoiId,
                __ignored = option.__ignored
            }));
        }

        var boxes = collection.Where(option => option.BoxCoded != 0 || option.BoxMoney != 0 || !string.IsNullOrEmpty(option.BoxText) || option.BoxBroadcastTextId != 0).ToList();
        if (boxes.Count > 0)
        {
            trans.Table(BoxTable).BulkInsert(boxes.Select(option => new
            {
                MenuId = option.MenuId,
                OptionIndex = option.OptionIndex,
                BoxCoded = option.BoxCoded,
                BoxMoney = option.BoxMoney,
                BoxText = option.BoxText,
                BoxBroadcastTextId = option.BoxBroadcastTextId,
                __ignored = option.__ignored
            }));
        }

        return trans.Close();
    }

    public IQuery Delete(IGossipMenuOption t)
    {
        var trans = Queries.BeginTransaction(DataDatabaseType.World);
        trans.Table(TableName).Where(row => row.Column<uint>("MenuId") == t.MenuId).Delete();
        trans.Table(ActionTable).Where(row => row.Column<uint>("MenuId") == t.MenuId).Delete();
        trans.Table(BoxTable).Where(row => row.Column<uint>("MenuId") == t.MenuId).Delete();
        return trans.Close();
    }
}
