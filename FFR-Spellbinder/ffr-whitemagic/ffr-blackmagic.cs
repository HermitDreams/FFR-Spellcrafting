using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ffr_spellbinder
{
    internal class ffr_blackmagic
    {
        public static string ffrBlackSpell = "";
        public static int ffrbmtier = 0;
        public static string BMag()
        {
            // Final Fantasy Randomizer Black Magic Generator. IRC Script by Linkshot. C# Port.
            var blackDice = new Random();

        // Black Spell Initiation
        blackmagic:
            // write -c BlackSpell.txt <-- Create txt file here
            bool ffrbmstrskip = false;
            bool ffrbmaccskip = false;
            int ffrbmeffect = blackDice.Next(1, 3);
            int ffrbmohko = blackDice.Next(1, 10);
            int ffrbmalter = blackDice.Next(1, 3);
            int ffrbmdebuff = blackDice.Next(1, 13);
            int ffrbmbuff = blackDice.Next(1, 6);
            int ffrbmelement = blackDice.Next(0, 9);
            int ffrbmtarget = blackDice.Next(1, 3);
            int ffrbmafflict = blackDice.Next(1, 9);
            int ffrbmpoison = blackDice.Next(1, 6);
            int ffrbmaccroll = blackDice.Next(0, 256);
            int ffrbmaccmath1 = (int)Math.Pow(2, blackDice.Next(5, 7)) - 1;
            int ffrbmaccmath2 = (int)Math.Pow(2, blackDice.Next(1, 9));
            int ffrbmacclow = Math.Min(ffrbmaccmath1, ffrbmaccmath2);
            int ffrbmacchigh = Math.Max(ffrbmaccmath1, ffrbmaccmath2);
            int ffrbmaccuracy = blackDice.Next(ffrbmacclow, ffrbmacchigh);
            double ffrbmstrength;
            if (Program.ffrspellbinding == true)
            {
                if (Program.ffrsplevel == 8) { ffrbmstrength = 100; }
                else { ffrbmstrength = Program.ffrsplevel * 10; }
            }
            else { ffrbmstrength = blackDice.Next(10, 101); }

            string ffrbmaccsay = "";
            bool ffrbmb2 = false;
            string ffrbmresist = "";
            bool ffrbmDualDenial = true;

            bool ffrbmresmagic = false;
            bool ffrbmresdecay = false;
            bool ffrbmresdragon = false;



            // Debugging
            Console.WriteLine($"ffrbmeffect rolled {ffrbmeffect} of 2");
            Console.WriteLine($"ffrbmohko rolled {ffrbmohko} of 9");
            Console.WriteLine($"ffrbmalter rolled {ffrbmalter} of 2");
            Console.WriteLine($"ffrbmdebuff rolled {ffrbmdebuff} of 12");
            Console.WriteLine($"ffrbmbuff rolled {ffrbmbuff} of 5");
            Console.WriteLine($"ffrbmelement rolled {ffrbmelement} of 8");
            Console.WriteLine($"ffrbmtarget rolled {ffrbmtarget} of 2");
            Console.WriteLine($"ffrbmstrength was set to {ffrbmstrength} of 100");
            Console.WriteLine($"ffrbmafflict rolled {ffrbmafflict} of 9");
            Console.WriteLine($"ffrbmpoison rolled {ffrbmpoison} of 5");
            Console.WriteLine($"ffrbmaccroll rolled {ffrbmaccroll} of 255");
            Console.WriteLine($"ffrbmaccuracy rolled {ffrbmaccuracy} of {ffrbmacclow} to {ffrbmacchigh}");

        bmbase:

            int ffrbmTypeByte = 0;
            int ffrbmEffByte = 0;
            int ffrbmTargByte = 0;
            int ffrbmElemByte = 0;
            int ffrbmAccByte = 0;

            #region AccInit

            // Sets Accuracy to Auto-Hit or Tiered
            if ((ffrbmaccroll < 2) || (ffrbmaccroll == 148) || (ffrbmaccsay == "Auto-Hit"))
            {
                if ((ffrbmeffect == 1) && (ffrbmohko < 4)) { colors.echo(11, $"Debug: Rejected Auto-Hit on Insta-Kill"); goto blackmagic; }
                else { ffrbmaccsay = "Auto-Hit"; }
                // if (L isincs % Program.ffrspflags) { write - i SpellLog.txt Accuracy Change: 3 / 256 succeeded.Spell is slated to get 255 Hit Bonus. | write - i SpellLog.txt - }
            }
            else if (Program.ffrspellbinding == true)
            {
                if (Program.ffrsplevel < 5) { ffrbmaccuracy = 5; ffrbmtier = 2; }
                if (Program.ffrsplevel > 4) { ffrbmaccuracy = 7; ffrbmtier = 3; }
                if (Program.ffrsplevel == 1) { ffrbmaccuracy = 4; ffrbmtier = 1; }
                if (Program.ffrsplevel == 8) { ffrbmaccuracy = 9; ffrbmtier = 4; }
                // if (L isincs % Program.ffrspflags) { write - i SpellLog.txt Accuracy Change: Spellbinder has set the Accuracy Tier to % ffrbmaccuracy to match Level % Program.ffrsplevel | write - i SpellLog.txt - }
            }
            else
            {
                ffrbmaccsay = ffrbmaccuracy.ToString();
                // if (L isincs % Program.ffrspflags) { write - i SpellLog.txt Accuracy Output has been set to % ffrbmaccsay | write - i SpellLog.txt - }
            }

            #endregion AccInit
            #region SlotPerms
            // Spellbinding Slot Permissions
            #region LOK2Fix
            // Compatibility with LOK2 fix
            if ((Program.ffrspflags.IndexOf("S") != -1) && (Program.ffrsplevel == 3) && (Program.ffrspslot == 4) && (Program.ffrspreroll < 5))
            {
                ffrbmeffect = 2;
                ffrbmalter = 1;
                ffrbmdebuff = 10;
            }
            #endregion LOK2Fix
            #region KeepDmg
            // -b2: Preserves all slots with damage spells
            if ((Program.ffrspflags.IndexOf("b2") != -1) && (Program.ffrspreroll < 5))
            {
                if (((Program.ffrsplevel != 4) && (Program.ffrspslot == 1)) || ((Program.ffrsplevel == 1) && (Program.ffrspslot == 4)) || ((Program.ffrsplevel == 3) && (Program.ffrspslot == 3)) || ((Program.ffrsplevel == 4) && (Program.ffrspslot == 4)))
                {
                    ffrbmeffect = 1;
                    ffrbmohko = blackDice.Next(4, 9);
                    ffrbmb2 = true;
                }
            }
            #endregion KeepDmg
            #region ConfuseBad
            if ((Program.ffrspflags.IndexOf("C") != -1) && (Program.ffrsplevel == 1) && (Program.ffrspslot == 1) && (ffrbmeffect == 2) && (ffrbmalter == 2) && (Program.ffrspreroll < 5))
            {
                colors.echo(11, $"Debug: Bounced Buff {ffrbmbuff} from FIRE slot");
                goto blackmagic;
            }
            #endregion ConfuseBad
            #endregion SlotPerms

            #region BaseEffect
            // Spell-building
            #region ElemAssign
            if (ffrbmelement != 0) { ffrbmElemByte = (int)(Math.Pow(2, (ffrbmelement - 1))); }
            // 0: Non-Elemental
            // 1: Status
            // 2: Poison / Stone
            // 3: Time
            // 4: Death
            // 5: Fire
            // 6: Ice
            // 7: Lightning
            // 8: Earth
            #endregion ElemAssign
            #region Violence
            // Black Magic is half Offensive Spells
            // The other half is Stat Alterations

            // 3 in 8 Offensive Spells are Insta-Kills
            // They have a lowered chance to multi-target
            // However, Damage Spells have a boosted chance
            if (ffrbmeffect == 1)
            {
                if ((ffrbmohko < 4) && ((Program.ffrspflags.IndexOf("k") == -1) || (Program.ffrsplevel > 4)))
                {
                    if (Program.ffrspellbinding == true) { ffrbmaccuracy -= 2; }
                    ffrbmstrskip = true;
                    ffrbmtarget -= blackDice.Next(0, 2);
                    // write - al2 BlackSpell.txt Type: Insta-Kill
                    ffrbmTypeByte = 3;
                    ffrbmEffByte = 1;
                }
                else {
                    ffrbmTypeByte = 1;
                    ffrbmtarget += blackDice.Next(0, 2);
                    if (Program.ffrspellbinding == true) {
                        if (Program.ffrsplevel < 3) { ffrbmaccuracy = 3; }
                        else if (Program.ffrsplevel < 8) { ffrbmaccuracy = 5; }
                    }
                    else { if (ffrbmtarget == 1) { ffrbmstrength = ffrbmstrength * blackDice.Next(100, 151) / 100; } }
                    if (Enumerable.Range(30, 20).Contains((int)ffrbmstrength)) { ffrbmtier = 2; }
                    else if (Enumerable.Range(50, 50).Contains((int)ffrbmstrength)) { ffrbmtier = 3; }
                    else if (ffrbmstrength >= 100) { ffrbmtier = 4; }
                    else { ffrbmtier = 1; }
                }
            }
            #endregion Violence
            #region Alterations
            // Decides whether the Alteration is a buff or debuff
            // Buffs are rarer than Debuffs
            if (ffrbmeffect == 2)
            {
                if (ffrbmalter == 2)
                {
                    if (blackDice.Next(0, 2) == 0)
                    {
                        ffrbmalter--;
                        colors.echo(11, $"Debug: Shifting Buff to Debuff");
                        goto bmbase;
                    }
                    else { colors.echo(7, $"Debug: Buff passed saving roll"); }
                }
                #region Debuffs
                if (ffrbmalter == 1)
                {
                    if ((Program.ffrspellbinding == true) && (ffrbmaccsay == "Auto-Hit") && (ffrbmdebuff < 8)) { ffrbmdebuff = 12; }
                    // Debug: echo 8 - s Debug: Passing through Debuff(Debuff: % ffrbmdebuff $+ , Element: % ffrbmelement $+ , Affliction: % ffrbmafflict $+ , Allies: % ffrbmallies $+ )
                    if (!Enumerable.Range(9, 2).Contains(ffrbmdebuff)) { ffrbmstrskip = true; }
                    if (ffrbmdebuff < 8) { ffrbmTypeByte = 3; }
                    #region Poison
                    if (ffrbmdebuff == 1)
                    {
                        if (Program.ffrspellbinding == true)
                        {
                            ffrbmaccuracy += 2;
                            if (ffrbmpoison > 2) { ffrbmaccuracy++; }
                            if ((ffrbmpoison == 3) && (Program.ffrsplevel > 1)) { ffrbmaccuracy += 2; }
                            if (ffrbmpoison > 3) { ffrbmaccuracy++; }
                            if (ffrbmpoison > 4) { ffrbmaccuracy += 2; }
                        }
                        ffrbmEffByte = (int)(4 + Math.Pow(2, (ffrbmpoison + 2)));
                        // Poison Debuff is always stacked on top
                        // So that the spell is useful to players
                        switch (ffrbmelement)
                        {
                            case 0: break;
                            case 3: break;
                            case 4: break;
                            case 5: break;
                            default: ffrbmElemByte = 2; break;
                        }
                        // Poisons come in Time, Death, and Fire flavours
                        // But mostly Poison
                    }
                    #endregion Poison
                    #region Dark
                    else if (ffrbmdebuff == 2)
                    {
                        switch (ffrbmelement)
                        {
                            case 0: break;
                            case 2: break;
                            case 3: break;
                            case 5: break;
                            case 8: break;
                            default: ffrbmElemByte = 1; break;
                        }
                        ffrbmEffByte = 8;
                    }
                    #endregion Dark
                    #region Stun
                    else if (ffrbmdebuff == 3)
                    {
                        if (Program.ffrspellbinding == true) { ffrbmaccuracy += 3; }
                        switch (ffrbmelement)
                        {
                            case 0: if (Program.ffrspellbinding == true) ffrbmaccuracy -= 8; break; // PIN that could hit Chaos at the same rate as XFER was Bad, Actually
                            case 3: if (Program.ffrspellbinding == true) ffrbmaccuracy -= 6; break; // adjusts STOP to vanilla acc
                            case 6: break;
                            case 7: break;
                            default: ffrbmElemByte = 1; break;
                        }
                        // write - al3 BlackSpell.txt Effect: Stun
                        ffrbmEffByte = 16;
                    }
                    #endregion Stun
                    #region Sleep
                    else if (ffrbmdebuff == 4)
                    {
                        if (Program.ffrspellbinding == true)
                        {
                            ffrbmaccuracy++;
                            if (Program.ffrsplevel > 1) { ffrbmaccuracy += 2; }
                        }
                        switch (ffrbmelement)
                        {
                            case 1: break;
                            case 3: break;
                            case 5: break;
                            case 6: break;
                            default: ffrbmElemByte = 0; break;
                        }
                        // write - al3 BlackSpell.txt Effect: Sleep
                        ffrbmEffByte = 32;
                    }
                    #endregion Sleep
                    #region Mute
                    else if (ffrbmdebuff == 5)
                    {
                        if (Program.ffrspellbinding == true) { ffrbmaccuracy += 2; }
                        switch (ffrbmelement)
                        {
                            case 0: break;
                            case 2: break;
                            case 3: break;
                            case 6: break;
                            default: ffrbmElemByte = 1; break;
                        }
                        // write - al3 BlackSpell.txt Effect: Mute
                        ffrbmEffByte = 64;
                    }
                    #endregion Mute
                    #region Confuse
                    else if (ffrbmdebuff == 6)
                    {
                        if (Program.ffrspellbinding == true) { ffrbmaccuracy += 4; }
                        switch (ffrbmelement)
                        {
                            case 0: break;
                            case 3: break;
                            case 5: break;
                            case 7: break;
                            default: ffrbmElemByte = 1; break;
                        }
                        // write - al3 BlackSpell.txt Effect: Confuse
                        ffrbmEffByte = 128;
                    }
                    #endregion Confuse
                    #region Cocktail
                    else if (ffrbmdebuff == 7)
                    {
                        if (Program.ffrspellbinding == true) { ffrbmaccuracy -= 2; }
                        switch (ffrbmelement)
                        {
                            case 0: break;
                            case 3: break;
                            case 4: break;
                            case 5: break;
                            case 6: break;
                            case 7: break;
                            case 8: break;
                            default: ffrbmElemByte = 1; break;
                        }
                        // write - al3 BlackSpell.txt Effect: Cocktail
                        ffrbmEffByte = 120;
                    }
                    #endregion Cocktail

                    #region Slow
                    else if (ffrbmdebuff == 8)
                    {
                        if (Program.ffrspellbinding == true) { ffrbmaccuracy += 4; }
                        switch (ffrbmelement)
                        {
                            case 0: if (Program.ffrspellbinding == true) ffrbmaccuracy -= 3; break;
                            case 1: break;
                            case 2: break;
                            case 4: break;
                            case 6: break;
                            default: ffrbmElemByte = 4; break;
                        }
                        ffrbmTypeByte = 4;
                    }
                    #endregion Slow
                    #region Fear
                    else if (ffrbmdebuff == 9)
                    {
                        if (Program.ffrspellbinding == true)
                        {
                            ffrbmaccuracy -= 2;
                            if (Program.ffrsplevel > 6) { ffrbmstrength = 40 + (blackDice.Next(0, 4) * 8); ffrbmtier = 4; }
                            else
                            {
                                ffrbmstrength = (int)(Program.ffrsplevel + 1) * 5;
                                if (ffrbmstrength < 40) { ffrbmtier = 3; }
                                if (ffrbmstrength < 30) { ffrbmtier = 2; }
                                if (ffrbmstrength < 20) { ffrbmtier = 1; }
                            }
                        }
                        switch (ffrbmelement)
                        {
                            case 1: break;
                            case 4: ffrbmstrength = (int)Math.Floor(ffrbmstrength * 1.2); break;
                            case 5: break;
                            case 8: break;
                            default: ffrbmElemByte = 0; break;
                        }
                        // write - al3 BlackSpell.txt Effect: Fear
                        ffrbmTypeByte = 5;
                    }
                    #endregion Fear
                    #region Locked
                    else if (ffrbmdebuff == 10)
                    {
                        if (Program.ffrspellbinding == true)
                        {
                            ffrbmaccuracy += 4;
                            if (Program.ffrsplevel == 8) { ffrbmstrength = 160 + (blackDice.Next(0, 3) * 40); ffrbmtier = 4; }
                            else { ffrbmstrength = (int)Program.ffrsplevel * 20; }
                            if (ffrbmstrength < 160) { ffrbmtier = 3; }
                            if (ffrbmstrength < 100) { ffrbmtier = 2; }
                            if (ffrbmstrength < 60) { ffrbmtier = 1; }
                        }
                        switch (ffrbmelement)
                        {
                            case 3: break;
                            case 6: break;
                            case 7: break;
                            case 8: break;
                            default: ffrbmElemByte = 0; break;
                        }
                        // write - al3 BlackSpell.txt Effect: Locked
                        ffrbmTypeByte = 12;
                    }
                    #endregion Locked
                    #region Dispel
                    else if (ffrbmdebuff == 11)
                    {
                        if ((Program.ffrspellbinding == true) && (Program.ffrsplevel < 8)) { ffrbmaccuracy++; }
                        switch (ffrbmelement)
                        {
                            case 1: break;
                            case 3: break;
                            case 4: break;
                            case 7: break;
                            default: if (Program.ffrspellbinding == true) ffrbmaccuracy -= 2; ffrbmElemByte = 0; break;
                        }
                        // write - al3 BlackSpell.txt Effect: Dispel
                        ffrbmTypeByte = 15;
                    }
                    #endregion Dispel

                    #region PowerWords
                    // Power Word Processing
                    else if (ffrbmdebuff == 12)
                    {
                        ffrbmTypeByte = 16;
                        ffrbmTargByte = 3;
                        ffrbmaccskip = true;
                        if ((Program.ffrspellbinding == true) && (Program.ffrsplevel < 6)) { colors.echo(13, $"Debug: Bounced a Power Word before Level 6"); goto blackmagic; }
                        if (ffrbmafflict == 9) // Power Word "Weaken"
                        {
                            switch (ffrbmelement)
                            {
                                case 0: if (blackDice.Next(1, 6) < 5) ffrbmElemByte = 1; break;
                                case 3: if (blackDice.Next(1, 5) < 4) ffrbmElemByte = 1; break;
                                case 4: break;
                                default: ffrbmElemByte = 1; break;
                            }
                            ffrbmEffByte = 120;
                        }
                        else if (ffrbmafflict == 1) // Power Word "Kill"
                        {
                            switch (ffrbmelement)
                            {
                                case 0: if (blackDice.Next(1, 11) < 10) ffrbmElemByte = 8; break;
                                case 2: break;
                                case 3: if (blackDice.Next(1, 6) < 5) ffrbmElemByte = 8; break;
                                case 8: break;
                                default: ffrbmElemByte = 8; break;
                            }
                            ffrbmEffByte = 1;
                        }
                        else if (ffrbmafflict == 2) // Power Word "Shatter"
                        {
                            switch (ffrbmelement)
                            {
                                case 0: if (blackDice.Next(1, 11) < 10) ffrbmElemByte = 2; break;
                                case 6: break;
                                default: ffrbmElemByte = 2; break;
                            }
                            ffrbmEffByte = 2;
                        }
                        else if ((ffrbmafflict == 3) || (ffrbmafflict == 8)) // Power Word "Decay"
                        {
                            switch (ffrbmelement)
                            {
                                case 0: if (blackDice.Next(1, 5) < 4) ffrbmElemByte = 2; break;
                                case 3: if (blackDice.Next(1, 3) < 2) ffrbmElemByte = 2; break;
                                case 4: break;
                                case 5: break;
                                default: ffrbmElemByte = 2; break;
                            }
                            ffrbmEffByte = 132;
                        }
                        else if (ffrbmafflict == 4) // Power Word "Blind"
                        {
                            switch (ffrbmelement)
                            {
                                case 0: break;
                                case 5: break;
                                case 7: break;
                                default: ffrbmElemByte = 1; break;
                            }
                            ffrbmEffByte = 8;
                        }
                        else if (ffrbmafflict == 5) // Power Word "Stun"
                        {
                            switch (ffrbmelement)
                            {
                                case 0: if (blackDice.Next(1, 5) < 4) ffrbmElemByte = 1; break;
                                case 3: if (blackDice.Next(1, 3) < 2) ffrbmElemByte = 1; break;
                                case 6: break;
                                case 7: break;
                                default: ffrbmElemByte = 1; break;
                            }
                            ffrbmEffByte = 16;
                        }
                        else if (ffrbmafflict < 8) // Power Word "Coma"
                        {
                            switch (ffrbmelement)
                            {
                                case 0: break;
                                case 5: break;
                                case 6: break;
                                default: ffrbmElemByte = 1; break;
                            }
                            ffrbmEffByte = 96;
                        }
                        else
                        {
                            colors.echo(4, $"Value {ffrbmafflict} out of range for Power Words"); return "Spell Failed";
                        }
                    }
                    #endregion PowerWords
                    else
                    {
                        colors.echo(4, $"Value {ffrbmdebuff} out of range for Debuffs"); return "Spell Failed";
                    }
                }
                #endregion Debuffs
                #region Buffs
                // Buffs
                // Black Magic isn't known for being helpful
                // All Buffs have initial power cut in half
                if (ffrbmalter == 2)
                {
                    ffrbmstrength = (int)Math.Floor(ffrbmstrength / 2);
                    ffrbmaccskip = true;
                    ffrbmElemByte = 0;
                    if (ffrbmbuff == 1) // "Absorb Up"
                    {
                        ffrbmtarget = 2;
                        ffrbmTypeByte = 8;
                        if (Program.ffrspellbinding == true)
                        {
                            if (Program.ffrsplevel == 8) { ffrbmstrength = 24; ffrbmtier = 4; }
                            if (Program.ffrsplevel < 8) { ffrbmstrength = 16; ffrbmtier = 3; }
                            if (Program.ffrsplevel < 5) { ffrbmstrength = 12; ffrbmtier = 2; }
                            if (Program.ffrsplevel < 3) { ffrbmstrength = 8; ffrbmtier = 1; }
                        }
                        else { ffrbmstrength = (int)Math.Ceiling(ffrbmstrength * blackDice.Next(8, 12) / 10); }
                    }
                    #region Resists                    
                    else if (ffrbmbuff == 2)
                    {
                        ffrbmstrskip = true;
                        ffrbmTypeByte = 9;
                        if (((Program.ffrspellbinding == true) && (Enumerable.Range(2, 3).Contains((int)Program.ffrsplevel))) || ((Program.ffrspellbinding == false) && (Enumerable.Range(10, 15).Contains((int)ffrbmstrength)))) { ffrbmtarget = 2; }
                        else { ffrbmtarget = 1; }
                        if (((Program.ffrspellbinding == true) && (Program.ffrsplevel == 8)) || ((Program.ffrspellbinding == false) && (blackDice.Next(1, 9) == 8)))
                        {
                            if (Program.ffrspwall == false)
                            {
                                if (Program.ffrspresist > 4)
                                {
                                    colors.echo(4, $"Resists already capped! Rerolling.");
                                    goto blackmagic;
                                }
                                // set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend all
                                Program.ffrspwall = true;
                                Program.ffrspresist++;
                            }
                            // write - a13 BlackSpell.txt Effect: Resist All
                            ffrbmEffByte = 255;
                            goto barrskip;
                        }
                        else if (ffrbmelement == 1)
                        {
                            ffrbmresist = "Status";
                            if (((Program.ffrspellbinding == true) && (Program.ffrsplevel > 4)) || ((Program.ffrspellbinding == false) && (ffrbmstrength > 24))) { ffrbmresmagic = true; }
                            else
                            {
                                if (Program.ffrspantiweak == false)
                                {
                                    if (Program.ffrspresist > 4)
                                    {
                                        colors.echo(4, $"Resists already capped! Rerolling.");
                                        goto blackmagic;
                                    }
                                    // else if (Program.ffrspresist == 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend hindrance }
                                    // else if (Program.ffrspresist < 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend weak }
                                    // else if (Program.ffrspresist == 4) { "Defend malus" }
                                    // else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend psi }
                                    Program.ffrspantiweak = true;
                                    Program.ffrspresist++;
                                }
                            }
                        }
                        else if (ffrbmelement == 2)
                        {
                            ffrbmresist = "Poison/Stone";
                            if (((Program.ffrspellbinding == true) && (Program.ffrsplevel > 4)) || ((Program.ffrspellbinding == false) && (ffrbmstrength > 24))) { ffrbmresdecay = true; }
                            else
                            {
                                if (Program.ffrspantibane == false)
                                {
                                    if (Program.ffrspresist > 4)
                                    {
                                        colors.echo(4, $"Resists already capped! Rerolling.");
                                        goto blackmagic;
                                    }
                                    // n585 = elseif(% Program.ffrspresist = 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend poisonous }
                                    // n586 = elseif(% Program.ffrspresist isnum 1 - 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend bane }
                                    // n587 =            else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend gas }
                                    Program.ffrspantibane = true;
                                    Program.ffrspresist++;
                                }
                            }
                        }
                        else if (ffrbmelement == 3)
                        {
                            ffrbmresist = "Time";
                            if (((Program.ffrspellbinding == true) && (Program.ffrsplevel > 4)) || ((Program.ffrspellbinding == false) && (ffrbmstrength > 24))) { ffrbmresdecay = true; }
                            else
                            {
                                if (Program.ffrspantizap == false)
                                {
                                    if (Program.ffrspresist > 4)
                                    {
                                        colors.echo(4, $"Resists already capped! Rerolling.");
                                        goto blackmagic;
                                    }
                                    //n603 = elseif(% Program.ffrspresist = 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend dimension }
                                    //n604 = elseif(% Program.ffrspresist isnum 1 - 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend time }
                                    //n605 =            else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend age }
                                    Program.ffrspantizap = true;
                                    Program.ffrspresist++;
                                }
                            }
                        }
                        else if (ffrbmelement == 4)
                        {
                            ffrbmresist = "Death";
                            if (((Program.ffrspellbinding == true) && (Program.ffrsplevel > 4)) || ((Program.ffrspellbinding == false) && (ffrbmstrength > 24)))
                            {
                                if (ffrbmdebuff < 7) { ffrbmresmagic = true; }
                                else { ffrbmresdecay = true; }
                            }
                            else
                            {
                                if (Program.ffrspantinecro == false)
                                {
                                    if (Program.ffrspresist > 4)
                                    {
                                        colors.echo(4, $"Resists already capped! Rerolling.");
                                        goto blackmagic;
                                    }
                                    //n621 = elseif(% Program.ffrspresist = 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend necrotic }
                                    //n622 = elseif(% Program.ffrspresist isnum 1 - 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend evil }
                                    //n623 =            else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend rub }
                                    Program.ffrspantinecro = true;
                                    Program.ffrspresist++;
                                }
                            }
                        }
                        else if (ffrbmelement == 5)
                        {
                            ffrbmresist = "Fire";
                            if (((Program.ffrspellbinding == true) && (Program.ffrsplevel > 4)) || ((Program.ffrspellbinding == false) && (ffrbmstrength > 24))) { ffrbmresdragon = true; }
                            else
                            {
                                if (Program.ffrspantifire == false)
                                {
                                    if (Program.ffrspresist > 4)
                                    {
                                        colors.echo(4, $"Resists already capped! Rerolling.");
                                        goto blackmagic;
                                    }
                                    //n642 = elseif(% Program.ffrspresist = 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend burning }
                                    //n643 = elseif(% Program.ffrspresist isnum 1 - 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend fire }
                                    //n644 =            else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend hot }
                                    Program.ffrspantifire = true;
                                    Program.ffrspresist++;
                                }
                            }
                        }
                        else if (ffrbmelement == 6)
                        {
                            ffrbmresist = "Ice";
                            if (((Program.ffrspellbinding == true) && (Program.ffrsplevel > 4)) || ((Program.ffrspellbinding == false) && (ffrbmstrength > 24))) { ffrbmresdragon = true; }
                            else
                            {
                                if (Program.ffrspantiice == false)
                                {
                                    if (Program.ffrspresist > 4)
                                    {
                                        colors.echo(4, $"Resists already capped! Rerolling.");
                                        goto blackmagic;
                                    }
                                    //n660 = elseif(% Program.ffrspresist = 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend freezing }
                                    //n661 = elseif(% Program.ffrspresist isnum 1 - 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend cold }
                                    //n662 =            else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend ice }
                                    Program.ffrspantiice = true;
                                    Program.ffrspresist++;
                                }
                            }
                        }
                        else if (ffrbmelement == 7)
                        {
                            ffrbmresist = "Lit";
                            if (((Program.ffrspellbinding == true) && (Program.ffrsplevel > 4)) || ((Program.ffrspellbinding == false) && (ffrbmstrength > 24))) { ffrbmresdragon = true; }
                            else
                            {
                                if (Program.ffrspantilightning == false)
                                {
                                    if (Program.ffrspresist > 4)
                                    {
                                        colors.echo(4, $"Resists already capped! Rerolling.");
                                        goto blackmagic;
                                    }
                                    //n678 = elseif(% Program.ffrspresist = 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend lightning }
                                    //n679 = elseif(% Program.ffrspresist isnum 1 - 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend volt }
                                    //n680 =            else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend ion }
                                    Program.ffrspantilightning = true;
                                    Program.ffrspresist++;
                                }
                            }
                        }
                        else if (ffrbmelement == 8)
                        {
                            ffrbmresist = "Earth";
                            if (((Program.ffrspellbinding == true) && (Program.ffrsplevel > 4)) || ((Program.ffrspellbinding == false) && (ffrbmstrength > 24))) { ffrbmresmagic = true; }
                            else
                            {
                                if (Program.ffrspantiquake == false)
                                {
                                    if (Program.ffrspresist > 4)
                                    {
                                        colors.echo(4, $"Resists already capped! Rerolling.");
                                        goto blackmagic;
                                    }
                                    //n696 = elseif(% Program.ffrspresist = 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend tectonic }
                                    // n697 = elseif(% Program.ffrspresist isnum 1 - 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend land }
                                    //n698 =            else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend geo }
                                    Program.ffrspantiquake = true;
                                    Program.ffrspresist++;
                                }
                            }
                        }
                        else // Regeneration
                        {
                            ffrbmstrskip = false;
                            ffrbmTypeByte = 6;
                            double ffrbmregmod = ffrbmstrength / 6;
                            if (Program.ffrspellbinding == true)
                            {
                                ffrbmregmod = Program.ffrsplevel;
                                if (Program.ffrsplevel < 7) { ffrbmtier = 3; }
                                else { ffrbmTypeByte = 13; }
                                if (Program.ffrsplevel < 5) { ffrbmtier = 2; }
                                if (Program.ffrsplevel < 3) { ffrbmtier = 1; }
                            }
                            ffrbmstrength = (double)Math.Floor((double)Math.Pow(blackDice.Next(18, 23) / 10, (blackDice.Next(25, 41) / 10 + ffrbmregmod / 2)) + (ffrbmregmod - 1) / 2);
                            if (Program.ffrspellbinding == false)
                            {
                                ffrbmtier = (int)Math.Ceiling(ffrbmstrength / blackDice.Next(32, 65));
                                if (ffrbmtier > 3) { colors.echo(7, $"Regen Power: {ffrbmstrength}"); }
                            }
                        }
                        if (ffrbmresmagic == true)
                        {
                            ffrbmresist = "Magic";
                            if (Program.ffrspantimagic == false)
                            {
                                if (Program.ffrspresist > 4)
                                {
                                    colors.echo(4, $"Resists already capped! Rerolling.");
                                    goto blackmagic;
                                }
                                //n712 = elseif(% Program.ffrspresist = 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend mortality }
                                //n713 = elseif(% Program.ffrspresist isnum 1 - 2) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend life }
                                //n714 = elseif(% Program.ffrspresist = 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend magic }
                                //n715 =          else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend bio }
                                Program.ffrspantimagic = true;
                                Program.ffrspresist++;
                            }
                        }
                        else if (ffrbmresdecay == true)
                        {
                            ffrbmresist = "Decay";
                            if (Program.ffrspantitoxin == false)
                            {
                                if (Program.ffrspresist > 4)
                                {
                                    colors.echo(4, $"Resists already capped! Rerolling.");
                                    goto blackmagic;
                                }
                                //n727 = elseif(% Program.ffrspresist = 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend futility }
                                //n728 = elseif(% Program.ffrspresist isnum 1 - 2) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend doom }
                                //n729 = elseif(% Program.ffrspresist = 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend waste }
                                //n730 =          else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend rot }
                                Program.ffrspantitoxin = true;
                                Program.ffrspresist++;
                            }
                        }
                        else if (ffrbmresdragon == true)
                        {
                            ffrbmresist = "Dragon";
                            if (Program.ffrspantidamage == false)
                            {
                                if (Program.ffrspresist > 4)
                                {
                                    colors.echo(4, $"Resists already capped! Rerolling.");
                                    goto blackmagic;
                                }
                                //n742 = elseif(% Program.ffrspresist = 0) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend elemental }
                                //n743 = elseif(% Program.ffrspresist isnum 1 - 2) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend wyrm }
                                //n744 = elseif(% Program.ffrspresist = 3) { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend spell }
                                //n745 =          else { set % Program.ffrspresmsg $+ $calc(% Program.ffrspresist + 1) Defend wiz }
                                Program.ffrspantidamage = true;
                                Program.ffrspresist++;
                            }
                        }
                        //n750 = write - al3 WhiteSpell.txt Effect: Resist % ffrbmresist
                        if (ffrbmelement > 0)
                        {
                            switch (ffrbmresist)
                            {
                                case "Status": ffrbmEffByte = 1; break;
                                case "Poison / Stone": ffrbmEffByte = 2; break;
                                case "Time": ffrbmEffByte = 4; break;
                                case "Death": ffrbmEffByte = 8; break;
                                case "Fire": ffrbmEffByte = 16; break;
                                case "Ice": ffrbmEffByte = 32; break;
                                case "Lit": ffrbmEffByte = 64; break;
                                case "Earth": ffrbmEffByte = 128; break;
                                case "Magic": ffrbmEffByte = 137; break;
                                case "Decay": ffrbmEffByte = 14; break;
                                case "Dragon": ffrbmEffByte = 112; break;
                                default: ffrbmEffByte = blackDice.Next(0, 256); break;
                            }
                        }
                    }
                    #endregion
                    else if (ffrbmbuff == 3) // Double Hits
                    {
                        if (((Program.ffrspellbinding == true) && (Program.ffrsplevel < 4)) || ((Program.ffrspellbinding == false) && (ffrbmstrength < 25))) { ffrbmtarget = 1; }
                        else if (((Program.ffrspellbinding == true) && (Program.ffrsplevel > 7)) || ((Program.ffrspellbinding == false) && (ffrbmstrength >= 40))) { ffrbmtarget = 3; }
                        else { ffrbmtarget = 2; }
                        ffrbmstrskip = true;
                        ffrbmTypeByte = 10;
                        // However, Black Magic likes to hurt things
                        // FAST hurts things. A lot.
                    }
                    else if (ffrbmbuff == 4) // Attack Up
                    {
                        ffrbmTypeByte = 11;
                        if (ffrbmtarget > 1)
                        {
                            if (Program.ffrspellbinding == true)
                            {
                                ffrbmstrength = (int)Math.Floor(Program.ffrsplevel * 3 + blackDice.Next(3, 6));
                                if (ffrbmstrength < 27) { ffrbmtier = 3; }
                                else { ffrbmtier = 4; }
                                if (ffrbmstrength < 18) { ffrbmtier = 2; }
                                if (ffrbmstrength < 12) { ffrbmtier = 1; }
                            }
                            else { ffrbmstrength = blackDice.Next(5, 20) + blackDice.Next(1, 11); }
                            if (blackDice.Next(1, 21) > blackDice.Next(1, 51))
                            {
                                ffrbmtarget = 3;
                                if (Program.ffrspellbinding == true) { ffrbmstrength = (int)Math.Floor(ffrbmstrength / ((blackDice.Next(100, 121) / 10 - Program.ffrsplevel) / 2) + 5); }
                                else { ffrbmstrength = (int)Math.Ceiling(ffrbmstrength * (blackDice.Next(75, 101) / 100)); }
                            }
                            else { colors.echo(7, $"Debug: Failed RALY attempt"); }
                        }
                        // TMPR hurts things too
                        // It comes in varying strengths and uses its own dice
                        // The final range ends up at 6 to 29
                        else
                        {
                            if (Program.ffrspellbinding == true)
                            {
                                ffrbmstrength = (int)Math.Floor(Math.Pow(2, (1.5 + Program.ffrsplevel / 2)) + (9 - Program.ffrsplevel) + Program.ffrsplevel / 2);
                                if (ffrbmstrength < 50) { ffrbmtier = 3; }
                                else { ffrbmtier = 4; }
                                if (ffrbmstrength < 22) { ffrbmtier = 2; }
                                if (ffrbmstrength < 15) { ffrbmtier = 1; }
                            }
                            else { ffrbmstrength = blackDice.Next(5, 50); }
                        }
                        // SABR is static when assigned to a level properly
                        // However, it varies wildly when left to its own devices
                    }
                    else if (ffrbmbuff == 5) // Evade Up
                    {
                        ffrbmtarget = 2;
                        ffrbmTypeByte = 14;
                        if (Program.ffrspellbinding == true)
                        {
                            if (Program.ffrsplevel == 8) { ffrbmstrength = 80; ffrbmtier = 4; }
                            if (Program.ffrsplevel < 8) { ffrbmstrength = 60; ffrbmtier = 3; }
                            if (Program.ffrsplevel < 5) { ffrbmstrength = 40; ffrbmtier = 2; }
                            if (Program.ffrsplevel < 3) { ffrbmstrength = 30; ffrbmtier = 1; }
                        }
                        else { ffrbmstrength = (int)Math.Floor(ffrbmstrength * blackDice.Next(10, 19) / 10); }
                    }
                    else
                    {
                        colors.echo(4, $"Value {ffrbmbuff} out of range for Buffs"); return "Spell Failed";
                    }
                }
                #endregion Buffs
            }
            #endregion Alterations
            #endregion BaseEffect
            #region MoreElement
            // Offensive Spells play with elements a bit
            // Fire, Ice, and Lit are the most common
            // But Black Magic has a bit of a fetish for Poison and Death
            // It only bundles Status and Earth with common elements
            // And won't use Time

            // Insta-Kills, however, are another story
            if (ffrbmeffect == 1)
            {
                if (ffrbmohko < 4)
                {
                    blackohko:
                    switch (ffrbmelement)
                    {
                        case 0: if (blackDice.Next(1, 11) < 10) ffrbmElemByte = 128; break;
                        case 1: ffrbmElemByte = 2; ffrbmEffByte = 2; if (Program.ffrspellbinding == true) ffrbmaccuracy++; break;
                        case 5: if (blackDice.Next(1, 5) < 4) ffrbmElemByte = 8; break;
                        case 6: if (blackDice.Next(1, 5) < 4) { ffrbmelement = blackDice.Next(1,3); goto blackohko; }; if (Program.ffrspellbinding == true) ffrbmaccuracy++; ffrbmEffByte = 2; break;
                        case 7: if (blackDice.Next(1, 5) < 4) ffrbmElemByte = 4; break;
                        default: break;
                    }
                }
                else
                {
                    if (((!Enumerable.Range(5,3).Contains(ffrbmelement)) && (blackDice.Next(1,4) == 3)) || ((ffrbmelement == 0) && (blackDice.Next(1,21) < blackDice.Next(1,21))))
                    {
                        ffrbmelement = blackDice.Next(5, 8);
                        colors.echo(11, $"Debug: Forcing element to Fire, Ice, or Lightning");
                        goto bmbase;
                    }
                    if (blackDice.Next(1, 21) > blackDice.Next(1, 61))
                    {
                        ffrbmDualDenial = false;
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("Debug: Dual Tech passed");
                        if (Enumerable.Range(5, 3).Contains(ffrbmelement) || ffrbmelement == 0) { colors.echo(11, $"...but it didn't matter."); }
                    }
                    if (ffrbmDualDenial == true)
                    {
                        colors.echo(11, "Debug: Dual Tech did not pass");
                        switch (ffrbmelement)
                        {
                            case 0: if (Program.ffrspellbinding == true) ffrbmaccuracy++; break;
                            case 1: ffrbmElemByte = 64; ffrbmelement = 7; break;
                            case 3: ffrbmElemByte = 32; ffrbmelement = 6; break;
                            case 4: ffrbmstrength = ffrbmstrength * 1.2; break;
                            case 8: ffrbmElemByte = 16; ffrbmelement = 5; break;
                            default: break;
                        }
                    }
                    else
                    {
                        switch (ffrbmelement)
                        {
                            case 0: if (Program.ffrspellbinding == true) ffrbmaccuracy++; break;
                            case 1: ffrbmElemByte = 65; ffrbmstrength = (int)Math.Floor(ffrbmstrength * 1.25); break; // Kinetic
                            case 2: ffrbmElemByte = 96; ffrbmstrength = (int)Math.Floor(ffrbmstrength * 1.1); break; // Storm
                            case 3: ffrbmElemByte = 80; ffrbmstrength = (int)Math.Floor(ffrbmstrength * 1.1); break; // Plasma
                            case 4: ffrbmElemByte = 48; ffrbmstrength = (int)Math.Floor(ffrbmstrength * 1.25); break; // Antipode
                            case 8: ffrbmElemByte = 144; ffrbmstrength = (int)Math.Floor(ffrbmstrength * 1.25); break; // Magma
                            default: break;
                        }
                    }
                }
            }
        #endregion MoreElement
        #region Target
        // -b2: Ensuring targeting remains

            if (ffrbmb2 == true)
            {
                if (Program.ffrsplevel < 8) { ffrbmtarget = 2; }
                else if (Program.ffrsplevel < 3) { ffrbmtarget = 1; }
            }

            // Assigns the target byte
            // First checks if FAST or TMPR are AoE
            // Then checks if it's a Buff
            // Because only TMPR and FAST and target all allies
        barrskip:
            if ((ffrbmeffect == 2) && (ffrbmtarget == 3)) {
                ffrbmTargByte = 2;
                goto targetjump;
            }
        if ((ffrbmeffect == 1) || (ffrbmalter == 1))
            {
                if (ffrbmtarget < 2)
                {
                    ffrbmTargByte = 3;
                    if (Program.ffrspellbinding == true) { 
                        if (ffrbmTypeByte == 1) { ffrbmaccuracy++; }
                        ffrbmaccuracy++;
                    }
                }
                else { ffrbmTargByte = 4; }
            }
        else { ffrbmTargByte = (ffrbmtarget == 1) ? 0 : 1; }
            #endregion Target
            #region Accuracy
        targetjump: // Just making sure All Allies doesn't get erased

            // Accuracy Balance
            // Defines the appropriate tiers of Accuracy for Spellbooks
            // Accuracy no longer caps
            if (Program.ffrspellbinding == true && (ffrbmTypeByte != 3 || PowerOf2.Divide(ffrbmEffByte-4) == false))
            {
                switch (ffrbmElemByte) // Set element penalties and bonuses
                {
                    case 2: ffrbmaccuracy += 2; break;
                    case 4: ffrbmaccuracy--; break;
                    case 8: ffrbmaccuracy--; break;
                    case 128: ffrbmaccuracy += 2; break;
                    default: break;
                }
            }
            if (Program.ffrspellbinding == true)
            {
                if (ffrbmaccuracy < 1) { ffrbmaccuracy = 1; }
                switch (ffrbmaccuracy)
                {
                    case 1: ffrbmaccsay = "0 "; ffrbmaccuracy = 0; break;
                    case 2: ffrbmaccsay = "5 "; ffrbmaccuracy = 5; break;
                    case 3: ffrbmaccsay = "8 "; ffrbmaccuracy = 8; break;
                    case 4: ffrbmaccsay = "16"; ffrbmaccuracy = 16; break;
                    case 5: ffrbmaccsay = "24"; ffrbmaccuracy = 24; break;
                    case 6: ffrbmaccsay = "32"; ffrbmaccuracy = 32; break;
                    case 7: ffrbmaccsay = "40"; ffrbmaccuracy = 40; break;
                    case 8: ffrbmaccsay = "48"; ffrbmaccuracy = 48; break;
                    case 9: ffrbmaccsay = "64"; ffrbmaccuracy = 64; break;
                    case 10: ffrbmaccsay = "107"; ffrbmaccuracy = 107; break;
                    case 11: ffrbmaccsay = "128"; ffrbmaccuracy = 128; break;
                    case 12: ffrbmaccsay = "152"; ffrbmaccuracy = 152; break;
                    case 13: ffrbmaccsay = "175"; ffrbmaccuracy = 175; break;
                    case 14: ffrbmaccsay = "210"; ffrbmaccuracy = 210; break;
                    default: ffrbmaccsay = "255"; ffrbmaccuracy = 255; break;
                }
            }
            if ((ffrbmaccuracy == 255) || (ffrbmaccsay == "Auto-Hit")) { colors.echo(7, $"Debug: Assumed Accuracy was Auto-Hit"); }
            #endregion Accuracy

            // Cleanup
            //
            // Writes only information relevant to the spell
            //  if (% ffrbmstrskip = 0) && (% ffrbmaccskip = 0) { write - a16 WhiteSpell.txt Power: % ffrbmstrength | write - al7 WhiteSpell.txt Acc Bonus: % ffrbmaccsay }
            // elseif(% ffrbmstrskip = 0) && (% ffrbmaccskip = 1) { write - a16 WhiteSpell.txt Power: % ffrbmstrength }
            // elseif(% ffrbmstrskip = 1) && (% ffrbmaccskip = 0) { write - a16 WhiteSpell.txt Acc Bonus: % ffrbmaccsay }
            if (ffrbmstrskip == false) { ffrbmEffByte = (int)ffrbmstrength; }
            if (ffrbmaccskip == false) { ffrbmAccByte = ffrbmaccuracy; }

            #region EnemyCheck
            // Prevents Confusion and Fear from landing in slots enemies use
            if ((Program.ffrspflags.IndexOf("e") != -1) && (Program.ffrspreroll < 5))
            {
                // if (L isincs % Program.ffrspflags) { write - i SpellLog.txt Flag "e" is evaluating this spell.Reroll counter is at % Program.ffrspreroll of 5 }
                if ((ffrbmeffect == 2) && (ffrbmalter == 1) && ((ffrbmdebuff == 6) || (ffrbmdebuff == 9)))
                {
                    string ffrbmEnemyReject = "Debug: Bounced CONF or FEAR during spellbinding";
                    if ((Program.ffrsplevel == 1) || (Program.ffrsplevel == 5) || (Program.ffrsplevel == 8)) { colors.echo(11, ffrbmEnemyReject); goto blackmagic; }
                    else if (((Program.ffrsplevel == 4) || (Program.ffrsplevel == 6) || (Program.ffrsplevel == 7)) && (Program.ffrspslot != 3)) { colors.echo(11, ffrbmEnemyReject); goto blackmagic; }
                    else if ((Program.ffrsplevel == 2) && ((Program.ffrspslot == 2) || (Program.ffrspslot == 4))) { colors.echo(11, ffrbmEnemyReject); goto blackmagic; }
                    else if ((Program.ffrsplevel == 3) && (Program.ffrspslot != 4)) { colors.echo(11, ffrbmEnemyReject); goto blackmagic; }
                }
                // if (L isincs % Program.ffrspflags) { write - i SpellLog.txt Flag "e" has finished evaluating this spell. | write - i SpellLog.txt - }
            }
            #endregion EnemyCheck
            // Doesn't open the file when Spellbinding
            // because that would open 64 files
            // Debug: if (% Program.ffrspellbinding = 1) && (% ffrbmbuff = 14) { / run WhiteSpell.txt | halt }
            //  if ($read(WhiteSpell.txt, w, *resist *)) { echo 8 - s Resist ID: % ffrbmbuff }
            //  if (% ffrbmaccsay > 64) { set % Program.ffrspredharm 1 }
            //  if (% ffrnowrite != 1) { / run WhiteSpell.txt | unset % ffr * }

            Console.WriteLine();
            colors.echo(9, $"Type Byte: {ffrbmTypeByte}");
            if (ffrbmEffByte != 0) { colors.echo(9, $"Effect Byte: {ffrbmEffByte}"); }
            colors.echo(9, $"Target Byte: {ffrbmTargByte}");
            colors.echo(9, $"Element Byte: {ffrbmElemByte}");
            if (ffrbmaccskip == false) { colors.echo(9, $"Accuracy Byte: {ffrbmAccByte}"); }

            Console.WriteLine();
            colors.echo(12, $"Magic Level: {Program.ffrsplevel}");
            colors.echo(12, $"Spell Slot: {Program.ffrspslot}");
            colors.echo(12, $"Tier {ffrbmtier} Power");
            string ffrBlackSpell = $"{ffrbmTypeByte}_{ffrbmEffByte}_{ffrbmTargByte}_{ffrbmElemByte}_{ffrbmAccByte}";

            Console.WriteLine();
            colors.echo(0, $"Returned {ffrBlackSpell} to ffr-spellbinder");
            Console.WriteLine();
            return ffrBlackSpell;
        }
    }
}
