using BannerlordTwitch.Localization;
using BannerlordTwitch.UI;
using JetBrains.Annotations;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace BLTAdoptAHero.Prestige
{
    public class PrestigeLevelDef
    {
        [LocDisplayName("Gold Per Kill Bonus %"),
         LocDescription("Added to cumulative gold bonus per kill (stacks with lower prestige levels)."),
         PropertyOrder(0), UsedImplicitly]
        public int GoldPerKillBonusPercent { get; set; } = 15;

        [LocDisplayName("XP Multiplier Bonus"),
         LocDescription("Added to cumulative XP multiplier (0.0 = no change, 1.0 = double XP from this level alone)."),
         PropertyOrder(1), UsedImplicitly]
        public float XPMultiplierBonus { get; set; } = 0.0f;

        [LocDisplayName("Damage Bonus %"),
         LocDescription("Added to cumulative damage dealt in battle (e.g. 10 = +10%). Stacks across prestige levels."),
         PropertyOrder(2), UsedImplicitly]
        public int DamageBonusPercent { get; set; } = 0;

        [LocDisplayName("Armor Bonus"),
         LocDescription("Flat armor effectiveness added per prestige level (applied to all body parts). Stacks cumulatively."),
         PropertyOrder(3), UsedImplicitly]
        public int ArmorBonus { get; set; } = 0;

        [LocDisplayName("Speed Bonus %"),
         LocDescription("Added to cumulative movement speed in battle (e.g. 5 = +5%). Stacks across prestige levels."),
         PropertyOrder(4), UsedImplicitly]
        public int SpeedBonusPercent { get; set; } = 0;

        [LocDisplayName("HP Bonus"),
         LocDescription("Flat HP added to hero base health limit. Stacks cumulatively across prestige levels."),
         PropertyOrder(5), UsedImplicitly]
        public int HPBonus { get; set; } = 0;

        [LocDisplayName("Chat Title"),
         LocDescription("Tag shown next to hero name in chat messages at this prestige level."),
         PropertyOrder(6), UsedImplicitly]
        public string ChatTitle { get; set; } = "";

        [LocDisplayName("Start Battle Invincible Seconds"),
         LocDescription("Seconds of invincibility at the start of each battle. 0 = disabled."),
         PropertyOrder(7), UsedImplicitly]
        public int StartBattleInvincibleSeconds { get; set; } = 0;
    }
}
