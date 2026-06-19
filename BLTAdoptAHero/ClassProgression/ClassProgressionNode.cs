using System.Collections.Generic;
using BannerlordTwitch.Localization;
using JetBrains.Annotations;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace BLTAdoptAHero.ClassProgression
{
    public class ClassProgressionNode
    {
        [LocDisplayName("Class Name"),
         LocDescription("Must exactly match the Name field of the HeroClassDef entry."),
         PropertyOrder(0), UsedImplicitly]
        public string ClassName { get; set; } = "";

        [LocDisplayName("Kills Required"),
         LocDescription("Number of battle kills needed to unlock this class (or to upgrade FROM this class to the next ones)."),
         PropertyOrder(1), UsedImplicitly]
        public int KillsRequired { get; set; } = 100;

        [LocDisplayName("Is Starting Class"),
         LocDescription("If true, newly adopted heroes begin in this class."),
         PropertyOrder(2), UsedImplicitly]
        public bool IsStartingClass { get; set; } = false;

        [LocDisplayName("Next Classes"),
         LocDescription("Class names available as upgrades from this class. Empty = dead end (max class)."),
         PropertyOrder(3), UsedImplicitly]
        public List<string> NextClasses { get; set; } = new();
    }
}
