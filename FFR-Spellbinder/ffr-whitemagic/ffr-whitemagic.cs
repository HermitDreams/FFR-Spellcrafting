using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.ComponentModel;
using System;

namespace ffr_spellbinder
{
    internal class ffr_whitemagic
    {
        public static string WMag()
        {
            // Final Fantasy Randomizer White Magic Generator. IRC Script by Linkshot. C# Port.
            var whiteDice = new Random();
            // Placeholder vars; remove later!!
            bool ffrspellbinding = false;
            double ffrsplevel = whiteDice.Next(1, 9);
            int ffrspslot = whiteDice.Next(1, 5);
            string ffrspflags = "ehS";
            int ffrspreroll = 0;
            int ffrspresist = 0;
            bool ffrspwall = false;
            bool ffrspantiweak = false;
            bool ffrspantibane = false;
            bool ffrspantizap = false;
            bool ffrspantinecro = false;
            bool ffrspantifire = false;
            bool ffrspantiice = false;
            bool ffrspantilightning = false;
            bool ffrspantiquake = false;
            bool ffrspantimagic = false;
            bool ffrspantitoxin = false;
            bool ffrspantidamage = false;
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
            int ffrwmtier = 0;
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

            #region Auto-Hit

            // Sets Accuracy to Auto-Hit or Tiered
            if ((ffrwmaccroll < 2) || (ffrwmaccroll == 148))
            {
                ffrwmaccsay = "Auto-Hit";
                // if (L isincs % ffrspflags) { write - i SpellLog.txt Accuracy Change: 3 / 256 succeeded.Spell is slated to get 255 Hit Bonus. | write - i SpellLog.txt - }
            }
            else if (ffrspellbinding == true)
            {
                if (ffrsplevel < 5) { ffrwmaccuracy = 5; ffrwmtier = 2; }
                if (ffrsplevel > 4) { ffrwmaccuracy = 7; ffrwmtier = 3; }
                if (ffrsplevel == 1) { ffrwmaccuracy = 4; ffrwmtier = 1; }
                if (ffrsplevel == 8) { ffrwmaccuracy = 9; ffrwmtier = 4; }
                // if (L isincs % ffrspflags) { write - i SpellLog.txt Accuracy Change: Spellbinder has set the Accuracy Tier to % ffrwmaccuracy to match Level % ffrsplevel | write - i SpellLog.txt - }
            }
            else
            {
                ffrwmaccsay = ffrwmaccuracy.ToString();
                // if (L isincs % ffrspflags) { write - i SpellLog.txt Accuracy Output has been set to % ffrwmaccsay | write - i SpellLog.txt - }
            }

            #endregion Auto-Hit
            #region SlotPerms
            // Spellbinding Slot Permissions
            #region HEL2Fix
            // Compatibility with HEL2 fix
            if ((ffrspflags.IndexOf("S") != -1) && (ffrsplevel == 5) && (ffrspslot == 4) && (ffrspreroll < 5))
            {
                ffrwmeffect = 4;
                ffrwmheal = 1;
                ffrwmallies = 3;
            }
            #endregion
            #region PreserveHealing
            // Makes sure Healing lands in 11 specific slots
            if ((ffrspflags.IndexOf("h") != -1) && (ffrspreroll < 5))
            {
                // if (L isincs % ffrspflags) { write - i SpellLog.txt Flag "h" is evaluating this spell.Reroll counter is at % ffrspreroll of 5. }
                if (ffrwmafflict == 8)
                {
                    ffrwmafflict = 3;
                    // if (L isincs % ffrspflags) { write - i SpellLog.txt Affliction Change: 8(Confusion) was changed to 3(Poison) to account for Pure }
                }
                if (ffrwmafflict == 5)
                {
                    ffrwmafflict = 3;
                    // if (L isincs % ffrspflags) { write - i SpellLog.txt AFfliction change: 5(Stun) was changed to 3(Poison) to account for Pure }
                }
                if ((ffrsplevel == 1) && (ffrspslot == 1))
                {
                    ffrwmeffect = 4;
                    ffrwmallies = 2;
                    ffrwmheal = 1;
                }
                else if ((ffrsplevel == 3) && (!Enumerable.Range(2, 2).Contains(ffrspslot)))
                {
                    ffrwmeffect = 4;
                    ffrwmheal = 1;
                    if (ffrspslot == 1)
                    {
                        ffrwmallies = 2;
                        // if (L isincs % ffrspflags) { write - i SpellLog.txt Slot 1 confirmed. Party Targeting has been set to 2(Single Ally) }
                    }
                    else if (ffrspslot == 4)
                    {
                        ffrwmallies = 3;
                        // if (L isincs % ffrspflags) { write - i SpellLog.txt Slot 4 confirmed. Party Targeting has been set to 3(Whole Party) }
                    }
                    else { Console.WriteLine($"ffrspslot failed to parse. Value was {ffrspslot}."); }
                }
                else if ((ffrsplevel == 4) && (ffrspslot == 1))
                {
                    ffrwmeffect = 4;
                    ffrwmallies = 2;
                    ffrwmheal = 7;
                    ffrwmafflict = 3;
                }
                else if ((ffrsplevel == 5) && (ffrspslot != 3))
                {
                    ffrwmeffect = 4;
                    if (ffrspslot == 2)
                    {
                        ffrwmallies = 2;
                        ffrwmheal = 7;
                        ffrwmafflict = 1;
                    }
                    else
                    {
                        ffrwmheal = 1;
                        if (ffrspslot == 1)
                        {
                            ffrwmallies = 2;
                            // if (L isincs % ffrspflags) { write - i SpellLog.txt Slot 1 confirmed. Party Targeting has been set to 2(Single Ally) }
                        }
                        else if (ffrspslot == 4)
                        {
                            ffrwmallies = 3;
                            // if (L isincs % ffrspflags) { write - i SpellLog.txt Slot 4 confirmed. Party Targeting has been set to 3(Whole Party) }
                        }
                        else { Console.WriteLine($"ffrspslot failed to parse. Value was {ffrspslot}."); }
                    }
                }
                else if ((ffrsplevel == 6) && (ffrspslot == 1))
                {
                    ffrwmeffect = 4;
                    ffrwmallies = 2;
                    ffrwmheal = 7;
                    ffrwmafflict = 2;
                }
                else if ((ffrsplevel == 7) && (!Enumerable.Range(2, 2).Contains(ffrspslot)))
                {
                    ffrwmeffect = 4;
                    if (ffrspslot == 1)
                    {
                        ffrwmallies = 2;
                        ffrwmheal = 10;
                    }
                    else if (ffrspslot == 4)
                    {
                        ffrwmallies = 3;
                        ffrwmheal = 1;
                    }
                }
                else if ((ffrsplevel == 8) && (ffrspslot == 1))
                {
                    ffrwmeffect = 4;
                    ffrwmallies = 3;
                    ffrwmheal = 7;
                    ffrwmafflict = 1;
                }
                else
                {
                    if ((ffrwmeffect == 4) && ((!Enumerable.Range(7, 3).Contains(ffrwmheal)) || ((Enumerable.Range(7, 3).Contains(ffrwmheal)) && (Enumerable.Range(1, 2).Contains(ffrwmafflict))) || ((ffrsplevel < 5) && ((Enumerable.Range(7, 3).Contains(ffrwmheal)) && (ffrwmafflict == 3)))))
                    {
                        colors.echo(13, $"Debug: Bounced healing type {ffrwmheal}-{ffrwmafflict} at slot {ffrsplevel}-{ffrspslot}");
                        goto whitemagic;
                    }
                }
                // if (L isincs % ffrspflags) { write - i SpellLog.txt Flag "h" has finished evaluating this spell. | write - i SpellLog.txt - }
            }
            #endregion
            #region EnemySanity
            // Prevents Harm, Confusion, Fear, Soft, and Life from landing in slots enemies use
            if ((ffrspflags.IndexOf("e") != -1) && (ffrspreroll < 5))
            {
                // if (L isincs % ffrspflags) { write - i SpellLog.txt Flag "e" is evaluating this spell.Reroll counter is at % ffrspreroll of 5 }
                if ((ffrwmeffect == 2) || ((ffrwmeffect == 3) && ((ffrwmdebuff == 5) || (ffrwmdebuff == 7) || ((ffrwmdebuff == 10) && (ffrwmafflict == 8)))) || ((ffrwmeffect == 4) && (Enumerable.Range(7, 3).Contains(ffrwmheal)) && (ffrwmafflict < 3)))
                {
                    if (((ffrsplevel == 1) && (!Enumerable.Range(2, 2).Contains(ffrspslot))) || ((ffrsplevel == 2) && (ffrspslot == 2)) || ((ffrsplevel == 6) && (ffrspslot > 2)) || ((ffrsplevel == 3) && (ffrspslot > 2)) || (((ffrsplevel == 5) || (ffrsplevel == 7)) && ((ffrspslot == 1) || (ffrspslot == 4))) || ((ffrsplevel == 8) && (ffrspslot > 2)))
                    {
                        if (ffrwmeffect == 3) { colors.echo(13, $"Debug: Bounced Debuff Type {ffrwmdebuff} at {ffrsplevel}-{ffrspslot}"); }
                        else if (ffrwmeffect == 4)
                        {
                            colors.echo(13, $"Debug: Bounced Cure Type {ffrwmafflict} at {ffrsplevel}-{ffrspslot}");
                        }
                        else { colors.echo(13, $"Debug: Bounced Harm Undead at {ffrsplevel}-{ffrspslot}"); }
                        goto whitemagic;
                    }
                }
                // if (L isincs % ffrspflags) { write - i SpellLog.txt Flag "e" has finished evaluating this spell. | write - i SpellLog.txt - }
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
                if (ffrspellbinding == true)
                {
                    if (ffrsplevel < 8) { ffrwmaccuracy = 5; }
                    if (ffrsplevel == 8) { ffrwmstrength = 100; }
                    else { ffrwmstrength = (int)ffrsplevel * 10; }
                }
                else { if (ffrwmenemies == 1) { ffrwmstrength = ffrwmstrength * whiteDice.Next(100, 151) / 100; } }
                ffrwmstrength = Convert.ToInt32(Math.Floor(ffrwmstrength * 0.8));
                if (ffrwmstrength == 96) { ffrwmstrength += 4; }
                if (Enumerable.Range(24, 16).Contains((int)ffrwmstrength)) { ffrwmtier = 2; }
                else if (Enumerable.Range(40, 40).Contains((int)ffrwmstrength)) { ffrwmtier = 3; }
                else if (ffrwmstrength >= 80) { ffrwmtier = 4; }
                else { ffrwmtier = 1; }
            }
            #endregion
            #region Prejudice
            else if (ffrwmeffect == 2)
            {
                // write - al2 WhiteSpell.txt Type: Harm Undead
                ffrwmTypeByte = 2;
                if (ffrspellbinding == true)
                {
                    if (ffrsplevel < 7) { ffrwmaccuracy = 5; }
                    else { ffrwmaccuracy++; }
                    if (ffrsplevel == 8) { ffrwmstrength = 100; }
                    else { ffrwmstrength = (int)(ffrsplevel + 1) * 10; }
                }
                if (ffrwmstrength > 30) { ffrwmtier = 2; }
                else { ffrwmtier = 1; }
                if (ffrwmstrength > 50) { ffrwmtier = 3; }
                if (ffrwmstrength > 70) { ffrwmtier = 4; }
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
                else { colors.echo(8, $"Debug: Debuff was allowed"); }
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
                return "Spell Failed";;
            }
            #endregion

            #region Debuffs
            // Debuff Selection
            // White gets an accuracy buff to MUTE, FEAR, and XFER families
            if (ffrwmeffect == 3)
            {
                if ((ffrspellbinding == true) && (ffrwmaccsay == "Auto-Hit") && (ffrwmdebuff < 6)) { ffrwmdebuff = 10; }
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
                    if (ffrspellbinding == true)
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
                    if (ffrspellbinding == true)
                    {
                        if (ffrsplevel > 1) { ffrwmaccuracy += 2; }
                        else { ffrwmaccuracy--; }
                    }
                    // write - al3 WhiteSpell.txt Effect: Sleep
                    ffrwmEffByte = 32;
                }
                else if (ffrwmdebuff == 4)
                {
                    // write - al3 WhiteSpell.txt Effect: Mute
                    ffrwmEffByte = 64;
                    if (ffrspellbinding == true) { ffrwmaccuracy += 4; }
                }
                else if (ffrwmdebuff == 5)
                {
                    if (ffrspellbinding == true) { ffrwmaccuracy += 2; }
                    // write - al3 WhiteSpell.txt Effect: Confuse
                    ffrwmEffByte = 128;
                }
                else if (ffrwmdebuff == 6)
                {
                    if (ffrspellbinding == true)
                    {
                        if (ffrwmelement == 0) { ffrwmaccuracy -= 3; }
                        else { ffrwmaccuracy += 2; }
                    }
                    // write - al3 WhiteSpell.txt Effect: Slow
                    ffrwmTypeByte = 4;
                }
                else if (ffrwmdebuff == 7)
                {
                    if (ffrspellbinding == true)
                    {
                        if (ffrsplevel > 6) { ffrwmstrength = 40 + (whiteDice.Next(0, 4) * 8); ffrwmtier = 4; }
                        else
                        {
                            ffrwmstrength = (int)(ffrsplevel + 1) * 5;
                            if (ffrwmstrength < 40) { ffrwmtier = 3; }
                            if (ffrwmstrength < 30) { ffrwmtier = 2; }
                            if (ffrwmstrength < 20) { ffrwmtier = 1; }
                        }
                    }
                    // write - al3 WhiteSpell.txt Effect: Fear
                    ffrwmTypeByte = 5;
                }
                else if (ffrwmdebuff == 8)
                {
                    if (ffrspellbinding == true)
                    {
                        ffrwmaccuracy += 2;
                        if (ffrsplevel == 8) { ffrwmstrength = 160 + (whiteDice.Next(0, 3) * 40); ffrwmtier = 4; }
                        else { ffrwmstrength = (int)ffrsplevel * 20; }
                        if (ffrwmstrength < 160) { ffrwmtier = 3; }
                        if (ffrwmstrength < 100) { ffrwmtier = 2; }
                        if (ffrwmstrength < 60) { ffrwmtier = 1; }
                    }
                    // write - al3 WhiteSpell.txt Effect: Locked
                    ffrwmTypeByte = 12;
                }
                else if (ffrwmdebuff == 9)
                {
                    if (ffrwmelement == 2) { ffrwmelement = 0; }
                    if (ffrspellbinding == true)
                    {
                        if (ffrsplevel < 8) { ffrwmaccuracy++; }
                        if (ffrwmelement != 0) { ffrwmaccuracy += 2; }
                    }
                    // write - al3 WhiteSpell.txt Effect: Stripped
                    ffrwmTypeByte = 15;
                }
                #region PowerWords
                // Power Word Processing
                else if (ffrwmdebuff == 10)
                {
                    ffrwmTypeByte = 16;
                    ffrwmaccskip = true;
                wmpword:
                    if ((ffrspellbinding == true) && (ffrsplevel < 6)) { colors.echo(13, $"Debug: Bounced a Power Word before Level 6"); goto whitemagic; }
                    if (ffrwmafflict < 2)
                    {
                        if (whiteDice.Next(1, 21) >= whiteDice.Next(1, 81))
                        {
                            colors.echo(8, $"Debug: FROG passed its saving roll!");
                            //write - al3 WhiteSpell.txt Effect: Power Word "Pacify"
                            //write - al4 WhiteSpell.txt Element: Creature // Status+Poison+Earth
                            //write - al5 WhiteSpell.txt Target: Single Enemy
                            ffrwmEffByte = 1;
                            ffrwmElemByte = 131;
                            ffrwmTargByte = 3;
                            ffrwmspecial = true;
                        }
                        else { colors.echo(13, $"Debug: FROG failed... Making a new spell."); goto whitemagic; }
                    }
                    else if (ffrwmafflict == 2)
                    {
                        if (whiteDice.Next(1, 21) >= whiteDice.Next(1, 81))
                        {
                            colors.echo(8, $"Debug: CAST passed its saving roll!");
                            //write - al3 WhiteSpell.txt Effect: Power Word "Preserve"
                            //write - al4 WhiteSpell.txt Element: Stone
                            //write - al5 WhiteSpell.txt Target: Single Ally
                            ffrwmEffByte = 2;
                            ffrwmElemByte = 2;
                            ffrwmTargByte = 1;
                            ffrwmspecial = true;
                        }
                        else { colors.echo(13, $"Debug: CAST failed... Making a new spell."); goto whitemagic; }
                    }
                    else if (ffrwmafflict == 3) { colors.echo(13, $"Debug: Rerolling a blank Power Word"); ffrwmafflict = whiteDice.Next(1, 9); goto wmpword; }
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
                    colors.echo(4, $"Value {ffrwmdebuff} out of range for Debuffs");return "Spell Failed";
                }
                #region Elements
                // Assigns an Element to the Debuff
                // Usually picks Status
                // Makes sure CAMP, FROG, and CAST don't get overwritten
                if ((ffrwmdebuff == 3) && (ffrwmelement == 2) && (ffrwmallies == 3))
                {
                    // write - a14 WhiteSpell.txt Element: Fire
                    // write - al5 WhiteSpell.txt Target: All Allies
                    ffrwmEffByte = 32;
                    ffrwmElemByte = 16;
                    ffrwmTargByte = 2;
                    ffrwmspecial = true;
                }
                else if ((ffrwmdebuff == 10) && (ffrwmafflict < 3)) { colors.echo(13, $"Debug: Element already set by Power Word"); }
                else
                {
                    if (ffrwmelement == 1)
                    {
                        //write - a14 WhiteSpell.txt Element: Status
                        ffrwmElemByte = 1;
                    }
                    else if (ffrwmelement == 3)
                    {
                        if (ffrspellbinding == true) { ffrwmaccuracy--; }
                        //write - a14 WhiteSpell.txt Element: Time
                        ffrwmElemByte = 4;
                    }
                    else if (ffrwmelement == 4)
                    {
                        if (ffrspellbinding == true) { ffrwmaccuracy += 2; }
                        //write - a14 WhiteSpell.txt Element: Earth
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
                        ffrwmTypeByte = 7;
                        if (ffrspellbinding == true)
                        {
                            if ((ffrsplevel > 6) && (ffrwmafflict > 2)) { ffrwmafflict = 0; }
                            while (ffrwmafflict == 5) { ffrwmafflict = whiteDice.Next(3, 9); }
                            if (ffrwmafflict == 8) { ffrwmafflict = 3; }
                            if (Enumerable.Range(3, 5).Contains(ffrwmafflict)) { ffrwmEffByte += 16; }
                        }
                        if (ffrwmafflict == 0)
                        {
                            if (ffrspellbinding == true)
                            {
                                if (ffrsplevel < 5) { colors.echo(13, $"Debug: Bounced CLER before Level 5"); goto whitemagic; }
                                else if (Enumerable.Range(5, 2).Contains((int)ffrsplevel)) { ffrwmallies = 2; }
                                else if (ffrsplevel > 6) { ffrwmallies = 3; }
                            }
                            // write - a13 WhiteSpell.txt Effect: Refresh
                            ffrwmEffByte = 252; // "Refresh" excludes Death and Stone
                        }
                        // Life must pass a 25% check
                        else if (ffrwmafflict == 1)
                        {
                            if ((ffrspflags.IndexOf("h") == -1) && ((ffrspellbinding == false) || (ffrsplevel < 3)))
                            {
                                if (whiteDice.Next(1, 5) == 4)
                                {
                                    //write - a13 WhiteSpell.txt Effect: Revive
                                    ffrwmEffByte = 1;
                                    ffrwmallies = 2;
                                }
                                else { colors.echo(13, $"Debug: Bounced at LIFE({whiteDice} vs 4)"); goto whitemagic; }
                            }
                            else
                            {
                                // write - al3 WhiteSpell.txt Effect: Revive
                                if ((ffrsplevel > 6) && (ffrwmallies == 3)) { ffrwmEffByte = 3; }
                                else
                                {
                                    ffrwmallies = 2;
                                    ffrwmEffByte = 1;
                                }
                            }
                        }
                        // Soft must win a coinflip
                        else if (ffrwmafflict == 2)
                        {
                            if (ffrwmallies == 1) { ffrwmallies += whiteDice.Next(1, 3); }
                            if ((ffrspflags.IndexOf("h") == -1) && ((ffrspellbinding == false) || (ffrsplevel < 3)))
                            {
                                if (whiteDice.Next(0, 2) == 1)
                                {
                                    //write - al3 WhiteSpell.txt Effect: Soften
                                    ffrwmEffByte = 2;
                                    ffrwmallies = 2;
                                }
                                else { colors.echo(13, $"Debug: Bounced at SOFT({whiteDice} vs 1)"); goto whitemagic; }
                            }
                            else
                            {
                                // write - al3 WhiteSpell.txt Effect: Soften
                                if (ffrsplevel < 7) { ffrwmallies = 2; }
                            }
                        }
                        else if (ffrwmafflict == 3)
                        {
                            if (ffrspellbinding == true)
                            {
                                if (ffrsplevel < 2) { colors.echo(13, $"Debug: Bounced PURE at Level 1"); goto whitemagic; }
                                else if (ffrsplevel < 5) { ffrwmallies = 2; }
                                else if (ffrsplevel > 4) { ffrwmallies = 3; }
                                ffrwmEffByte += 128;
                            }
                            // write - al3 WhiteSpell.txt Effect: Antidote
                            ffrwmEffByte += 4;
                        }
                        else if (ffrwmafflict == 4)
                        {
                            if (ffrspellbinding == true)
                            {
                                if (ffrsplevel == 1) { ffrwmallies = 2; }
                                else if (ffrsplevel > 1) { ffrwmallies = 3; }
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
                            if (ffrspellbinding == true)
                            {
                                if (ffrsplevel == 1) { ffrwmallies = 2; }
                                else if (ffrsplevel > 1) { ffrwmallies = 3; }
                            }
                            else { ffrwmallies = whiteDice.Next(2, 4); }
                            // write - al3 WhiteSpell.txt Effect: Wake
                            ffrwmEffByte += 32;
                        }
                        else if (ffrwmafflict == 7)
                        {
                            if (ffrspellbinding == true)
                            {
                                if (ffrsplevel < 4) { ffrwmallies = 2; }
                                else if (ffrsplevel < 7) { ffrwmallies = 3; }
                            }
                            else { ffrwmallies = whiteDice.Next(2, 4); }
                            // write - al3 WhiteSpell.txt Effect: Voice
                            ffrwmEffByte += 64;
                        }
                        else if (ffrwmafflict == 8)
                        {
                            // write - al3 WhiteSpell.txt Effect: Clarify
                            ffrwmEffByte = 128;
                            ffrwmallies = whiteDice.Next(2, 4);
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
                        if (ffrspellbinding == true)
                        {
                            if (ffrsplevel > 5) { ffrwmallies = 2; }
                            else { colors.echo(13, $"Debug: Spellbinding rejected CUR4 at Level {ffrsplevel}"); goto whitemagic; }
                            if ((ffrsplevel == 8) && (whiteDice.Next(1, 21) >= whiteDice.Next(1, 81))) { ffrwmallies = 3; }
                        }
                        // write to file: "Effect: Full Restore"
                        ffrwmTypeByte = 13;
                    }
                    #endregion
                }
                else { ffrwmTypeByte = 6; }
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
                    ffrwmTypeByte = 9;
                    if (((ffrspellbinding == true) && (ffrsplevel == 8)) || (ffrwmbuff == 0))
                    {
                        if ((ffrspellbinding == false) && (ffrwmstrength >= 80)) { ffrwmallies = 3; }
                        else if ((ffrspellbinding == true) && (ffrsplevel < 5)) { colors.echo(13, $"Debug: Spellbinding bounced WALL at Level {ffrsplevel}"); goto whitemagic; }
                        else if ((ffrspellbinding == true) && (ffrsplevel == 8) && (whiteDice.Next(1, 21) >= whiteDice.Next(1, 81))) { ffrwmallies = 3; }
                        else { ffrwmallies = 2; }
                        // echo 8 - s Checking for SANC rolls. ( $+ $v1 vs $v2 $+ )
                        if (ffrspwall == false)
                        {
                            if (ffrspresist > 4)
                            {
                                colors.echo(4, $"Resists already capped! Rerolling.");
                                goto whitemagic;
                            }
                            // set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend all
                            ffrspwall = true;
                            ffrspresist++;
                        }
                        // write - a13 WhiteSpell.txt Effect: Resist All
                        ffrwmEffByte = 255;
                        goto walljump;
                    }
                    else if (ffrwmbuff == 1)
                    {
                        ffrwmresist = "Status";
                        if ((((ffrspellbinding == true) && (ffrsplevel < 5)) || ((ffrspellbinding == false) && (ffrwmstrength < 50))))
                        {
                            if ((((ffrspellbinding == true) && (ffrsplevel == 1)) || ((ffrspellbinding == false) && (ffrwmstrength <= 10)))) { ffrwmallies = 2; }
                            if (ffrspantiweak == false)
                            {
                                if (ffrspresist > 4)
                                {
                                    colors.echo(4, $"Resists already capped! Rerolling.");
                                    goto whitemagic;
                                }
                                // else if (ffrspresist == 0) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend hindrance }
                                // else if (ffrspresist < 3) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend weak }
                                // else if (ffrspresist == 4) { "Defend malus" }
                                // else { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend psi }
                                ffrspantiweak = true;
                                ffrspresist++;
                            }
                        }
                        else { ffrwmresmagic = true; }
                    }
                    else if (ffrwmbuff == 2)
                    {
                        ffrwmresist = "Poison / Stone";
                        if ((((ffrspellbinding == true) && (ffrsplevel < 5)) || ((ffrspellbinding == false) && (ffrwmstrength < 50))))
                        {
                            if ((((ffrspellbinding == true) && (ffrsplevel == 1)) || ((ffrspellbinding == false) && (ffrwmstrength <= 10)))) { ffrwmallies = 2; }
                            if (ffrspantibane == false)
                            {
                                if (ffrspresist > 4)
                                {
                                    colors.echo(4, $"Resists already capped! Rerolling.");
                                    goto whitemagic;
                                }
                                // n585 = elseif(% ffrspresist = 0) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend poisonous }
                                // n586 = elseif(% ffrspresist isnum 1 - 3) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend bane }
                                // n587 =            else { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend gas }
                                ffrspantibane = true;
                                ffrspresist++;
                            }
                        }
                        else { ffrwmresdecay = true; }
                    }
                    else if (ffrwmbuff == 3)
                    {
                        ffrwmresist = "Time";
                        if ((((ffrspellbinding == true) && (ffrsplevel < 5)) || ((ffrspellbinding == false) && (ffrwmstrength < 50))))
                        {
                            if ((((ffrspellbinding == true) && (ffrsplevel == 1)) || ((ffrspellbinding == false) && (ffrwmstrength <= 10)))) { ffrwmallies = 2; }
                            if (ffrspantizap == false)
                            {
                                if (ffrspresist > 4)
                                {
                                    colors.echo(4, $"Resists already capped! Rerolling.");
                                    goto whitemagic;
                                }
                                //n603 = elseif(% ffrspresist = 0) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend dimension }
                                //n604 = elseif(% ffrspresist isnum 1 - 3) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend time }
                                //n605 =            else { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend age }
                                ffrspantizap = true;
                                ffrspresist++;
                            }
                        }
                        else { ffrwmresdecay = true; }
                    }
                    else if (ffrwmbuff == 4)
                    {
                        ffrwmresist = "Death";
                        if ((((ffrspellbinding == true) && (ffrsplevel < 5)) || ((ffrspellbinding == false) && (ffrwmstrength < 50))))
                        {
                            if ((((ffrspellbinding == true) && (ffrsplevel == 1)) || ((ffrspellbinding == false) && (ffrwmstrength <= 10)))) { ffrwmallies = 2; }
                            if (ffrspantinecro == false)
                            {
                                if (ffrspresist > 4)
                                {
                                    colors.echo(4, $"Resists already capped! Rerolling.");
                                    goto whitemagic;
                                }
                                //n621 = elseif(% ffrspresist = 0) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend necrotic }
                                //n622 = elseif(% ffrspresist isnum 1 - 3) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend evil }
                                //n623 =            else { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend rub }
                                ffrspantinecro = true;
                                ffrspresist++;
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
                        if ((((ffrspellbinding == true) && (ffrsplevel < 5)) || ((ffrspellbinding == false) && (ffrwmstrength < 50))))
                        {
                            if ((((ffrspellbinding == true) && (ffrsplevel == 1)) || ((ffrspellbinding == false) && (ffrwmstrength <= 10)))) { ffrwmallies = 2; }
                            if (ffrspantifire == false)
                            {
                                if (ffrspresist > 4)
                                {
                                    colors.echo(4, $"Resists already capped! Rerolling.");
                                    goto whitemagic;
                                }
                                //n642 = elseif(% ffrspresist = 0) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend burning }
                                //n643 = elseif(% ffrspresist isnum 1 - 3) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend fire }
                                //n644 =            else { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend hot }
                                ffrspantifire = true;
                                ffrspresist++;
                            }
                        }
                        else { ffrwmresdragon = true; }
                    }
                    else if (ffrwmbuff == 6)
                    {
                        ffrwmresist = "Ice";
                        if ((((ffrspellbinding == true) && (ffrsplevel < 5)) || ((ffrspellbinding == false) && (ffrwmstrength < 50))))
                        {
                            if ((((ffrspellbinding == true) && (ffrsplevel == 1)) || ((ffrspellbinding == false) && (ffrwmstrength <= 10)))) { ffrwmallies = 2; }
                            if (ffrspantiice == false)
                            {
                                if (ffrspresist > 4)
                                {
                                    colors.echo(4, $"Resists already capped! Rerolling.");
                                    goto whitemagic;
                                }
                                //n660 = elseif(% ffrspresist = 0) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend freezing }
                                //n661 = elseif(% ffrspresist isnum 1 - 3) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend cold }
                                //n662 =            else { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend ice }
                                ffrspantiice = true;
                                ffrspresist++;
                            }
                        }
                        else { ffrwmresdragon = true; }
                    }
                    else if (ffrwmbuff == 7)
                    {
                        ffrwmresist = "Lit";
                        if ((((ffrspellbinding == true) && (ffrsplevel < 5)) || ((ffrspellbinding == false) && (ffrwmstrength < 50))))
                        {
                            if ((((ffrspellbinding == true) && (ffrsplevel == 1)) || ((ffrspellbinding == false) && (ffrwmstrength <= 10)))) { ffrwmallies = 2; }
                            if (ffrspantilightning == false)
                            {
                                if (ffrspresist > 4)
                                {
                                    colors.echo(4, $"Resists already capped! Rerolling.");
                                    goto whitemagic;
                                }
                                //n678 = elseif(% ffrspresist = 0) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend lightning }
                                //n679 = elseif(% ffrspresist isnum 1 - 3) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend volt }
                                //n680 =            else { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend ion }
                                ffrspantilightning = true;
                                ffrspresist++;
                            }
                        }
                        else { ffrwmresdragon = true; }
                    }
                    else if (ffrwmbuff == 8)
                    {
                        ffrwmresist = "Earth";
                        if ((((ffrspellbinding == true) && (ffrsplevel < 5)) || ((ffrspellbinding == false) && (ffrwmstrength < 50))))
                        {
                            if ((((ffrspellbinding == true) && (ffrsplevel == 1)) || ((ffrspellbinding == false) && (ffrwmstrength <= 10)))) { ffrwmallies = 2; }
                            if (ffrspantiquake == false)
                            {
                                if (ffrspresist > 4)
                                {
                                    colors.echo(4, $"Resists already capped! Rerolling.");
                                    goto whitemagic;
                                }
                                //n696 = elseif(% ffrspresist = 0) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend tectonic }
                                // n697 = elseif(% ffrspresist isnum 1 - 3) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend land }
                                //n698 =            else { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend geo }
                                ffrspantiquake = true;
                                ffrspresist++;
                            }
                        }
                        else { ffrwmresmagic = true; }
                    }
                    if (ffrwmresmagic == true)
                    {
                        ffrwmresist = "Magic";
                        if (ffrspantimagic == false)
                        {
                            if (ffrspresist > 4)
                            {
                                colors.echo(4, $"Resists already capped! Rerolling.");
                                goto whitemagic;
                            }
                            //n712 = elseif(% ffrspresist = 0) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend mortality }
                            //n713 = elseif(% ffrspresist isnum 1 - 2) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend life }
                            //n714 = elseif(% ffrspresist = 3) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend magic }
                            //n715 =          else { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend bio }
                            ffrspantimagic = true;
                            ffrspresist++;
                        }
                    }
                    else if (ffrwmresdecay == true)
                    {
                        ffrwmresist = "Decay";
                        if (ffrspantitoxin == false)
                        {
                            if (ffrspresist > 4)
                            {
                                colors.echo(4, $"Resists already capped! Rerolling.");
                                goto whitemagic;
                            }
                            //n727 = elseif(% ffrspresist = 0) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend futility }
                            //n728 = elseif(% ffrspresist isnum 1 - 2) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend doom }
                            //n729 = elseif(% ffrspresist = 3) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend waste }
                            //n730 =          else { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend rot }
                            ffrspantitoxin = true;
                            ffrspresist++;
                        }
                    }
                    else if (ffrwmresdragon == true)
                    {
                        ffrwmresist = "Dragon";
                        if (ffrspantidamage == false)
                        {
                            if (ffrspresist > 4)
                            {
                                colors.echo(4, $"Resists already capped! Rerolling.");
                                goto whitemagic;
                            }
                            //n742 = elseif(% ffrspresist = 0) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend elemental }
                            //n743 = elseif(% ffrspresist isnum 1 - 2) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend wyrm }
                            //n744 = elseif(% ffrspresist = 3) { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend spell }
                            //n745 =          else { set % ffrspresmsg $+ $calc(% ffrspresist + 1) Defend wiz }
                            ffrspantidamage = true;
                            ffrspresist++;
                        }
                    }
                    //n750 = write - al3 WhiteSpell.txt Effect: Resist % ffrwmresist
                    switch (ffrwmresist)
                    {
                        case "Status": ffrwmEffByte = 1; break;
                        case "Poison / Stone": ffrwmEffByte = 2; break;
                        case "Time": ffrwmEffByte = 4; break;
                        case "Death": ffrwmEffByte = 8; break;
                        case "Fire": ffrwmEffByte = 16; break;
                        case "Ice": ffrwmEffByte = 32; break;
                        case "Lit": ffrwmEffByte = 64; break;
                        case "Earth": ffrwmEffByte = 128; break;
                        case "Magic": ffrwmEffByte = 137; break;
                        case "Decay": ffrwmEffByte = 14; break;
                        case "Dragon": ffrwmEffByte = 112; break;
                        default: ffrwmEffByte = whiteDice.Next(0, 256); break;
                    }
                }
                #endregion
                else if (ffrwmbuff < 11)
                {
                    // write - al3 WhiteSpell.txt Effect: Absorb Up
                    ffrwmTypeByte = 8;
                }
                else if (ffrwmbuff < 13)
                {
                    //write - al3 WhiteSpell.txt Effect: Evade Up
                    ffrwmTypeByte = 14;
                }
                else if (ffrwmbuff == 13)
                {
                    ffrwmstrskip = true;
                    if (ffrwmallies == 1)
                    {
                        // write - al3 WhiteSpell.txt Effect: Double Hits 
                        ffrwmTypeByte = 10;
                    }
                    else { colors.echo(13, $"Debug: Bounced at HAST(Target was {ffrwmallies} instead of 1)"); goto whitemagic; }
                    // White FAST is only allowed to cast on self
                    // This implementation also makes it pass
                    // a 1-in-3 check to succeed
                }
                else if (ffrwmbuff == 14)
                {
                    ffrwmaccskip = false;
                    if (ffrwmallies == 3) { ffrwmstrength = (int)Math.Floor(ffrwmstrength * 0.15); }
                    else if (ffrwmallies == 2) { ffrwmstrength = (int)Math.Floor(ffrwmstrength * 0.20); }
                    else { ffrwmstrength = (int)Math.Floor(ffrwmstrength * 0.25); }
                    //n769 = write - al3 WhiteSpell.txt Effect: Attack Up
                    ffrwmTypeByte = 11;
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
                    if (ffrspellbinding == true) { ffrwmaccuracy += 2; }
                    // n782 = write - al4 WhiteSpell.txt Element: Stone
                    ffrwmElemByte = 2;
                }
                else if (ffrwmelement == 3)
                {
                    if (ffrspellbinding == true) { ffrwmaccuracy--; }
                    //n786 = write - al4 WhiteSpell.txt Element: Time
                    ffrwmElemByte = 4;
                }
                else if (ffrwmelement == 4)
                {
                    if (ffrspellbinding == true) { ffrwmaccuracy += 2; }
                    //n790 = write - al4 WhiteSpell.txt Element: Earth
                    ffrwmElemByte = 128;
                }
                else if ((ffrwmelement == 0) && (whiteDice.Next(1, 21) >= whiteDice.Next(1, 21)))
                {
                    //n793 = write - al4 WhiteSpell.txt Element: None
                    ffrwmElemByte = 0;
                    if (ffrspellbinding == true) { ffrwmaccuracy++; }
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
                        if (ffrspellbinding == true) { ffrwmaccuracy++; }
                        //n811 = write - al5 WhiteSpell.txt Target: Single Enemy
                        ffrwmTargByte = 3;
                    }
                    else if (ffrwmenemies == 2)
                    {
                        //write - al5 WhiteSpell.txt Target: Enemy Party
                        ffrwmTargByte = 4;
                    }
                    else { colors.echo(4, $"Value {ffrwmenemies} out of range for Enemy Target"); return "Spell Failed"; }
                }
                else if (ffrwmeffect > 3)
                {
                    if (ffrwmallies == 1)
                    {
                        if (ffrwmeffect == 4) { ffrwmallies += whiteDice.Next(1, 3); }
                        else
                        {
                            //    n820 = write - al5 WhiteSpell.txt Target: Caster
                            ffrwmTargByte = 0;
                            #region SelfBuffPower
                            if (ffrspellbinding == true)
                            {
                                if (ffrwmTypeByte == 8)
                                {
                                    if (ffrsplevel == 8) { ffrwmstrength = 64; ffrwmtier = 4; }
                                    if (ffrsplevel < 8) { ffrwmstrength = 32; ffrwmtier = 3; }
                                    if (ffrsplevel < 5) { ffrwmstrength = 24; ffrwmtier = 2; }
                                    if (ffrsplevel < 3) { ffrwmstrength = 16; ffrwmtier = 1; }
                                }
                                else if (ffrwmTypeByte == 14)
                                {
                                    if (ffrsplevel == 8) { ffrwmstrength = 240; ffrwmtier = 4; }
                                    if (ffrsplevel < 8) { ffrwmstrength = 120; ffrwmtier = 3; }
                                    if (ffrsplevel < 5) { ffrwmstrength = 80; ffrwmtier = 2; }
                                    if (ffrsplevel < 3) { ffrwmstrength = 60; ffrwmtier = 1; }
                                }
                                else if (ffrwmTypeByte == 11)
                                {
                                    if (ffrsplevel == 8) { ffrwmstrength = 16; ffrwmtier = 4; }
                                    if (ffrsplevel < 8) { ffrwmstrength = 12; ffrwmtier = 3; }
                                    if (ffrsplevel < 5) { ffrwmstrength = 10; ffrwmtier = 2; }
                                    if (ffrsplevel < 3) { ffrwmstrength = 8; ffrwmtier = 1; }
                                }
                            }
                            else if (ffrwmTypeByte == 14) { ffrwmstrength = ffrwmstrength * 2; }
                        }
                    }
                    #endregion
                    if (ffrwmallies == 2)
                    {
                        if ((ffrwmTypeByte == 8) || (ffrwmTypeByte == 14)) { ffrwmallies++; }
                        else
                        {
                            // write - al5 WhiteSpell.txt Target: Single Ally
                            ffrwmTargByte = 1;
                            #region CUREPower
                            if (ffrwmeffect == 4)
                            {
                                if ((ffrspellbinding == true) || (ffrspflags.IndexOf("h") != -1))
                                {
                                    if (ffrsplevel != 8)
                                    {
                                        ffrwmstrength = (int)Math.Floor(Math.Pow(2, (3.5 + ffrsplevel / 2)) + (ffrsplevel - 1) / 2); // CURE formula, accurate to FF1
                                    }
                                    else { ffrwmstrength = whiteDice.Next(66, 185); }
                                }
                                else { ffrwmstrength = (int)Math.Floor(ffrwmstrength * 1.6); }
                                if (ffrwmstrength > 32) { ffrwmtier = 2; }
                                else { ffrwmtier = 1; }
                                if (ffrwmstrength > 65) { ffrwmtier = 3; }
                            }
                            #endregion
                            #region BLESPower
                            else
                            {
                                if (ffrspellbinding == true)
                                {
                                    ffrwmaccuracy--;
                                    if (ffrsplevel == 8) { ffrwmstrength = 12; ffrwmtier = 4; }
                                    if (ffrsplevel < 8) { ffrwmstrength = 10; ffrwmtier = 3; }
                                    if (ffrsplevel < 5) { ffrwmstrength = 8; ffrwmtier = 2; }
                                    if (ffrsplevel < 3) { ffrwmstrength = 5; ffrwmtier = 1; }
                                }
                            }
                            #endregion
                        }
                    }
                    if (ffrwmallies == 3)
                    {
                        //n872 = write - al5 WhiteSpell.txt Target: All Allies
                        ffrwmTargByte = 2;
                        #region HEALPower
                        if (ffrwmeffect == 4)
                        {
                            if ((ffrspellbinding == true) || (ffrspflags.IndexOf("h") != -1))
                            {
                                if (ffrsplevel != 8) { ffrwmstrength = (int)Math.Floor(6 * Math.Pow(2, (ffrsplevel / 2 - 0.5))); } // HEAL formula, accurate to FF1
                                else { ffrwmstrength = whiteDice.Next(48, 136); }
                            }
                            else { ffrwmstrength -= 4; }
                            if (ffrwmstrength > 23) { ffrwmtier = 2; }
                            else { ffrwmtier = 1; }
                            if (ffrwmstrength > 47) { ffrwmtier = 3; }
                        }
                        #endregion
                        #region PartyBuffPower
                        else if ((ffrwmeffect == 5) && (ffrspellbinding == true))
                        {
                            if (ffrwmTypeByte == 8)
                            {
                                if (ffrsplevel == 8) { ffrwmstrength = 24; ffrwmtier = 4; }
                                if (ffrsplevel < 8) { ffrwmstrength = 16; ffrwmtier = 3; }
                                if (ffrsplevel < 5) { ffrwmstrength = 12; ffrwmtier = 2; }
                                if (ffrsplevel < 3) { ffrwmstrength = 8; ffrwmtier = 1; }
                            }
                            else if (ffrwmTypeByte == 14)
                            {
                                if (ffrsplevel == 8) { ffrwmstrength = 80; ffrwmtier = 4; }
                                if (ffrsplevel < 8) { ffrwmstrength = 60; ffrwmtier = 3; }
                                if (ffrsplevel < 5) { ffrwmstrength = 40; ffrwmtier = 2; }
                                if (ffrsplevel < 3) { ffrwmstrength = 30; ffrwmtier = 1; }
                            }
                            else if (ffrwmTypeByte == 11)
                            {
                                ffrwmaccskip = false;
                                ffrwmaccuracy -= 2;
                                if (ffrsplevel == 8) { ffrwmstrength = 10; ffrwmtier = 4; }
                                if (ffrsplevel < 8) { ffrwmstrength = 8; ffrwmtier = 3; }
                                if (ffrsplevel < 5) { ffrwmstrength = 5; ffrwmtier = 2; }
                                if (ffrsplevel < 3) { ffrwmstrength = 3; ffrwmtier = 1; }
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
            if (ffrspellbinding == true)
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

            // Doesn't open the file when Spellbinding
            // because that would open 64 files
            // Debug: if (% ffrspellbinding = 1) && (% ffrwmbuff = 14) { / run WhiteSpell.txt | halt }
            //  if ($read(WhiteSpell.txt, w, *resist *)) { echo 8 - s Resist ID: % ffrwmbuff }
            //  if (% ffrwmaccsay > 64) { set % ffrspredharm 1 }
            //  if (% ffrnowrite != 1) { / run WhiteSpell.txt | unset % ffr * }

            Console.WriteLine();
            colors.echo(9, $"Type Byte: {ffrwmTypeByte}");
            if (ffrwmEffByte != 0) { colors.echo(9, $"Effect Byte: {ffrwmEffByte}"); }
            colors.echo(9, $"Target Byte: {ffrwmTargByte}");
            colors.echo(9, $"Element Byte: {ffrwmElemByte}");
            if (ffrwmaccskip == false) { colors.echo(9, $"Accuracy Byte: {ffrwmAccByte}"); }

            Console.WriteLine();
            colors.echo(12, $"Magic Level: {ffrsplevel}");
            colors.echo(12, $"Spell Slot: {ffrspslot}");
            colors.echo(12, $"Tier {ffrwmtier} Power");
            string ffrWhiteSpell = $"{ffrwmTypeByte}_{ffrwmEffByte}_{ffrwmTargByte}_{ffrwmElemByte}_{ffrwmAccByte}";

            Console.WriteLine();
            colors.echo(0, $"Returned {ffrWhiteSpell} to ffr-spellbinder");
            return ffrWhiteSpell;
        }
    }
}
