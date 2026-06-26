# BLT 5.3.1 for Bannerlord 1.3.15

A fork of **BannerlordTwitch (BLT) 5.2.4** with Dragon/Chariot mount support, T7/T8 Prestige tiers, configurable elite tiers, full power progression and **MakeBltGreatAgain** powers fully integrated — no extra folders needed.

Maintained by **MesmerTurn** — Bannerlord streamer on Twitch.

> Includes a ready-to-play **DnD Classes Edition** (see Releases): 11 Dungeons & Dragons style classes as the bundled default, with every power scaling as your hero grows.

---

## What's Included
- **BannerlordTwitch** — core Twitch integration
- **BLTAdoptAHero** — hero system with all MakeBltGreatAgain powers built in
- **BLTBuffet** — buff system
- **BLTConfigure** — in-game configuration UI

---

## What This Fork Adds

### DnD Classes Edition (bundled default config)
11 themed classes (Fighter, Barbarian, Rogue, Ranger, Wizard, Warlock, Cleric, Paladin, Druid, Monk, Bard), each with a 3-level progression and a mix of active + passive powers. Fresh installs get them automatically — the bundled config loads on first launch.

### Full Power Progression
Every active/passive power used by the classes scales with the hero's **kills/battles per class** — and keeps growing past the top tier (infinite). Only "higher = stronger" stats scale; cooldowns/slow-limits stay fixed. The default config pre-fills sensible ramps (tier 0 = base, then +25% / +50% / +75%). Tune in *BLT Configure → Global Configs → Power Progression*.

### Configurable Elite Tiers (T7 / T8)
| Tier | Effect |
|------|--------|
| **T7** | tunable combat power multiplier (damage + armor) on top of the elite item modifier |
| **T8** | tunable max-HP multiplier in battle |

Both can be enabled/disabled and the multipliers set in *BLT Configure → Global Configs → Common Config → Upgrades*.

### Dragon & Chariot Mounts (RoT 8.0)
When creating/editing a class in BLT Configure, **Use Dragon** / **Use Chariot** checkboxes assign tiered mounts (works without RoT — the mount is simply not assigned).

### MakeBltGreatAgain Powers
All MBGA powers are built directly into BLTAdoptAHero — strike, aura, survival and support powers. No separate mod folder needed.

### Other Changes
- **AddDamage crash fix** — guards a `NullReferenceException` when an Add Damage hero hits while unarmed/kicking.
- PrestigeSpeed patch crash fixed.
- Version watermark shows **BLT v5.3.1**.

---

## Installation
1. Download the latest `.zip` from the [Releases](../../releases) page.
2. Extract into your game's Modules folder: `Mount & Blade II Bannerlord\Modules\` — you should see `BannerlordTwitch`, `BLTAdoptAHero`, `BLTBuffet`, `BLTConfigure`.
3. Launch the game and enable all 4 BLT modules in the launcher.

## Requirements
- **Mount & Blade II: Bannerlord** `1.3.15`
- **Realm of Thrones 8.0** (optional — only needed for Dragon/Chariot mounts)

---

## Credits
### BLT — BannerlordTwitch
**billw** — the original creator of BLT who built the entire foundation this mod runs on. A massive, heartfelt thank you — none of this would exist without your work. 🙏

**Randomchair22** — for maintaining BLT 5.2.4, creating the Dragon fork and keeping it alive.

**kanboru201** — for support, testing and always making things better.

### Warm thanks to the community
**GeneralEddy** — for showing what BLT can truly become and always pushing for more.

**Doravaro** — the streamer who first introduced me to BLT and sparked everything that followed.

---

## License
BannerlordTwitch — original license by billw.
MakeBltGreatAgain — MIT, free to use, modify and share.
