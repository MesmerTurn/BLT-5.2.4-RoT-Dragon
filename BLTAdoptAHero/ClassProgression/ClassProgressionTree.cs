using System.Collections.Generic;
using System.Linq;
using BannerlordTwitch.Localization;
using JetBrains.Annotations;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace BLTAdoptAHero.ClassProgression
{
    public class ClassProgressionTree
    {
        [LocDisplayName("Enable Class Progression"),
         LocDescription("If enabled, heroes unlock new classes by earning kills in battle."),
         PropertyOrder(0), UsedImplicitly]
        public bool Enabled { get; set; } = true;

        [LocDisplayName("Class Nodes"),
         LocDescription("Full tree definition. Each node lists the class name, kills required, and available upgrades."),
         PropertyOrder(1), UsedImplicitly]
        public List<ClassProgressionNode> Nodes { get; set; } = new()
        {
            // --- Piechota (starting class) ---
            new ClassProgressionNode { ClassName = "Piechota", KillsRequired = 25, IsStartingClass = true,
                NextClasses = new() { "Mnich", "Łucznik", "Strażnik", "Lekka jazda", "Rozbójnik" } },

            // --- Tier 2 ---
            new ClassProgressionNode { ClassName = "Mnich",       KillsRequired = 50,
                NextClasses = new() { "Paladyn" } },
            new ClassProgressionNode { ClassName = "Łucznik",     KillsRequired = 50,
                NextClasses = new() { "Cieżki łucznik" } },
            new ClassProgressionNode { ClassName = "Strażnik",    KillsRequired = 50,
                NextClasses = new() { "Halabarda", "Gwardzista" } },
            new ClassProgressionNode { ClassName = "Lekka jazda", KillsRequired = 50,
                NextClasses = new() { "Cieżka Jazda" } },
            new ClassProgressionNode { ClassName = "Rozbójnik",   KillsRequired = 50,
                NextClasses = new() { "Viking" } },

            // --- Tier 3 ---
            new ClassProgressionNode { ClassName = "Paladyn",         KillsRequired = 100,
                NextClasses = new() { "Witch Hunter", "Inkwizytor" } },
            new ClassProgressionNode { ClassName = "Cieżki łucznik",  KillsRequired = 100,
                NextClasses = new() { "Glewuk" } },
            new ClassProgressionNode { ClassName = "Halabarda",       KillsRequired = 100,
                NextClasses = new() { "Yobusame" } },
            new ClassProgressionNode { ClassName = "Gwardzista",      KillsRequired = 100,
                NextClasses = new() { "Yobusame" } },
            new ClassProgressionNode { ClassName = "Cieżka Jazda",    KillsRequired = 100,
                NextClasses = new() { "Husarz" } },
            new ClassProgressionNode { ClassName = "Viking",          KillsRequired = 100,
                NextClasses = new() { "Huskarl", "Berserker" } },

            // --- Tier 4 ---
            new ClassProgressionNode { ClassName = "Witch Hunter", KillsRequired = 100,
                NextClasses = new() { "Dragon Hunter" } },
            new ClassProgressionNode { ClassName = "Inkwizytor",   KillsRequired = 100,
                NextClasses = new() { "Dragon Hunter" } },
            new ClassProgressionNode { ClassName = "Glewuk",       KillsRequired = 100,
                NextClasses = new() { "Veteran" } },
            new ClassProgressionNode { ClassName = "Yobusame",     KillsRequired = 100,
                NextClasses = new() { "Veteran" } },
            new ClassProgressionNode { ClassName = "Husarz",       KillsRequired = 100,
                NextClasses = new() { "Gladiator" } },
            new ClassProgressionNode { ClassName = "Huskarl",      KillsRequired = 100,
                NextClasses = new() { "Jarl" } },
            new ClassProgressionNode { ClassName = "Berserker",    KillsRequired = 100,
                NextClasses = new() { "Jarl" } },

            // --- Tier 5 ---
            new ClassProgressionNode { ClassName = "Dragon Hunter", KillsRequired = 100,
                NextClasses = new() { } },
            new ClassProgressionNode { ClassName = "Veteran",       KillsRequired = 100,
                NextClasses = new() { "Gladiator" } },
            new ClassProgressionNode { ClassName = "Jarl",          KillsRequired = 100,
                NextClasses = new() { "Gladiator" } },

            // --- Max class ---
            new ClassProgressionNode { ClassName = "Gladiator", KillsRequired = 0,
                NextClasses = new() { } },
        };

        // --- Helpers ---

        public ClassProgressionNode GetNode(string className)
            => Nodes.FirstOrDefault(n => n.ClassName == className);

        public ClassProgressionNode GetStartingNode()
            => Nodes.FirstOrDefault(n => n.IsStartingClass) ?? Nodes.FirstOrDefault();

        /// <summary>Returns true when hero has earned enough kills to leave their current class.</summary>
        public bool CanUpgrade(string currentClass, int killCount)
        {
            var node = GetNode(currentClass);
            return node != null && node.NextClasses.Count > 0 && killCount >= node.KillsRequired;
        }

        /// <summary>Returns available next class names for the current class.</summary>
        public List<string> GetNextClasses(string currentClass)
            => GetNode(currentClass)?.NextClasses ?? new List<string>();

        /// <summary>Returns true if targetClass is a valid upgrade from currentClass.</summary>
        public bool IsValidUpgrade(string currentClass, string targetClass)
            => GetNode(currentClass)?.NextClasses.Contains(targetClass) ?? false;

        /// <summary>Build status string for chat.</summary>
        public string GetStatusString(string currentClass, int killCount)
        {
            var node = GetNode(currentClass);
            if (node == null) return $"Unknown class: {currentClass}";

            if (node.NextClasses.Count == 0)
                return $"[{currentClass}] MAX CLASS";

            int needed = node.KillsRequired - killCount;
            if (needed > 0)
                return $"[{currentClass}] | Kills: {killCount}/{node.KillsRequired} (need {needed} more) | Next: {string.Join(" / ", node.NextClasses)}";

            if (node.NextClasses.Count == 1)
                return $"[{currentClass}] READY! Type !upgrade to advance to {node.NextClasses[0]}";

            return $"[{currentClass}] READY! Type !upgrade <class> to choose: {string.Join(" / ", node.NextClasses)}";
        }
    }
}
