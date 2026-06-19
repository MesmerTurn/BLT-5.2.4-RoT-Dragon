using System;
using System.Linq;
using BannerlordTwitch;
using BannerlordTwitch.Localization;
using BannerlordTwitch.Rewards;
using JetBrains.Annotations;
using TaleWorlds.MountAndBlade;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace BLTAdoptAHero
{
    [LocDisplayName("Upgrade Class"),
     LocDescription("Shows class progression status. If kills are sufficient and a target class is specified (or only one upgrade exists), advances the hero to the next class."),
     UsedImplicitly]
    public class UpgradeClass : ActionHandlerBase
    {
        private class Settings : IDocumentable
        {
            [LocDisplayName("Status Only"),
             LocDescription("If true, only shows progression status — never actually upgrades the class."),
             PropertyOrder(0), UsedImplicitly]
            public bool StatusOnly { get; set; } = false;

            [LocDisplayName("Reset Equipment Tier"),
             LocDescription("If true, resets hero equipment to T1 on class upgrade (keeping custom items)."),
             PropertyOrder(1), UsedImplicitly]
            public bool ResetEquipmentOnUpgrade { get; set; } = true;

            public void GenerateDocumentation(IDocumentationGenerator generator)
            {
                var tree = BLTAdoptAHeroModule.CommonConfig.ClassProgression;
                generator.PropertyValuePair("Progression Enabled", $"{tree.Enabled}");
                generator.PropertyValuePair("Starting Class", tree.GetStartingNode()?.ClassName ?? "none");
            }
        }

        protected override Type ConfigType => typeof(Settings);

        protected override void ExecuteInternal(ReplyContext context, object config,
            Action<string> onSuccess, Action<string> onFailure)
        {
            var settings = config as Settings ?? new Settings();
            var tree = BLTAdoptAHeroModule.CommonConfig.ClassProgression;

            if (!tree.Enabled)
            {
                onFailure("Class progression is disabled.");
                return;
            }

            var adoptedHero = BLTAdoptAHeroCampaignBehavior.Current.GetAdoptedHero(context.UserName);
            if (adoptedHero == null)
            {
                onFailure(AdoptAHero.NoHeroMessage);
                return;
            }

            string currentClass = BLTAdoptAHeroCampaignBehavior.Current.GetProgressionClass(adoptedHero);
            int killCount = BLTAdoptAHeroCampaignBehavior.Current.GetProgressionKills(adoptedHero);

            // If no class assigned yet, assign starting class
            if (string.IsNullOrEmpty(currentClass))
            {
                currentClass = tree.GetStartingNode()?.ClassName ?? "";
                BLTAdoptAHeroCampaignBehavior.Current.SetProgressionClass(adoptedHero, currentClass);
            }

            var node = tree.GetNode(currentClass);
            if (node == null)
            {
                onFailure($"Unknown class '{currentClass}' — contact the streamer.");
                return;
            }

            // --- Status only or not enough kills ---
            if (settings.StatusOnly || !tree.CanUpgrade(currentClass, killCount))
            {
                onSuccess(tree.GetStatusString(currentClass, killCount));
                return;
            }

            // --- Determine target class ---
            string targetClass = null;
            var nextClasses = tree.GetNextClasses(currentClass);

            if (nextClasses.Count == 0)
            {
                onSuccess($"[{currentClass}] You are at the maximum class!");
                return;
            }

            string arg = context.Args?.Trim();

            if (nextClasses.Count == 1)
            {
                // Only one option — auto-pick
                targetClass = nextClasses[0];
            }
            else if (!string.IsNullOrEmpty(arg))
            {
                // Try to match by partial name (case-insensitive)
                targetClass = nextClasses.FirstOrDefault(c =>
                    c.Equals(arg, StringComparison.OrdinalIgnoreCase) ||
                    c.StartsWith(arg, StringComparison.OrdinalIgnoreCase));

                if (targetClass == null)
                {
                    onFailure($"Unknown class '{arg}'. Available: {string.Join(", ", nextClasses)}");
                    return;
                }
            }
            else
            {
                // Multiple options, no argument given — show status
                onSuccess(tree.GetStatusString(currentClass, killCount));
                return;
            }

            // --- Can't upgrade during battle ---
            if (Mission.Current != null)
            {
                onFailure("Cannot upgrade class during an active battle!");
                return;
            }

            // --- Execute upgrade ---
            BLTAdoptAHeroCampaignBehavior.Current.SetProgressionClass(adoptedHero, targetClass);
            BLTAdoptAHeroCampaignBehavior.Current.ResetProgressionKills(adoptedHero);

            // Sync HeroClassDef if a matching class def exists
            var matchingClassDef = BLTAdoptAHeroModule.HeroClassConfig.ValidClasses
                .FirstOrDefault(c => c.Name.ToString().Equals(targetClass, StringComparison.OrdinalIgnoreCase));

            if (matchingClassDef != null)
            {
                BLTAdoptAHeroCampaignBehavior.Current.SetEquipmentClass(adoptedHero, matchingClassDef);

                if (settings.ResetEquipmentOnUpgrade)
                {
                    var customItems = BLTAdoptAHeroCampaignBehavior.Current.GetCustomItems(adoptedHero);
                    EquipHero.UpgradeEquipment(
                        adoptedHero,
                        targetTier: 0,
                        classDef: matchingClassDef,
                        replaceSameTier: true,
                        customKeepFilter: element => customItems.Any(c => c.Item == element.Item),
                        restrictedItemIds: BLTAdoptAHeroModule.CommonConfig.RestrictedItemIds
                    );
                }
            }

            string resetNote = (settings.ResetEquipmentOnUpgrade && matchingClassDef != null)
                ? " Equipment reset to T1."
                : (matchingClassDef == null ? " (No matching ClassDef found — only progression class updated.)" : "");

            onSuccess($"[{context.UserName}] Upgraded: {currentClass} → {targetClass}!{resetNote} Kills reset. Next: {tree.GetStatusString(targetClass, 0)}");
        }
    }
}
