using LinqToDB;
using WDE.MySqlDatabaseCommon.CommonModels;

namespace WDE.TrinityMySqlDatabase.Models;

/// <summary>
/// TrinityCore BfA 8.3.7 world database (TDB 837.20101).
/// Loot tables, conditions, gossip_menu, quest tables, spawn groups etc. come from <see cref="BaseTrinityDatabase"/>.
/// </summary>
public class TrinityBfaDatabase : BaseTrinityDatabase
{
    public ITable<MySqlCreatureTemplateBfa> CreatureTemplate => this.GetTable<MySqlCreatureTemplateBfa>();
    public ITable<CreatureTemplateModel> CreatureTemplateModel => this.GetTable<CreatureTemplateModel>();
    public ITable<MySqlCreatureBfa> Creature => this.GetTable<MySqlCreatureBfa>();
    public ITable<TrinityString> Strings => this.GetTable<TrinityString>();
    public ITable<MySqlGameObjectCata> GameObject => this.GetTable<MySqlGameObjectCata>();
    public ITable<MySqlCreatureModelInfoShadowlands> CreatureModelInfo => this.GetTable<MySqlCreatureModelInfoShadowlands>();
    public ITable<MySqlCreatureAddonAC> CreatureAddon => this.GetTable<MySqlCreatureAddonAC>();
    public ITable<MySqlCreatureTemplateAddonAC> CreatureTemplateAddon => this.GetTable<MySqlCreatureTemplateAddonAC>();
    public ITable<MySqlAzerothQuestTemplateAddon> QuestTemplateAddon => this.GetTable<MySqlAzerothQuestTemplateAddon>();
    public ITable<MySqlPlayerChoice> PlayerChoice => this.GetTable<MySqlPlayerChoice>();
    public ITable<MySqlPlayerChoiceResponse> PlayerChoiceResponse => this.GetTable<MySqlPlayerChoiceResponse>();
    public ITable<MySqlQuestObjective> QuestObjective => this.GetTable<MySqlQuestObjective>();
    public ITable<MySqlEventScriptNoCommentLine> EventScripts => this.GetTable<MySqlEventScriptNoCommentLine>();
    public ITable<MySqlSpellScriptNoCommentLine> SpellScripts => this.GetTable<MySqlSpellScriptNoCommentLine>();
    public ITable<MySqlWaypointScriptNoCommentLine> WaypointScripts => this.GetTable<MySqlWaypointScriptNoCommentLine>();
    public ITable<MySqlSmartScriptLineBfa> SmartScript => this.GetTable<MySqlSmartScriptLineBfa>();
    public ITable<NonMasterWaypointData> WaypointData => this.GetTable<NonMasterWaypointData>();
    public ITable<SmartScriptWaypointBfa> SmartScriptWaypointBfa => this.GetTable<SmartScriptWaypointBfa>();
    public ITable<MySqlGossipMenuOptionBfa> GossipMenuOptionsBfa => this.GetTable<MySqlGossipMenuOptionBfa>();
    public ITable<MySqlGossipMenuOptionActionBfa> GossipMenuOptionActions => this.GetTable<MySqlGossipMenuOptionActionBfa>();
    public ITable<MySqlGossipMenuOptionBoxBfa> GossipMenuOptionBoxes => this.GetTable<MySqlGossipMenuOptionBoxBfa>();
    public ITable<MySqlAreaTriggerTemplateBfa> AreaTriggerTemplateBfa => this.GetTable<MySqlAreaTriggerTemplateBfa>();
    public ITable<MySqlConversationActorBfa> ConversationActor => this.GetTable<MySqlConversationActorBfa>();
}
