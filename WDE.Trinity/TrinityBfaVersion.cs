using System;
using System.Collections.Generic;
using WDE.Common.CoreVersion;
using WDE.Common.Database;
using WDE.Common.Types;
using WDE.Module.Attributes;

namespace WDE.Trinity
{
    /// <summary>
    /// TrinityCore master branch as it was for Battle for Azeroth 8.3.7 (build 35662, TDB 837.20101).
    /// The world database schema differs a lot from the current TrinityCore master (11.x),
    /// so it gets its own tag and database provider.
    /// </summary>
    [AutoRegister]
    [SingleInstance]
    public class TrinityBfaVersion : ICoreVersion, IDatabaseFeatures, ISmartScriptFeatures, IConditionFeatures, IGameVersionFeatures, IEventAiFeatures
    {
        public string Tag => "TrinityBfa";
        public string FriendlyName => "TrinityCore BfA 8.3.7";
        public ImageUri Icon { get; } = new ImageUri("Icons/core_tc.png");
        public ISmartScriptFeatures SmartScriptFeatures => this;
        public IConditionFeatures ConditionFeatures => this;
        public IGameVersionFeatures GameVersionFeatures => this;
        public IDatabaseFeatures DatabaseFeatures => this;
        public IEventAiFeatures EventAiFeatures => this;
        public PhasingType PhasingType => PhasingType.PhaseIds;
        public GameVersion Version { get; } = new(8, 3, 7, 35662);

        // npc_text in 8.3.7 uses BroadcastTextID0-7 (no text0_0), broadcast_text lives in the hotfixes database,
        // creature_classlevelstats has no base hp/damage columns and areatrigger_create_properties does not exist yet.
        public ISet<Type> UnsupportedTables { get; } = new HashSet<Type>()
        {
            typeof(INpcText),
            typeof(ICreatureClassLevelStat),
            typeof(IBroadcastText),
            typeof(IAreaTriggerCreateProperties)
        };
        public bool AlternativeTrinityDatabase => false;
        public WaypointTables SupportedWaypoints => WaypointTables.WaypointData | WaypointTables.SmartScriptWaypoint | WaypointTables.ScriptWaypoint;
        public bool SpawnGroupTemplateHasType => false;

        public ISet<SmartScriptType> SupportedTypes { get; } = new HashSet<SmartScriptType>
        {
            SmartScriptType.Creature,
            SmartScriptType.GameObject,
            SmartScriptType.AreaTrigger,
            SmartScriptType.Quest,
            SmartScriptType.TimedActionList,
            SmartScriptType.Scene
        };

        public DatabaseTable TableName => DatabaseTable.WorldTable("smart_scripts");
        public bool DifficultyInSeparateColumn => false;
        public string ConditionsFile => "SmartData/conditions.json";
        public string ConditionGroupsFile => "SmartData/conditions_groups.json";
        public string ConditionSourcesFile => "SmartData/condition_sources.json";
        public bool HasConditionStringValue => false;
        public CharacterRaces AllRaces => CharacterRaces.AllBfA;
        public CharacterClasses AllClasses => CharacterClasses.AllBfA;
        public EventScriptType SupportedEventScripts => EventScriptType.Event | EventScriptType.Waypoint | EventScriptType.Spell;
    }
}
