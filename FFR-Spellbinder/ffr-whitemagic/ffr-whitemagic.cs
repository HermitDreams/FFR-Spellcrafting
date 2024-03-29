﻿namespace ffr_spellbinder
{
    internal class ffr_whitemagic
    {
        public static string ffrWhiteSpell = "";
        public static string WMag()
        {
            // Final Fantasy Randomizer White Magic Generator. IRC Script by Linkshot. C# Port.
            var whiteDice = new Random();
        // White Spell Initiation
        whitemagic:
            // write -c WhiteSpell.txt <-- Create txt file here
            bool ffrwmstrskip = false;
            bool ffrwmaccskip = false;
            int ffrwmeffect = whiteDice.Next(1, 6);
            int ffrwmdebuff = whiteDice.Next(1, 11);
            int ffrwmheal = whiteDice.Next(1, 11);
            int ffrwmbuff = whiteDice.Next(0, 15);
            int ffrwmelement = whiteDice.Next(0, 5);
            int ffrwmallies = whiteDice.Next(1, 4);
            int ffrwmenemies = whiteDice.Next(1, 3);
            double ffrwmstrength = whiteDice.Next(10, 101);
            int ffrwmafflict = whiteDice.Next(0, 9);
            int ffrwmaccroll = whiteDice.Next(0, 256);
            int ffrwmaccmath1 = (int)Math.Pow(2, whiteDice.Next(5, 7)) - 1;
            int ffrwmaccmath2 = (int)Math.Pow(2, whiteDice.Next(1, 9));
            int ffrwmacclow = Math.Min(ffrwmaccmath1, ffrwmaccmath2);
            int ffrwmacchigh = Math.Max(ffrwmaccmath1, ffrwmaccmath2);
            int ffrwmaccuracy = whiteDice.Next(ffrwmacclow, ffrwmacchigh);

            string ffrwmaccsay = "";
            bool ffrwmspecial = false;
            string ffrwmresist = "";
            bool ffrwmBecameStatus = false;

            bool ffrwmresmagic = false;
            bool ffrwmresdecay = false;
            bool ffrwmresdragon = false;



            // Debugging
            Console.WriteLine($"ffrwmeffect rolled {ffrwmeffect} of 5");
            Console.WriteLine($"ffrwmdebuff rolled {ffrwmdebuff} of 10");
            Console.WriteLine($"ffrwmheal rolled {ffrwmheal} of 10");
            Console.WriteLine($"ffrwmbuff rolled {ffrwmbuff} of 14");
            Console.WriteLine($"ffrwmelement rolled {ffrwmelement} of 4");
            Console.WriteLine($"ffrwmallies rolled {ffrwmallies} of 3");
            Console.WriteLine($"ffrwmstrength rolled {ffrwmstrength} of 100");
            Console.WriteLine($"ffrwmafflict rolled {ffrwmafflict} of 8");
            Console.WriteLine($"ffrwmaccroll rolled {ffrwmaccroll} of 255");
            Console.WriteLine($"ffrwmaccuracy rolled {ffrwmaccuracy} of {ffrwmacclow} to {ffrwmacchigh}");

        wmbase:

            int ffrwmTypeByte = 0;
            int ffrwmEffByte = 0;
            int ffrwmTargByte = 0;
            int ffrwmElemByte = 0;
            int ffrwmAccByte = 0;

            Program.ffrspRmPerms = true;
            Program.ffrspWmPerms = true;
            Program.ffrspRwPerms = true;

            #region Auto-Hit

            // Sets Accuracy to Auto-Hit or Tiered
            if ((ffrwmaccroll < 2) || (ffrwmaccroll == 148))
            {
                ffrwmaccsay = "Auto-Hit";
                // if (L isincs % Program.ffrspflags) { write - i SpellLog.txt Accuracy Change: 3 / 256 succeeded.Spell is slated to get 255 Hit Bonus. | write - i SpellLog.txt - }
            }
            else if (Program.ffrspellbinding == true)
            {
                if (Program.ffrsplevel < 5) { ffrwmaccuracy = 5; Program.ffrsptier = 2; }
                if (Program.ffrsplevel > 4) { ffrwmaccuracy = 7; Program.ffrsptier = 3; }
                if (Program.ffrsplevel == 1) { ffrwmaccuracy = 4; Program.ffrsptier = 1; }
                if (Program.ffrsplevel == 8) { ffrwmaccuracy = 9; Program.ffrsptier = 4; }
                // if (L isincs % Program.ffrspflags) { write - i SpellLog.txt Accuracy Change: Spellbinder has set the Accuracy Tier to % ffrwmaccuracy to match Level % Program.ffrsplevel | write - i SpellLog.txt - }
            }
            else
            {
                ffrwmaccsay = ffrwmaccuracy.ToString();
                // if (L isincs % Program.ffrspflags) { write - i SpellLog.txt Accuracy Output has been set to % ffrwmaccsay | write - i SpellLog.txt - }
            }

            #endregion Auto-Hit
            #region SlotPerms
            // Spellbinding Slot Permissions
            #region HEL2Fix
            // Compatibility with HEL2 fix
            if ((Program.ffrspflags.Contains("S")) && (Program.ffrsplevel == 5) && (Program.ffrspslot == 4) && (Program.ffrspreroll < 5))
            {
                ffrwmeffect = 4;
                ffrwmheal = 1;
                ffrwmallies = 3;
            }
            #endregion
            #region PreserveHealing
            // Makes sure Healing lands in 11 specific slots
            if ((Program.ffrspflags.Contains("h")) && (Program.ffrspreroll < 5))
            {
                // if (L isincs % Program.ffrspflags) { write - i SpellLog.txt Flag "h" is evaluating this spell.Reroll counter is at % Program.ffrspreroll of 5. }
                if (ffrwmafflict == 8)
                {
                    ffrwmafflict = 3;
                    // if (L isincs % Program.ffrspflags) { write - i SpellLog.txt Affliction Change: 8(Confusion) was changed to 3(Poison) to account for Pure }
                }
                if (ffrwmafflict == 5)
                {
                    ffrwmafflict = 3;
                    // if (L isincs % Program.ffrspflags) { write - i SpellLog.txt AFfliction change: 5(Stun) was changed to 3(Poison) to account for Pure }
                }
                if ((Program.ffrsplevel == 1) && (Program.ffrspslot == 1))
                {
                    ffrwmeffect = 4;
                    ffrwmallies = 2;
                    ffrwmheal = 1;
                }
                else if ((Program.ffrsplevel == 3) && (!Enumerable.Range(2, 2).Contains(Program.ffrspslot)))
                {
                    ffrwmeffect = 4;
                    ffrwmheal = 1;
                    if (Program.ffrspslot == 1)
                    {
                        ffrwmallies = 2;
                        // if (L isincs % Program.ffrspflags) { write - i SpellLog.txt Slot 1 confirmed. Party Targeting has been set to 2(Single Ally) }
                    }
                    else if (Program.ffrspslot == 4)
                    {
                        ffrwmallies = 3;
                        // if (L isincs % Program.ffrspflags) { write - i SpellLog.txt Slot 4 confirmed. Party Targeting has been set to 3(Whole Party) }
                    }
                    else { Console.WriteLine($"Program.ffrspslot failed to parse. Value was {Program.ffrspslot}."); }
                }
                else if ((Program.ffrsplevel == 4) && (Program.ffrspslot == 1))
                {
                    ffrwmeffect = 4;
                    ffrwmallies = 2;
                    ffrwmheal = 7;
                    ffrwmafflict = 3;
                }
                else if ((Program.ffrsplevel == 5) && (Program.ffrspslot != 3))
                {
                    ffrwmeffect = 4;
                    if (Program.ffrspslot == 2)
                    {
                        ffrwmallies = 2;
                        ffrwmheal = 7;
                        ffrwmafflict = 1;
                    }
                    else
                    {
                        ffrwmheal = 1;
                        if (Program.ffrspslot == 1)
                        {
                            ffrwmallies = 2;
                            // if (L isincs % Program.ffrspflags) { write - i SpellLog.txt Slot 1 confirmed. Party Targeting has been set to 2(Single Ally) }
                        }
                        else if (Program.ffrspslot == 4)
                        {
                            ffrwmallies = 3;
                            // if (L isincs % Program.ffrspflags) { write - i SpellLog.txt Slot 4 confirmed. Party Targeting has been set to 3(Whole Party) }
                        }
                        else { Console.WriteLine($"Program.ffrspslot failed to parse. Value was {Program.ffrspslot}."); }
                    }
                }
                else if ((Program.ffrsplevel == 6) && (Program.ffrspslot == 1))
                {
                    ffrwmeffect = 4;
                    ffrwmallies = 2;
                    ffrwmheal = 7;
                    ffrwmafflict = 2;
                }
                else if ((Program.ffrsplevel == 7) && (!Enumerable.Range(2, 2).Contains(Program.ffrspslot)))
                {
                    ffrwmeffect = 4;
                    if (Program.ffrspslot == 1)
                    {
                        ffrwmallies = 2;
                        ffrwmheal = 10;
                    }
                    else if (Program.ffrspslot == 4)
                    {
                        ffrwmallies = 3;
                        ffrwmheal = 1;
                    }
                }
                else if ((Program.ffrsplevel == 8) && (Program.ffrspslot == 1))
                {
                    ffrwmeffect = 4;
                    ffrwmallies = 2;
                    ffrwmheal = 7;
                    ffrwmafflict = 1;
                }
                else
                {
                    if ((ffrwmeffect == 4) && ((!Enumerable.Range(7, 3).Contains(ffrwmheal)) || ((Enumerable.Range(7, 3).Contains(ffrwmheal)) && (Enumerable.Range(1, 2).Contains(ffrwmafflict))) || ((Program.ffrsplevel < 5) && ((Enumerable.Range(7, 3).Contains(ffrwmheal)) && (ffrwmafflict == 3)))))
                    {
                        colors.echo(13, $"Debug: Bounced healing type {ffrwmheal}-{ffrwmafflict} at slot {Program.ffrsplevel}-{Program.ffrspslot}");
                        goto whitemagic;
                    }
                }
                // if (L isincs % Program.ffrspflags) { write - i SpellLog.txt Flag "h" has finished evaluating this spell. | write - i SpellLog.txt - }
            }
            #endregion
            #region EnemySanity
            // Prevents Harm, Confusion, Fear, Soft, and Life from landing in slots enemies use
            if ((Program.ffrspflags.Contains("e")) && (Program.ffrspreroll < 5))
            {
                // if (L isincs % Program.ffrspflags) { write - i SpellLog.txt Flag "e" is evaluating this spell.Reroll counter is at % Program.ffrspreroll of 5 }
                if ((ffrwmeffect == 2) || ((ffrwmeffect == 3) && ((ffrwmdebuff == 5) || (ffrwmdebuff == 7) || ((ffrwmdebuff == 10) && (ffrwmafflict == 8)))) || ((ffrwmeffect == 4) && (Enumerable.Range(7, 3).Contains(ffrwmheal)) && (ffrwmafflict < 3)))
                {
                    if (((Program.ffrsplevel == 1) && (!Enumerable.Range(2, 2).Contains(Program.ffrspslot))) || ((Program.ffrsplevel == 2) && (Program.ffrspslot == 2)) || ((Program.ffrsplevel == 6) && (Program.ffrspslot > 2)) || ((Program.ffrsplevel == 3) && (Program.ffrspslot > 2)) || (((Program.ffrsplevel == 5) || (Program.ffrsplevel == 7)) && ((Program.ffrspslot == 1) || (Program.ffrspslot == 4))) || ((Program.ffrsplevel == 8) && (Program.ffrspslot > 2)))
                    {
                        if (ffrwmeffect == 3) { colors.echo(13, $"Debug: Bounced Debuff Type {ffrwmdebuff} at {Program.ffrsplevel}-{Program.ffrspslot}"); }
                        else if (ffrwmeffect == 4)
                        {
                            colors.echo(13, $"Debug: Bounced Cure Type {ffrwmafflict} at {Program.ffrsplevel}-{Program.ffrspslot}");
                        }
                        else { colors.echo(13, $"Debug: Bounced Harm Undead at {Program.ffrsplevel}-{Program.ffrspslot}"); }
                        goto whitemagic;
                    }
                }
                // if (L isincs % Program.ffrspflags) { write - i SpellLog.txt Flag "e" has finished evaluating this spell. | write - i SpellLog.txt - }
            }
            #endregion
            #endregion

            #region BaseEffect
            // Spell-building
            #region Damage
            // Checks for Raw Damage and forces it to pass
            // a 10% check, then writes spell type to file
            if (ffrwmeffect == 1)
            {
                if (whiteDice.Next(1, 11) == 10)
                {
                    // write - al2 WhiteSpell.txt Type: Damage
                    ffrwmTypeByte = 1;
                }
                else { colors.echo(13, $"Debug: Bounced at Damage({whiteDice} vs 10)"); goto whitemagic; }
                if (Program.ffrspellbinding == true)
                {
                    if (Program.ffrsplevel < 8) { ffrwmaccuracy = 5; }
                    if (Program.ffrsplevel == 8) { ffrwmstrength = 100; }
                    else { ffrwmstrength = (int)Program.ffrsplevel * 10; }
                }
                else { if (ffrwmenemies == 1) { ffrwmstrength = ffrwmstrength * whiteDice.Next(100, 151) / 100; } }
                ffrwmstrength = Convert.ToInt32(Math.Floor(ffrwmstrength * 0.8));
                // if (ffrwmstrength == 96) { ffrwmstrength += 4; }
                if (Enumerable.Range(24, 16).Contains((int)ffrwmstrength)) { Program.ffrsptier = 2; }
                else if (Enumerable.Range(40, 40).Contains((int)ffrwmstrength)) { Program.ffrsptier = 3; }
                else if (ffrwmstrength >= 80) { Program.ffrsptier = 4; }
                else { Program.ffrsptier = 1; }
                if (ffrwmstrength > 50 && ffrwmenemies != 1) Program.ffrspRmPerms = false;
                if (ffrwmstrength > 60) Program.ffrspWmPerms = false;
                if (ffrwmstrength > 70) Program.ffrspRmPerms = false;
                if (ffrwmstrength >= 80 && ffrwmenemies != 1) Program.ffrspRwPerms = false;
            }
            #endregion
            #region Prejudice
            else if (ffrwmeffect == 2)
            {
                // write - al2 WhiteSpell.txt Type: Harm Undead
                ffrwmTypeByte = 2;
                Program.ffrspRwPerms = false;
                if (Program.ffrspellbinding == true)
                {
                    if (Program.ffrsplevel < 7) { ffrwmaccuracy = 5; }
                    else { ffrwmaccuracy++; }
                    if (Program.ffrsplevel == 8) { ffrwmstrength = 100; }
                    else { ffrwmstrength = (int)(Program.ffrsplevel + 1) * 10; }
                }
                if (ffrwmstrength > 30) { Program.ffrsptier = 2; }
                else { Program.ffrsptier = 1; }
                if (ffrwmstrength > 50) { Program.ffrsptier = 3; }
                if (ffrwmstrength >= 80) { Program.ffrsptier = 4; Program.ffrspWmPerms = false; }
            }
            #endregion
            #region EffectNameCleanup
            // There are only 3 white debuffs in the original spell list
            // Out of around 30 spells, that's 10% of them
            // So only half of the debuffs pass the lot check
            else if (ffrwmeffect == 3)
            {
                if ((whiteDice.Next(0, 2) == 0) && (ffrwmBecameStatus == false))
                {
                    ffrwmeffect += whiteDice.Next(1, 3);
                    if (ffrwmeffect == 4) { colors.echo(13, $"Debug: Shifting Debuff to Heal"); }
                    else if (ffrwmeffect == 5) { colors.echo(13, $"Debug: Shifting Debuff to Buff"); }
                    goto wmbase;
                }
                else if (ffrwmBecameStatus == false) { colors.echo(8, $"Debug: Debuff was allowed"); }
                // write - al2 WhiteSpell.txt Type: Debuff
            }
            else if (ffrwmeffect == 4)
            {
                // write - al2 WhiteSpell.txt Type: Heal
                colors.echo(8, $"Heal Type: {ffrwmheal} & {ffrwmafflict}");
                ffrwmaccskip = true;
            }
            else if (ffrwmeffect == 5)
            {
                // write - al2 WhiteSpell.txt Type: Buff
                ffrwmaccskip = true;
            }
            else
            {
                colors.echo(4, $"Value {ffrwmeffect} out of range for Base Effect");
                return "Spell Failed";
            }
            #endregion

            #region Debuffs
            // Debuff Selection
            // White gets an accuracy buff to MUTE, FEAR, and XFER families
            if (ffrwmeffect == 3)
            {
                if ((Program.ffrspellbinding == true) && (ffrwmaccsay == "Auto-Hit") && (ffrwmdebuff < 6)) { ffrwmdebuff = 10; }
                // Debug: echo 8 - s Debug: Passing through Debuff(Debuff: % ffrwmdebuff $+ , Element: % ffrwmelement $+ , Affliction: % ffrwmafflict $+ , Allies: % ffrwmallies $+ )
                if (!Enumerable.Range(7, 2).Contains(ffrwmdebuff)) { ffrwmstrskip = true; }
                if (Enumerable.Range(1, 5).Contains(ffrwmdebuff)) { ffrwmTypeByte = 3; }
                if (ffrwmdebuff == 1)
                {
                    // write - al3 WhiteSpell.txt Effect: Darkness
                    ffrwmEffByte = 8;
                }
                else if (ffrwmdebuff == 2)
                {
                    if (Program.ffrspellbinding == true)
                    {
                        ffrwmaccuracy++;
                        if (ffrwmelement == 0) { ffrwmaccuracy -= 5; }
                        else if (ffrwmelement == 3) { ffrwmaccuracy -= 3; }
                    }
                    // write - al3 WhiteSpell.txt Effect: Stun
                    ffrwmEffByte = 16;
                }
                else if (ffrwmdebuff == 3)
                {
                    if (Program.ffrspellbinding == true)
                    {
                        if (Program.ffrsplevel > 1) { ffrwmaccuracy += 2; }
                        else { ffrwmaccuracy--; }
                    }
                    // write - al3 WhiteSpell.txt Effect: Sleep
                    ffrwmEffByte = 32;
                }
                else if (ffrwmdebuff == 4)
                {
                    // write - al3 WhiteSpell.txt Effect: Mute
                    ffrwmEffByte = 64;
                    if (Program.ffrspellbinding == true) { ffrwmaccuracy += 4; }
                }
                else if (ffrwmdebuff == 5)
                {
                    if (Program.ffrspellbinding == true) { ffrwmaccuracy += 2; }
                    // write - al3 WhiteSpell.txt Effect: Confuse
                    ffrwmEffByte = 128;
                }
                else if (ffrwmdebuff == 6)
                {
                    if (Program.ffrspellbinding == true)
                    {
                        if (ffrwmelement == 0) { ffrwmaccuracy -= 3; }
                        else { ffrwmaccuracy += 2; }
                    }
                    // write - al3 WhiteSpell.txt Effect: Slow
                    ffrwmTypeByte = 4;
                }
                else if (ffrwmdebuff == 7)
                {
                    Program.ffrspRwPerms = false;
                    if (Program.ffrspellbinding == true)
                    {
                        if (Program.ffrsplevel > 6) { ffrwmstrength = 40 + (whiteDice.Next(0, 4) * 8); Program.ffrsptier = 4; }
                        else
                        {
                            ffrwmstrength = (int)(Program.ffrsplevel + 1) * 5;
                            if (ffrwmstrength < 40) { Program.ffrsptier = 3; }
                            if (ffrwmstrength < 30) { Program.ffrsptier = 2; }
                            if (ffrwmstrength < 20) { Program.ffrsptier = 1; }
                        }
                    }
                    else
                    {
                        if (ffrwmBecameStatus == false) ffrwmstrength = Math.Floor(ffrwmstrength * 0.75);
                        if (ffrwmstrength < 40) { Program.ffrsptier = 3; }
                        else Program.ffrsptier = 4;
                        if (ffrwmstrength < 30) { Program.ffrsptier = 2; }
                        if (ffrwmstrength < 20) { Program.ffrsptier = 1; }
                    }
                    // write - al3 WhiteSpell.txt Effect: Fear
                    if (ffrwmstrength >= 40) Program.ffrspWmPerms = false;
                    ffrwmTypeByte = 5;
                }
                else if (ffrwmdebuff == 8)
                {
                    if (Program.ffrspellbinding == true)
                    {
                        ffrwmaccuracy += 2;
                        if (Program.ffrsplevel == 8) { ffrwmstrength = 160 + (whiteDice.Next(0, 3) * 40); Program.ffrsptier = 4; }
                        else { ffrwmstrength = (int)Program.ffrsplevel * 20; }
                        if (ffrwmstrength < 160) { Program.ffrsptier = 3; }
                        if (ffrwmstrength < 100) { Program.ffrsptier = 2; }
                        if (ffrwmstrength < 60) { Program.ffrsptier = 1; }
                    }
                    else
                    {
                        if (ffrwmBecameStatus == false)
                        {
                            ffrwmstrength = Math.Floor(ffrwmstrength * 2.5 - 5);
                            Program.ffrsptier = (int)Math.Floor((ffrwmstrength + 20) / 40);
                        }
                    }
                    // write - al3 WhiteSpell.txt Effect: Locked
                    if (ffrwmstrength >= 160) Program.ffrspWmPerms = false;
                    ffrwmTypeByte = 14;
                }
                else if (ffrwmdebuff == 9)
                {
                    if (ffrwmelement == 2) { ffrwmelement = 0; }
                    if (Program.ffrspellbinding == true)
                    {
                        if (Program.ffrsplevel < 8) { ffrwmaccuracy++; }
                        if (ffrwmelement != 0) { ffrwmaccuracy += 2; }
                    }
                    // write - al3 WhiteSpell.txt Effect: Stripped
                    ffrwmTypeByte = 17;
                }
                #region PowerWords
                // Power Word Processing
                else if (ffrwmdebuff == 10)
                {
                    Program.ffrspRwPerms = false;
                    ffrwmTypeByte = 18;
                    ffrwmTargByte = 2;
                    ffrwmaccskip = true;
                wmpword:
                    if ((Program.ffrspellbinding == true) && (Program.ffrsplevel < 6)) { colors.echo(13, $"Debug: Bounced a Power Word before Level 6"); goto whitemagic; }
                    if (ffrwmafflict < 2)
                    {
                        if (whiteDice.Next(1, 21) >= whiteDice.Next(1, 81))
                        {
                            colors.echo(8, $"Debug: FROG passed its saving roll!");
                            //write - al3 WhiteSpell.txt Effect: Power Word "Pacify"
                            //write - al4 WhiteSpell.txt Element: Creature // Status+Poison+Earth
                            //write - al5 WhiteSpell.txt Target: Single Enemy
                            Program.ffrspWmPerms = false;
                            ffrwmEffByte = 1;
                            ffrwmElemByte = 131;
                            ffrwmTargByte = 2;
                            ffrwmspecial = true;
                        }
                        else { colors.echo(13, $"Debug: FROG failed... Trying common Power Word."); ffrwmafflict = whiteDice.Next(4, 9); goto wmpword; }
                    }
                    else if (ffrwmafflict < 4)
                    {
                        if (whiteDice.Next(1, 21) >= whiteDice.Next(1, 81))
                        {
                            colors.echo(8, $"Debug: CAST passed its saving roll!");
                            //write - al3 WhiteSpell.txt Effect: Power Word "Preserve"
                            //write - al4 WhiteSpell.txt Element: Stone
                            //write - al5 WhiteSpell.txt Target: Single Ally
                            Program.ffrspWmPerms = false;
                            ffrwmEffByte = 2;
                            ffrwmElemByte = 2;
                            ffrwmTargByte = 16;
                            ffrwmspecial = true;
                        }
                        else { colors.echo(13, $"Debug: CAST failed... Trying common Power Word."); ffrwmafflict = whiteDice.Next(4, 9); goto wmpword; }
                    }
                    // else if (ffrwmafflict == 3) { colors.echo(13, $"Debug: Rerolling a blank Power Word"); ffrwmafflict = whiteDice.Next(1, 9); goto wmpword; }
                    else if (ffrwmafflict == 4)
                    {
                        //write - al3 WhiteSpell.txt Effect: Power Word "Blind"
                        ffrwmEffByte = 8;
                    }
                    else if (ffrwmafflict == 5)
                    {
                        //write - al3 WhiteSpell.txt Effect: Power Word "Stun"
                        ffrwmEffByte = 16;
                    }
                    else if (ffrwmafflict == 6)
                    {
                        //write - al3 WhiteSpell.txt Effect: Power Word "Naptime"
                        ffrwmEffByte = 32;
                    }
                    else if (ffrwmafflict == 7)
                    {
                        //write - al3 WhiteSpell.txt Effect: Power Word "Silence"
                        ffrwmEffByte = 64;
                    }
                    else if (ffrwmafflict == 8)
                    {
                        //write - al3 WhiteSpell.txt Effect: Power Word "Betray"
                        ffrwmEffByte = 128;
                    }
                    else
                    {
                        colors.echo(4, $"Value {ffrwmafflict} out of range for Power Words"); return "Spell Failed";
                    }
                }
                #endregion
                else
                {
                    colors.echo(4, $"Value {ffrwmdebuff} out of range for Debuffs"); return "Spell Failed";
                }
                #region Elements
                // Assigns an Element to the Debuff
                // Usually picks Status
                // Makes sure CAMP, FROG, and CAST don't get overwritten
                if ((ffrwmdebuff == 3) && (ffrwmelement == 2) && (ffrwmallies == 3))
                {
                    // write - a14 WhiteSpell.txt Element: Fire
                    // write - al5 WhiteSpell.txt Target: All Allies
                    Program.ffrspRwPerms = false;
                    ffrwmEffByte = 32;
                    ffrwmElemByte = 16;
                    ffrwmTargByte = 8;
                    ffrwmspecial = true;
                }
                else if ((ffrwmdebuff == 10) && (ffrwmafflict < 4)) { colors.echo(13, $"Debug: Element already set by Power Word"); }
                else
                {
                    if (ffrwmelement == 1)
                    {
                        //write - a14 WhiteSpell.txt Element: Status
                        ffrwmElemByte = 1;
                    }
                    else if (ffrwmelement == 3)
                    {
                        if (Program.ffrspellbinding == true) { ffrwmaccuracy--; }
                        //write - a14 WhiteSpell.txt Element: Time
                        ffrwmElemByte = 4;
                    }
                    else if (ffrwmelement == 4)
                    {
                        if (Program.ffrspellbinding == true) { ffrwmaccuracy += 2; }
                        //write - a14 WhiteSpell.txt Element: Earth
                        Program.ffrspRmPerms = false;
                        ffrwmElemByte = 128;
                    }
                    else
                    {
                        if ((ffrwmdebuff != 9) && (whiteDice.Next(1, 11) != 10))
                        {
                            //write - a14 WhiteSpell.txt Element: Status
                            ffrwmelement = 1;
                            ffrwmElemByte = 1;
                            ffrwmBecameStatus = true;
                            goto wmbase;
                        }
                        else
                        {
                            //write - al4 WhiteSpell.txt Non-Elemental
                            colors.echo(8, $"Debug: Did not assign element to debuff");
                        }
                    }
                }
                #endregion
            }
            #endregion
            #region Healing
            // Healing Selection
            // 30 % of Healing Spells are Status Removal
            // 10 % of Healing Spells are CUR4
            else if (ffrwmeffect == 4)
            {
                if (ffrwmheal > 6)
                {
                    ffrwmstrskip = true;
                    #region RemoveNegativeConditionEffect
                    if (ffrwmheal != 10)
                    {
                        ffrwmTypeByte = 8;
                        if (Program.ffrspellbinding == true)
                        {
                            if ((Program.ffrsplevel > 6) && (ffrwmafflict > 2)) { ffrwmafflict = 0; }
                            while (ffrwmafflict == 5) { ffrwmafflict = whiteDice.Next(3, 9); }
                            if (ffrwmafflict == 8) { ffrwmafflict = 3; }
                            if (Enumerable.Range(3, 5).Contains(ffrwmafflict)) { ffrwmEffByte += 16; }
                        }
                        if (ffrwmafflict == 0)
                        {
                            if (Program.ffrspellbinding == true)
                            {
                                if (Program.ffrsplevel < 5) { colors.echo(13, $"Debug: Bounced CLER before Level 5"); goto whitemagic; }
                                else if (Enumerable.Range(5, 2).Contains((int)Program.ffrsplevel)) { ffrwmallies = 2; }
                                else if (Program.ffrsplevel > 6) { ffrwmallies = 3; }
                            }
                            // write - a13 WhiteSpell.txt Effect: Refresh
                            Program.ffrspRmPerms = false;
                            if (ffrwmallies != 2) { Program.ffrspRwPerms = false; Program.ffrspWmPerms = false; }
                            ffrwmEffByte = 252; // "Refresh" excludes Death and Stone
                        }
                        // Life must pass a 75% check
                        else if (ffrwmafflict == 1)
                        {
                            if (ffrwmallies == 1) ffrwmallies += whiteDice.Next(1, 3);
                            if ((!Program.ffrspflags.Contains("h")) && Program.ffrspellbinding == false)
                            {
                                if (ffrwmstrength >= 40)
                                {
                                    //write - a13 WhiteSpell.txt Effect: Revive
                                    if (ffrwmstrength < 60) ffrwmallies = 2;
                                    if (ffrwmstrength < 80) ffrwmEffByte = 1;
                                    else
                                    {
                                        ffrwmEffByte = 255;
                                        if (ffrwmstrength < 90) ffrwmallies = 2;
                                    }
                                }
                                else { colors.echo(13, $"Debug: Bounced at LIFE"); goto whitemagic; }
                            }
                            else
                            {
                                if (Program.ffrspflags.Contains("h") || (whiteDice.Next(1, 5) < 4 && Program.ffrsplevel >= 4))
                                {
                                    // write - al3 WhiteSpell.txt Effect: Revive
                                    if (ffrwmallies > 2 && Program.ffrsplevel < 6) ffrwmallies = 2;
                                    if (((Program.ffrsplevel == 7) && (whiteDice.Next(1, 5) == 4)) || Program.ffrsplevel >= 8)
                                    {
                                        ffrwmEffByte = 255;
                                        if (ffrwmallies > 2 && Program.ffrsplevel == 7) ffrwmallies = 2;
                                    }
                                    else
                                    {
                                        ffrwmEffByte = 1;
                                        if (Program.ffrsplevel > 6) ffrwmallies = 3;
                                    }
                                }
                                else { colors.echo(13, $"Debug: Bounced at LIFE"); goto whitemagic; }
                            }
                            Program.ffrspRmPerms = false;
                            if (ffrwmallies == 3 || ffrwmEffByte == 255) Program.ffrspRwPerms = false;
                            if (ffrwmEffByte == 255) Program.ffrspWmPerms = false;
                        }
                        // Soft must also pass
                        else if (ffrwmafflict == 2)
                        {
                            if (ffrwmallies == 1) ffrwmallies += whiteDice.Next(1, 3);
                            if ((!Program.ffrspflags.Contains("h")) && Program.ffrspellbinding == false)
                            {
                                if (ffrwmstrength >= 40)
                                {
                                    if (ffrwmstrength < 70) ffrwmallies = 2;
                                }
                                else { colors.echo(13, $"Debug: Bounced at SOFT"); goto whitemagic; }
                            }
                            else
                            {
                                if (Program.ffrspflags.Contains("h") || (whiteDice.Next(1, 5) < 4 && Program.ffrsplevel >= 4))
                                {
                                    if (Program.ffrsplevel < 7) ffrwmallies = 2;
                                    else ffrwmallies = 3;
                                }
                                else { colors.echo(13, $"Debug: Bounced at SOFT"); goto whitemagic; }
                            }
                            Program.ffrspRwPerms = false;
                            if (ffrwmallies == 3) Program.ffrspWmPerms = false;
                            // write - al3 WhiteSpell.txt Effect: Soften
                            ffrwmEffByte = 2;
                        }
                        else if (ffrwmafflict == 3)
                        {
                            if (Program.ffrspellbinding == true)
                            {
                                if (Program.ffrsplevel < 2) { colors.echo(13, $"Debug: Bounced PURE at Level 1"); goto whitemagic; }
                                else if (Program.ffrsplevel < 5) { ffrwmallies = 2; }
                                else if (Program.ffrsplevel > 4) { ffrwmallies = 3; }
                                ffrwmEffByte += 128;
                            }
                            // write - al3 WhiteSpell.txt Effect: Antidote
                            if (ffrwmallies == 3) Program.ffrspRmPerms = false;
                            ffrwmEffByte += 4;
                        }
                        else if (ffrwmafflict == 4)
                        {
                            if (Program.ffrspellbinding == true)
                            {
                                if (Program.ffrsplevel == 1) { ffrwmallies = 2; }
                                else if (Program.ffrsplevel > 1) { ffrwmallies = 3; }
                            }
                            // write - al3 WhiteSpell.txt Effect: Eyesight
                            ffrwmEffByte += 8;
                        }
                        else if (ffrwmafflict == 5)
                        {
                            // write - al3 WhiteSpell.txt Effect: Limber
                            ffrwmEffByte = 16;
                            ffrwmallies = whiteDice.Next(2, 4);
                        }
                        else if (ffrwmafflict == 6)
                        {
                            if (Program.ffrspellbinding == true)
                            {
                                if (Program.ffrsplevel == 1) { ffrwmallies = 2; }
                                else if (Program.ffrsplevel > 1) { ffrwmallies = 3; }
                            }
                            else { ffrwmallies = whiteDice.Next(2, 4); }
                            // write - al3 WhiteSpell.txt Effect: Wake
                            ffrwmEffByte += 32;
                        }
                        else if (ffrwmafflict == 7)
                        {
                            if (Program.ffrspellbinding == true)
                            {
                                if (Program.ffrsplevel < 4) { ffrwmallies = 2; }
                                else if (Program.ffrsplevel < 7) { ffrwmallies = 3; }
                            }
                            else { ffrwmallies = whiteDice.Next(2, 4); }
                            // write - al3 WhiteSpell.txt Effect: Voice
                            if (ffrwmallies == 3) Program.ffrspRmPerms = false;
                            ffrwmEffByte += 64;
                        }
                        else if (ffrwmafflict == 8)
                        {
                            // write - al3 WhiteSpell.txt Effect: Clarify
                            ffrwmEffByte = 128;
                            ffrwmallies = whiteDice.Next(2, 4);
                            if (ffrwmallies == 3) Program.ffrspRmPerms = false;
                        }
                        // You cannot heal yourself of anything
                        // but Poison and Dark, so other purifiers
                        // make sure it targets an ally or the party
                        else
                        {
                            colors.echo(4, $"Value {ffrwmafflict} out of range for Restoratives"); return "Spell Failed";
                        }
                    }
                    #endregion
                    #region CUR4
                    if (ffrwmheal == 10)
                    {
                        if (Program.ffrspellbinding == true)
                        {
                            if (Program.ffrsplevel > 5) { ffrwmallies = 2; }
                            else { colors.echo(13, $"Debug: Spellbinding rejected CUR4 at Level {Program.ffrsplevel}"); goto whitemagic; }
                            if ((Program.ffrsplevel == 8) && (whiteDice.Next(1, 21) >= whiteDice.Next(1, 81))) { ffrwmallies = 3; }
                        }
                        if (Program.ffrspellbinding == false && ffrwmstrength < 80) ffrwmallies = 2;
                        // write to file: "Effect: Full Restore"
                        Program.ffrspRwPerms = false;
                        Program.ffrspWmPerms = false;
                        ffrwmTypeByte = 15;
                    }
                    #endregion
                }
                else { ffrwmTypeByte = 7; }
            }
            #endregion
            #region Buffs
            // Buff Selection
            // White Magic has trouble dealing damage
            // so FAST and TMPR are rarer and weaker
            else if (ffrwmeffect == 5)
            {
                // Debug: echo 8 - s Debug: Passing through Buff(Buff: % ffrwmbuff $+ , Target: % ffrwmallies $+ )
                #region Resists
                // need to refactor this to be more compact there is SO much redundancy here
                if (ffrwmbuff < 9)
                {
                    ffrwmstrskip = true;
                    ffrwmallies = 3;
                    ffrwmTypeByte = 10;
                    if (((Program.ffrspellbinding == true) && (Program.ffrsplevel == 8)) || (ffrwmbuff == 0))
                    {
                        if ((Program.ffrspellbinding == false) && (ffrwmstrength >= 80)) { ffrwmallies = 3; }
                        else if ((Program.ffrspellbinding == true) && (Program.ffrsplevel < 5)) { colors.echo(13, $"Debug: Spellbinding bounced WALL at Level {Program.ffrsplevel}"); goto whitemagic; }
                        else if ((Program.ffrspellbinding == true) && (Program.ffrsplevel == 8) && (whiteDice.Next(1, 21) >= whiteDice.Next(1, 81))) { ffrwmallies = 3; }
                        else { ffrwmallies = 2; }
                        // echo 8 - s Checking for SANC rolls. ( $+ $v1 vs $v2 $+ )
                        if (Program.ffrspwall == 0)
                        {
                            if (Program.ffrspResistCount > 4)
                            {
                                colors.echo(4, $"Resists already capped! Rerolling.");
                                goto whitemagic;
                            }
                            // set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend all
                            Program.ffrspwall = Program.ffrspResistCount + 1;
                            Program.ffrspResistCount++;
                        }
                        // write - a13 WhiteSpell.txt Effect: Resist All
                        ffrwmEffByte = 255;
                        Program.ffrspRmPerms = false;
                        if (ffrwmallies == 3)
                        {
                            Program.ffrspRwPerms = false;
                            Program.ffrspWmPerms = false;
                        }
                        Program.ffrspCurrentResist = Program.ffrspwall;
                        ffrwmbuff = 0;
                        goto walljump;
                    }
                    else if (ffrwmbuff == 1)
                    {
                        ffrwmresist = "Status";
                        if ((((Program.ffrspellbinding == true) && (Program.ffrsplevel < 5)) || ((Program.ffrspellbinding == false) && (ffrwmstrength < 50))))
                        {
                            if ((((Program.ffrspellbinding == true) && (Program.ffrsplevel == 1)) || ((Program.ffrspellbinding == false) && (ffrwmstrength <= 10)))) { ffrwmallies = 2; }
                            if (Program.ffrspantiweak == 0)
                            {
                                if (Program.ffrspResistCount > 4)
                                {
                                    colors.echo(4, $"Resists already capped! Rerolling.");
                                    goto whitemagic;
                                }
                                // else if (Program.ffrspResistCount == 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend hindrance }
                                // else if (Program.ffrspResistCount < 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend weak }
                                // else if (Program.ffrspResistCount == 4) { "Defend malus" }
                                // else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend psi }
                                Program.ffrspantiweak = Program.ffrspResistCount + 1;
                                Program.ffrspResistCount++;
                            }
                        }
                        else { ffrwmresmagic = true; }
                    }
                    else if (ffrwmbuff == 2)
                    {
                        ffrwmresist = "Poison / Stone";
                        if ((((Program.ffrspellbinding == true) && (Program.ffrsplevel < 5)) || ((Program.ffrspellbinding == false) && (ffrwmstrength < 50))))
                        {
                            if ((((Program.ffrspellbinding == true) && (Program.ffrsplevel == 1)) || ((Program.ffrspellbinding == false) && (ffrwmstrength <= 10)))) { ffrwmallies = 2; }
                            if (Program.ffrspantibane == 0)
                            {
                                if (Program.ffrspResistCount > 4)
                                {
                                    colors.echo(4, $"Resists already capped! Rerolling.");
                                    goto whitemagic;
                                }
                                // n585 = elseif(% Program.ffrspResistCount = 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend poisonous }
                                // n586 = elseif(% Program.ffrspResistCount isnum 1 - 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend bane }
                                // n587 =            else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend gas }
                                Program.ffrspantibane = Program.ffrspResistCount + 1;
                                Program.ffrspResistCount++;
                            }
                        }
                        else { ffrwmresdecay = true; }
                    }
                    else if (ffrwmbuff == 3)
                    {
                        ffrwmresist = "Time";
                        if ((((Program.ffrspellbinding == true) && (Program.ffrsplevel < 5)) || ((Program.ffrspellbinding == false) && (ffrwmstrength < 50))))
                        {
                            if ((((Program.ffrspellbinding == true) && (Program.ffrsplevel == 1)) || ((Program.ffrspellbinding == false) && (ffrwmstrength <= 10)))) { ffrwmallies = 2; }
                            if (Program.ffrspantizap == 0)
                            {
                                if (Program.ffrspResistCount > 4)
                                {
                                    colors.echo(4, $"Resists already capped! Rerolling.");
                                    goto whitemagic;
                                }
                                //n603 = elseif(% Program.ffrspResistCount = 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend dimension }
                                //n604 = elseif(% Program.ffrspResistCount isnum 1 - 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend time }
                                //n605 =            else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend age }
                                Program.ffrspantizap = Program.ffrspResistCount + 1;
                                Program.ffrspResistCount++;
                            }
                        }
                        else { ffrwmresdecay = true; }
                    }
                    else if (ffrwmbuff == 4)
                    {
                        ffrwmresist = "Death";
                        if ((((Program.ffrspellbinding == true) && (Program.ffrsplevel < 5)) || ((Program.ffrspellbinding == false) && (ffrwmstrength < 50))))
                        {
                            if ((((Program.ffrspellbinding == true) && (Program.ffrsplevel == 1)) || ((Program.ffrspellbinding == false) && (ffrwmstrength <= 10)))) { ffrwmallies = 2; }
                            if (Program.ffrspantinecro == 0)
                            {
                                if (Program.ffrspResistCount > 4)
                                {
                                    colors.echo(4, $"Resists already capped! Rerolling.");
                                    goto whitemagic;
                                }
                                //n621 = elseif(% Program.ffrspResistCount = 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend necrotic }
                                //n622 = elseif(% Program.ffrspResistCount isnum 1 - 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend evil }
                                //n623 =            else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend rub }
                                Program.ffrspantinecro = Program.ffrspResistCount + 1;
                                Program.ffrspResistCount++;
                            }
                        }
                        else
                        {
                            if (ffrwmheal < 6) { ffrwmresmagic = true; }
                            else { ffrwmresdecay = true; }
                        }
                    }
                    else if (ffrwmbuff == 5)
                    {
                        ffrwmresist = "Fire";
                        if ((((Program.ffrspellbinding == true) && (Program.ffrsplevel < 5)) || ((Program.ffrspellbinding == false) && (ffrwmstrength < 50))))
                        {
                            if ((((Program.ffrspellbinding == true) && (Program.ffrsplevel == 1)) || ((Program.ffrspellbinding == false) && (ffrwmstrength <= 10)))) { ffrwmallies = 2; }
                            if (Program.ffrspantifire == 0)
                            {
                                if (Program.ffrspResistCount > 4)
                                {
                                    colors.echo(4, $"Resists already capped! Rerolling.");
                                    goto whitemagic;
                                }
                                //n642 = elseif(% Program.ffrspResistCount = 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend burning }
                                //n643 = elseif(% Program.ffrspResistCount isnum 1 - 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend fire }
                                //n644 =            else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend hot }
                                Program.ffrspantifire = Program.ffrspResistCount + 1;
                                Program.ffrspResistCount++;
                            }
                        }
                        else { ffrwmresdragon = true; }
                    }
                    else if (ffrwmbuff == 6)
                    {
                        ffrwmresist = "Ice";
                        if ((((Program.ffrspellbinding == true) && (Program.ffrsplevel < 5)) || ((Program.ffrspellbinding == false) && (ffrwmstrength < 50))))
                        {
                            if ((((Program.ffrspellbinding == true) && (Program.ffrsplevel == 1)) || ((Program.ffrspellbinding == false) && (ffrwmstrength <= 10)))) { ffrwmallies = 2; }
                            if (Program.ffrspantiice == 0)
                            {
                                if (Program.ffrspResistCount > 4)
                                {
                                    colors.echo(4, $"Resists already capped! Rerolling.");
                                    goto whitemagic;
                                }
                                //n660 = elseif(% Program.ffrspResistCount = 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend freezing }
                                //n661 = elseif(% Program.ffrspResistCount isnum 1 - 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend cold }
                                //n662 =            else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend ice }
                                Program.ffrspantiice = Program.ffrspResistCount + 1;
                                Program.ffrspResistCount++;
                            }
                        }
                        else { ffrwmresdragon = true; }
                    }
                    else if (ffrwmbuff == 7)
                    {
                        ffrwmresist = "Lit";
                        if ((((Program.ffrspellbinding == true) && (Program.ffrsplevel < 5)) || ((Program.ffrspellbinding == false) && (ffrwmstrength < 50))))
                        {
                            if ((((Program.ffrspellbinding == true) && (Program.ffrsplevel == 1)) || ((Program.ffrspellbinding == false) && (ffrwmstrength <= 10)))) { ffrwmallies = 2; }
                            if (Program.ffrspantilightning == 0)
                            {
                                if (Program.ffrspResistCount > 4)
                                {
                                    colors.echo(4, $"Resists already capped! Rerolling.");
                                    goto whitemagic;
                                }
                                //n678 = elseif(% Program.ffrspResistCount = 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend lightning }
                                //n679 = elseif(% Program.ffrspResistCount isnum 1 - 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend volt }
                                //n680 =            else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend ion }
                                Program.ffrspantilightning = Program.ffrspResistCount + 1;
                                Program.ffrspResistCount++;
                            }
                        }
                        else { ffrwmresdragon = true; }
                    }
                    else if (ffrwmbuff == 8)
                    {
                        ffrwmresist = "Earth";
                        if ((((Program.ffrspellbinding == true) && (Program.ffrsplevel < 5)) || ((Program.ffrspellbinding == false) && (ffrwmstrength < 50))))
                        {
                            if ((((Program.ffrspellbinding == true) && (Program.ffrsplevel == 1)) || ((Program.ffrspellbinding == false) && (ffrwmstrength <= 10)))) { ffrwmallies = 2; }
                            if (Program.ffrspantiquake == 0)
                            {
                                if (Program.ffrspResistCount > 4)
                                {
                                    colors.echo(4, $"Resists already capped! Rerolling.");
                                    goto whitemagic;
                                }
                                //n696 = elseif(% Program.ffrspResistCount = 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend tectonic }
                                // n697 = elseif(% Program.ffrspResistCount isnum 1 - 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend land }
                                //n698 =            else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend geo }
                                Program.ffrspantiquake = Program.ffrspResistCount + 1;
                                Program.ffrspResistCount++;
                            }
                        }
                        else { ffrwmresmagic = true; }
                    }
                    if (ffrwmresmagic == true)
                    {
                        ffrwmresist = "Magic";
                        if (Program.ffrspantimagic == 0)
                        {
                            if (Program.ffrspResistCount > 4)
                            {
                                colors.echo(4, $"Resists already capped! Rerolling.");
                                goto whitemagic;
                            }
                            //n712 = elseif(% Program.ffrspResistCount = 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend mortality }
                            //n713 = elseif(% Program.ffrspResistCount isnum 1 - 2) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend life }
                            //n714 = elseif(% Program.ffrspResistCount = 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend magic }
                            //n715 =          else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend bio }
                            Program.ffrspantimagic = Program.ffrspResistCount + 1;
                            Program.ffrspResistCount++;
                        }
                    }
                    else if (ffrwmresdecay == true)
                    {
                        ffrwmresist = "Decay";
                        if (Program.ffrspantitoxin == 0)
                        {
                            if (Program.ffrspResistCount > 4)
                            {
                                colors.echo(4, $"Resists already capped! Rerolling.");
                                goto whitemagic;
                            }
                            //n727 = elseif(% Program.ffrspResistCount = 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend futility }
                            //n728 = elseif(% Program.ffrspResistCount isnum 1 - 2) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend doom }
                            //n729 = elseif(% Program.ffrspResistCount = 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend waste }
                            //n730 =          else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend rot }
                            Program.ffrspantitoxin = Program.ffrspResistCount + 1;
                            Program.ffrspResistCount++;
                        }
                    }
                    else if (ffrwmresdragon == true)
                    {
                        ffrwmresist = "Dragon";
                        if (Program.ffrspantidamage == 0)
                        {
                            if (Program.ffrspResistCount > 4)
                            {
                                colors.echo(4, $"Resists already capped! Rerolling.");
                                goto whitemagic;
                            }
                            //n742 = elseif(% Program.ffrspResistCount = 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend elemental }
                            //n743 = elseif(% Program.ffrspResistCount isnum 1 - 2) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend wyrm }
                            //n744 = elseif(% Program.ffrspResistCount = 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend spell }
                            //n745 =          else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspResistCount + 1) Defend wiz }
                            Program.ffrspantidamage = Program.ffrspResistCount + 1;
                            Program.ffrspResistCount++;
                        }
                    }
                    //n750 = write - al3 WhiteSpell.txt Effect: Resist % ffrwmresist
                    switch (ffrwmresist)
                    {
                        case "Status": ffrwmEffByte = 1; Program.ffrspCurrentResist = Program.ffrspantiweak; break;
                        case "Poison / Stone": ffrwmEffByte = 2; Program.ffrspCurrentResist = Program.ffrspantibane; break;
                        case "Time": ffrwmEffByte = 4; Program.ffrspCurrentResist = Program.ffrspantizap; break;
                        case "Death": ffrwmEffByte = 8; Program.ffrspCurrentResist = Program.ffrspantinecro; break;
                        case "Fire": ffrwmEffByte = 16; Program.ffrspCurrentResist = Program.ffrspantifire; break;
                        case "Ice": ffrwmEffByte = 32; Program.ffrspCurrentResist = Program.ffrspantiice; break;
                        case "Lit": ffrwmEffByte = 64; Program.ffrspCurrentResist = Program.ffrspantilightning; break;
                        case "Earth": ffrwmEffByte = 128; Program.ffrspCurrentResist = Program.ffrspantiquake; break;
                        case "Magic": ffrwmEffByte = 137; Program.ffrspCurrentResist = Program.ffrspantimagic; Program.ffrspRmPerms = false; break;
                        case "Decay": ffrwmEffByte = 14; Program.ffrspCurrentResist = Program.ffrspantitoxin; Program.ffrspRmPerms = false; break;
                        case "Dragon": ffrwmEffByte = 112; Program.ffrspCurrentResist = Program.ffrspantidamage; Program.ffrspRmPerms = false; break;
                        default: ffrwmEffByte = whiteDice.Next(0, 256); break;
                    }
                }
                #endregion
                else if (ffrwmbuff < 11)
                {
                    // write - al3 WhiteSpell.txt Effect: Absorb Up
                    ffrwmTypeByte = 9;
                }
                else if (ffrwmbuff < 13)
                {
                    //write - al3 WhiteSpell.txt Effect: Evade Up
                    ffrwmTypeByte = 16;
                }
                else if (ffrwmbuff == 13)
                {
                    ffrwmstrskip = true;
                    if (ffrwmallies == 1)
                    {
                        // write - al3 WhiteSpell.txt Effect: Double Hits 
                        ffrwmTypeByte = 12;
                    }
                    else { colors.echo(13, $"Debug: Bounced at HAST(Target was {ffrwmallies} instead of 1)"); goto whitemagic; }
                    // White FAST is only allowed to cast on self
                    // This implementation also makes it pass
                    // a 1-in-3 check to succeed
                }
                else if (ffrwmbuff == 14)
                {
                    ffrwmaccskip = false;
                    if (Program.ffrspellbinding == false)
                    {
                        if (ffrwmallies == 3)
                        {
                            ffrwmaccuracy = (int)Math.Floor(ffrwmaccuracy * 0.2);
                            if (ffrwmaccuracy < 32) { Program.ffrsptier = 3; }
                            else Program.ffrsptier = 4;
                            if (ffrwmaccuracy < 16) { Program.ffrsptier = 2; }
                            if (ffrwmaccuracy < 8) { Program.ffrsptier = 1; }
                        }
                        else if (ffrwmallies == 2)
                        {
                            ffrwmaccuracy = (int)Math.Floor(ffrwmaccuracy * 0.3);
                            if (ffrwmaccuracy < 48) { Program.ffrsptier = 3; }
                            else Program.ffrsptier = 4;
                            if (ffrwmaccuracy < 24) { Program.ffrsptier = 2; }
                            if (ffrwmaccuracy < 16) { Program.ffrsptier = 1; }
                        }
                        else
                        {
                            ffrwmaccuracy = (int)Math.Floor(ffrwmaccuracy * 0.53);
                            if (ffrwmaccuracy < 64) { Program.ffrsptier = 3; }
                            else Program.ffrsptier = 4;
                            if (ffrwmaccuracy < 40) { Program.ffrsptier = 2; }
                            if (ffrwmaccuracy < 24) { Program.ffrsptier = 1; }
                        }
                    }
                    //n769 = write - al3 WhiteSpell.txt Effect: Attack Up
                    ffrwmTypeByte = 13;
                    // White TMPR is cut down in power by at least 75%
                }
                else
                {
                    colors.echo(4, $"Value {ffrwmbuff} out of range for Buffs"); return "Spell Failed";
                }
            }
            #endregion
            #endregion
            #region Element
            // Damage Element Selection
            // White Magic struggles with damage all-around
            // so it doesn't have access to Fire, Ice, nor Lit
            // it also can't access Death, but that's flavour
            else if (ffrwmeffect == 1)
            {
                if (ffrwmelement == 1)
                {
                    //write - al4 WhiteSpell.txt Element: Status
                    ffrwmElemByte = 1;
                }
                else if (ffrwmelement == 2)
                {
                    if (Program.ffrspellbinding == true) { ffrwmaccuracy += 2; }
                    // n782 = write - al4 WhiteSpell.txt Element: Stone
                    ffrwmElemByte = 2;
                    Program.ffrspRwPerms = false;
                }
                else if (ffrwmelement == 3)
                {
                    if (Program.ffrspellbinding == true) { ffrwmaccuracy--; }
                    //n786 = write - al4 WhiteSpell.txt Element: Time
                    ffrwmElemByte = 4;
                }
                else if (ffrwmelement == 4)
                {
                    if (Program.ffrspellbinding == true) { ffrwmaccuracy += 2; }
                    //n790 = write - al4 WhiteSpell.txt Element: Earth
                    ffrwmElemByte = 128;
                    Program.ffrspRmPerms = false;
                }
                else if ((ffrwmelement == 0) && (whiteDice.Next(1, 21) >= whiteDice.Next(1, 21)))
                {
                    //n793 = write - al4 WhiteSpell.txt Element: None
                    ffrwmElemByte = 0;
                    if (Program.ffrspellbinding == true) { ffrwmaccuracy++; }
                    if (ffrwmenemies == 2) Program.ffrspRmPerms = false;
                }
                else { colors.echo(13, $"FADE failed saving roll. Rolling new spell."); goto whitemagic; }
            }
            // Harm Undead is always non-elemental
            // I thought about giving it Fire, but
            // that would just be bonus damage
            // (except on Lich2)
            else if (ffrwmeffect == 2)
            {
                //write - a14 WhiteSpell.txt Element: None
                ffrwmElemByte = 0;
            }
            else { colors.echo(4, $"Value {ffrwmeffect} out of range for Damage Type"); return "Spell Failed"; }
        #endregion
        #region Target
        // Writes the Spell Target to a file
        walljump:
            if (ffrwmspecial == false)
            {
                if (ffrwmeffect < 4)
                {
                    if (ffrwmenemies == 1)
                    {
                        if (Program.ffrspellbinding == true)
                        {
                            if (ffrwmeffect < 3) { ffrwmaccuracy++; }
                            ffrwmaccuracy++;
                        }
                        //n811 = write - al5 WhiteSpell.txt Target: Single Enemy
                        ffrwmTargByte = 2;
                    }
                    else if (ffrwmenemies == 2)
                    {
                        //write - al5 WhiteSpell.txt Target: Enemy Party
                        ffrwmTargByte = 1;
                    }
                    else { colors.echo(4, $"Value {ffrwmenemies} out of range for Enemy Target"); return "Spell Failed"; }
                }
                else if (ffrwmeffect > 3)
                {
                    if (ffrwmallies == 1)
                    {
                        if (ffrwmeffect == 4)
                        {
                            ffrwmallies += whiteDice.Next(1, 3);
                            if (ffrwmallies == 2 && ffrwmheal < 10) { Program.ffrspRwPerms = true; Program.ffrspWmPerms = true; }
                        }
                        else
                        {
                            //    n820 = write - al5 WhiteSpell.txt Target: Caster
                            ffrwmTargByte = 4;
                            #region SelfBuffPower
                            if (Program.ffrspellbinding == true)
                            {
                                if (ffrwmTypeByte == 9)
                                {
                                    if (Program.ffrsplevel == 8) { ffrwmstrength = 64; Program.ffrsptier = 4; }
                                    if (Program.ffrsplevel < 8) { ffrwmstrength = 32; Program.ffrsptier = 3; }
                                    if (Program.ffrsplevel < 5) { ffrwmstrength = 24; Program.ffrsptier = 2; }
                                    if (Program.ffrsplevel < 3) { ffrwmstrength = 16; Program.ffrsptier = 1; }
                                }
                                else if (ffrwmTypeByte == 16)
                                {
                                    if (Program.ffrsplevel == 8) { ffrwmstrength = 240; Program.ffrsptier = 4; }
                                    if (Program.ffrsplevel < 8) { ffrwmstrength = 120; Program.ffrsptier = 3; }
                                    if (Program.ffrsplevel < 5) { ffrwmstrength = 80; Program.ffrsptier = 2; }
                                    if (Program.ffrsplevel < 3) { ffrwmstrength = 60; Program.ffrsptier = 1; }
                                }
                                else if (ffrwmTypeByte == 13)
                                {
                                    if (Program.ffrsplevel == 8) { ffrwmstrength = 16; Program.ffrsptier = 4; }
                                    if (Program.ffrsplevel < 8) { ffrwmstrength = 12; Program.ffrsptier = 3; }
                                    if (Program.ffrsplevel < 5) { ffrwmstrength = 10; Program.ffrsptier = 2; }
                                    if (Program.ffrsplevel < 3) { ffrwmstrength = 8; Program.ffrsptier = 1; }
                                }
                            }
                            else if (ffrwmTypeByte == 9)
                            {
                                ffrwmstrength = Math.Floor(ffrwmstrength * 0.64);
                                if (ffrwmstrength < 48) { Program.ffrsptier = 3; }
                                else Program.ffrsptier = 4;
                                if (ffrwmstrength < 28) { Program.ffrsptier = 2; }
                                if (ffrwmstrength < 20) { Program.ffrsptier = 1; }
                            }
                            else if (ffrwmTypeByte == 13) ffrwmstrength = Math.Floor(ffrwmstrength * 0.2);
                            else if (ffrwmTypeByte == 16)
                            {
                                ffrwmstrength = Math.Floor(ffrwmstrength * 2.4);
                                if (ffrwmstrength < 200) { Program.ffrsptier = 3; }
                                else Program.ffrsptier = 4;
                                if (ffrwmstrength < 100) { Program.ffrsptier = 2; }
                                if (ffrwmstrength < 75) { Program.ffrsptier = 1; }
                            }
                        }
                        if (ffrwmTypeByte != 12)
                        {
                            if (Program.ffrsptier == 4) Program.ffrspWmPerms = false;
                            if (Program.ffrsptier >= 3) Program.ffrspRwPerms = false;
                            if (Program.ffrsptier >= 2) Program.ffrspRmPerms = false;
                        }
                    }
                    #endregion
                    if (ffrwmallies == 2)
                    {
                        if ((ffrwmTypeByte == 9) || (ffrwmTypeByte == 16)) { ffrwmallies++; }
                        else
                        {
                            // write - al5 WhiteSpell.txt Target: Single Ally
                            ffrwmTargByte = 16;
                            #region CUREPower
                            if (ffrwmeffect == 4)
                            {
                                if ((Program.ffrspellbinding == true) || (Program.ffrspflags.Contains("h")))
                                {
                                    if (Program.ffrsplevel != 8)
                                    {
                                        ffrwmstrength = (int)Math.Floor(Math.Pow(2, (3.5 + Program.ffrsplevel / 2)) + (Program.ffrsplevel - 1) / 2); // CURE formula, accurate to FF1
                                    }
                                    else { ffrwmstrength = whiteDice.Next(66, 185); }
                                }
                                else { ffrwmstrength = (int)Math.Floor(ffrwmstrength * 1.6); }
                                if (ffrwmstrength > 32) { Program.ffrsptier = 2; }
                                else { Program.ffrsptier = 1; }
                                if (ffrwmstrength > 65) { Program.ffrsptier = 3; }
                            }
                            #endregion
                            #region BLESPower
                            else
                            {
                                if (Program.ffrspellbinding == true)
                                {
                                    ffrwmaccuracy--;
                                    if (Program.ffrsplevel == 8) { ffrwmstrength = 12; Program.ffrsptier = 4; }
                                    if (Program.ffrsplevel < 8) { ffrwmstrength = 10; Program.ffrsptier = 3; }
                                    if (Program.ffrsplevel < 5) { ffrwmstrength = 8; Program.ffrsptier = 2; }
                                    if (Program.ffrsplevel < 3) { ffrwmstrength = 5; Program.ffrsptier = 1; }
                                }
                                else ffrwmstrength = Math.Floor(ffrwmstrength * 0.14);
                            }
                            #endregion
                            if (ffrwmheal < 7 || ffrwmeffect == 5)
                            {
                                if (Program.ffrsptier == 4)
                                {
                                    Program.ffrspWmPerms = false;
                                    if (ffrwmbuff != 0) Program.ffrspRwPerms = false;
                                }
                                if (Program.ffrsptier >= 3) Program.ffrspRmPerms = false;
                                if (Program.ffrspellbinding == true && Program.ffrsplevel == 5) Program.ffrspRmPerms = true;
                                else if (Program.ffrspellbinding == false && ((ffrwmheal < 7 && ffrwmstrength == 66) || (ffrwmeffect == 5 && ffrwmaccuracy == 24))) Program.ffrspRmPerms = true;
                            }
                        }
                    }
                    if (ffrwmallies == 3)
                    {
                        //n872 = write - al5 WhiteSpell.txt Target: All Allies
                        ffrwmTargByte = 8;
                        #region HEALPower
                        if (ffrwmeffect == 4)
                        {
                            if ((Program.ffrspellbinding == true) || (Program.ffrspflags.Contains("h")))
                            {
                                if (Program.ffrsplevel != 8) { ffrwmstrength = (int)Math.Floor(6 * Math.Pow(2, (Program.ffrsplevel / 2 - 0.5))); } // HEAL formula, accurate to FF1
                                else { ffrwmstrength = whiteDice.Next(48, 136); }
                            }
                            else { ffrwmstrength -= 4; }
                            if (ffrwmstrength > 23) { Program.ffrsptier = 2; }
                            else { Program.ffrsptier = 1; }
                            if (ffrwmstrength > 47) { Program.ffrsptier = 3; }
                            if (ffrwmheal < 7) Program.ffrspRwPerms = false;
                        }
                        #endregion
                        #region PartyBuffPower
                        else if (ffrwmeffect == 5)
                        {
                            if (Program.ffrspellbinding == true)
                            {
                                if (ffrwmTypeByte == 9)
                                {
                                    if (Program.ffrsplevel == 8) { ffrwmstrength = 24; Program.ffrsptier = 4; }
                                    if (Program.ffrsplevel < 8) { ffrwmstrength = 16; Program.ffrsptier = 3; }
                                    if (Program.ffrsplevel < 5) { ffrwmstrength = 12; Program.ffrsptier = 2; }
                                    if (Program.ffrsplevel < 3) { ffrwmstrength = 8; Program.ffrsptier = 1; }
                                }
                                else if (ffrwmTypeByte == 16)
                                {
                                    if (Program.ffrsplevel == 8) { ffrwmstrength = 80; Program.ffrsptier = 4; }
                                    if (Program.ffrsplevel < 8) { ffrwmstrength = 60; Program.ffrsptier = 3; }
                                    if (Program.ffrsplevel < 5) { ffrwmstrength = 40; Program.ffrsptier = 2; }
                                    if (Program.ffrsplevel < 3) { ffrwmstrength = 30; Program.ffrsptier = 1; }
                                }
                                else if (ffrwmTypeByte == 13)
                                {
                                    ffrwmaccskip = false;
                                    ffrwmaccuracy -= 2;
                                    if (Program.ffrsplevel == 8) { ffrwmstrength = 10; Program.ffrsptier = 4; }
                                    if (Program.ffrsplevel < 8) { ffrwmstrength = 8; Program.ffrsptier = 3; }
                                    if (Program.ffrsplevel < 5) { ffrwmstrength = 5; Program.ffrsptier = 2; }
                                    if (Program.ffrsplevel < 3) { ffrwmstrength = 3; Program.ffrsptier = 1; }
                                }
                            }
                            else if (ffrwmTypeByte == 9)
                            {
                                ffrwmstrength = Math.Floor(ffrwmstrength * 0.3);
                                if (ffrwmstrength < 24) { Program.ffrsptier = 3; }
                                else Program.ffrsptier = 4;
                                if (ffrwmstrength < 16) { Program.ffrsptier = 2; }
                                if (ffrwmstrength < 12) { Program.ffrsptier = 1; }
                            }
                            else if (ffrwmTypeByte == 13) ffrwmstrength = Math.Floor(ffrwmstrength * 0.12);
                            else if (ffrwmTypeByte == 16)
                            {
                                ffrwmstrength = Math.Floor(ffrwmstrength * 0.8);
                                if (ffrwmstrength < 70) { Program.ffrsptier = 3; }
                                else Program.ffrsptier = 4;
                                if (ffrwmstrength < 50) { Program.ffrsptier = 2; }
                                if (ffrwmstrength < 35) { Program.ffrsptier = 1; }
                            }
                            if (Program.ffrsptier == 4) Program.ffrspWmPerms = false;
                            if (ffrwmTypeByte != 10)
                            {
                                if (Program.ffrsptier >= 3) Program.ffrspRwPerms = false;
                                if (Program.ffrsptier >= 2) Program.ffrspRmPerms = false;
                            }
                        }
                        #endregion
                    }
                }
                else { colors.echo(4, $"Value {ffrwmeffect} out of range for Power Assignment"); return "Spell Failed"; }
            }
            else { colors.echo(13, $"Debug: Target already assigned by Rare Spell"); }
            #endregion
            #region Accuracy
            // Accuracy Balance
            // Defines the appropriate tiers of Accuracy for Spellbooks
            // Accuracy no longer caps
            if (Program.ffrspellbinding == true)
            {
                if (ffrwmaccuracy < 1) { ffrwmaccuracy = 1; }
                switch (ffrwmaccuracy)
                {
                    case 1: ffrwmaccsay = "0 "; ffrwmaccuracy = 0; break;
                    case 2: ffrwmaccsay = "5 "; ffrwmaccuracy = 5; break;
                    case 3: ffrwmaccsay = "8 "; ffrwmaccuracy = 8; break;
                    case 4: ffrwmaccsay = "16"; ffrwmaccuracy = 16; break;
                    case 5: ffrwmaccsay = "24"; ffrwmaccuracy = 24; break;
                    case 6: ffrwmaccsay = "32"; ffrwmaccuracy = 32; break;
                    case 7: ffrwmaccsay = "40"; ffrwmaccuracy = 40; break;
                    case 8: ffrwmaccsay = "48"; ffrwmaccuracy = 48; break;
                    case 9: ffrwmaccsay = "64"; ffrwmaccuracy = 64; break;
                    case 10: ffrwmaccsay = "107"; ffrwmaccuracy = 107; break;
                    case 11: ffrwmaccsay = "128"; ffrwmaccuracy = 128; break;
                    case 12: ffrwmaccsay = "152"; ffrwmaccuracy = 152; break;
                    case 13: ffrwmaccsay = "175"; ffrwmaccuracy = 175; break;
                    case 14: ffrwmaccsay = "210"; ffrwmaccuracy = 210; break;
                    default: ffrwmaccsay = "255"; ffrwmaccuracy = 255; break;
                }
                if (Program.ffrsplevel == 8) Program.ffrspWmPerms = false;
            }
            if ((ffrwmaccuracy == 255) || (ffrwmaccsay == "Auto-Hit")) { colors.echo(8, $"Debug: Assumed Accuracy was Auto-Hit"); }
            #endregion

            // Cleanup
            //
            // Writes only information relevant to the spell
            //  if (% ffrwmstrskip = 0) && (% ffrwmaccskip = 0) { write - a16 WhiteSpell.txt Power: % ffrwmstrength | write - al7 WhiteSpell.txt Acc Bonus: % ffrwmaccsay }
            // elseif(% ffrwmstrskip = 0) && (% ffrwmaccskip = 1) { write - a16 WhiteSpell.txt Power: % ffrwmstrength }
            // elseif(% ffrwmstrskip = 1) && (% ffrwmaccskip = 0) { write - a16 WhiteSpell.txt Acc Bonus: % ffrwmaccsay }
            if (ffrwmstrskip == false) { ffrwmEffByte = (int)ffrwmstrength; }
            if (ffrwmaccskip == false) { ffrwmAccByte = ffrwmaccuracy; }
            if (ffrwmAccByte > 64) Program.ffrspRmPerms = false;

            // Doesn't open the file when Spellbinding
            // because that would open 64 files
            // Debug: if (% Program.ffrspellbinding = 1) && (% ffrwmbuff = 14) { / run WhiteSpell.txt | halt }
            //  if ($read(WhiteSpell.txt, w, *resist *)) { echo 8 - s Resist ID: % ffrwmbuff }
            //  if (% ffrwmaccsay > 64) { set % Program.ffrspredharm 1 }
            //  if (% ffrnowrite != 1) { / run WhiteSpell.txt | unset % ffr * }

            Console.WriteLine();
            if (ffrwmaccskip == false) { colors.echo(9, $"Accuracy Byte: {ffrwmAccByte}"); }
            if (ffrwmEffByte != 0) { colors.echo(9, $"Effect Byte: {ffrwmEffByte}"); }
            colors.echo(9, $"Element Byte: {ffrwmElemByte}");
            colors.echo(9, $"Target Byte: {ffrwmTargByte}");
            colors.echo(9, $"Type Byte: {ffrwmTypeByte}");

            Console.WriteLine();
            colors.echo(12, $"Magic Level: {Program.ffrsplevel}");
            colors.echo(12, $"Spell Slot: {Program.ffrspslot}");
            colors.echo(12, $"Tier {Program.ffrsptier} Power");
            ffrWhiteSpell = $"{ffrwmAccByte}.{ffrwmEffByte}_{ffrwmElemByte}-{ffrwmTargByte}~{ffrwmTypeByte}";

            Console.WriteLine();
            colors.echo(0, $"Returned {ffrWhiteSpell} to ffr-spellbinder");
            Console.WriteLine();
            return ffrWhiteSpell;
        }
    }
}
