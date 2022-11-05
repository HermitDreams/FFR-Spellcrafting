# Final Fantasy Randomizer: Spellbinder
This program is to be used by mIRC, in tandem with FFHackster2017 and Final Fantasy Randomizer. Designed by Linkshot, 2018-2022.
### NOTE
This is partially incompatible with Final Fantasy Randomizer, as it does not ensure the New Consumables logic will pass.

(More on how to avoid this in the "Applying" section)

However, the latest files in `patches` were confirmed to work before being posted publicly.

# Cloning the C# Port

## 1. Setup
Put the `FFR-Spellbinder` and `output` folders at the root of your repository. The SVN will be inside the first level of `FFR-Spellbinder`.

**There are no PATHs.** You will have to edit the `filepath` var to match your directory.

The `.ini` files and `patches` folder at the root of this git repository are unnecessary. VS will not use them.

## 2. Use
Upon running the exe or from inside VS, a splash screen showing you the available flags will pop up.

After telling it what options you want, the code will run.

When finished, it will open up the `table` of 64 spells and, if `-i` was selected, Item Magic as well.

It will also run flips.exe and ask you what you want to patch.

## 3. Applying
When flips.exe runs, select a non-randomized version of FF1 for NES (USA).

Name the file whatever you want, but make it easy to identify.

As long as the spell list contains a `CUR`, a `PUR`, and `SOFT`, you can feed this patched copy into Final Fantasy Randomizer as your base ROM.

(This dependency might be lifted in the future)

Do note that without the `-S` flag on, FFR will drastically alter the spell in B3-4 and W5-4.

Regardless, it will also change the accuracy of B7-3.

(These compromises might also be lifted in the future)

# Installing the IRC Script

## 1. Setup
Put whole repository **except the folders** in mIRC's scripts folder, located in your AppData -> Roaming

## 2. Use
Commands are /spell, /wmag, and /bmag

### 2a. /spell
- Outputs and opens a text file with 64 spells, sorted from White 1-1 to Black 8-4. Also includes which Battle Messages will be used or changed.
- Spells are balanced based on the level they were generated for.
- Accepts the flags b, b2, C, e, h, i, S, x. These are Case Sensitive.
- WARP and EXIT are never moved.
- Refer to Glossary.txt for a full list of spell names and effects.
- Replaces the 76th battle message (Go mad) with Frozen if Ice Ailment/Petrify exists, or Ablaze if Fire Poison/Confusion exists in the spellbook.
- Resist messages are replaced in the order that the spells occur, trying to fit the original message's length and adding spaces if necessary.
  - There cannot be more than 5 different resist types for this reason.
  - (The current [Add Spaces] message is NOT accurate and a remnant from an earlier method. Ensure "Defend lightning" is 16 spaces long, "Defend fire" 11, etc)

#### FLAGS
- b: Will scrap the generated spell list and mark the output as DEBUG ONLY if it lacks a minimum of 6 AoE Damage spells in Black Magic
- b2: Will force the following spell slots to contain specific spell types:
   - Black 1-1, 1-3, and 2-1: Single Target Damage
   - Black 3-1, 3-3, 4-4, 5-1, 6-1, 7-1: AoE Damage
   - Black 8-1: Any Damage
- C: Forces Black 1-1 to be an Offensive spell, ensuring the Confusion ailment does not cause the afflicted target to buff itself nor its allies
- e: Ensures spell slots that enemies use do not contain offensive spells that would not affect the player nor spells that don't work in battle
- h: Does not alter any spell used out of battle, nor make duplicates of those spells
- i: Assigns the following items spells with specific traits: (Affected spell slots will be marked with the assigned item)
   - Light Axe: AoE Damage in White Magic. Starts seeking at Level 3.
   - Heal Helm/Staff: AoE Healing in White Magic. Starts seeking at Level 3.
   - Mage Staff: AoE Damage in Black Magic. Starts seeking at Level 3. Yields to Thor/Zeus and Bane Sword.
   - Defense: Single Target Defensive Buff. Starts seeking at Level 3 in White and Level 5 in Black.
   - Wizard Staff: AoE Ailment/Debuff in Black Magic. Starts seeking at Level 5.
   - Thor Hammer / Zeus Gauntlet: AoE Lightning in Black Magic. Starts seeking at Level 3.
   - Bane Sword: AoE Poison in Black Magic. Starts seeking at Level 5.
   - White Shirt: AoE Defensive Buff in White Magic. Starts seeking at Level 3.
   - Black Shirt: AoE Damage in Black Magic. Starts seeking at Level 4. Yields to Thor/Zeus, Bane Sword, and Mage Staff.
   - Power Gauntlet: Offensive Buff. Starts seeking at Level 6.
   - NOTE: All assignments seek forward first and then backwards.
- S: Ensures Black 3-4 is an Evasion Down spell and White 5-4 is an AoE Health Restore spell, for compatibility with the Spell Fixes flag in FFR.
- x: Turns off all level-balancing measures and allows spell names to duplicate if they can have different power levels.

### 2b: /wmag
Outputs a single White Magic Spell without naming it

### 2c: /bmag
Outputs a single Black Magic spell without naming it
