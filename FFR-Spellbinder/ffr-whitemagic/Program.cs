// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
// FF1 Spellbinder, for use with Final Fantasy Randomizer. IRC Script and C# port by Linkshot, 2018-2022
namespace ffr_spellbinder
{
    internal class Program
    {
        // Flag List (Accept only these)
        // e: Enemy Slot Sanity Checker
        // h: Out-of-Battle Healing Preservation
        // b: Minimum 6 AoEs in Black Magic
        // b2: Preserve all Black Damage slots
        // k: No InstaKills below Level 5
        // C: Ensure Confusion is harmful
        // i: Assign appropriate Item Magic
        // S: LOK2 & HEL2 spell fixes compatibility
        // x: Turn off level balancing
        public static string ffrspflags = "-ehkS";
        public static int ffrspblackAoE = 0;
        public static bool ffrspellbinding = true;
        public static double ffrsplevel = 1;
        public static int ffrspslot = 0;
        public static string ffrspmagic = "white";
        public static int ffrspreroll = 0;
        public static int ffrspresist = 0;
        #region ResistVars
        public static bool ffrspwall = false;
        public static bool ffrspantiweak = false;
        public static bool ffrspantibane = false;
        public static bool ffrspantizap = false;
        public static bool ffrspantinecro = false;
        public static bool ffrspantifire = false;
        public static bool ffrspantiice = false;
        public static bool ffrspantilightning = false;
        public static bool ffrspantiquake = false;
        public static bool ffrspantimagic = false;
        public static bool ffrspantitoxin = false;
        public static bool ffrspantidamage = false;
        #endregion ResistVars
        public static int ffrspbatmsgloop = 0;

        public static string ffrspname = "";
        public static string ffrspgfxcolor = "";
        public static string ffrspgfxshape = "";
        public static string ffrSpell = "";

        #region CastingItems
        public static string ffrLightAxe = "";
        public static string ffrHealGear = "";
        public static string ffrDefense = "";
        public static string ffrWhiteShirt = "";
        public static string ffrPowerBonk = "";
        public static string ffrThorZeus = "";
        public static string ffrBaneSword = "";
        public static string ffrMageStaff = "";
        public static string ffrBlackShirt = "";
        public static string ffrWizardStaff = "";
        #endregion CastingItems
        static void Main()
        {           
            while (ffrspbatmsgloop !>= 77)
            {
                ffrspbatmsgloop++;
                // if (ffrspbatmsgloop != 76) { ffrspunmsg + ffrspbatmsgloop = [Unassigned] }
            }
            string ffrspmsg76 = "Go Mad [Unused]";
        sploop:
            ffrspreroll = 0;
            ffrspslot++;
            if (ffrsplevel == 5 && ffrspslot == 3 && ffrspmagic == "black") { Console.WriteLine("WARP"); goto sploop; } // ffrSpell = "WARP" and add to array later }
            else if (ffrsplevel == 6 && ffrspslot == 2 && ffrspmagic == "white") { Console.WriteLine("EXIT"); goto sploop; } // ffrSpell = "EXIT" and add to array later }
        spdupe:
            if (ffrspmagic == "white") { ffr_whitemagic.WMag(); }
            // Console.WriteLine("=");
            else if (ffrspmagic == "black") { ffr_blackmagic.BMag(); }

            #region Library
            // Big case block of what to queue for array here
            #endregion Library

            // Check for ffrspname in the array containing all results
            // if true: ffrspreroll++; goto spdupe;
            // if false: add result to array

            ffr_whitemagic.ffrwmtier = 0;
            ffr_blackmagic.ffrbmtier = 0;
            if (ffrspslot == 4) {
                if (ffrspmagic == "white") {
                    Console.WriteLine("-----");
                    ffrspslot = 0;
                    ffrspmagic = "black";
                    goto sploop;
                }
                else if (ffrspmagic == "black")
                {
                    Console.WriteLine("=====");
                    if (ffrsplevel == 8) // Array finished populating
                    {
                        #region ItemMagic
                        // start going through array of spells at specific slots, forward and then backward
                        // deny that slot being assigned twice
                        // if (ffrspflags.IndexOf("i") != -1) {
                        // ffrLightAxe: TypeByte 1 or 2. TargByte 4. Array entries of (modulo 8 > 3) only. Start seeking at 17th entry.
                        // ffrHealGear: TypeByte 6, 7, or 13. TargByte 2. Array entries of (modulo 8 > 3) only. Start seeking at 17th entry.
                        // ffrDefense: TypeByte 8, 9, or 12. TargByte 0. Start seeking at 17th entry.
                        // ffrWhiteShirt: TypeByte 8, 9, or 12. TargByte 2. Start seeking at 17th entry.
                        // ffrPowerBonk: TypeByte 10 or 11. TargByte 0 or 2. Start seeking at 33rd entry.
                        // ffrThorZeus: ElemByte 64. TargByte 4. Start seeking at 21st entry.
                        // ffrBaneSword: ElemByte 2 or (TypeByte 3 && EffByte - 4 is a power of 2). TargByte 4. Array entires of (modulo 8 < 4) only. Start seeking at 37th entry.
                        // ffrMageStaff: TypeByte 1. TargByte 4. Array entries of (modulo 8 < 4) only. Start seeking at 21st entry.
                        // ffrBlackShirt: TypeByte 1. TargByte 4. Array entries of (modulo 8 < 4) only. Start seeking at 29th entry.
                        // ffrWizardStaff: TypeByte > 2. (TypeByte != 3 || EffByte > 3). TargByte 4. Starts seeking at 9th entry.
                        // }
                        // if all succeed, echo.colors(9,$"Item Magic verified!");
                        // else prompt user to proceed or retry
                        #endregion ItemMagic
                        #region BlackAoE
                        // if (ffrspflags.IndexOf("b") != -1) {
                        // Check for 6 of TypeByte 1 && TargByte 4 at (modulo 8 < 4) entries
                        // if true: echo.colors(9,$"{amount} damage spells detected!");
                        // if false: clear array and start over
                        // }
                        #endregion BlackAoE
                        #region BattleMessages
                        // if (ffrspresist < 5) { ffrmsg28 = "Defend all [Unassigned]" }
                        // if (ffrspresist < 4) { ffrmsg25 = "Defend magic [Unassigned]" }
                        // if (ffrspresist < 3) { ffrmsg16 = "Defend cold [Unassigned]" }
                        // if (ffrspresist < 2) { ffrmsg12 = "Defend fire [Unassigned]" }
                        // if (ffrspresist < 1) { ffrmsg8 = "Defend lightning [Unassigned]" }
                        // Assign Message 76:
                        // if: TypeByte 3 or 16. ElemByte 32. { ffrmsg76 = "Frozen" }
                        // else if: TypeByte 3 or 16. EffByte 128 || (EffByte - 4 is a power of 2). ElemByte 16. { ffrmsg76 = "Ablaze" }
                        // else if: TypeByte 10. TargByte 0. Array entries of (modulo 8 < 4) only. { ffrmsg76 = "Go mad" }
                        // Write down applicable battle messages and whether they were assigned
                        // (1, 2, 3, 5, 8, 10, 11, 12, 13, 15, 16, 18, 21, 22, 24, 25, 27, 28, 29, 30, 31, 43, 47, 74, 76, 77)
                        #endregion BattleMessages
                        colors.echo(9, $"Generated 64 spells! Flags used: {ffrspflags}");
                    }
                    else if (ffrsplevel > 8) { colors.echo(4, $"ffrsplevel reached {ffrsplevel}, which exceeds 8!"); }
                    else
                    {
                        ffrsplevel++;
                        ffrspslot = 0;
                        ffrspmagic = "white";
                        goto sploop;
                    }
                }
                else { colors.echo(4, $"String {ffrspmagic} is invalid for ffrspmagic!"); }
            }
            else if (ffrspslot > 4) { colors.echo(4, $"ffrspslot reached {ffrspslot}, which exceeds 4!"); }
            else { goto sploop; }
        }
    }
}