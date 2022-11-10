// See https://aka.ms/new-console-template for more information
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Timers;
using static System.Formats.Asn1.AsnWriter;
// FF1 Spellbinder, for use with Final Fantasy Randomizer. IRC Script and C# port by Linkshot, 2018-2022
namespace ffr_spellbinder
{
    internal class Program
    {
        #region Initialization
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
        public static string ffrspflags = "";
        public static bool ffrspellbinding = true;
        public static int ffrspblackAoE = 0;
        public static double ffrsplevel = 1;
        public static int ffrspslot = 0;
        public static string ffrspmagic = "white";
        public static int ffrspreroll = 0;
        public static int ffrspResistCount = 0;
        public static int ffrspCurrentResist = 0;
        public static int ffrsptier = 0;
        #region ResistVars
        public static int ffrspwall = 0;
        public static int ffrspantiweak = 0;
        public static int ffrspantibane = 0;
        public static int ffrspantizap = 0;
        public static int ffrspantinecro = 0;
        public static int ffrspantifire = 0;
        public static int ffrspantiice = 0;
        public static int ffrspantilightning = 0;
        public static int ffrspantiquake = 0;
        public static int ffrspantimagic = 0;
        public static int ffrspantitoxin = 0;
        public static int ffrspantidamage = 0;
        #endregion ResistVars
        public static int ffrspbatmsgloop = 0;

        public static string ffrspname = "";
        public static string ffrsptarg = "";
        public static string ffrspelem = "";
        public static string ffrsptype = "";
        public static string ffrspeff = "";
        public static string ffrspgfxcolor = "";
        public static string ffrspgfxshape = "";
        public static string ffrSpell = "";

        public static int ffrspShapeByte;
        public static int ffrspColorByte;
        public static int ffrspMsgByte = 0;
        public static int ffrspRmPermByte = 0;
        public static int ffrspWmPermByte = 0;
        public static int ffrspBmPermByte = 0;
        public static int ffrspRwPermByte = 0;

        public static bool ffrspRmPerms = true;
        public static bool ffrspWmPerms = true;
        public static bool ffrspBmPerms = true;
        public static bool ffrspRwPerms = true;

        #region CastingItems
        public static int ffrLightAxe;
        public static int ffrHealGear;
        public static int ffrDefense;
        public static int ffrWhiteShirt;
        public static int ffrPowerBonk;
        public static int ffrThorZeus;
        public static int ffrBaneSword;
        public static int ffrMageStaff;
        public static int ffrBlackShirt;
        public static int ffrWizardStaff;

        public static bool ffrLightAxeLocked = false;
        public static bool ffrHealGearLocked = false;
        public static bool ffrDefenseLocked = false;
        public static bool ffrWhiteShirtLocked = false;
        public static bool ffrPowerBonkLocked = false;
        public static bool ffrThorZeusLocked = false;
        public static bool ffrBaneSwordLocked = false;
        public static bool ffrMageStaffLocked = false;
        public static bool ffrBlackShirtLocked = false;
        public static bool ffrWizardStaffLocked = false;
        #endregion CastingItems
        #endregion Initialization
        static void Main()
        {
            colors.echo(0, "Select flags! (Case Sensitive)");
            colors.echo(0, " ====================================================== ", 2);
            colors.echo(0, "| - b: Minimum 6 Spellbombs                            |", 2);
            colors.echo(0, "|   - b2: Preserve Damage Slots                        |", 2);
            colors.echo(0, "| - C: Confusion status can't cast buffs               |", 2);
            colors.echo(0, "| - e: Enemies will only cast spells they can use      |", 2);
            colors.echo(0, "| - h: Out-of-Battle Spells don't change               |", 2);
            colors.echo(0, "| - i: Assigns appropriate spells to each Casting Item |", 2);
            colors.echo(0, "| - k: No Instakills below Level 5                     |", 2);
            colors.echo(0, "| - n: Hide the name of every spell                    |", 2);
            colors.echo(0, "|   - n2: Hide names and allow duplicate types         |", 2);
            colors.echo(0, "| - r: Assign permissions based on spell contents      |", 2);
            colors.echo(0, "| - S: Complies with FFR's bugfixes to LOK2 and HEL2   |", 2);
            colors.echo(0, "| - x: Ignores magic level when generating spells      |", 2);
            colors.echo(0, " ====================================================== ", 2);
            colors.echo(12, "NOTE: All flags will give up after 5 failures to fill a spell slot");
            colors.echo(2, "(If something seems out of place, that's probably what happened)");
            ffrspflags = $"{Console.ReadLine()}";
            // Thread.Yield();
            #region Reinitialization
            var ffrspretries = 0;
        retry:
            colors.echo(ffrspretries, $"Spellbinding! Attempt {ffrspretries + 1}..");
            if (ffrspflags.Contains("x")) ffrspellbinding = false;
            ffrspblackAoE = 0;
            ffrsplevel = 1;
            ffrspslot = 0;
            ffrspmagic = "white";
            ffrspreroll = 0;
            ffrspResistCount = 0;
            ffrspCurrentResist = 0;
            #region ResistVars
            ffrspwall = 0;
            ffrspantiweak = 0;
            ffrspantibane = 0;
            ffrspantizap = 0;
            ffrspantinecro = 0;
            ffrspantifire = 0;
            ffrspantiice = 0;
            ffrspantilightning = 0;
            ffrspantiquake = 0;
            ffrspantimagic = 0;
            ffrspantitoxin = 0;
            ffrspantidamage = 0;
            #endregion ResistVars
            ffrspbatmsgloop = 0;

            ffrspname = "";
            ffrsptarg = "";
            ffrspelem = "";
            ffrsptype = "";
            ffrspeff = "";
            ffrspgfxcolor = "";
            ffrspgfxshape = "";
            ffrSpell = "";

            ffrspShapeByte = 0;
            ffrspColorByte = 0;
            ffrspMsgByte = 0;
            ffrspRmPermByte = 0;
            ffrspWmPermByte = 0;
            ffrspBmPermByte = 0;
            ffrspRwPermByte = 0;

            #region CastingItems
            ffrLightAxe = 0;
            ffrHealGear = 0;
            ffrDefense = 0;
            ffrWhiteShirt = 0;
            ffrPowerBonk = 0;
            ffrThorZeus = 0;
            ffrBaneSword = 0;
            ffrMageStaff = 0;
            ffrBlackShirt = 0;
            ffrWizardStaff = 0;

            ffrLightAxeLocked = false;
            ffrHealGearLocked = false;
            ffrDefenseLocked = false;
            ffrWhiteShirtLocked = false;
            ffrPowerBonkLocked = false;
            ffrThorZeusLocked = false;
            ffrBaneSwordLocked = false;
            ffrMageStaffLocked = false;
            ffrBlackShirtLocked = false;
            ffrWizardStaffLocked = false;
            #endregion CastingItems
            var ffrspTable = new List<string>();
            var ffrspBatMsg = new List<string>();
            var ffrspResMsg = new List<string>();
            var itemCheck = new List<string>();
            var ffrNameHide = new List<string>();
            var ffrpath = @"C:\Users\Linkshot\Utilities\FFR-Spellbinder\output\";
            var book = @$"table\SpellTable_{DateTime.UtcNow.Ticks.ToString()}.txt";
            var rune = @$"patch\FFR-Custom-Spells_({ffrspflags})_{DateTime.UtcNow.Ticks.ToString()}.ips";
            #endregion Reinitialization
            using (TextWriter quill = File.CreateText($@"{ffrpath}{book}"))
            {
                using (BinaryWriter etch = new BinaryWriter(File.OpenWrite($@"{ffrpath}{rune}")))
                {
                    var ffrspNamePatch = new List<char>();
                    var ffrspSpellPatch = new List<byte>();
                    var ffrspMsgTextPatch = new List<char>();
                    var ffrspSpellMsgPatch = new List<byte>();
                        var ffrspRmPermsPatch = new List<byte>();
                        var ffrspWmPermsPatch = new List<byte>();
                        var ffrspBmPermsPatch = new List<byte>();
                        var ffrspKnPermsPatch = new List<byte>();
                        var ffrspNjPermsPatch = new List<byte>();
                        var ffrspRwPermsPatch = new List<byte>();
                    var ffrPatchCount = 0;
                    etch.Write(Encoding.ASCII.GetBytes("PATCH"));
                    // while (ffrspbatmsgloop !>= 77)
                    // {
                    // ffrspbatmsgloop++;
                    // if (ffrspbatmsgloop != 76) { ffrspunmsg $+ ffrspbatmsgloop = [Unassigned] }
                    // }
                    // string ffrspmsg76 = "Go Mad [Unused]";
                    int ffrSpellAcc;
                    int ffrSpellEff;
                    int ffrSpellElem;
                    int ffrSpellTarg;
                    int ffrSpellType;
                sploop:
                    ffrspreroll = 0;
                    ffrspslot++;
                    if (ffrsplevel == 5 && ffrspslot == 3 && ffrspmagic == "black")
                        #region WARP
                    {
                        ffrsptype = "Travel back one floor. ";
                        ffrSpellAcc = 255;
                        ffrSpellEff = 0;
                        ffrSpellElem = 0;
                        ffrSpellTarg = 4;
                        ffrSpellType = 0;
                        ffrspShapeByte = 0xD8;
                        ffrspColorByte = 42;
                        ffrspMsgByte = 0x24;
                        ffrspgfxcolor = "Dark Green";
                        ffrspgfxshape = "Glowing Ball";
                        ffrspname = "WARP";
                        if (ffrspflags.Contains("r"))
                        {
                            ffrspRmPerms = false;
                            ffrspBmPerms = false;
                            ffrspRwPerms = true;
                        }
                        goto spwrite;
                    }
                    #endregion WARP
                    else if (ffrsplevel == 6 && ffrspslot == 2 && ffrspmagic == "white")
                        #region EXIT
                    {
                        ffrsptype = "Return to World Map. ";
                        ffrSpellAcc = 255;
                        ffrSpellEff = 0;
                        ffrSpellElem = 0;
                        ffrSpellTarg = 8;
                        ffrSpellType = 0;
                        ffrspShapeByte = 0xDC;
                        ffrspColorByte = 42;
                        ffrspMsgByte = 0x24;
                        ffrspgfxcolor = "Dark Green";
                        ffrspgfxshape = "Bursting Ball";
                        ffrspname = "EXIT";
                        if (ffrspflags.Contains("r"))
                        {
                            ffrspRmPerms = false;
                            ffrspWmPerms = false;
                            ffrspRwPerms = true;
                        }
                        goto spwrite;
                    }
                #endregion EXIT
                spdupe:
                    ffrsptier = 0;
                    ffrspname = "";
                    ffrsptarg = "";
                    ffrspelem = "";
                    ffrsptype = "";
                    ffrspeff = "";
                    ffrspgfxcolor = "";
                    ffrspgfxshape = "";
                    ffrSpell = "";
                    ffrspShapeByte = 0xA8; // Fistchuks, yo
                    ffrspColorByte = 0x2E; // Black
                    ffrspMsgByte = 0x00;
                    ffrspRmPerms = true;
                    ffrspWmPerms = true;
                    ffrspBmPerms = true;
                    ffrspRwPerms = true;
                    if (ffrspmagic == "white") { ffrSpell = ffr_whitemagic.WMag(); }
                    // Console.WriteLine("=");
                    else if (ffrspmagic == "black") { ffrSpell = ffr_blackmagic.BMag(); }

                    int ffrSpellSep1 = ffrSpell.IndexOf(".");
                    int ffrSpellSep2 = ffrSpell.IndexOf("_");
                    int ffrSpellSep3 = ffrSpell.IndexOf("-");
                    int ffrSpellSep4 = ffrSpell.IndexOf("~");

                    ffrSpellAcc = Convert.ToInt32(ffrSpell.Substring(0, ffrSpellSep1));
                    ffrSpellEff = Convert.ToInt32(ffrSpell.Substring(ffrSpellSep1 + 1, ffrSpellSep2 - ffrSpellSep1 - 1));
                    ffrSpellElem = Convert.ToInt32(ffrSpell.Substring(ffrSpellSep2 + 1, ffrSpellSep3 - ffrSpellSep2 - 1));
                    ffrSpellTarg = Convert.ToInt32(ffrSpell.Substring(ffrSpellSep3 + 1, ffrSpellSep4 - ffrSpellSep3 - 1));
                    ffrSpellType = Convert.ToInt32(ffrSpell.Substring(ffrSpellSep4 + 1));

                    #region Graphics
                    // Assign color and shape based on stats
                    // Damage and Harm can assign themselves in the library
                    #region Shapes
                    switch (ffrSpellType)
                    {
                        case 3: ffrspShapeByte = 232; break;
                        case 4 or 5 or 14 or 17: ffrspShapeByte = 184; break;
                        case 7 or 15: ffrspShapeByte = (ffrSpellTarg == 8) ? 192 : 188; break;
                        case 8: ffrspShapeByte = (ffrSpellTarg == 8) ? 224 : 228; break;
                        case 9 or 10 or 12 or 13 or 16: ffrspShapeByte = (ffrSpellTarg == 8) ? 180 : 176; break;
                        case 18: ffrspShapeByte = 196; break;
                    }
                    if (ffrSpellAcc == 255) ffrspShapeByte = 196;
                    #endregion Shapes
                    #region Colours
                    switch (ffrSpellElem)
                    {
                        case 0: ffrspColorByte = (ffrspmagic == "white") ? 36 : 40; break;
                        case 1 or 65: ffrspColorByte = 37; break;
                        case 2 or 96: ffrspColorByte = 32; if (ffrspmagic == "black" && (((ffrSpellType == 1 || ffrSpellEff == 1) && ffrSpellElem == 2) || ((ffrSpellEff & 4) > 0 && ffrSpellType == 3) || (ffrSpellType == 18 && ffrSpellEff != 2))) ffrspColorByte = 35; break;
                        case 4 or 80: ffrspColorByte = 42; break;
                        case 8: ffrspColorByte = 45; break;
                        case 16: ffrspColorByte = 38; break;
                        case 32: ffrspColorByte = 44; break;
                        case 48: ffrspColorByte = 33; break;
                        case 64: ffrspColorByte = 34; break;
                        case 128 or 131 or 144: ffrspColorByte = 39; if (ffrspmagic == "white") ffrspColorByte = 41; break;
                    }
                    switch (ffrSpellType)
                    {
                        case 7:
                            switch (ffrsptier)
                            {
                                case 1: ffrspColorByte = 41; if (ffrspmagic == "black") ffrspColorByte = 45; break;
                                case 2: ffrspColorByte = 43; if (ffrspmagic == "black") ffrspColorByte = 39; break;
                                case 3: ffrspColorByte = 44; if (ffrspmagic == "black") ffrspColorByte = 33; break;
                                case 4: ffrspColorByte = 34; if (ffrspmagic == "black") ffrspColorByte = 42; break;
                            }
                            break;
                        case 8:
                            switch (ffrSpellEff)
                            {
                                case 1: ffrspColorByte = (ffrspmagic == "black") ? 45 : 43; break;
                                case 2: ffrspColorByte = 32; break;
                                case 3 or 252: ffrspColorByte = (ffrspmagic == "black") ? 40 : 36; break;
                                case 4 or 20 or 132 or 148: ffrspColorByte = 41; break;
                                case 8 or 24: ffrspColorByte = 38; break;
                                case 16: ffrspColorByte = 37; break;
                                case 32 or 48: ffrspColorByte = 42; break;
                                case 64 or 80: ffrspColorByte = 33; break;
                                case 128 or 144: ffrspColorByte = 34; break;
                            }
                            break;
                        case 9: ffrspColorByte = (ffrspmagic == "black") ? 44 : 41; break;
                        case 10:
                            switch (ffrSpellEff)
                            {
                                case 1 or 137: ffrspColorByte = 37; break;
                                case 2 or 14: ffrspColorByte = 35; break;
                                case 4: ffrspColorByte = 42; break;
                                case 8: ffrspColorByte = 45; break;
                                case 16: ffrspColorByte = 38; break;
                                case 32: ffrspColorByte = 44; break;
                                case 64: ffrspColorByte = 34; break;
                                case 112: ffrspColorByte = 33; break;
                                case 128: ffrspColorByte = 39; if (ffrspmagic == "black") ffrspColorByte = 41; break;
                            }
                            break;
                        case 12: if (ffrSpellTarg != 16) ffrspColorByte = 37; if (ffrspmagic == "white" || ffrSpellTarg == 16) ffrspColorByte = 42; break;
                        case 13: if (ffrSpellTarg == 4) ffrspColorByte = (ffrspmagic == "black") ? 33 : 32; else ffrspColorByte = (ffrspmagic == "white") ? 43 : 38; break;
                        case 15: ffrspColorByte = (ffrspmagic == "black") ? 42 : 34; break;
                        case 16: ffrspColorByte = (ffrspmagic == "black") ? 35 : 34; break;
                    }
                    #endregion Colours
                    #endregion Graphics

                    #region Library
                    // Big case block of what to queue for array here
                    if (ffrsptier > 4) ffrsptier = 4;
                    #region DamageSpells
                    if (ffrSpellType < 3)
                    {
                        if (ffrSpellAcc == 255) ffrspMsgByte = 0x2C;
                        else ffrspMsgByte = 0x00;
                        if (ffrspmagic == "white" || Enumerable.Range(64, 2).Contains(ffrSpellElem)) ffrspShapeByte = (ffrSpellTarg == 1) ? 204 : 200;
                        else ffrspShapeByte = (ffrSpellTarg == 2) ? 212 : 208;
                        if (ffrSpellType == 2)
                        {
                            if (Math.Floor((decimal)ffrSpellEff / 10) % 2 == 1 || ffrSpellEff >= 100) ffrspColorByte = 38;
                            else ffrspColorByte = 43;
                            switch (ffrSpellTarg)
                            {
                                case 1: if (ffrsptier > 1) ffrspname = $"HRM{ffrsptier}"; if (ffrsptier == 1) ffrspname = "HARM"; if (ffrSpellEff >= 100) ffrspname = "VANQ"; break;
                                case 2: if (ffrsptier > 1) ffrspname = $"JUG{ffrsptier}"; if (ffrsptier == 1) ffrspname = "JUDG"; break;
                                default: ffrspname = $"TARG{ffrSpellTarg}.HARM_UNDEAD"; break;
                            }
                        }
                        else
                        {
                            switch (ffrSpellElem)
                            {
                                case 0: // Non-Elemental
                                    #region NUKEandFADE
                                    switch (ffrSpellTarg)
                                    {
                                        case 1:
                                            if (ffrspmagic == "white") { if (ffrsptier > 1) ffrspname = $"RAZ{ffrsptier}"; else ffrspname = "RAZE"; if (ffrsptier == 4) ffrspname = "FADE"; }
                                            else if (ffrspmagic == "black") { if (ffrsptier > 1) ffrspname = $"BOM{ffrsptier}"; else ffrspname = "BOMB"; if (ffrsptier == 4) ffrspname = "NUKE"; }
                                            else ffrspname = $"FIELD.NO_ELEM.DAMAGE";
                                            break;
                                        case 2:
                                            if (ffrspmagic == "white") { if (ffrsptier > 1) ffrspname = $"SUN{ffrsptier}"; else ffrspname = "SUN "; }
                                            else if (ffrspmagic == "black") { if (ffrsptier > 1) ffrspname = $"GYS{ffrsptier}"; else ffrspname = "GYSR"; }
                                            else ffrspname = $"SOLO.NO_ELEM.DAMAGE";
                                            break;
                                        default: ffrspname = $"TARG{ffrSpellTarg}.NO_ELEM.DAMAGE"; break;
                                    }
                                    break;
                                #endregion NUKEandFADE
                                case 1: // Status
                                    switch (ffrSpellTarg)
                                    {
                                        case 1: if (ffrsptier > 1) ffrspname = $"PAN{ffrsptier}"; else ffrspname = "PAIN"; if (ffrsptier == 4) ffrspname = "ACHE"; break;
                                        case 2: if (ffrsptier > 1) ffrspname = $"HRT{ffrsptier}"; if (ffrsptier == 1) ffrspname = "HURT"; break;
                                        default: ffrspname = $"TARG{ffrSpellTarg}.STATUS.DAMAGE"; break;
                                    }
                                    break;
                                case 2: // Poison
                                    #region BILEandWAIL
                                    switch (ffrSpellTarg)
                                    {
                                        case 1:
                                            if (ffrspmagic == "white") { if (ffrsptier > 1) ffrspname = $"BEM{ffrsptier}"; else ffrspname = "BEAM"; if (ffrsptier == 4) ffrspname = "WAIL"; }
                                            else if (ffrspmagic == "black") { if (ffrsptier > 1) ffrspname = $"FUM{ffrsptier}"; else ffrspname = "FUME"; if (ffrsptier == 4) ffrspname = "BILE"; }
                                            else ffrspname = $"FIELD.POISON.DAMAGE";
                                            break;
                                        case 2:
                                            if (ffrspmagic == "white") { if (ffrsptier > 1) ffrspname = $"LAZ{ffrsptier}"; else ffrspname = "LAZR"; }
                                            else if (ffrspmagic == "black") { ffrspShapeByte = 200; if (ffrsptier > 1) ffrspname = $"VNM{ffrsptier}"; else ffrspname = "VENM"; }
                                            else ffrspname = $"SOLO.POISON.DAMAGE";
                                            break;
                                        default: ffrspname = $"TARG{ffrSpellTarg}.POISON.DAMAGE"; break;
                                    }
                                    break;
                                #endregion BILEandWAIL
                                case 4: // Time
                                    switch (ffrSpellTarg)
                                    {
                                        case 1: if (ffrsptier > 1) ffrspname = $"TIM{ffrsptier}"; else ffrspname = "TIME"; if (ffrsptier == 4) ffrspname = "KOMP"; break;
                                        case 2: if (ffrsptier > 1) ffrspname = $"SQZ{ffrsptier}"; if (ffrsptier == 1) ffrspname = "SQEZ"; break;
                                        default: ffrspname = $"TARG{ffrSpellTarg}.TIME.DAMAGE"; break;
                                    }
                                    break;
                                case 8: // Death
                                    switch (ffrSpellTarg)
                                    {
                                        case 1: if (ffrsptier > 1) ffrspname = $"NEC{ffrsptier}"; else ffrspname = "NECR"; if (ffrsptier == 4) ffrspname = "DOOM"; break;
                                        case 2: if (ffrsptier > 1) ffrspname = $"ROT{ffrsptier}"; if (ffrsptier == 1) ffrspname = "ROT "; break;
                                        default: ffrspname = $"TARG{ffrSpellTarg}.DEATH.DAMAGE"; break;
                                    }
                                    break;
                                case 16: // Fire
                                    switch (ffrSpellTarg)
                                    {
                                        case 1: if (ffrsptier > 1) ffrspname = $"FIR{ffrsptier}"; else ffrspname = "FIRE"; if (ffrsptier == 4) ffrspname = "BOIL"; break;
                                        case 2: if (ffrsptier > 1) ffrspname = $"BRN{ffrsptier}"; if (ffrsptier == 1) ffrspname = "BURN"; break;
                                        default: ffrspname = $"TARG{ffrSpellTarg}.FIRE.DAMAGE"; break;
                                    }
                                    break;
                                case 32: // Ice
                                    switch (ffrSpellTarg)
                                    {
                                        case 1: if (ffrsptier > 1) ffrspname = $"ICE{ffrsptier}"; else ffrspname = "ICE "; if (ffrsptier == 4) ffrspname = "HAIL"; break;
                                        case 2: if (ffrsptier > 1) ffrspname = $"SNO{ffrsptier}"; if (ffrsptier == 1) ffrspname = "SNOW"; break;
                                        default: ffrspname = $"TARG{ffrSpellTarg}.ICE.DAMAGE"; break;
                                    }
                                    break;
                                case 48: // Antipode
                                    switch (ffrSpellTarg)
                                    {
                                        case 1: if (ffrsptier > 1) ffrspname = $"POD{ffrsptier}"; else ffrspname = "PODE"; if (ffrsptier == 4) ffrspname = "ATOM"; break;
                                        case 2: if (ffrsptier > 1) ffrspname = $"HYD{ffrsptier}"; if (ffrsptier == 1) ffrspname = "HYDR"; break;
                                        default: ffrspname = $"TARG{ffrSpellTarg}.ICE.FIRE.DAMAGE"; break;
                                    }
                                    break;
                                case 64: // Lightning
                                    switch (ffrSpellTarg)
                                    {
                                        case 1: if (ffrsptier > 1) ffrspname = $"LIT{ffrsptier}"; else ffrspname = "LIT "; if (ffrsptier == 4) ffrspname = "VOLT"; break;
                                        case 2: if (ffrsptier > 1) ffrspname = $"ION{ffrsptier}"; if (ffrsptier == 1) ffrspname = "ION "; break;
                                        default: ffrspname = $"TARG{ffrSpellTarg}.LIGHTNING.DAMAGE"; break;
                                    }
                                    break;
                                case 65: // Kinetic
                                    switch (ffrSpellTarg)
                                    {
                                        case 1: if (ffrsptier > 1) ffrspname = $"WAV{ffrsptier}"; else ffrspname = "WAVE"; if (ffrsptier == 4) ffrspname = "YANK"; break;
                                        case 2: if (ffrsptier > 1) ffrspname = $"KIN{ffrsptier}"; if (ffrsptier == 1) ffrspname = "KIN "; break;
                                        default: ffrspname = $"TARG{ffrSpellTarg}.LIGHTNING.STATUS.DAMAGE"; break;
                                    }
                                    break;
                                case 80: // Plasma
                                    switch (ffrSpellTarg)
                                    {
                                        case 1: if (ffrsptier > 1) ffrspname = $"PLZ{ffrsptier}"; else ffrspname = "PLAZ"; if (ffrsptier == 4) ffrspname = "NOVA"; break;
                                        case 2: if (ffrsptier > 1) ffrspname = $"NRG{ffrsptier}"; if (ffrsptier == 1) ffrspname = "ENRG"; break;
                                        default: ffrspname = $"TARG{ffrSpellTarg}.LIGHTNING.FIRE.DAMAGE"; break;
                                    }
                                    break;
                                case 96: // Storm
                                    switch (ffrSpellTarg)
                                    {
                                        case 1: if (ffrsptier > 1) ffrspname = $"AIR{ffrsptier}"; else ffrspname = "AIR "; if (ffrsptier == 4) ffrspname = "GALE"; break;
                                        case 2: if (ffrsptier > 1) ffrspname = $"ARC{ffrsptier}"; if (ffrsptier == 1) ffrspname = "ARC "; break;
                                        default: ffrspname = $"TARG{ffrSpellTarg}.LIGHTNING.ICE.DAMAGE"; break;
                                    }
                                    break;
                                case 128: // Earth
                                    switch (ffrSpellTarg)
                                    {
                                        case 1: if (ffrsptier > 1) ffrspname = $"GEO{ffrsptier}"; else ffrspname = "GEO "; if (ffrsptier == 4) ffrspname = "GAIA"; break;
                                        case 2: if (ffrsptier > 1) ffrspname = $"LND{ffrsptier}"; if (ffrsptier == 1) ffrspname = "LAND"; break;
                                        default: ffrspname = $"TARG{ffrSpellTarg}.EARTH.DAMAGE"; break;
                                    }
                                    break;
                                case 144: // Magma
                                    switch (ffrSpellTarg)
                                    {
                                        case 1: if (ffrsptier > 1) ffrspname = $"LAV{ffrsptier}"; else ffrspname = "LAVA"; if (ffrsptier == 4) ffrspname = "RUPT"; break;
                                        case 2: if (ffrsptier > 1) ffrspname = $"ASH{ffrsptier}"; if (ffrsptier == 1) ffrspname = "ASH "; break;
                                        default: ffrspname = $"TARG{ffrSpellTarg}.EARTH.FIRE.DAMAGE"; break;
                                    }
                                    break;
                                default: ffrspname = $"ELEM{ffrSpellElem}.DAMAGE"; break;
                            }

                        }
                    }
                    #endregion DamageSpells
                    #region NegEffect
                    else if (ffrSpellType == 3)
                    {
                        if (ffrSpellEff == 1) ffrspShapeByte = (ffrSpellTarg == 1) ? 220 : 216;
                        if (ffrSpellEff == 2) ffrspShapeByte = (ffrSpellTarg == 1) ? 204 : 200;
                        switch (ffrSpellEff)
                        {
                            case 1: // Death
                                #region InstantDeath
                                switch (ffrSpellElem)
                                {
                                    case 0: // blank message
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "END "; break;
                                            case 2: ffrspname = "FALL"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.NON_ELEM.NEG_EFFECT.DEATH"; break;
                                        }
                                        break;
                                    case 2: // Poison smoke
                                        ffrspMsgByte = 0x4D;
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "BANE"; break;
                                            case 2: ffrspname = "CHOK"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.POISON.NEG_EFFECT.DEATH"; break;
                                        }
                                        break;
                                    case 4: // Exile to 4th Dimension (Time)
                                        ffrspMsgByte = 0x1F;
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "ZAP!"; break;
                                            case 2: ffrspname = "BYE!"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.TIME.NEG_EFFECT.DEATH"; break;
                                        }
                                        break;
                                    case 8: // Erased (Death)
                                        ffrspMsgByte = 0x15;
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "ERAD"; break;
                                            case 2: ffrspname = "RUB "; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.DEATH.NEG_EFFECT.DEATH"; break;
                                        }
                                        break;
                                    case 16: // Erased (Fire)
                                        ffrspMsgByte = 0x15;
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "EVAP"; break;
                                            case 2: ffrspname = "MELT"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.FIRE.NEG_EFFECT.DEATH"; break;
                                        }
                                        break;
                                    case 64: // Exile to 4th Dimension (Lightning)
                                        ffrspMsgByte = 0x1F;
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "BZZT"; break;
                                            case 2: ffrspname = "SMIT"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.LIGHTNING.NEG_EFFECT.DEATH"; break;
                                        }
                                        break;
                                    case 128: // Fell into crack
                                        ffrspMsgByte = 0x16;
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "QAKE"; break;
                                            case 2: ffrspname = "SWAL"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.EARTH.NEG_EFFECT.DEATH"; break;
                                        }
                                        break;
                                    default: ffrspname = $"ELEM{ffrSpellElem}.NEG_EFFECT.DEATH"; break;
                                }
                                break;
                            #endregion InstantDeath
                            case 2: // Stone
                                #region Petrification
                                switch (ffrSpellElem)
                                {
                                    case 2:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "WREK"; break;
                                            case 2: ffrspname = "BRAK"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.POISON.NEG_EFFECT.STONE"; break;
                                        }
                                        break;
                                    case 32:
                                        ffrspMsgByte = 0x4C;
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "FREZ"; break;
                                            case 2: ffrspname = "CRYO"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.ICE.NEG_EFFECT.STONE"; break;
                                        }
                                        break;
                                    default: ffrspname = $"ELEM{ffrSpellElem}.NEG_EFFECT.STONE"; break;
                                }
                                break;
                            #endregion Petrification
                            case 8: // Dark
                                #region Blind
                                switch (ffrSpellElem)
                                {
                                    case 0:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "COVR"; break;
                                            case 2: ffrspname = "VEIL"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.NON_ELEM.NEG_EFFECT.DARK"; break;
                                        }
                                        break;
                                    case 1:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: if (ffrspmagic == "white") { ffrspColorByte = 43; ffrspname = "GLOW"; } else if (ffrspmagic == "black") { ffrspColorByte = 45; ffrspname = "DARK"; } break;
                                            case 2: ffrspname = "DIM "; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.STATUS.NEG_EFFECT.DARK"; break;
                                        }
                                        break;
                                    case 2:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "HAZE"; break;
                                            case 2: ffrspname = "SMOG"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.POISON.NEG_EFFECT.DARK"; break;
                                        }
                                        break;
                                    case 4:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "VOID"; break;
                                            case 2: ffrspname = "BLUR"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.TIME.NEG_EFFECT.DARK"; break;
                                        }
                                        break;
                                    case 16:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "HUMD"; break;
                                            case 2: ffrspname = "PEPR"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.FIRE.NEG_EFFECT.DARK"; break;
                                        }
                                        break;
                                    case 128:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "TAR "; break;
                                            case 2: ffrspname = "SAND"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.EARTH.NEG_EFFECT.DARK"; break;
                                        }
                                        break;
                                    default: ffrspname = $"ELEM{ffrSpellElem}.NEG_EFFECT.DARK"; break;
                                }
                                break;
                            #endregion Blind
                            case 16: // Stun
                                #region Paralyze
                                ffrspMsgByte = 0x0D;
                                switch (ffrSpellElem)
                                {
                                    case 0:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "HALT"; break;
                                            case 2: ffrspname = "PIN "; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.NON_ELEM.NEG_EFFECT.STUN"; break;
                                        }
                                        break;
                                    case 1:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "BIND"; break;
                                            case 2: ffrspname = "HOLD"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.STATUS.NEG_EFFECT.STUN"; break;
                                        }
                                        break;
                                    case 2:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "RIG "; break;
                                            case 2: ffrspname = "SICK"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.POISON.NEG_EFFECT.STUN"; break;
                                        }
                                        break;
                                    case 4:
                                        ffrspMsgByte = 0x1E;
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "STOP"; break;
                                            case 2: ffrspname = "PAUS"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.TIME.NEG_EFFECT.STUN"; break;
                                        }
                                        break;
                                    case 32:
                                        ffrspMsgByte = 0x4C;
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "SQAL"; break;
                                            case 2: ffrspname = "ZERO"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.ICE.NEG_EFFECT.STUN"; break;
                                        }
                                        break;
                                    case 64:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "EMP "; break;
                                            case 2: ffrspname = "FRY "; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.LIGHTNING.NEG_EFFECT.STUN"; break;
                                        }
                                        break;
                                    case 128:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "VINE"; break;
                                            case 2: ffrspname = "SNAR"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.EARTH.NEG_EFFECT.STUN"; break;
                                        }
                                        break;
                                    default: ffrspname = $"ELEM{ffrSpellElem}.NEG_EFFECT.STUN"; break;
                                }
                                break;
                            #endregion Paralysis
                            case 32: // Asleep
                                #region Sleep
                                switch (ffrSpellElem)
                                {
                                    case 0:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "SLEP"; break;
                                            case 2: ffrspname = "NAP "; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.NON_ELEM.NEG_EFFECT.SLEEP"; break;
                                        }
                                        break;
                                    case 1:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "LULL"; break;
                                            case 2: ffrspname = "HYP "; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.STATUS.NEG_EFFECT.SLEEP"; break;
                                        }
                                        break;
                                    case 2:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "GAS "; break;
                                            case 2: ffrspname = "K.O."; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.POISON.NEG_EFFECT.SLEEP"; break;
                                        }
                                        break;
                                    case 4:
                                        ffrspMsgByte = 0x1E;
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "JUMP"; break;
                                            case 2: ffrspname = "SKIP"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.TIME.NEG_EFFECT.SLEEP"; break;
                                        }
                                        break;
                                    case 8:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "DEPR"; break;
                                            case 2: ffrspname = "PASS"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.DEATH.NEG_EFFECT.SLEEP"; break;
                                        }
                                        break;
                                    case 16:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "SOOT"; break;
                                            case 2: ffrspname = "COZY"; break;
                                            case 8: if (ffrsptier == 4) ffrsptier--; if (ffrsptier > 1) ffrspname = $"CMP{ffrsptier}"; else ffrspname = "CAMP"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.FIRE.NEG_EFFECT.SLEEP"; break;
                                        }
                                        break;
                                    case 32:
                                        ffrspMsgByte = 0x4C;
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "HIB "; break;
                                            case 2: ffrspname = "CHIL"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.ICE.NEG_EFFECT.SLEEP"; break;
                                        }
                                        break;
                                    case 128:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "WAFT"; break;
                                            case 2: ffrspname = "BED "; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.EARTH.NEG_EFFECT.SLEEP"; break;
                                        }
                                        break;
                                    default: ffrspname = $"ELEM{ffrSpellElem}.NEG_EFFECT.SLEEP"; break;
                                }
                                break;
                            #endregion Sleep
                            case 64: // Mute
                                #region Silence
                                switch (ffrSpellElem)
                                {
                                    case 0:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "QIET"; break;
                                            case 2: ffrspname = "HUSH"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.NON_ELEM.NEG_EFFECT.MUTE"; break;
                                        }
                                        break;
                                    case 1:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "MUTE"; break;
                                            case 2: ffrspname = "MUFF"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.STATUS.NEG_EFFECT.MUTE"; break;
                                        }
                                        break;
                                    case 2:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "FLU "; break;
                                            case 2: ffrspname = "COGH"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.POISON.NEG_EFFECT.MUTE"; break;
                                        }
                                        break;
                                    case 4:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "VACU"; break;
                                            case 2: ffrspname = "ECHO"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.TIME.NEG_EFFECT.MUTE"; break;
                                        }
                                        break;
                                    case 32:
                                        ffrspMsgByte = 0x4C;
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "HOWL"; break;
                                            case 2: ffrspname = "BREZ"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.ICE.NEG_EFFECT.MUTE"; break;
                                        }
                                        break;
                                    case 128:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "FUNL"; break;
                                            case 2: ffrspname = "MUDL"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.EARTH.NEG_EFFECT.MUTE"; break;
                                        }
                                        break;
                                    default: ffrspname = $"ELEM{ffrSpellElem}.NEG_EFFECT.MUTE"; break;
                                }
                                break;
                            #endregion Silence
                            case 120: // Cocktail
                                switch (ffrSpellElem)
                                {
                                    case 4: ffrspMsgByte = 0x1E; break;
                                    case 32: ffrspMsgByte = 0x4C; break;
                                    default: ffrspMsgByte = 0x00; break;
                                }
                                switch (ffrSpellTarg)
                                {
                                    case 1: ffrspname = "HEX "; break;
                                    case 2: ffrspname = "CURS"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.NEG_EFFECT.DARK_STUN_SLEEP_MUTE"; break;
                                }
                                break;
                            case 128: // Confuse
                                #region Confusion
                                switch (ffrSpellElem)
                                {
                                    case 0:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "HAVC"; break;
                                            case 2: ffrspname = "TURN"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.NON_ELEM.NEG_EFFECT.CONFUSE"; break;
                                        }
                                        break;
                                    case 1:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "CONF"; break;
                                            case 2: ffrspname = "TRIK"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.STATUS.NEG_EFFECT.CONFUSE"; break;
                                        }
                                        break;
                                    case 2:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "VIR "; break;
                                            case 2: ffrspname = "POX "; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.POISON.NEG_EFFECT.CONFUSE"; break;
                                        }
                                        break;
                                    case 4:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "SPIN"; break;
                                            case 2: ffrspname = "FLIP"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.TIME.NEG_EFFECT.CONFUSE"; break;
                                        }
                                        break;
                                    case 16: // "Ablaze" if "Frozen" is not present
                                        // ffrspMsgByte = 0x4C;
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "PYRO"; break;
                                            case 2: ffrspname = "TORC"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.FIRE.NEG_EFFECT.CONFUSE"; break;
                                        }
                                        break;
                                    case 64:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "RAVE"; break;
                                            case 2: ffrspname = "RAY "; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.LIGHTNING.NEG_EFFECT.CONFUSE"; break;
                                        }
                                        break;
                                    case 128:
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "MAZE"; break;
                                            case 2: ffrspname = "SEED"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.EARTH.NEG_EFFECT.CONFUSE"; break;
                                        }
                                        break;
                                    default: ffrspname = $"ELEM{ffrSpellElem}.NEG_EFFECT.CONFUSE"; break;
                                }
                                break;
                            #endregion Confusion
                            default:
                                if ((ffrSpellEff & 4) > 0) // Poison
                                #region Poisons
                                {
                                    ffrspMsgByte = 0x4D;
                                    switch (ffrSpellEff - 4)
                                    {
                                        case 8:
                                            switch (ffrSpellTarg)
                                            {
                                                case 1: ffrspname = "HAZE"; break;
                                                case 2: ffrspname = "SMOG"; break;
                                                default: ffrspname = $"TARG{ffrSpellTarg}.NEG_EFFECT.POISON_DARK"; break;
                                            }
                                            break;
                                        case 16:
                                            switch (ffrSpellTarg)
                                            {
                                                case 1: ffrspname = "RIG "; break;
                                                case 2: ffrspname = "SICK"; break;
                                                default: ffrspname = $"TARG{ffrSpellTarg}.NEG_EFFECT.POISON_STUN"; break;
                                            }
                                            break;
                                        case 32:
                                            switch (ffrSpellTarg)
                                            {
                                                case 1: ffrspname = "GAS "; break;
                                                case 2: ffrspname = "K.O."; break;
                                                default: ffrspname = $"TARG{ffrSpellTarg}.NEG_EFFECT.POISON_SLEEP"; break;
                                            }
                                            break;
                                        case 64:
                                            switch (ffrSpellTarg)
                                            {
                                                case 1: ffrspname = "FLU "; break;
                                                case 2: ffrspname = "COGH"; break;
                                                default: ffrspname = $"TARG{ffrSpellTarg}.NEG_EFFECT.POISON_MUTE"; break;
                                            }
                                            break;
                                        case 128:
                                            switch (ffrSpellTarg)
                                            {
                                                case 1: ffrspname = "VIR "; break;
                                                case 2: ffrspname = "POX "; break;
                                                default: ffrspname = $"TARG{ffrSpellTarg}.NEG_EFFECT.POISON_CONFUSE"; break;
                                            }
                                            break;
                                        default: ffrspname = $"NEG_EFFECT.POISON_EFF{ffrSpellEff}"; break;
                                    }
                                    break;
                                }
                                #endregion Poisons
                                else ffrspname = $"NEG_EFF{ffrSpellEff}"; break;
                        }
                    }
                    #endregion NegEffect
                    #region AttackRateDown
                    else if (ffrSpellType == 4)
                    {
                        ffrspMsgByte = 0x0B;
                        switch (ffrSpellElem)
                        {
                            case 0:
                                switch (ffrSpellTarg)
                                {
                                    case 1: ffrspname = "SLOW"; break;
                                    case 2: ffrspname = "TIRE"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.NON_ELEM.ATTACK_DOWN"; break;
                                }
                                break;
                            case 1:
                                switch (ffrSpellTarg)
                                {
                                    case 1: ffrspname = "DULL"; break;
                                    case 2: ffrspname = "HMPR"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.STATUS.ATTACK_DOWN"; break;
                                }
                                break;
                            case 2:
                                switch (ffrSpellTarg)
                                {
                                    case 1: ffrspname = "MIST"; ffrspColorByte = 32; break;
                                    case 2: ffrspname = "NUMB"; ffrspColorByte = 35; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.POISON.ATTACK_DOWN"; break;
                                }
                                break;
                            case 4:
                                switch (ffrSpellTarg)
                                {
                                    case 1: ffrspname = "BACK"; break;
                                    case 2: ffrspname = "AGE "; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.TIME.ATTACK_DOWN"; break;
                                }
                                break;
                            case 8:
                                switch (ffrSpellTarg)
                                {
                                    case 1: ffrspname = "MIAS"; break;
                                    case 2: ffrspname = "ATRO"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.DEATH.ATTACK_DOWN"; break;
                                }
                                break;
                            case 32:
                                switch (ffrSpellTarg)
                                {
                                    case 1: ffrspname = "COLD"; break;
                                    case 2: ffrspname = "BRRR"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.NON_ELEM.ATTACK_DOWN"; break;
                                }
                                break;
                            case 128:
                                switch (ffrSpellTarg)
                                {
                                    case 1: ffrspname = "BOG "; break;
                                    case 2: ffrspname = "SAP "; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.NON_ELEM.ATTACK_DOWN"; break;
                                }
                                break;
                            default: ffrspname = $"ELEM{ffrSpellElem}.ATTACK_DOWN"; break;
                        }
                    }
                    #endregion AttackRateDown
                    #region LowerMorale
                    else if (ffrSpellType == 5)
                    {
                        ffrspMsgByte = 0x0F;
                        switch (ffrSpellElem)
                        {
                            case 0:
                                switch (ffrSpellTarg)
                                {
                                    case 1: if (ffrsptier > 1) ffrspname = $"RPL{ffrsptier}"; else ffrspname = "REPL"; break;
                                    case 2: if (ffrsptier > 1) ffrspname = $"PSH{ffrsptier}"; if (ffrsptier == 1) ffrspname = "PUSH"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.NON_ELEM.LOWER_MORALE"; break;
                                }
                                break;
                            case 1:
                                switch (ffrSpellTarg)
                                {
                                    case 1: if (ffrsptier > 1) ffrspname = $"FER{ffrsptier}"; else ffrspname = "FEAR"; break;
                                    case 2: if (ffrsptier > 1) ffrspname = $"ANX{ffrsptier}"; if (ffrsptier == 1) ffrspname = "ANX "; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.STATUS.LOWER_MORALE"; break;
                                }
                                break;
                            case 4:
                                switch (ffrSpellTarg)
                                {
                                    case 1: if (ffrsptier > 1) ffrspname = $"GRO{ffrsptier}"; else ffrspname = "GROW"; break;
                                    case 2: if (ffrsptier > 1) ffrspname = $"SIZ{ffrsptier}"; if (ffrsptier == 1) ffrspname = "SIZE"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.TIME.LOWER_MORALE"; break;
                                }
                                break;
                            case 8:
                                switch (ffrSpellTarg)
                                {
                                    case 1: if (ffrsptier > 1) ffrspname = $"PES{ffrsptier}"; else ffrspname = "PESS"; break;
                                    case 2: if (ffrsptier > 1) ffrspname = $"HNT{ffrsptier}"; if (ffrsptier == 1) ffrspname = "HANT"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.DEATH.LOWER_MORALE"; break;
                                }
                                break;
                            case 16:
                                switch (ffrSpellTarg)
                                {
                                    case 1: if (ffrsptier > 1) ffrspname = $"IGN{ffrsptier}"; else ffrspname = "IGNI"; break;
                                    case 2: if (ffrsptier > 1) ffrspname = $"STV{ffrsptier}"; if (ffrsptier == 1) ffrspname = "STAV"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.FIRE.LOWER_MORALE"; break;
                                }
                                break;
                            case 128:
                                switch (ffrSpellTarg)
                                {
                                    case 1: if (ffrsptier > 1) ffrspname = $"GAP{ffrsptier}"; else ffrspname = "GAPE"; break;
                                    case 2: if (ffrsptier > 1) ffrspname = $"TER{ffrsptier}"; if (ffrsptier == 1) ffrspname = "TERR"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.EARTH.LOWER_MORALE"; break;
                                }
                                break;
                            default: ffrspname = $"ELEM{ffrSpellElem}.LOWER_MORALE"; break;
                        }
                    }
                    #endregion LowerMorale
                    #region HealthUp
                    else if (ffrSpellType == 7)
                    {
                        ffrspMsgByte = 0x01;
                        switch (ffrSpellTarg)
                        {
                            case 4: if (ffrsptier > 1) ffrspname = $"RGN{ffrsptier}"; if (ffrsptier == 1) ffrspname = "REGN"; break;
                            case 8: if (ffrsptier > 1) ffrspname = $"HEL{ffrsptier}"; else ffrspname = "HEAL"; break;
                            case 16: if (ffrsptier > 1) ffrspname = $"CUR{ffrsptier}"; else ffrspname = "CURE"; break;
                            default: ffrspname = $"TARG{ffrSpellTarg}.HP_UP"; break;
                        }
                    }
                    #endregion HealthUp
                    #region PosEffect
                    else if (ffrSpellType == 8)
                    {
                        if (ffrSpellEff == 255) ffrspShapeByte = (ffrSpellTarg == 8) ? 192 : 188;
                        switch (ffrSpellEff)
                        {
                            case 1:
                                switch (ffrSpellTarg)
                                {
                                    case 2: ffrspname = "UNDO"; break;
                                    case 8: ffrspname = "LIFa"; break;
                                    case 16: ffrspname = "LIFE"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.POS_EFFECT.DEATH"; break;
                                }
                                break;
                            case 2:
                                switch (ffrSpellTarg)
                                {
                                    case 8: ffrspname = "SOFa"; break;
                                    case 16: ffrspname = "SOFT"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.POS_EFFECT.STONE"; break;
                                }
                                break;
                            case 4 or 20 or 132 or 148:
                                switch (ffrSpellTarg)
                                {
                                    case 4: ffrspname = "REST"; break;
                                    case 8: ffrspname = "PURa"; break;
                                    case 16: ffrspname = "PURE"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.POS_EFFECT.POISON"; break;
                                }
                                break;
                            case 8 or 24:
                                switch (ffrSpellTarg)
                                {
                                    case 4: ffrspname = "WASH"; break;
                                    case 8: ffrspname = "LMPa"; break;
                                    case 16: ffrspname = "LAMP"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.POS_EFFECT.DARK"; break;
                                }
                                break;
                            case 16:
                                switch (ffrSpellTarg)
                                {
                                    case 8: ffrspname = "MNDa"; break;
                                    case 16: ffrspname = "MEND"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.POS_EFFECT.STUN"; break;
                                }
                                break;
                            case 32 or 48:
                                switch (ffrSpellTarg)
                                {
                                    case 8: ffrspname = "WAKa"; break;
                                    case 16: ffrspname = "WAKE"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.POS_EFFECT.SLEEP"; break;
                                }
                                break;
                            case 64 or 80:
                                switch (ffrSpellTarg)
                                {
                                    case 8: ffrspname = "VOXa"; break;
                                    case 16: ffrspname = "VOX "; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.POS_EFFECT.MUTE"; break;
                                }
                                break;
                            case 128 or 144:
                                switch (ffrSpellTarg)
                                {
                                    case 8: ffrspname = "TAMa"; break;
                                    case 16: ffrspname = "TAME"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.POS_EFFECT.CONFUSE"; break;
                                }
                                break;
                            case 252:
                                switch (ffrSpellTarg)
                                {
                                    case 4: ffrspname = "BATH"; break;
                                    case 8: ffrspname = "CLRa"; break;
                                    case 16: ffrspname = "CLER"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.POS_EFFECT.ALL"; break;
                                }
                                break;
                            case 255:
                                switch (ffrSpellTarg)
                                {
                                    case 8: ffrspname = "LIFx"; break;
                                    case 16: ffrspname = "LIF2"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.POS_EFFECT.DEATH.STONE"; break;
                                }
                                break;
                            default: ffrspname = $"POS_EFFECT.EFF{ffrSpellEff}"; break;
                        }
                    }
                    #endregion PosEffect
                    #region ArmorUp
                    else if (ffrSpellType == 9)
                    {
                        ffrspMsgByte = 0x02;
                        switch (ffrSpellTarg)
                        {
                            case 4: if (ffrsptier > 1) ffrspname = $"BUK{ffrsptier}"; if (ffrsptier == 1) ffrspname = "BUKL"; break;
                            case 8: if (ffrsptier > 1) ffrspname = $"FOG{ffrsptier}"; else ffrspname = "FOG "; break;
                            case 16: if (ffrsptier > 1) ffrspname = $"ARM{ffrsptier}"; else ffrspname = "ARM "; break;
                            default: ffrspname = $"TARG{ffrSpellTarg}.ARMOR_UP"; break;
                        }
                    }
                    #endregion ArmorUp
                    #region Resists
                    else if (ffrSpellType == 10)
                    {
                        switch (ffrspCurrentResist)
                        {
                            case 1: ffrspMsgByte = 0x08; break;
                            case 2: ffrspMsgByte = 0x0C; break;
                            case 3: ffrspMsgByte = 0x10; break;
                            case 4: ffrspMsgByte = 0x19; break;
                            case 5: ffrspMsgByte = 0x1C; break;
                        }
                        switch (ffrSpellEff)
                        {
                            case 1:
                                switch (ffrSpellTarg)
                                {
                                    case 4: ffrspname = "GRAC"; break;
                                    case 8: ffrspname = "AWEK"; break;
                                    case 16: ffrspname = "BRAC"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.RESIST.STATUS"; break;
                                }
                                break;
                            case 2:
                                switch (ffrSpellTarg)
                                {
                                    case 4: ffrspname = "IMUN"; break;
                                    case 8: ffrspname = "ABAN"; break;
                                    case 16: ffrspname = "VAXX"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.RESIST.POISON"; break;
                                }
                                break;
                            case 4:
                                switch (ffrSpellTarg)
                                {
                                    case 4: ffrspname = "PHAS"; break;
                                    case 8: ffrspname = "AZAP"; break;
                                    case 16: ffrspname = "XCEP"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.RESIST.TIME"; break;
                                }
                                break;
                            case 8:
                                switch (ffrSpellTarg)
                                {
                                    case 4: ffrspname = "AURA"; break;
                                    case 8: ffrspname = "ANEC"; break;
                                    case 16: ffrspname = "SAFE"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.RESIST.DEATH"; break;
                                }
                                break;
                            case 14: // Decay
                                switch (ffrSpellTarg)
                                {
                                    case 4: ffrspname = "STAG"; break;
                                    case 8: ffrspname = "ATOX"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.RESIST.POISON_TIME_DEATH"; break;
                                }
                                break;
                            case 16:
                                switch (ffrSpellTarg)
                                {
                                    case 4: ffrspname = "COOL"; break;
                                    case 8: ffrspname = "AFIR"; break;
                                    case 16: ffrspname = "DOUS"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.RESIST.FIRE"; break;
                                }
                                break;
                            case 32:
                                switch (ffrSpellTarg)
                                {
                                    case 4: ffrspname = "WARM"; break;
                                    case 8: ffrspname = "AICE"; break;
                                    case 16: ffrspname = "COAT"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.RESIST.ICE"; break;
                                }
                                break;
                            case 64:
                                switch (ffrSpellTarg)
                                {
                                    case 4: ffrspname = "SURG"; break;
                                    case 8: ffrspname = "ALIT"; break;
                                    case 16: ffrspname = "OHM "; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.RESIST.LIGHTNING"; break;
                                }
                                break;
                            case 112: // Spell
                                switch (ffrSpellTarg)
                                {
                                    case 4: ffrspname = "PRSM"; break;
                                    case 8: ffrspname = "ADMG"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.RESIST.FIRE_ICE_LIGHTNING"; break;
                                }
                                break;
                            case 128:
                                switch (ffrSpellTarg)
                                {
                                    case 4: ffrspname = "FLOT"; break;
                                    case 8: ffrspname = "AQAK"; break;
                                    case 16: ffrspname = "LIFT"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.RESIST.EARTH"; break;
                                }
                                break;
                            case 137: // Magic
                                switch (ffrSpellTarg)
                                {
                                    case 4: ffrspname = "PROT"; break;
                                    case 8: ffrspname = "ARUB"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.RESIST.STATUS_DEATH_EARTH"; break;
                                }
                                break;
                            case 255: // All
                                switch (ffrSpellTarg)
                                {
                                    case 4: ffrspname = "BARR"; break;
                                    case 8: ffrspname = "SANC"; break;
                                    case 16: ffrspname = "WALL"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.RESIST.ALL"; break;
                                }
                                break;
                            default: ffrspname = $"RESIST.ELEM{ffrSpellEff}"; break;
                        }
                    }
                    #endregion Resists
                    #region DoubleHits
                    else if (ffrSpellType == 12)
                    {
                        ffrspMsgByte = 0x12;
                        var funnySpeed = new Random();
                        switch (ffrSpellTarg)
                        {
                            case 4: if (ffrspmagic == "white") ffrspname = "HAST"; if (ffrspmagic == "black") { ffrspname = (funnySpeed.Next(1, 51) == 50) ? "BRSK" : "BSRK"; } break;
                            case 8: ffrspname = "FURY"; break;
                            case 16: ffrspname = "FAST"; break;
                            default: ffrspname = $"TARG{ffrSpellTarg}.DOUBLE_HITS"; break;
                        }
                    }
                    #endregion DoubleHits
                    #region WeaponsStronger
                    else if (ffrSpellType == 13)
                    {
                        switch (ffrspmagic)
                        {
                            case "white":
                                ffrspMsgByte = 0x1B;
                                switch (ffrSpellTarg)
                                {
                                    case 4: if (ffrsptier > 1) ffrspname = $"FOC{ffrsptier}"; if (ffrsptier == 1) ffrspname = "FOCS"; break;
                                    case 8: if (ffrsptier > 1) ffrspname = $"VIG{ffrsptier}"; else ffrspname = "VIGR"; break;
                                    case 16: if (ffrsptier > 1) ffrspname = $"BLS{ffrsptier}"; else ffrspname = "BLES"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.ENCHANT"; break;
                                }
                                break;
                            case "black":
                                ffrspMsgByte = 0x0A;
                                switch (ffrSpellTarg)
                                {
                                    case 4: if (ffrsptier > 1) ffrspname = $"SAB{ffrsptier}"; if (ffrsptier == 1) ffrspname = "SABR"; break;
                                    case 8: if (ffrsptier > 1) ffrspname = $"RAL{ffrsptier}"; else ffrspname = "RALY"; break;
                                    case 16: if (ffrsptier > 1) ffrspname = $"TMP{ffrsptier}"; else ffrspname = "TMPR"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.STRENGTHEN"; break;
                                }
                                break;
                            default: ffrspname = $"{ffrspmagic}.BOOST"; break;
                        }
                    }
                    #endregion WeaponsStronger
                    #region EvadeDown
                    else if (ffrSpellType == 14)
                    {
                        ffrspMsgByte = 0x05;
                        switch (ffrSpellElem)
                        {
                            case 0:
                                switch (ffrSpellTarg)
                                {
                                    case 1: if (ffrsptier > 1) ffrspname = $"SEL{ffrsptier}"; else ffrspname = "SEAL"; break;
                                    case 2: if (ffrsptier > 1) ffrspname = $"LOK{ffrsptier}"; else ffrspname = "LOCK"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.NON_ELEM.EASY_HIT"; break;
                                }
                                break;
                            case 1:
                                switch (ffrSpellTarg)
                                {
                                    case 1: if (ffrsptier > 1) ffrspname = $"NET{ffrsptier}"; else ffrspname = "NET "; break;
                                    case 2: if (ffrsptier > 1) ffrspname = $"CAG{ffrsptier}"; else ffrspname = "CAGE"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.STATUS.EASY_HIT"; break;
                                }
                                break;
                            case 4:
                                switch (ffrSpellTarg)
                                {
                                    case 1: if (ffrsptier > 1) ffrspname = $"GRV{ffrsptier}"; else ffrspname = "GRAV"; break;
                                    case 2: if (ffrsptier > 1) ffrspname = $"WEL{ffrsptier}"; else ffrspname = "WELL"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.TIME.EASY_HIT"; break;
                                }
                                break;
                            case 32:
                                switch (ffrSpellTarg)
                                {
                                    case 1: if (ffrsptier > 1) ffrspname = $"GLC{ffrsptier}"; else ffrspname = "GLAC"; break;
                                    case 2: if (ffrsptier > 1) ffrspname = $"FLO{ffrsptier}"; else ffrspname = "FLOE"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.ICE.EASY_HIT"; break;
                                }
                                break;
                            case 64:
                                switch (ffrSpellTarg)
                                {
                                    case 1: if (ffrsptier > 1) ffrspname = $"WIR{ffrsptier}"; else ffrspname = "WIRE"; break;
                                    case 2: if (ffrsptier > 1) ffrspname = $"BOL{ffrsptier}"; else ffrspname = "BOLT"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.LIGHTNING.EASY_HIT"; break;
                                }
                                break;
                            case 128:
                                switch (ffrSpellTarg)
                                {
                                    case 1: if (ffrsptier > 1) ffrspname = $"OUB{ffrsptier}"; else ffrspname = "OUBL"; break;
                                    case 2: if (ffrsptier > 1) ffrspname = $"PIT{ffrsptier}"; else ffrspname = "PIT "; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.EARTH.EASY_HIT"; break;
                                }
                                break;
                            default: ffrspname = $"ELEM{ffrSpellElem}.EASY_HIT"; break;
                        }
                    }
                    #endregion EvadeDown
                    #region MaxHP
                    else if (ffrSpellType == 15)
                    {
                        ffrspMsgByte = 0x18;
                        switch (ffrspellbinding)
                        {
                            case true:
                                switch (ffrSpellTarg)
                                {
                                    case 4: ffrspname = "RGN4"; break;
                                    case 8: ffrspname = "HEL4"; break;
                                    case 16: ffrspname = "CUR4"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.MAX_HP"; break;
                                }
                                break;
                            case false:
                                switch (ffrSpellTarg)
                                {
                                    case 4: ffrspname = "FIX "; break;
                                    case 8: ffrspname = "MAXa"; break;
                                    case 16: ffrspname = "MAX "; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.MAX_HP"; break;
                                }
                                break;
                        }
                    }
                    #endregion MaxHP
                    #region EvadeUp
                    else if (ffrSpellType == 16)
                    {
                        ffrspMsgByte = 0x03;
                        switch (ffrSpellTarg)
                        {
                            case 4: if (ffrsptier > 1) ffrspname = $"RUS{ffrsptier}"; if (ffrsptier == 1) ffrspname = "RUSE"; break;
                            case 8: if (ffrsptier > 1) ffrspname = $"INV{ffrsptier}"; else ffrspname = "INVS"; break;
                            case 16: if (ffrsptier > 1) ffrspname = $"HID{ffrsptier}"; else ffrspname = "HIDE"; break;
                            default: ffrspname = $"TARG{ffrSpellTarg}.EVADE_UP"; break;
                        }
                    }
                    #endregion EvadeUp
                    #region RemoveResists
                    else if (ffrSpellType == 17)
                    {
                        ffrspMsgByte = 0x1D;
                        switch (ffrSpellElem)
                        {
                            case 0:
                                switch (ffrSpellTarg)
                                {
                                    case 1: if (ffrspmagic == "white") ffrspname = $"XPOS"; if (ffrspmagic == "black") ffrspname = "PURG"; break;
                                    case 2: if (ffrspmagic == "white") ffrspname = $"XFER"; if (ffrspmagic == "black") ffrspname = "VULN"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.NON_ELEM.REMOVE_RESISTS"; break;
                                }
                                break;
                            case 1:
                                switch (ffrSpellTarg)
                                {
                                    case 1: ffrspname = "NEUT"; break;
                                    case 2: ffrspname = "NFIL"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.STATUS.REMOVE_RESISTS"; break;
                                }
                                break;
                            case 4:
                                switch (ffrSpellTarg)
                                {
                                    case 1: ffrspname = "NULL"; break;
                                    case 2: ffrspname = "SET "; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.TIME.REMOVE_RESISTS"; break;
                                }
                                break;
                            case 8:
                                switch (ffrSpellTarg)
                                {
                                    case 1: ffrspname = "WIPE"; break;
                                    case 2: ffrspname = "DSPL"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.DEATH.REMOVE_RESISTS"; break;
                                }
                                break;
                            case 64:
                                switch (ffrSpellTarg)
                                {
                                    case 1: ffrspname = "NEG "; break;
                                    case 2: ffrspname = "RIP "; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.LIGHTNING.REMOVE_RESISTS"; break;
                                }
                                break;
                            case 128:
                                switch (ffrSpellTarg)
                                {
                                    case 1: ffrspname = "DRAN"; break;
                                    case 2: ffrspname = "PIRC"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.EARTH.REMOVE_RESISTS"; break;
                                }
                                break;
                            default: ffrspname = $"ELEM{ffrSpellElem}.REMOVE_RESISTS"; break;
                        }
                    }
                    #endregion RemoveResists
                    #region PowerWord
                    else if (ffrSpellType == 18)
                    {
                        if (ffrSpellElem == 32) ffrspMsgByte = 0x4C;
                        switch (ffrSpellEff)
                        {
                            case 1:
                                if (ffrspmagic == "white") ffrspname = "FROG"; ffrspMsgByte = 0x2F;
                                if (ffrspmagic == "black")
                                {
                                    ffrspname = "XXXX";
                                    switch (ffrSpellElem)
                                    {
                                        case 2: ffrspMsgByte = 0x4D; break;
                                        case 4 or 64: ffrspMsgByte = 0x1F; break;
                                        case 8 or 16: ffrspMsgByte = 0x15; break;
                                        case 128: ffrspMsgByte = 0x16; break;
                                    }
                                }
                                break;
                            case 2: if (ffrspmagic == "white") ffrspname = "CAST"; if (ffrspmagic == "black") ffrspname = "PETR"; break;
                            case 8: ffrspname = "BLND"; break;
                            case 16: ffrspname = "STUN"; if (ffrSpellElem == 4) ffrspMsgByte = 0x1E; break;
                            case 32: ffrspname = "ZZZZ"; if (ffrSpellElem == 4) ffrspMsgByte = 0x1E; break;
                            case 64: ffrspname = "JINX"; break;
                            case 96: ffrspname = "COMA"; if (ffrSpellElem == 4) ffrspMsgByte = 0x1E; break;
                            case 124: ffrspname = "WEAK"; if (ffrSpellElem == 4) ffrspMsgByte = 0x1E; break;
                            case 128: ffrspname = "FOOL"; break;
                            case 132: ffrspname = "DCAY"; break;
                            default: ffrspname = $"POWER_WORD.EFF{ffrSpellEff}"; break;
                        }
                    }
                    #endregion PowerWord
                    else ffrspname = ffrSpell;
                    #endregion Library

                    #region TargetName
                    switch (ffrSpellTarg)
                    {
                        case 1: ffrsptarg = "All Enemies"; break;
                        case 2: ffrsptarg = "One Enemy"; break;
                        case 4: ffrsptarg = "the Caster"; break;
                        case 8: ffrsptarg = "the Whole Party"; break;
                        case 16: ffrsptarg = "a Single Ally"; break;
                        default: ffrsptarg = "an Unknown Destination"; break;
                    }
                    #endregion TargetName
                    #region ElementName
                    switch (ffrSpellElem)
                    {
                        case 0: ffrspelem = "Raw Magic"; break;
                        case 1: ffrspelem = "Status"; break;
                        case 2: ffrspelem = "Poison"; if (ffrspmagic == "white") ffrspelem = "Stone"; break;
                        case 4: ffrspelem = "Time"; break;
                        case 8: ffrspelem = "Death"; break;
                        case 16: ffrspelem = "Fire"; break;
                        case 32: ffrspelem = "Ice"; break;
                        case 48: ffrspelem = "Antipode (Fire and Ice)"; break;
                        case 64: ffrspelem = "Lightning"; break;
                        case 65: ffrspelem = "Kinetic (Status and Lightning)"; break;
                        case 80: ffrspelem = "Plasma (Fire and Lightning)"; break;
                        case 96: ffrspelem = "Storm (Ice and Lightning)"; break;
                        case 128: ffrspelem = "Earth"; break;
                        case 131: ffrspelem = "Becoming a Frog (Status, Poison, Earth)"; break;
                        case 144: ffrspelem = "Magma (Fire and Earth)"; break;
                        default: ffrspelem = "Undefined"; break;
                    }
                    #endregion ElementName
                    #region EffectName
                    switch (ffrSpellType)
                    {
                        case 3 or 18:
                            switch (ffrSpellEff)
                            {
                                case 1: ffrspeff = $"{ffrspelem} Instantly Kills {ffrsptarg}"; break;
                                case 2: if (ffrSpellElem == 2) ffrspelem = "Stone"; ffrspeff = $"Petrifies {ffrsptarg} with {ffrspelem}"; break;
                                case 8 or 12: ffrspeff = $"Blinds {ffrsptarg} with {ffrspelem}"; break;
                                case 16 or 20: ffrspeff = $"Paralyzes {ffrsptarg} with {ffrspelem}"; break;
                                case 32 or 36: ffrspeff = $"Pacifies {ffrsptarg} with {ffrspelem}"; break;
                                case 64 or 68: ffrspeff = $"Silences {ffrsptarg} with {ffrspelem}"; break;
                                case 96: ffrspeff = $"Pacifies and Silences {ffrsptarg} with {ffrspelem}"; break;
                                case 120: ffrspeff = $"Blinds, Paralyzes, Pacifies, and Silences {ffrsptarg} with {ffrspelem}"; break;
                                case 128 or 132: ffrspeff = $"Confuses {ffrsptarg} with {ffrspelem}"; break;
                                default: ffrspeff = $"Indeterminate effect on {ffrsptarg}"; break;
                            }
                            break;
                        case 8:
                            string ffrspmend = /* (PowerOf2.Divide(ffrSpellEff - 16) == true && ffrSpellEff != 32) */ ((ffrSpellEff & 16) > 0) ? " and Paralysis" : "";
                            switch (ffrSpellEff)
                            {
                                case 1: ffrspeff = $"Revives {ffrsptarg} to 1 HP if fallen"; break;
                                case 2: ffrspeff = $"Softens {ffrsptarg} if Petrified"; break;
                                case 4 or 20: ffrspeff = $"Removes Poison{ffrspmend} from {ffrsptarg}"; break;
                                case 8 or 24: ffrspeff = $"Removes Blindness{ffrspmend} from {ffrsptarg}"; break;
                                case 16: ffrspeff = $"Removes Paralysis from {ffrsptarg}"; break;
                                case 32 or 48: ffrspeff = $"Wakes {ffrsptarg} from Sleep{ffrspmend}"; break;
                                case 64 or 80: ffrspeff = $"Removes Silence{ffrspmend} from {ffrsptarg}"; break;
                                case 128 or 144: ffrspeff = $"Snaps {ffrsptarg} out of Confusion{ffrspmend}"; break;
                                case 132 or 148: ffrspeff = $"Cures {ffrsptarg} of Poison and Confusion{ffrspmend}"; break;
                                case 252: ffrspeff = $"Removes all non-lethal ailments from {ffrsptarg}"; break;
                                case 255: ffrspeff = $"Revives {ffrsptarg} to full health if fallen"; break;
                                default: ffrspeff = $"Questionable remedy"; break;
                            }
                            break;
                        case 10:
                            switch (ffrSpellEff)
                            {
                                case 1: ffrspelem = "Status"; break;
                                case 2: ffrspelem = "Poison"; break;
                                case 4: ffrspelem = "Time"; break;
                                case 8: ffrspelem = "Death"; break;
                                case 14: ffrspelem = "Decay (Poison, Time, Death)"; break;
                                case 16: ffrspelem = "Fire"; break;
                                case 32: ffrspelem = "Ice"; break;
                                case 64: ffrspelem = "Lightning"; break;
                                case 112: ffrspelem = "Arcane (Fire, Ice, Lightning)"; break;
                                case 128: ffrspelem = "Earth"; break;
                                case 137: ffrspelem = "Life (Status, Death, Earth)"; break;
                                case 255: ffrspelem = "Full Elemental"; break;
                                default: ffrspelem = "Undefined"; break;
                            }
                            break;
                    }
                    #endregion EffectName
                    #region TypeName
                    switch (ffrSpellType)
                    {
                        case 1: ffrsptype = $"{ffrSpellEff} {ffrspelem} Damage to {ffrsptarg}. +{ffrSpellAcc} Crit Chance. "; break;
                        case 2: ffrsptype = $"{ffrSpellEff} Prejudicial Damage to {ffrsptarg}. +{ffrSpellAcc} Crit Chance. "; break;
                        case 3: ffrsptype = $"{ffrspeff}. +{ffrSpellAcc} Chance to Hit. "; if ((ffrSpellEff & 4) > 0) ffrsptype += "Venomous. "; break;
                        case 4: ffrsptype = $"{ffrspelem}-based Slowdown on {ffrsptarg}. +{ffrSpellAcc} Chance to Hit. "; break;
                        case 5: ffrsptype = $"{ffrSpellEff} {ffrspelem} Intimidation on {ffrsptarg}. +{ffrSpellAcc} Chance to Hit. "; break;
                        case 7: ffrsptype = $"{ffrSpellEff} Health to {ffrsptarg}. "; break;
                        case 8: ffrsptype = $"{ffrspeff}. "; break;
                        case 9: ffrsptype = $"{ffrSpellEff} Armor Boost for {ffrsptarg}. "; break;
                        case 10: ffrsptype = $"Grants {ffrspelem} Resistance to {ffrsptarg}. "; break;
                        case 12: ffrsptype = $"Doubles the Attack Rate of {ffrsptarg}. "; break;
                        case 13:
                            if (ffrSpellAcc != 0) ffrsptype = $"Improves {ffrsptarg}'s Melee Damage by {ffrSpellEff} and Hit Rate by {ffrSpellAcc}. ";
                            else ffrsptype = $"Adds {ffrSpellEff} Damage to {ffrsptarg}'s Melee Attacks. ";
                            break;
                        case 14: ffrsptype = $"Uses {ffrspelem} to Reduce the Evasion of {ffrsptarg} by {ffrSpellEff}. +{ffrSpellAcc} Chance to Hit. "; break;
                        case 15: ffrsptype = $"Brings {ffrsptarg} back to full health. "; break;
                        case 16: ffrsptype = $"Improves the Evasion of {ffrsptarg} by {ffrSpellEff}. "; break;
                        case 17: ffrsptype = $"Uses {ffrspelem} to Remove all Resistance from {ffrsptarg}. +{ffrSpellAcc} Chance to Hit. "; break;
                        case 18: ffrsptype = $"{ffrspeff}, if vulnerable. "; if ((ffrSpellEff & 4) > 0) ffrsptype += "Venomous. "; break;
                        default: ffrsptype = $"Unspecified spell. "; break;
                    }
                    #endregion TypeName
                    #region ShapeName
                    switch (ffrspShapeByte)
                    {
                        case 176: ffrspgfxshape = "Bar of Light"; break;
                        case 180: ffrspgfxshape = "Twinkling Bar"; break;
                        case 184: ffrspgfxshape = "Twinkles"; break;
                        case 188: ffrspgfxshape = "Merging Stars"; break;
                        case 192: ffrspgfxshape = "Stars"; break;
                        case 196: ffrspgfxshape = "Sparkling Bolt"; break;
                        case 200: ffrspgfxshape = "Energy Bolt"; break;
                        case 204: ffrspgfxshape = "Flaring Bolt"; break;
                        case 208: ffrspgfxshape = "Energy Flare"; break;
                        case 212: ffrspgfxshape = "Fizzling Flare"; break;
                        case 216: ffrspgfxshape = "Glowing Ball"; break;
                        case 220: ffrspgfxshape = "Bursting Ball"; break;
                        case 224: ffrspgfxshape = "Magic Burst"; break;
                        case 228: ffrspgfxshape = "Directed Burst"; break;
                        case 232: ffrspgfxshape = "Palm Blast"; break;
                        default: ffrspgfxshape = "Accident"; break;
                    }
                    #endregion ShapeName
                    #region ColorName
                    switch (ffrspColorByte)
                    {
                        case 32: ffrspgfxcolor = "White"; break;
                        case 33: ffrspgfxcolor = "Light Blue"; break;
                        case 34: ffrspgfxcolor = "Dark Blue"; break;
                        case 35: ffrspgfxcolor = "Purple"; break;
                        case 36: ffrspgfxcolor = "Pink"; break;
                        case 37: ffrspgfxcolor = "Magenta"; break;
                        case 38: ffrspgfxcolor = "Red"; break;
                        case 39: ffrspgfxcolor = "Orange"; break;
                        case 40: ffrspgfxcolor = "Yellow"; break;
                        case 41: ffrspgfxcolor = "Bright Green"; break;
                        case 42: ffrspgfxcolor = "Dark Green"; break;
                        case 43: ffrspgfxcolor = "Pale Cyan"; break;
                        case 44: ffrspgfxcolor = "Bright Cyan"; break;
                        case 45: ffrspgfxcolor = "Gray"; break;
                        case 46: ffrspgfxcolor = "Black"; break;
                        default: ffrspgfxcolor = "Malformed"; break;
                    }
                    #endregion ColorName
                    #region BattleMessages
                    while (ffrspbatmsgloop < 78)
                    {
                        switch (ffrspbatmsgloop)
                        #region MessageList
                        {
                            case 0x01: ffrspBatMsg.Add("HP up!"); break;
                            case 0x02: ffrspBatMsg.Add("Armor up"); break;
                            case 0x03: ffrspBatMsg.Add("Easy to dodge"); break;
                            case 0x05: ffrspBatMsg.Add("Easy to hit"); break;
                            case 0x08: ffrspBatMsg.Add("Defend lightning"); break;
                            case 0x0A: ffrspBatMsg.Add("Weapons stronger"); break;
                            case 0x0B: ffrspBatMsg.Add("Attack rate down"); break;
                            case 0x0C: ffrspBatMsg.Add("Defend fire"); break;
                            case 0x0D: ffrspBatMsg.Add("Attack halted"); break;
                            case 0x0F: ffrspBatMsg.Add("Became terrified"); break;
                            case 0x10: ffrspBatMsg.Add("Defend cold"); break;
                            case 0x12: ffrspBatMsg.Add("Quick shot"); break;
                            case 0x15: ffrspBatMsg.Add("Erased"); break;
                            case 0x16: ffrspBatMsg.Add("Fell into crack"); break;
                            case 0x18: ffrspBatMsg.Add("HP max!"); break;
                            case 0x19: ffrspBatMsg.Add("Defend magic"); break;
                            case 0x1B: ffrspBatMsg.Add("Weapon became enchanted"); break;
                            case 0x1C: ffrspBatMsg.Add("Defend all"); break;
                            case 0x1D: ffrspBatMsg.Add("Defenseless"); break;
                            case 0x1E: ffrspBatMsg.Add("Time stopped"); break;
                            case 0x1F: ffrspBatMsg.Add("Exile to 4th dimension"); break;
                            case 0x24: ffrspBatMsg.Add("Can't run"); break;
                            case 0x2C: ffrspBatMsg.Add("Critical hit!!"); break;
                            case 0x2F: ffrspBatMsg.Add("Stopped"); break;
                            case 0x4A: ffrspBatMsg.Add("Ineffective now"); break;
                            case 0x4C: ffrspBatMsg.Add("Frozen"); break;
                            case 0x4D: ffrspBatMsg.Add("Poison smoke"); break;
                            default: ffrspBatMsg.Add(""); break;
                        }
                        #endregion MessageList
                        ffrspbatmsgloop++;
                    }
                    #endregion BattleMessages
                    #region ResistMessages
                    if (ffrspResistCount > ffrspResMsg.Count)
                    {
                        switch (ffrSpellEff)
                        {
                            #region StatusResist
                            case 1:
                                switch (ffrspResistCount)
                                {
                                    case 1: ffrspResMsg.Add("hindrance"); break;
                                    case 2 or 3: ffrspResMsg.Add("weak"); break;
                                    case 4: ffrspResMsg.Add("trick"); break;
                                    case 5: ffrspResMsg.Add("psi"); break;
                                }
                                break;
                            #endregion StatusResist
                            #region PoisonResist
                            case 2:
                                switch (ffrspResistCount)
                                {
                                    case 1: ffrspResMsg.Add("poisonous"); break;
                                    case 2 or 3: ffrspResMsg.Add("bane"); break;
                                    case 4: ffrspResMsg.Add("smoke"); break;
                                    case 5: ffrspResMsg.Add("gas"); break;
                                }
                                break;
                            #endregion PoisonResist
                            #region TimeResist
                            case 4:
                                switch (ffrspResistCount)
                                {
                                    case 1: ffrspResMsg.Add("dimension"); break;
                                    case 2 or 3: ffrspResMsg.Add("time"); break;
                                    case 4: ffrspResMsg.Add("exile"); break;
                                    case 5: ffrspResMsg.Add("zap"); break;
                                }
                                break;
                            #endregion TimeResist
                            #region DeathResist
                            case 8:
                                switch (ffrspResistCount)
                                {
                                    case 1: ffrspResMsg.Add("bleakness"); break;
                                    case 2 or 3: ffrspResMsg.Add("doom"); break;
                                    case 4: ffrspResMsg.Add("death"); break;
                                    case 5: ffrspResMsg.Add("rot"); break;
                                }
                                break;
                            #endregion DeathResist
                            #region DecayResist
                            case 14:
                                switch (ffrspResistCount)
                                {
                                    case 1: ffrspResMsg.Add("futility "); break;
                                    case 2 or 3: ffrspResMsg.Add("omen"); break;
                                    case 4: ffrspResMsg.Add("decay"); break;
                                    case 5: ffrspResMsg.Add("age"); break;
                                }
                                break;
                            #endregion DecayResist
                            #region FireResist
                            case 16:
                                switch (ffrspResistCount)
                                {
                                    case 1: ffrspResMsg.Add("pyromancy"); break;
                                    case 2 or 3: ffrspResMsg.Add("fire"); break;
                                    case 4: ffrspResMsg.Add("blaze"); break;
                                    case 5: ffrspResMsg.Add("hot"); break;
                                }
                                break;
                            #endregion FireResist
                            #region IceResist
                            case 32:
                                switch (ffrspResistCount)
                                {
                                    case 1: ffrspResMsg.Add("frostbite"); break;
                                    case 2 or 3: ffrspResMsg.Add("cold"); break;
                                    case 4: ffrspResMsg.Add("chill"); break;
                                    case 5: ffrspResMsg.Add("ice"); break;
                                }
                                break;
                            #endregion IceResist
                            #region LightningResist
                            case 64:
                                switch (ffrspResistCount)
                                {
                                    case 1: ffrspResMsg.Add("lightning"); break;
                                    case 2 or 3: ffrspResMsg.Add("volt"); break;
                                    case 4: ffrspResMsg.Add("spark"); break;
                                    case 5: ffrspResMsg.Add("ion"); break;
                                }
                                break;
                            #endregion LightningResist
                            #region ArcaneResist
                            case 112:
                                switch (ffrspResistCount)
                                {
                                    case 1: ffrspResMsg.Add("elemental"); break;
                                    case 2 or 3: ffrspResMsg.Add("wild"); break;
                                    case 4: ffrspResMsg.Add("spell"); break;
                                    case 5: ffrspResMsg.Add("wiz"); break;
                                }
                                break;
                            #endregion ArcaneResist
                            #region EarthResist
                            case 128:
                                switch (ffrspResistCount)
                                {
                                    case 1: ffrspResMsg.Add("tectonic "); break;
                                    case 2 or 3: ffrspResMsg.Add("land"); break;
                                    case 4: ffrspResMsg.Add("crack"); break;
                                    case 5: ffrspResMsg.Add("geo"); break;
                                }
                                break;
                            #endregion EarthResist
                            #region MagicResist
                            case 137:
                                switch (ffrspResistCount)
                                {
                                    case 1: ffrspResMsg.Add("willpower"); break;
                                    case 2 or 3: ffrspResMsg.Add("life"); break;
                                    case 4: ffrspResMsg.Add("magic"); break;
                                    case 5: ffrspResMsg.Add("bio"); break;
                                }
                                break;
                            #endregion MagicResist
                            #region WALL
                            case 255:
                                switch (ffrspResistCount)
                                {
                                    case 1: ffrspResMsg.Add("all      "); break;
                                    case 2 or 3: ffrspResMsg.Add("all "); break;
                                    case 4: ffrspResMsg.Add("all  "); break;
                                    case 5: ffrspResMsg.Add("all"); break;
                                }
                                break;
                                #endregion WALL
                        }
                        switch (ffrspResistCount)
                        {
                            case 1: ffrspBatMsg[0x08] = ffrspBatMsg[0x08].Replace("lightning", ffrspResMsg[0]); break;
                            case 2: ffrspBatMsg[0x0C] = ffrspBatMsg[0x0C].Replace("fire", ffrspResMsg[1]); break;
                            case 3: ffrspBatMsg[0x10] = ffrspBatMsg[0x10].Replace("cold", ffrspResMsg[2]); break;
                            case 4: ffrspBatMsg[0x19] = ffrspBatMsg[0x19].Replace("magic", ffrspResMsg[3]); break;
                            case 5: ffrspBatMsg[0x1C] = ffrspBatMsg[0x1C].Replace("all", ffrspResMsg[4]); break;
                        }
                    }
                #endregion ResistMessages

                // Check for ffrspname in the array containing all results
                spwrite:                    
                    if (ffrspflags.Contains("n2")) ffrspname = $"{ffrspmagic.Substring(0, 1).ToUpper()}{ffrsplevel}-{ffrspslot}";
                    if (ffrspTable.Contains(ffrspname))
                    {
                        colors.echo(4, $"{ffrspname} already exists! Rerolling.");
                        ffrspreroll++;
                        goto spdupe;
                    }
                    if (ffrspflags.Contains("n")) ffrNameHide.Add($"{ffrspmagic.Substring(0, 1).ToUpper()}{ffrsplevel}-{ffrspslot}");
                    #region BlackDamageAssess
                    if (ffrSpellType == 1 && ffrSpellTarg == 1 && ffrspmagic == "black" && ffrspflags.Contains("b")) ffrspblackAoE++;
                    #endregion BlackDamageAssess
                    #region ItemMagicFlag
                    if (ffrspflags.Contains("i"))
                    {
                        if (ffrSpellTarg == 1)
                        {
                            switch (ffrSpellElem)
                            {
                                case 2: if (ffrBaneSwordLocked == false) { ffrBaneSword = ffrspTable.Count; if (!itemCheck.Contains("Bane Sword")) itemCheck.Add("Bane Sword"); if (ffrsplevel >= 5) ffrBaneSwordLocked = true; } break;
                                case 64: if (ffrThorZeusLocked == false) { ffrThorZeus = ffrspTable.Count; if (!itemCheck.Contains("Thor H/Zeus G")) itemCheck.Add("Thor H/Zeus G"); if (ffrsplevel >= 3) ffrThorZeusLocked = true; } break;
                            }
                            switch (ffrSpellType)
                            {
                                case 1 or 2:
                                    if (ffrspmagic == "white")
                                    {
                                        if (ffrLightAxeLocked == false && ffrBaneSword != ffrspTable.Count)
                                        {
                                            ffrLightAxe = ffrspTable.Count;
                                            if (!itemCheck.Contains("Light Axe")) itemCheck.Add("Light Axe");
                                            if (ffrsplevel >= 3) ffrLightAxeLocked = true;
                                        }
                                    }
                                    else
                                    {
                                        if (ffrMageStaffLocked == false && Math.Max(ffrThorZeus, ffrBaneSword) != ffrspTable.Count)
                                        {
                                            ffrMageStaff = ffrspTable.Count;
                                            if (!itemCheck.Contains("Mage Staff")) itemCheck.Add("Mage Staff");
                                            if (ffrsplevel >= 3) ffrMageStaffLocked = true;
                                        }
                                        if (ffrBlackShirtLocked == false && Math.Max(Math.Max(ffrThorZeus, ffrBaneSword), ffrMageStaff) != ffrspTable.Count)
                                        {
                                            ffrBlackShirt = ffrspTable.Count;
                                            if (!itemCheck.Contains("Black Shirt")) itemCheck.Add("Black Shirt");
                                            if (ffrsplevel >= 4) ffrBlackShirtLocked = true;
                                        }
                                    }
                                    break;
                                case 3 or 4 or 5 or 14 or 17:
                                    if (ffrSpellType == 3 && (ffrSpellEff & 4) > 0)
                                    {
                                        if (ffrBaneSwordLocked == false) 
                                        { 
                                            ffrBaneSword = ffrspTable.Count;
                                            if (!itemCheck.Contains("Bane Sword")) itemCheck.Add("Bane Sword");
                                            if (ffrsplevel >= 5) ffrBaneSwordLocked = true;
                                        }
                                    }
                                    if (ffrSpellType != 3 || (ffrSpellEff & 3) == 0)
                                    {
                                        if (ffrWizardStaffLocked == false && Math.Max(ffrThorZeus, ffrBaneSword) != ffrspTable.Count)
                                        {
                                            ffrWizardStaff = ffrspTable.Count;
                                            if (!itemCheck.Contains("Wizard Staff")) itemCheck.Add("Wizard Staff");
                                            if (ffrsplevel >= 4) ffrWizardStaffLocked = true;
                                        }
                                    }
                                    break;
                            }
                        }
                        if (ffrSpellTarg == 8)
                        {
                            switch (ffrSpellType)
                            {

                                case 7 or 8 or 15:
                                    if (ffrHealGearLocked == false)
                                    {
                                        ffrHealGear = ffrspTable.Count;
                                        if (!itemCheck.Contains("Heal Helm/Rod")) itemCheck.Add("Heal Helm/Rod");
                                        if (ffrspflags.Contains("h"))
                                        {
                                            var healDice = new Random();
                                            if (healDice.Next(4) + 1 < 4) { ffrHealGearLocked = true; }
                                            else colors.echo(13, "Advancing Heal Gear!");
                                        }
                                        else if (ffrsplevel >= 3) ffrHealGearLocked = true;
                                    }
                                    break;
                                case 9 or 10 or 16:
                                    if (ffrWhiteShirtLocked == false)
                                    {
                                        ffrWhiteShirt = ffrspTable.Count;
                                        if (!itemCheck.Contains("White Shirt")) itemCheck.Add("White Shirt");
                                        if (ffrsplevel >= 3) ffrWhiteShirtLocked = true;
                                    }
                                    break;
                            }
                        }
                        if (ffrSpellTarg == 4)
                        {
                            switch (ffrSpellType)
                            {
                                case 9 or 10 or 16:
                                    if (ffrDefenseLocked == false)
                                    {
                                        ffrDefense = ffrspTable.Count;
                                        if (!itemCheck.Contains("Defense Sword")) itemCheck.Add("Defense Sword");
                                        if ((ffrsplevel >= 3 && ffrspmagic == "white") || (ffrsplevel >= 5 && ffrspmagic == "black")) ffrDefenseLocked = true;
                                    }
                                    break;
                            }
                        }
                        if (Enumerable.Range(12, 2).Contains(ffrSpellType) && (ffrSpellTarg & 12) >= 4)
                        {
                            if (ffrPowerBonkLocked == false)
                            {
                                ffrPowerBonk = ffrspTable.Count;
                                if (!itemCheck.Contains("Power Bonk")) itemCheck.Add("Power Bonk");
                                if (ffrsplevel >= 5) ffrPowerBonkLocked = true;
                            }
                        }
                    }
                    #endregion ItemMagicFlag
                    #region Permissions
                    if (ffrspflags.Contains("r"))
                    {
                        var permSlot = Math.Pow(2, (4 - ffrspslot));
                        if (ffrspmagic == "black") ffrspWmPerms = false;
                        if (ffrspmagic == "white")
                        {
                            ffrspBmPerms = false;
                            permSlot = Math.Pow(2, (8 - ffrspslot));
                        }
                        if (ffrspRwPerms == false)
                        {
                            ffrspRmPerms = false;
                            ffrspRwPermByte += (int)permSlot;
                        }
                        if (ffrspWmPerms == false) ffrspWmPermByte += (int)permSlot;
                        if (ffrspBmPerms == false) ffrspBmPermByte += (int)permSlot;
                        if (ffrspRmPerms == false) ffrspRmPermByte += (int)permSlot;
                    }
                    #endregion Permissions
                    ffrspTable.Add(ffrspname);
                    if (ffrspflags.Contains("n")) ffrspname = $"{ffrspmagic.Substring(0, 1).ToUpper()}{ffrsplevel}-{ffrspslot}";
                    ffrPatchCount = 1;
                    while (ffrPatchCount <= 4)
                    {
                        ffrspNamePatch.Add(FF1CharMap.Transmute(ffrspname.Substring(ffrPatchCount - 1, 1)));
                        ffrPatchCount++;
                    }
                    ffrspNamePatch.Add((char)0x00);
                    ffrspSpellMsgPatch.Add((byte)ffrspMsgByte);
                    if (ffrspreroll >= 5) { colors.echo(12, $"Rerolled {ffrspreroll} times! Ignored flags to avoid collisions."); }
                    quill.WriteLine($"{ffrspname}: {ffrsptype}{ffrspgfxcolor} {ffrspgfxshape}. \"{ffrspBatMsg[ffrspMsgByte]}\"");
                    colors.echo(0, $"Wrote \"{ffrspname}: {ffrsptype}{ffrspgfxcolor} {ffrspgfxshape}. \"{ffrspBatMsg[ffrspMsgByte]}\"\" to {book}");
                    Console.WriteLine();
                    ffrspSpellPatch.Add((byte)ffrSpellAcc);
                    ffrspSpellPatch.Add((byte)ffrSpellEff);
                    ffrspSpellPatch.Add((byte)ffrSpellElem);
                    ffrspSpellPatch.Add((byte)ffrSpellTarg);
                    ffrspSpellPatch.Add((byte)ffrSpellType);
                    ffrspSpellPatch.Add((byte)ffrspShapeByte);
                    ffrspSpellPatch.Add((byte)ffrspColorByte);
                    ffrspSpellPatch.Add((byte)0x00);
                    ffrspname = "";

                    ffrsptier = 0;
                    if (ffrspslot == 4)
                    {
                        if (ffrspmagic == "white")
                        {
                            quill.WriteLine("-----");
                            ffrspslot = 0;
                            ffrspmagic = "black";
                            goto sploop;
                        }
                        else if (ffrspmagic == "black")
                        {
                            quill.Write("=====");
                            #region PermBytes
                            if (ffrspflags.Contains("r"))
                            {
                                int knightByte = ffrspRwPermByte | ffrspWmPermByte;
                                int ninjaByte = ffrspRwPermByte | ffrspBmPermByte;
                                ffrspRwPermsPatch.Add((byte)ffrspRwPermByte);
                                ffrspWmPermsPatch.Add((byte)ffrspWmPermByte);
                                ffrspBmPermsPatch.Add((byte)ffrspBmPermByte);
                                ffrspRmPermsPatch.Add((byte)ffrspRmPermByte);
                                if (ffrsplevel <= 3) ffrspKnPermsPatch.Add((byte)knightByte);
                                if (ffrsplevel <= 4) ffrspNjPermsPatch.Add((byte)ninjaByte);
                            }
                            #endregion PermBytes
                            if (ffrsplevel == 8) // Array finished populating
                            {
                                if (ffrspflags.Contains("n"))
                                {
                                    var ffrNameCount = 0;
                                    ffrspTable.Clear();
                                    while (ffrNameCount <= 63)
                                    { 
                                        ffrspTable.Add(ffrNameHide[ffrNameCount]);
                                        ffrNameCount++;
                                    }
                                }
                                #region BlackAoE
                                if (ffrspflags.Contains("b"))
                                {
                                    if (ffrspblackAoE >= 6) { colors.echo(11, $"{ffrspblackAoE} damage spells detected!"); }
                                    else
                                    {
                                        ffrspretries++;
                                        colors.echo(4, "Failed to meet Spellbomb Quota. Taking a short rest before retrying...");
                                        Thread.Sleep(3000);
                                        goto retry;
                                    }
                                }
                                #endregion BlackAoE
                                #region WritingItemMagic
                                if (ffrspflags.Contains("i"))
                                    try
                                    {
                                        quill.WriteLine();
                                        quill.Flush(); // refilling ink before writing items
                                        quill.WriteLine("CASTING ITEMS");
                                        quill.WriteLine($"    {itemCheck[itemCheck.IndexOf("Light Axe")]}: {ffrspTable[ffrLightAxe]}");
                                        quill.WriteLine($"   {itemCheck[itemCheck.IndexOf("Mage Staff")]}: {ffrspTable[ffrMageStaff]}");
                                        quill.WriteLine($"  {itemCheck[itemCheck.IndexOf("Black Shirt")]}: {ffrspTable[ffrBlackShirt]}");
                                        quill.WriteLine($"{itemCheck[itemCheck.IndexOf("Thor H/Zeus G")]}: {ffrspTable[ffrThorZeus]}");
                                        quill.WriteLine($"   {itemCheck[itemCheck.IndexOf("Bane Sword")]}: {ffrspTable[ffrBaneSword]}");
                                        quill.WriteLine($"   {itemCheck[itemCheck.IndexOf("Power Bonk")]}: {ffrspTable[ffrPowerBonk]}");
                                        quill.WriteLine($"{itemCheck[itemCheck.IndexOf("Defense Sword")]}: {ffrspTable[ffrDefense]}");
                                        quill.WriteLine($"  {itemCheck[itemCheck.IndexOf("White Shirt")]}: {ffrspTable[ffrWhiteShirt]}");
                                        quill.WriteLine($" {itemCheck[itemCheck.IndexOf("Wizard Staff")]}: {ffrspTable[ffrWizardStaff]}");
                                        quill.WriteLine($"{itemCheck[itemCheck.IndexOf("Heal Helm/Rod")]}: {ffrspTable[ffrHealGear]}");
                                        colors.echo(13, $"Assigned all {itemCheck.Count} Casting Items!");
                                    }
                                    catch
                                    {
                                        ffrspretries++;
                                        colors.echo(4, "Failed to write Item Magic. Taking a short rest before retrying...");
                                        Thread.Sleep(3000);
                                        goto retry;
                                    }
                                #endregion WritingItemMagic
                                /* Header for Spell Names */
                                etch.Write((byte)0x02); etch.Write((byte)0xBE); etch.Write((byte)0x13); etch.Write((byte)0x01); etch.Write((byte)0x3F);
                                var ffrEtchCount = 0;
                                while (ffrEtchCount <= 0x13E)
                                {
                                    etch.Write((byte)ffrspNamePatch[ffrEtchCount]);
                                    // if ((ffrEtchCount + 1) % 4 == 0 && ffrEtchCount != 0x13F) etch.Write((byte)0x00);
                                    ffrEtchCount++;
                                }
                                etch.Flush();
                                #region BattleMessages
                                #region ResistPatch
                                if (ffrspResistCount >= 1)
                                {
                                    etch.Write((byte)0x02); etch.Write((byte)0xCC); etch.Write((byte)0xA1); etch.Write((byte)0x00); etch.Write((byte)0x09);
                                    ffrEtchCount = 0;
                                    while (ffrEtchCount <= 0x08)
                                    {
                                        etch.Write((byte)FF1CharMap.Transmute(ffrspResMsg[0].Substring(ffrEtchCount, 1)));
                                        ffrEtchCount++;
                                    }
                                }
                                if (ffrspResistCount >= 2)
                                {
                                    etch.Write((byte)0x02); etch.Write((byte)0xCC); etch.Write((byte)0xDE); etch.Write((byte)0x00); etch.Write((byte)0x04);
                                    ffrEtchCount = 0;
                                    while (ffrEtchCount <= 0x03)
                                    {
                                        etch.Write((byte)FF1CharMap.Transmute(ffrspResMsg[1].Substring(ffrEtchCount, 1)));
                                        ffrEtchCount++;
                                    }
                                }
                                if (ffrspResistCount >= 3)
                                {
                                    etch.Write((byte)0x02); etch.Write((byte)0xCD); etch.Write((byte)0x15); etch.Write((byte)0x00); etch.Write((byte)0x04);
                                    ffrEtchCount = 0;
                                    while (ffrEtchCount <= 0x03)
                                    {
                                        etch.Write((byte)FF1CharMap.Transmute(ffrspResMsg[2].Substring(ffrEtchCount, 1)));
                                        ffrEtchCount++;
                                    }
                                }
                                if (ffrspResistCount >= 4)
                                {
                                    etch.Write((byte)0x02); etch.Write((byte)0xCD); etch.Write((byte)0x79); etch.Write((byte)0x00); etch.Write((byte)0x05);
                                    ffrEtchCount = 0;
                                    while (ffrEtchCount <= 0x04)
                                    {
                                        etch.Write((byte)FF1CharMap.Transmute(ffrspResMsg[3].Substring(ffrEtchCount, 1)));
                                        ffrEtchCount++;
                                    }
                                }
                                if (ffrspResistCount >= 5)
                                {
                                    etch.Write((byte)0x02); etch.Write((byte)0xCD); etch.Write((byte)0xB1); etch.Write((byte)0x00); etch.Write((byte)0x03);
                                    ffrEtchCount = 0;
                                    while (ffrEtchCount <= 0x02)
                                    {
                                        etch.Write((byte)FF1CharMap.Transmute(ffrspResMsg[4].Substring(ffrEtchCount, 1)));
                                        ffrEtchCount++;
                                    }
                                }
                                #endregion ResistPatch
                                etch.Write((byte)0x02); etch.Write((byte)0xCF); etch.Write((byte)0x48); etch.Write((byte)0x00); etch.Write((byte)0x06);
                                ffrEtchCount = 0;
                                while (ffrEtchCount <= 0x05)
                                {
                                    etch.Write((byte)FF1CharMap.Transmute(ffrspBatMsg[0x4C].Substring(ffrEtchCount, 1)));
                                    ffrEtchCount++;
                                }
                                etch.Flush();
                                // Assign Message 76:
                                // if: TypeByte 3 or 18. ElemByte 32. { ffrmsg76 = "Frozen" }
                                // else if: TypeByte 3 or 18. EffByte 128 || (EffByte - 4 is a power of 2). ElemByte 16. { ffrmsg76 = "Ablaze" }
                                // else if: TypeByte 12. TargByte 4. Array entries of (modulo 8 < 4) only. { ffrmsg76 = "Go mad" }
                                // Write down applicable battle messages and whether they were assigned
                                // (1, 2, 3, 5, 8, 10, 11, 12, 13, 15, 16, 18, 21, 22, 24, 25, 27, 28, 29, 30, 31, 43, 47, 74, 76, 77)
                                #endregion BattleMessages
                                #region ItemMagicBytes
                                if (ffrspflags.Contains("i"))
                                {
                                    etch.Write((byte)0x03); etch.Write((byte)0x00); etch.Write((byte)0xF3); etch.Write((byte)0x00); etch.Write((byte)0x01); etch.Write((byte)(ffrLightAxe + 1));
                                    etch.Write((byte)0x03); etch.Write((byte)0x00); etch.Write((byte)0xFB); etch.Write((byte)0x00); etch.Write((byte)0x01); etch.Write((byte)(ffrHealGear + 1));
                                    etch.Write((byte)0x03); etch.Write((byte)0x01); etch.Write((byte)0x03); etch.Write((byte)0x00); etch.Write((byte)0x01); etch.Write((byte)(ffrMageStaff + 1));
                                    etch.Write((byte)0x03); etch.Write((byte)0x01); etch.Write((byte)0x0B); etch.Write((byte)0x00); etch.Write((byte)0x01); etch.Write((byte)(ffrDefense + 1));
                                    etch.Write((byte)0x03); etch.Write((byte)0x01); etch.Write((byte)0x13); etch.Write((byte)0x00); etch.Write((byte)0x01); etch.Write((byte)(ffrWizardStaff + 1));
                                    etch.Write((byte)0x03); etch.Write((byte)0x01); etch.Write((byte)0x2B); etch.Write((byte)0x00); etch.Write((byte)0x01); etch.Write((byte)(ffrThorZeus + 1));
                                    etch.Write((byte)0x03); etch.Write((byte)0x01); etch.Write((byte)0x33); etch.Write((byte)0x00); etch.Write((byte)0x01); etch.Write((byte)(ffrBaneSword + 1));
                                    // Separating weapons and armor for readability
                                    etch.Write((byte)0x03); etch.Write((byte)0x01); etch.Write((byte)0x8B); etch.Write((byte)0x00); etch.Write((byte)0x01); etch.Write((byte)(ffrWhiteShirt + 1));
                                    etch.Write((byte)0x03); etch.Write((byte)0x01); etch.Write((byte)0x8F); etch.Write((byte)0x00); etch.Write((byte)0x01); etch.Write((byte)(ffrBlackShirt + 1));
                                    etch.Write((byte)0x03); etch.Write((byte)0x01); etch.Write((byte)0xCB); etch.Write((byte)0x00); etch.Write((byte)0x01); etch.Write((byte)(ffrHealGear + 1));
                                    etch.Write((byte)0x03); etch.Write((byte)0x01); etch.Write((byte)0xE3); etch.Write((byte)0x00); etch.Write((byte)0x01); etch.Write((byte)(ffrThorZeus + 1));
                                    etch.Write((byte)0x03); etch.Write((byte)0x01); etch.Write((byte)0xE7); etch.Write((byte)0x00); etch.Write((byte)0x01); etch.Write((byte)(ffrPowerBonk + 1));
                                }
                                #endregion ItemMagicBytes
                                /* Header for Spell Data */
                                etch.Write((byte)0x03); etch.Write((byte)0x01); etch.Write((byte)0xF0); etch.Write((byte)0x01); etch.Write((byte)0xFF); 
                                ffrEtchCount = 0;
                                while (ffrEtchCount <= 0x1FE)
                                {
                                    etch.Write((byte)ffrspSpellPatch[ffrEtchCount]);
                                    ffrEtchCount++;
                                }
                                etch.Flush();
                                /* Header for Spell Message Pointers */
                                etch.Write((byte)0x03); etch.Write((byte)0x04); etch.Write((byte)0xD0); etch.Write((byte)0x00); etch.Write((byte)0x40);    
                                ffrEtchCount = 0;
                                while (ffrEtchCount <= 0x3F)
                                {
                                    etch.Write((byte)ffrspSpellMsgPatch[ffrEtchCount]);
                                    ffrEtchCount++;
                                }
                                #region EtchPerms
                                if (ffrspflags.Contains("r"))
                                {
                                    int etchedClasses = 0;
                                    while (etchedClasses != 6)
                                    {
                                        ffrEtchCount = 0;
                                        switch (etchedClasses)
                                        {
                                            case 0: // Red Mage
                                                etch.Write((byte)0x03); etch.Write((byte)0xAD); etch.Write((byte)0x40); etch.Write((byte)0x00); etch.Write((byte)0x08);
                                                while (ffrEtchCount <= 0x07)
                                                {
                                                    etch.Write((byte)ffrspRmPermsPatch[ffrEtchCount]);
                                                    ffrEtchCount++;
                                                }
                                                break;
                                            case 1: // White Mage
                                                etch.Write((byte)0x03); etch.Write((byte)0xAD); etch.Write((byte)0x48); etch.Write((byte)0x00); etch.Write((byte)0x08);
                                                while (ffrEtchCount <= 0x07)
                                                {
                                                    etch.Write((byte)ffrspWmPermsPatch[ffrEtchCount]);
                                                    ffrEtchCount++;
                                                }
                                                break;
                                            case 2: // Black Mage
                                                etch.Write((byte)0x03); etch.Write((byte)0xAD); etch.Write((byte)0x50); etch.Write((byte)0x00); etch.Write((byte)0x08);
                                                while (ffrEtchCount <= 0x07)
                                                {
                                                    etch.Write((byte)ffrspBmPermsPatch[ffrEtchCount]);
                                                    ffrEtchCount++;
                                                }
                                                break;
                                            case 3: // Knight
                                                etch.Write((byte)0x03); etch.Write((byte)0xAD); etch.Write((byte)0x58); etch.Write((byte)0x00); etch.Write((byte)0x03);
                                                while (ffrEtchCount <= 0x02)
                                                {
                                                    etch.Write((byte)ffrspKnPermsPatch[ffrEtchCount]);
                                                    ffrEtchCount++;
                                                }
                                                break;
                                            case 4: // Ninja
                                                etch.Write((byte)0x03); etch.Write((byte)0xAD); etch.Write((byte)0x60); etch.Write((byte)0x00); etch.Write((byte)0x04);
                                                while (ffrEtchCount <= 0x03)
                                                {
                                                    etch.Write((byte)ffrspNjPermsPatch[ffrEtchCount]);
                                                    ffrEtchCount++;
                                                }
                                                break;
                                            case 5: // Red Wizard
                                                etch.Write((byte)0x03); etch.Write((byte)0xAD); etch.Write((byte)0x70); etch.Write((byte)0x00); etch.Write((byte)0x08);
                                                while (ffrEtchCount <= 0x07)
                                                {
                                                    etch.Write((byte)ffrspRwPermsPatch[ffrEtchCount]);
                                                    ffrEtchCount++;
                                                }
                                                break;
                                        }
                                        etchedClasses++;
                                    }
                                }
                                #endregion EtchPerms
                                etch.Write(Encoding.ASCII.GetBytes("EOF"));
                                colors.echo(9, $"Generated 64 spells! Flags used: -{ffrspflags}. Rerolls: {ffrspretries}");
                                Process.Start("notepad.exe", $@"{ffrpath}{book}");
                                Process.Start(@$"{ffrpath}\flips\flips.exe", $@"{ffrpath}{rune}");
                            }
                            else if (ffrsplevel > 8) { colors.echo(4, $"ffrsplevel reached {ffrsplevel}, which exceeds 8!"); }
                            else
                            {
                                quill.WriteLine();
                                if (ffrspflags.Contains("r"))
                                {
                                    ffrspRmPermByte = 0;
                                    ffrspWmPermByte = 0;
                                    ffrspBmPermByte = 0;
                                    ffrspRwPermByte = 0;
                                }
                                if (ffrsplevel == 4) quill.Flush(); // halfway point! refilling ink!!
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
    }
}