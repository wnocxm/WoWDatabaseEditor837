using System.Collections.Generic;
using System.Linq;
using LinqToDB.Mapping;
using WDE.Common.Database;
using WDE.Common.Utils;

namespace WDE.TrinityMySqlDatabase.Models;

// Models for the TrinityCore BfA 8.3.7 world database (TDB 837.20101).
// Only tables whose layout differs from the other TrinityCore models live here.

[Table(Name = "creature_template")]
public class MySqlCreatureTemplateBfa : ICreatureTemplate
{
    [PrimaryKey]
    [Identity]
    [Column(Name = "entry")]
    public uint Entry { get; set; }

    [Column(Name = "difficulty_entry_1")]
    public uint DifficultyEntry1 { get; set; }

    [Column(Name = "difficulty_entry_2")]
    public uint DifficultyEntry2 { get; set; }

    [Column(Name = "difficulty_entry_3")]
    public uint DifficultyEntry3 { get; set; }

    [Column(Name = "scale")]
    public float Scale { get; set; }

    [Column(Name = "gossip_menu_id")]
    public uint GossipMenuId { get; set; }

    [Column(Name = "minlevel")]
    public short MinLevel { get; set; }

    [Column(Name = "maxlevel")]
    public short MaxLevel { get; set; }

    [Column(Name = "name")]
    public string? _Name { get; set; } = "";

    public string Name => _Name ?? "";

    [Column(Name = "subname")]
    public string? SubName { get; set; } = "";

    [Column(Name = "AIName")]
    public string AIName { get; set; } = "";

    [Column(Name = "ScriptName")]
    public string ScriptName { get; set; } = "";

    [Column(Name = "unit_flags")]
    public GameDefines.UnitFlags UnitFlags { get; set; }

    [Column(Name = "unit_flags2")]
    public GameDefines.UnitFlags2 UnitFlags2 { get; set; }

    [Column(Name = "npcflag")]
    public GameDefines.NpcFlags NpcFlags { get; set; }

    [Column(Name = "speed_walk")]
    public float SpeedWalk { get; set; }

    [Column(Name = "speed_run")]
    public float SpeedRun { get; set; }

    [Column(Name = "faction")]
    public uint FactionTemplate { get; set; }

    public uint? EquipmentTemplateId => null;

    [Column(Name = "IconName")]
    public string? IconName { get; set; }

    [Column(Name = "RequiredExpansion")]
    public short RequiredExpansion { get; set; }

    [Column(Name = "rank")]
    public byte Rank { get; set; }

    [Column(Name = "unit_class")]
    public byte UnitClass { get; set; }

    [Column(Name = "family")]
    public int Family { get; set; }

    [Column(Name = "type")]
    public byte Type { get; set; }

    [Column(Name = "type_flags")]
    public uint TypeFlags { get; set; }

    [Column(Name = "VehicleId")]
    public uint VehicleId { get; set; }

    [Column(Name = "HealthModifier")]
    public float HealthMod { get; set; }

    [Column(Name = "ManaModifier")]
    public float ManaMod { get; set; }

    [Column(Name = "RacialLeader")]
    public bool RacialLeader { get; set; }

    [Column(Name = "movementId")]
    public uint MovementId { get; set; }

    [Column(Name = "KillCredit1")]
    public uint KillCredit1 { get; set; }

    [Column(Name = "KillCredit2")]
    public uint KillCredit2 { get; set; }

    [Column(Name = "flags_extra")]
    public uint FlagsExtra { get; set; }

    [Column(Name = "InhabitType")]
    public GameDefines.InhabitType InhabitType { get; set; }

    [Column(Name = "lootid")]
    public uint LootId { get; set; }

    [Column(Name = "pickpocketloot")]
    public uint PickpocketLootId { get; set; }

    [Column(Name = "skinloot")]
    public uint SkinningLootId { get; set; }

    public int LootCount => 1;

    public uint GetLootId(int index) => LootId;

    // models are stored in creature_template_model since 8.x
    private IReadOnlyList<CreatureTemplateModel>? models;

    public int ModelsCount => models?.Count ?? 0;

    public uint GetModel(int index)
    {
        if (models == null)
            return 0;
        return models.FirstOrDefault(x => x.Index == index)?.CreatureDisplayId ?? 0;
    }

    public ICreatureTemplate WithModels(IReadOnlyList<CreatureTemplateModel> models)
    {
        this.models = models;
        return this;
    }
}

[Table(Name = "creature")]
public class MySqlCreatureBfa : ICreature
{
    [PrimaryKey]
    [Identity]
    [Column(Name = "guid")]
    public uint Guid { get; set; }

    [Column(Name = "id")]
    public uint Entry { get; set; }

    [Column(Name = "map")]
    public int Map { get; set; }

    public uint? PhaseMask => null;

    [Column(Name = "PhaseId")]
    public int? phaseId { get; set; }

    public SmallReadOnlyList<int>? PhaseId => phaseId is null or 0 ? null : new SmallReadOnlyList<int>(phaseId.Value);

    [Column(Name = "PhaseGroup")]
    public int? PhaseGroup { get; set; }

    [Column(Name = "modelid")]
    public uint Model { get; set; }

    [Column(Name = "equipment_id")]
    public int EquipmentId { get; set; }

    [Column(Name = "position_x")]
    public float X { get; set; }

    [Column(Name = "position_y")]
    public float Y { get; set; }

    [Column(Name = "position_z")]
    public float Z { get; set; }

    [Column(Name = "orientation")]
    public float O { get; set; }

    [Column(Name = "MovementType")]
    public MovementType MovementType { get; set; }

    // renamed to wander_distance in later TrinityCore versions
    [Column(Name = "spawndist")]
    public float WanderDistance { get; set; }

    public uint SpawnKey => 0;
}

// 8.3.7 gossip_menu_option keeps only the option itself, actions and boxes are in separate tables
[Table(Name = "gossip_menu_option")]
public class MySqlGossipMenuOptionBfa : IGossipMenuOption
{
    [PrimaryKey]
    [Column(Name = "MenuId")]
    public uint MenuId { get; set; }

    [PrimaryKey]
    [Column(Name = "OptionIndex")]
    public uint OptionIndex { get; set; }

    [Column(Name = "OptionIcon")]
    public GossipOptionIcon Icon { get; set; }

    [Column(Name = "OptionText")]
    public string? Text { get; set; }

    [Column(Name = "OptionBroadcastTextId")]
    public int BroadcastTextId { get; set; }

    [Column(Name = "OptionType")]
    public GossipOption OptionType { get; set; }

    [Column(Name = "OptionNpcFlag")]
    public uint NpcFlag { get; set; }

    [Column(Name = "VerifiedBuild")]
    public int VerifiedBuild { get; set; }

    public int ActionMenuId { get; set; }

    public uint ActionPoiId { get; set; }

    public uint ActionScriptId => 0;

    public uint BoxCoded { get; set; }

    public uint BoxMoney { get; set; }

    public string? BoxText { get; set; }

    public int BoxBroadcastTextId { get; set; }

    public bool HasOptionType => true;

    public MySqlGossipMenuOptionBfa SetAction(MySqlGossipMenuOptionActionBfa? action)
    {
        if (action != null)
        {
            ActionMenuId = (int)action.ActionMenuId;
            ActionPoiId = action.ActionPoiId;
        }
        return this;
    }

    public MySqlGossipMenuOptionBfa SetBox(MySqlGossipMenuOptionBoxBfa? box)
    {
        if (box != null)
        {
            BoxCoded = box.BoxCoded;
            BoxMoney = box.BoxMoney;
            BoxText = box.BoxText;
            BoxBroadcastTextId = (int)box.BoxBroadcastTextId;
        }
        return this;
    }
}

[Table(Name = "gossip_menu_option_action")]
public class MySqlGossipMenuOptionActionBfa
{
    [PrimaryKey]
    [Column(Name = "MenuId")]
    public uint MenuId { get; set; }

    [PrimaryKey]
    [Column(Name = "OptionIndex")]
    public uint OptionIndex { get; set; }

    [Column(Name = "ActionMenuId")]
    public uint ActionMenuId { get; set; }

    [Column(Name = "ActionPoiId")]
    public uint ActionPoiId { get; set; }
}

[Table(Name = "gossip_menu_option_box")]
public class MySqlGossipMenuOptionBoxBfa
{
    [PrimaryKey]
    [Column(Name = "MenuId")]
    public uint MenuId { get; set; }

    [PrimaryKey]
    [Column(Name = "OptionIndex")]
    public uint OptionIndex { get; set; }

    [Column(Name = "BoxCoded")]
    public uint BoxCoded { get; set; }

    [Column(Name = "BoxMoney")]
    public uint BoxMoney { get; set; }

    [Column(Name = "BoxText")]
    public string? BoxText { get; set; }

    [Column(Name = "BoxBroadcastTextId")]
    public uint BoxBroadcastTextId { get; set; }
}

[Table(Name = "areatrigger_template")]
public class MySqlAreaTriggerTemplateBfa : IAreaTriggerTemplate
{
    [PrimaryKey]
    [Column(Name = "Id")]
    public uint Id { get; set; }

    // no server side area triggers in 8.3.7
    public bool IsServerSide => false;

    public string? Name => null;

    [Column(Name = "ScriptName")]
    public string? ScriptName { get; set; }

    public string? AIName => null;
}

[Table(Name = "conversation_actors")]
public class MySqlConversationActorBfa : IConversationActor
{
    [Column(Name = "ConversationId")]
    public uint ConversationId { get; set; }

    [Column(Name = "ConversationActorId")]
    public uint ConversationActorId { get; set; }

    [Column(Name = "Idx")]
    public uint Idx { get; set; }

    public int CreatureId => 0;
}

[Table(Name = "smart_scripts")]
public class MySqlSmartScriptLineBfa : BaseSqlSmartScriptLine
{
    public override SmallReadOnlyList<uint>? Difficulties => default;

    [Column(Name = "event_param5")]
    public override long EventParam5 { get; set; }

    public override long ActionParam7 { get; set; }
}

// 8.3.7 `waypoints` has no orientation and delay columns
[Table(Name = "waypoints")]
public class SmartScriptWaypointBfa : ISmartScriptWaypoint
{
    [PrimaryKey]
    [Column(Name = "entry")]
    public uint PathId { get; set; }

    [PrimaryKey]
    [Column(Name = "pointid")]
    public uint PointId { get; set; }

    [Column(Name = "position_x")]
    public float X { get; set; }

    [Column(Name = "position_y")]
    public float Y { get; set; }

    [Column(Name = "position_z")]
    public float Z { get; set; }

    public float? Orientation => null;

    public float? Velocity => null;

    public uint Delay => 0;

    public bool? SmoothTransition => null;

    [Column(Name = "point_comment")]
    public string? Comment { get; set; }

    public UniversalWaypoint ToUniversal()
    {
        return new UniversalWaypoint()
        {
            PathId = PathId,
            PointId = PointId,
            X = X,
            Y = Y,
            Z = Z,
            Orientation = Orientation,
            Velocity = Velocity,
            Delay = Delay,
            SmoothTransition = SmoothTransition,
            Comment = Comment
        };
    }
}

// hotfixes database

[Table(Name = "broadcast_text")]
public class MySqlBroadcastTextBfa : IBroadcastText
{
    [PrimaryKey]
    [Column(Name = "ID")]
    public uint Id { get; set; }

    [Column(Name = "LanguageID")]
    public uint Language { get; set; }

    [Column(Name = "Text")]
    public string? Text { get; set; }

    [Column(Name = "Text1")]
    public string? Text1 { get; set; }

    [Column(Name = "EmoteID1")]
    public uint EmoteId1 { get; set; }

    [Column(Name = "EmoteID2")]
    public uint EmoteId2 { get; set; }

    [Column(Name = "EmoteID3")]
    public uint EmoteId3 { get; set; }

    [Column(Name = "EmoteDelay1")]
    public uint EmoteDelay1 { get; set; }

    [Column(Name = "EmoteDelay2")]
    public uint EmoteDelay2 { get; set; }

    [Column(Name = "EmoteDelay3")]
    public uint EmoteDelay3 { get; set; }

    [Column(Name = "SoundEntriesID1")]
    public uint Sound1 { get; set; }

    [Column(Name = "SoundEntriesID2")]
    public uint Sound2 { get; set; }

    public uint SoundEntriesId => Sound1;

    [Column(Name = "EmotesID")]
    public uint EmotesId { get; set; }

    [Column(Name = "Flags")]
    public uint Flags { get; set; }

    [PrimaryKey]
    [Column(Name = "VerifiedBuild")]
    public int VerifiedBuild { get; set; }
}

[Table(Name = "broadcast_text_locale")]
public class MySqlBroadcastTextLocaleBfa : IBroadcastTextLocale
{
    [PrimaryKey]
    [Column(Name = "ID")]
    public uint Id { get; set; }

    [PrimaryKey]
    [Column(Name = "locale")]
    public string Locale { get; set; } = "";

    [Column(Name = "Text_lang")]
    public string? Text { get; set; }

    [Column(Name = "Text1_lang")]
    public string? Text1 { get; set; }
}
