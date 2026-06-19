using System.Collections.Generic;
using BannerlordTwitch.Localization;
using BannerlordTwitch.UI;
using JetBrains.Annotations;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace BLTAdoptAHero.Prestige
{
    public class PrestigeSettings
    {
        [LocDisplayName("Min Tier Required"),
         LocDescription("Minimum equipment tier the hero must have to prestige. 4 = T4, 6 = T6, 8 = T8 (max)."),
         PropertyOrder(0), UsedImplicitly]
        public int MinTierRequired { get; set; } = 8;

        [LocDisplayName("Require Kills"),
         LocDescription("Battle kills required between prestiges. 0 = no kill requirement."),
         PropertyOrder(1), UsedImplicitly]
        public int RequireKills { get; set; } = 500;

        [LocDisplayName("Require Channel Points"),
         LocDescription("If enabled, !prestige only executes when triggered as a Channel Point Reward (not a free command)."),
         PropertyOrder(2), UsedImplicitly]
        public bool RequireChannelPoints { get; set; } = true;

        [LocDisplayName("Channel Points Cost"),
         LocDescription("Channel points cost — set the same value on the Twitch reward in BLT Configure."),
         PropertyOrder(3), UsedImplicitly]
        public int ChannelPointsCost { get; set; } = 25000;

        [LocDisplayName("Max Prestige Level"),
         LocDescription("Maximum prestige level (must not exceed number of entries in Prestige Levels list)."),
         PropertyOrder(4), UsedImplicitly]
        public int MaxPrestigeLevel { get; set; } = 5;

        [LocDisplayName("Prestige Levels"),
         LocDescription("Bonus definitions for P1 through P5. All bonuses are cumulative."),
         PropertyOrder(5), ExpandableObject, UsedImplicitly]
        public List<PrestigeLevelDef> Levels { get; set; } = new()
        {
            new PrestigeLevelDef { GoldPerKillBonusPercent = 15, XPMultiplierBonus = 0f, DamageBonusPercent = 5,  ArmorBonus = 3,  SpeedBonusPercent = 0, HPBonus = 0,   ChatTitle = "[P1]", StartBattleInvincibleSeconds = 0 },
            new PrestigeLevelDef { GoldPerKillBonusPercent = 15, XPMultiplierBonus = 0f, DamageBonusPercent = 5,  ArmorBonus = 3,  SpeedBonusPercent = 3, HPBonus = 0,   ChatTitle = "[P2]", StartBattleInvincibleSeconds = 0 },
            new PrestigeLevelDef { GoldPerKillBonusPercent = 15, XPMultiplierBonus = 1f, DamageBonusPercent = 5,  ArmorBonus = 4,  SpeedBonusPercent = 3, HPBonus = 25,  ChatTitle = "[P3]", StartBattleInvincibleSeconds = 0 },
            new PrestigeLevelDef { GoldPerKillBonusPercent = 0,  XPMultiplierBonus = 1f, DamageBonusPercent = 5,  ArmorBonus = 5,  SpeedBonusPercent = 4, HPBonus = 25,  ChatTitle = "[P4]", StartBattleInvincibleSeconds = 0 },
            new PrestigeLevelDef { GoldPerKillBonusPercent = 0,  XPMultiplierBonus = 0f, DamageBonusPercent = 10, ArmorBonus = 5,  SpeedBonusPercent = 5, HPBonus = 50,  ChatTitle = "[P5]", StartBattleInvincibleSeconds = 10 },
        };

        // --- Cumulative getters ---

        public int GetCumulativeGoldBonusPercent(int prestigeLevel)
        {
            int total = 0;
            for (int i = 0; i < prestigeLevel && i < Levels.Count; i++)
                total += Levels[i].GoldPerKillBonusPercent;
            return total;
        }

        public float GetCumulativeXPMultiplier(int prestigeLevel)
        {
            float total = 1.0f;
            for (int i = 0; i < prestigeLevel && i < Levels.Count; i++)
                total += Levels[i].XPMultiplierBonus;
            return total;
        }

        public int GetCumulativeDamageBonusPercent(int prestigeLevel)
        {
            int total = 0;
            for (int i = 0; i < prestigeLevel && i < Levels.Count; i++)
                total += Levels[i].DamageBonusPercent;
            return total;
        }

        public int GetCumulativeArmorBonus(int prestigeLevel)
        {
            int total = 0;
            for (int i = 0; i < prestigeLevel && i < Levels.Count; i++)
                total += Levels[i].ArmorBonus;
            return total;
        }

        public int GetCumulativeSpeedBonusPercent(int prestigeLevel)
        {
            int total = 0;
            for (int i = 0; i < prestigeLevel && i < Levels.Count; i++)
                total += Levels[i].SpeedBonusPercent;
            return total;
        }

        public int GetCumulativeHPBonus(int prestigeLevel)
        {
            int total = 0;
            for (int i = 0; i < prestigeLevel && i < Levels.Count; i++)
                total += Levels[i].HPBonus;
            return total;
        }

        public int GetInvincibleSeconds(int prestigeLevel)
        {
            int max = 0;
            for (int i = 0; i < prestigeLevel && i < Levels.Count; i++)
                if (Levels[i].StartBattleInvincibleSeconds > max)
                    max = Levels[i].StartBattleInvincibleSeconds;
            return max;
        }

        public string GetChatTitle(int prestigeLevel)
        {
            string title = "";
            for (int i = 0; i < prestigeLevel && i < Levels.Count; i++)
                if (!string.IsNullOrEmpty(Levels[i].ChatTitle))
                    title = Levels[i].ChatTitle;
            return title;
        }

        // Human-readable summary of all cumulative bonuses for given prestige level
        public string GetBonusSummary(int prestigeLevel)
        {
            var parts = new System.Collections.Generic.List<string>();
            int gold = GetCumulativeGoldBonusPercent(prestigeLevel);
            float xp = GetCumulativeXPMultiplier(prestigeLevel);
            int dmg = GetCumulativeDamageBonusPercent(prestigeLevel);
            int arm = GetCumulativeArmorBonus(prestigeLevel);
            int spd = GetCumulativeSpeedBonusPercent(prestigeLevel);
            int hp = GetCumulativeHPBonus(prestigeLevel);
            int inv = GetInvincibleSeconds(prestigeLevel);

            if (gold > 0) parts.Add($"+{gold}% gold/kill");
            if (xp > 1f)  parts.Add($"{xp:F1}x XP");
            if (dmg > 0)  parts.Add($"+{dmg}% dmg");
            if (arm > 0)  parts.Add($"+{arm} armor");
            if (spd > 0)  parts.Add($"+{spd}% speed");
            if (hp > 0)   parts.Add($"+{hp} HP");
            if (inv > 0)  parts.Add($"{inv}s invincibility");

            return parts.Count > 0 ? string.Join(", ", parts) : "no bonuses";
        }
    }
}
