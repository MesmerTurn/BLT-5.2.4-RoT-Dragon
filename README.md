# BLT 5.2.4 + RoT 8.0 Dragon/Chariot Mount Support

A fork of **BannerlordTwitch (BLT) 5.2.4** with added support for dragon and chariot mounts from **Realm of Thrones 8.0** as mount type options in the BLT class configuration UI.

## What This Fork Adds

When creating or editing a class in BLT Configure, two new checkboxes appear:

| Option | T1–T3 | T4–T6 |
|---|---|---|
| **Use Dragon (RoT)** | `dragon_black`, `dragon_brown`, `dragon_gold` (ground) | `dragon_black2`, `dragon_brown2`, `dragon_gold2` (flying) |
| **Use Chariot (RoT)** | `chariot1`, `chariot2`, `chariot3` | `chariot4`, `chariot5`, `chariot6` (advanced) |

The mount tier is assigned automatically based on the hero's equipment tier (T1–T6).  
If you don't have ROT 8.0 installed — the checkboxes are visible but the mount simply won't be assigned (no crashes).

---

## Requirements

- **Mount & Blade II Bannerlord** `e1.3.15`
- **BannerlordTwitch (BLT) 5.2.4** — already included in the ZIP
- **Realm of Thrones 8.0** for Bannerlord 1.3.15 (optional — without it the mount won't be assigned)

---

## Installation

### Option A — Ready-to-use ZIP (recommended)

1. Download `BLT_5.2.4_RoT_Dragon.zip` from the [Releases](../../releases) page
2. Extract the contents into your game's Modules folder:
   ```
   Mount & Blade II Bannerlord\Modules\
   ```
   You should see 4 folders appear:
   ```
   Modules\
   ├── BannerlordTwitch\
   ├── BLTAdoptAHero\
   ├── BLTBuffet\
   └── BLTConfigure\
   ```
3. **Do not** install the original BLT 5.2.4 separately — this ZIP already includes it with the modification applied.
4. Launch the game via the Launcher and enable all 4 BLT modules.

### Option B — Modified files only (advanced)

If you already have BLT 5.2.4 built from source and only want to add dragon/chariot support:

1. Replace these two files in the `BLTAdoptAHero` project:
   - `BLTAdoptAHero/Actions/EquipHero.cs`
   - `BLTAdoptAHero/Actions/Util/HeroClassDef.cs`
2. Build the `BLTAdoptAHero` project in Release mode.
3. Copy the new `BLTAdoptAHero.dll` to:
   ```
   Modules\BLTAdoptAHero\bin\Win64_Shipping_Client\
   ```

---

## How to Use In-Game

1. Open **BLT Configure** (icon in the top-left HUD during a session).
2. Go to **Classes**.
3. Create or edit a class.
4. Check **Use Dragon (RoT)** or **Use Chariot (RoT)**.
5. Save and use the `!equip` command — the hero will receive a dragon or chariot appropriate to their current tier.

---

## Compatibility

- Fully compatible with **MakeBltGreatAgain (MBGA)**.
- Works without ROT 8.0 — the checkboxes won't crash the game, the mount is simply not assigned.
- No additional addons or DLLs required.

---

## What Was Changed

Modified files relative to the original BLT 5.2.4:

- `HeroClassDef.cs` — added `UseDragon` and `UseChariot` properties
- `EquipHero.cs` — added mount assignment logic based on hero equipment tier

Original BLT 5.2.4: © randomchair — [repository](https://github.com/nwilliams-kobold/Bannerlord-Twitch)
