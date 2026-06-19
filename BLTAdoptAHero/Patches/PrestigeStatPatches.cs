using HarmonyLib;
using JetBrains.Annotations;
using TaleWorlds.CampaignSystem;
using TaleWorlds.MountAndBlade;

namespace BLTAdoptAHero.Patches
{
    // HP: T8 double-HP + cumulative flat HP bonus from prestige
    [HarmonyPatch(typeof(Agent), "BaseHealthLimit", MethodType.Getter), UsedImplicitly]
    public static class PrestigeHealthPatch
    {
        [UsedImplicitly]
        public static void Postfix(Agent __instance, ref float __result)
        {
            var hero = (__instance.Character as CharacterObject)?.HeroObject;
            if (hero == null) return;

            int tier = BLTAdoptAHeroCampaignBehavior.Current?.GetEquipmentTier(hero) ?? -1;
            if (tier >= 7)
                __result *= 2f;

            int prestige = BLTAdoptAHeroCampaignBehavior.Current?.GetPrestigeLevel(hero) ?? 0;
            if (prestige > 0)
                __result += BLTAdoptAHeroModule.CommonConfig.PrestigeConfig.GetCumulativeHPBonus(prestige);
        }
    }

    // Damage: multiply blow damage by prestige damage bonus
    [HarmonyPatch(typeof(Mission), "RegisterBlow"), UsedImplicitly]
    public static class PrestigeDamagePatch
    {
        [UsedImplicitly]
        public static void Prefix(Agent attacker, ref Blow blow)
        {
            if (attacker == null) return;
            var hero = (attacker.Character as CharacterObject)?.HeroObject;
            if (hero == null) return;

            int prestige = BLTAdoptAHeroCampaignBehavior.Current?.GetPrestigeLevel(hero) ?? 0;
            if (prestige <= 0) return;

            int dmgBonus = BLTAdoptAHeroModule.CommonConfig.PrestigeConfig.GetCumulativeDamageBonusPercent(prestige);
            if (dmgBonus <= 0) return;

            float mult = 1f + dmgBonus / 100f;
            blow.BaseMagnitude *= mult;
            blow.InflictedDamage = (int)(blow.InflictedDamage * mult);
        }
    }

    // Speed: multiply agent forward speed by prestige speed bonus
    [HarmonyPatch(typeof(Agent), "MaximumForwardUnlimitedSpeed", MethodType.Getter), UsedImplicitly]
    public static class PrestigeSpeedPatch
    {
        [UsedImplicitly]
        public static void Postfix(Agent __instance, ref float __result)
        {
            var hero = (__instance.Character as CharacterObject)?.HeroObject;
            if (hero == null) return;

            int prestige = BLTAdoptAHeroCampaignBehavior.Current?.GetPrestigeLevel(hero) ?? 0;
            if (prestige <= 0) return;

            int spdBonus = BLTAdoptAHeroModule.CommonConfig.PrestigeConfig.GetCumulativeSpeedBonusPercent(prestige);
            if (spdBonus > 0)
                __result *= 1f + spdBonus / 100f;
        }
    }

    // Armor: add flat armor bonus to adopted heroes
    [HarmonyPatch(typeof(Agent), "GetBaseArmorEffectivenessForBodyPart"), UsedImplicitly]
    public static class PrestigeArmorPatch
    {
        [UsedImplicitly]
        public static void Postfix(Agent __instance, ref float __result)
        {
            var hero = (__instance.Character as CharacterObject)?.HeroObject;
            if (hero == null) return;

            int prestige = BLTAdoptAHeroCampaignBehavior.Current?.GetPrestigeLevel(hero) ?? 0;
            if (prestige <= 0) return;

            int armorBonus = BLTAdoptAHeroModule.CommonConfig.PrestigeConfig.GetCumulativeArmorBonus(prestige);
            if (armorBonus > 0)
                __result += armorBonus;
        }
    }
}
