// See https://aka.ms/new-console-template for more information
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
        public static int ffrsptier = 0;
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
        public static string ffrsptarg = "";
        public static string ffrspelem = "";
        public static string ffrsptype = "";
        public static string ffrspeff = "";
        public static string ffrspgfxcolor = "";
        public static string ffrspgfxshape = "";
        public static string ffrSpell = "";

        public static int ffrspShapeByte;
        public static int ffrspColorByte;

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
            var ffrspTable = new List<string>();
            var ffrpath = @"C:\Users\Linkshot\Utilities\FFR-Spellbinder\output\";
            var book = @$"table\SpellTable_{DateTime.UtcNow.Ticks.ToString()}.txt";
            var rune = @$"patch\FFR-Custom-Spells_({ffrspflags})_{DateTime.UtcNow.Ticks.ToString()}.ips";
            using (TextWriter quill = File.CreateText($@"{ffrpath}{book}"))
            {
                using (BinaryWriter etch = new BinaryWriter(File.OpenWrite($@"{ffrpath}{rune}")))
                {
                    var ffrspNamePatch = new List<char>();
                    var ffrspSpellPatch = new List<byte>();
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
                    if (ffrsplevel == 5 && ffrspslot == 3 && ffrspmagic == "black") {
                        ffrsptype = "Travel back one floor. ";
                        ffrSpellAcc = 255;
                        ffrSpellEff = 0;
                        ffrSpellElem = 0;
                        ffrSpellTarg = 4;
                        ffrSpellType = 0;
                        ffrspShapeByte = 0xD8;
                        ffrspColorByte = 42;
                        ffrspgfxcolor = "Dark Green";
                        ffrspgfxshape = "Glowing Ball";
                        ffrspname = "WARP";
                        goto spwrite;
                    }
                    else if (ffrsplevel == 6 && ffrspslot == 2 && ffrspmagic == "white") {
                        ffrsptype = "Return to World Map. ";
                        ffrSpellAcc = 255;
                        ffrSpellEff = 0;
                        ffrSpellElem = 0;
                        ffrSpellTarg = 8;
                        ffrSpellType = 0;
                        ffrspShapeByte = 0xDC;
                        ffrspColorByte = 42;
                        ffrspgfxcolor = "Dark Green";
                        ffrspgfxshape = "Bursting Ball";
                        ffrspname = "EXIT";
                        goto spwrite;
                    }
                spdupe:
                    ffrSpell = "";
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
                        case 2 or 96: ffrspColorByte = 32; if (ffrspmagic == "black" && (((ffrSpellType == 1 || ffrSpellEff == 1) && ffrSpellElem == 2) || (PowerOf2.Divide(ffrSpellEff - 4) == true && ffrSpellType == 3 && ffrSpellEff != 8) || (ffrSpellType == 18 && ffrSpellEff != 2))) ffrspColorByte = 35; break;
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
                    #region DamageSpells
                    if (ffrSpellType < 3)
                    {
                        if (ffrspmagic == "white" || Enumerable.Range(64, 2).Contains(ffrSpellElem)) ffrspShapeByte = (ffrSpellTarg == 1) ? 204 : 200;
                        else ffrspShapeByte = (ffrSpellTarg == 2) ? 212 : 208;
                        if (ffrSpellType == 2)
                        {
                            if (Math.Floor((decimal)ffrSpellEff / 10) % 2 == 1 || ffrSpellEff >= 100) ffrspColorByte = 38;
                            else ffrspColorByte = 43;
                            switch (ffrSpellTarg)
                            {
                                case 1: if (ffrsplevel < 8) ffrspname = $"HRM{ffrsptier}"; if (ffrsptier == 1) ffrspname = "HARM"; if (ffrsplevel == 8) ffrspname = "VANQ"; break;
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
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "BANE"; break;
                                            case 2: ffrspname = "CHOK"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.POISON.NEG_EFFECT.DEATH"; break;
                                        }
                                        break;
                                    case 4: // Exile to 4th Dimension (Time)
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "ZAP!"; break;
                                            case 2: ffrspname = "BYE!"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.TIME.NEG_EFFECT.DEATH"; break;
                                        }
                                        break;
                                    case 8: // Erased (Death)
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "ERAD"; break;
                                            case 2: ffrspname = "RUB "; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.DEATH.NEG_EFFECT.DEATH"; break;
                                        }
                                        break;
                                    case 16: // Erased (Fire)
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "EVAP"; break;
                                            case 2: ffrspname = "MELT"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.FIRE.NEG_EFFECT.DEATH"; break;
                                        }
                                        break;
                                    case 64: // Exile to 4th Dimension (Lightning)
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "BZZT"; break;
                                            case 2: ffrspname = "SMIT"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.LIGHTNING.NEG_EFFECT.DEATH"; break;
                                        }
                                        break;
                                    case 128: // Fell into crack
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
                                        switch (ffrSpellTarg)
                                        {
                                            case 1: ffrspname = "STOP"; break;
                                            case 2: ffrspname = "PAUS"; break;
                                            default: ffrspname = $"TARG{ffrSpellTarg}.TIME.NEG_EFFECT.STUN"; break;
                                        }
                                        break;
                                    case 32:
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
                                    case 16:
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
                                if (PowerOf2.Divide(ffrSpellEff - 4) == true) // Poison
                                #region Poisons
                                {
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
                        if (ffrSpellEff == 3) ffrspShapeByte = (ffrSpellTarg == 8) ? 192 : 188;
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
                            case 3:
                                switch (ffrSpellTarg)
                                {
                                    case 8: ffrspname = "LIF2"; break; // change to LIFx later
                                    case 16: ffrspname = "LIF2"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.POS_EFFECT.DEATH.STONE"; break;
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
                            case 252:
                                switch (ffrSpellTarg)
                                {
                                    case 4: ffrspname = "BATH"; break;
                                    case 8: ffrspname = "CLRa"; break;
                                    case 16: ffrspname = "CLER"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.POS_EFFECT.ALL"; break;
                                }
                                break;
                            default: ffrspname = $"POS_EFFECT.EFF{ffrSpellEff}"; break;
                        }
                    }
                    #endregion PosEffect
                    #region ArmorUp
                    else if (ffrSpellType == 9)
                    {
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
                                switch (ffrSpellTarg)
                                {
                                    case 4: if (ffrsptier > 1) ffrspname = $"FOC{ffrsptier}"; if (ffrsptier == 1) ffrspname = "FOCS"; break;
                                    case 8: if (ffrsptier > 1) ffrspname = $"VIG{ffrsptier}"; else ffrspname = "VIGR"; break;
                                    case 16: if (ffrsptier > 1) ffrspname = $"BLS{ffrsptier}"; else ffrspname = "BLES"; break;
                                    default: ffrspname = $"TARG{ffrSpellTarg}.ENCHANT"; break;
                                }
                                break;
                            case "black":
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
                        switch (ffrSpellEff)
                        {
                            case 1: if (ffrspmagic == "white") ffrspname = "FROG"; if (ffrspmagic == "black") ffrspname = "XXXX"; break;
                            case 2: if (ffrspmagic == "white") ffrspname = "CAST"; if (ffrspmagic == "black") ffrspname = "PETR"; break;
                            case 8: ffrspname = "BLND"; break;
                            case 16: ffrspname = "STUN"; break;
                            case 32: ffrspname = "ZZZZ"; break;
                            case 64: ffrspname = "JINX"; break;
                            case 96: ffrspname = "COMA"; break;
                            case 124: ffrspname = "WEAK"; break;
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
                            string ffrspmend = (PowerOf2.Divide(ffrSpellEff - 16) == true && ffrSpellEff != 32) ? " and Paralysis" : "";
                            switch (ffrSpellEff)
                            {
                                case 1: ffrspeff = $"Revives {ffrsptarg} to 1 HP if fallen"; break;
                                case 2: ffrspeff = $"Softens {ffrsptarg} if Petrified"; break;
                                case 3: ffrspeff = $"Revives {ffrsptarg} to full health if fallen"; break;
                                case 4 or 20: ffrspeff = $"Removes Poison{ffrspmend} from {ffrsptarg}"; break;
                                case 8 or 24: ffrspeff = $"Removes Blindness{ffrspmend} from {ffrsptarg}"; break;
                                case 16: ffrspeff = $"Removes Paralysis from {ffrsptarg}"; break;
                                case 32 or 48: ffrspeff = $"Wakes {ffrsptarg} from Sleep{ffrspmend}"; break;
                                case 64 or 80: ffrspeff = $"Removes Silence{ffrspmend} from {ffrsptarg}"; break;
                                case 128 or 144: ffrspeff = $"Snaps {ffrsptarg} out of Confusion{ffrspmend}"; break;
                                case 132 or 148: ffrspeff = $"Cures {ffrsptarg} of Poison and Confusion{ffrspmend}"; break;
                                case 252: ffrspeff = $"Removes all non-lethal ailments from {ffrsptarg}"; break;
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
                        case 3: ffrsptype = $"{ffrspeff}. +{ffrSpellAcc} Chance to Hit. "; if (PowerOf2.Divide(ffrSpellEff - 4) == true && ffrSpellEff != 8) ffrsptype += "Venomous. "; break;
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
                        case 18: ffrsptype = $"{ffrspeff}, if vulnerable. "; if (PowerOf2.Divide(ffrSpellEff - 4) == true && ffrSpellEff != 8) ffrsptype += "Venomous. "; break;
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
                    }
                    #endregion ColorName

                    // Check for ffrspname in the array containing all results
                    spwrite:
                    if (ffrspTable.IndexOf(ffrspname) != -1)
                    {
                        colors.echo(4, $"{ffrspname} already exists! Rerolling.");
                        ffrspreroll++;
                        goto spdupe;
                    }
                    ffrspTable.Add(ffrspname);
                    ffrPatchCount = 1;
                    while (ffrPatchCount <= 4)
                    {
                        switch (ffrspname.Substring(ffrPatchCount - 1, 1))
                        {
                            #region FF1CharMap
                            case "0": ffrspNamePatch.Add((char)0x80); break;
                            case "1": ffrspNamePatch.Add((char)0x81); break;
                            case "2": ffrspNamePatch.Add((char)0x82); break;
                            case "3": ffrspNamePatch.Add((char)0x83); break;
                            case "4": ffrspNamePatch.Add((char)0x84); break;
                            case "5": ffrspNamePatch.Add((char)0x85); break;
                            case "6": ffrspNamePatch.Add((char)0x86); break;
                            case "7": ffrspNamePatch.Add((char)0x87); break;
                            case "8": ffrspNamePatch.Add((char)0x88); break;
                            case "9": ffrspNamePatch.Add((char)0x89); break;
                            case "A": ffrspNamePatch.Add((char)0x8A); break;
                            case "B": ffrspNamePatch.Add((char)0x8B); break;
                            case "C": ffrspNamePatch.Add((char)0x8C); break;
                            case "D": ffrspNamePatch.Add((char)0x8D); break;
                            case "E": ffrspNamePatch.Add((char)0x8E); break;
                            case "F": ffrspNamePatch.Add((char)0x8F); break;
                            case "G": ffrspNamePatch.Add((char)0x90); break;
                            case "H": ffrspNamePatch.Add((char)0x91); break;
                            case "I": ffrspNamePatch.Add((char)0x92); break;
                            case "J": ffrspNamePatch.Add((char)0x93); break;
                            case "K": ffrspNamePatch.Add((char)0x94); break;
                            case "L": ffrspNamePatch.Add((char)0x95); break;
                            case "M": ffrspNamePatch.Add((char)0x96); break;
                            case "N": ffrspNamePatch.Add((char)0x97); break;
                            case "O": ffrspNamePatch.Add((char)0x98); break;
                            case "P": ffrspNamePatch.Add((char)0x99); break;
                            case "Q": ffrspNamePatch.Add((char)0x9A); break;
                            case "R": ffrspNamePatch.Add((char)0x9B); break;
                            case "S": ffrspNamePatch.Add((char)0x9C); break;
                            case "T": ffrspNamePatch.Add((char)0x9D); break;
                            case "U": ffrspNamePatch.Add((char)0x9E); break;
                            case "V": ffrspNamePatch.Add((char)0x9F); break;
                            case "W": ffrspNamePatch.Add((char)0xA0); break;
                            case "X": ffrspNamePatch.Add((char)0xA1); break;
                            case "Y": ffrspNamePatch.Add((char)0xA2); break;
                            case "Z": ffrspNamePatch.Add((char)0xA3); break;
                            case "a": ffrspNamePatch.Add((char)0xA4); break;
                            case "b": ffrspNamePatch.Add((char)0xA5); break;
                            case "c": ffrspNamePatch.Add((char)0xA6); break;
                            case "d": ffrspNamePatch.Add((char)0xA7); break;
                            case "e": ffrspNamePatch.Add((char)0xA8); break;
                            case "f": ffrspNamePatch.Add((char)0xA9); break;
                            case "g": ffrspNamePatch.Add((char)0xAA); break;
                            case "h": ffrspNamePatch.Add((char)0xAB); break;
                            case "i": ffrspNamePatch.Add((char)0xAC); break;
                            case "j": ffrspNamePatch.Add((char)0xAD); break;
                            case "k": ffrspNamePatch.Add((char)0xAE); break;
                            case "l": ffrspNamePatch.Add((char)0xAF); break;
                            case "m": ffrspNamePatch.Add((char)0xB0); break;
                            case "n": ffrspNamePatch.Add((char)0xB1); break;
                            case "o": ffrspNamePatch.Add((char)0xB2); break;
                            case "p": ffrspNamePatch.Add((char)0xB3); break;
                            case "q": ffrspNamePatch.Add((char)0xB4); break;
                            case "r": ffrspNamePatch.Add((char)0xB5); break;
                            case "s": ffrspNamePatch.Add((char)0xB6); break;
                            case "t": ffrspNamePatch.Add((char)0xB7); break;
                            case "u": ffrspNamePatch.Add((char)0xB8); break;
                            case "v": ffrspNamePatch.Add((char)0xB9); break;
                            case "w": ffrspNamePatch.Add((char)0xBA); break;
                            case "x": ffrspNamePatch.Add((char)0xBB); break;
                            case "y": ffrspNamePatch.Add((char)0xBC); break;
                            case "z": ffrspNamePatch.Add((char)0xBD); break;
                            case "\"": ffrspNamePatch.Add((char)0xBE); break;
                            case ",": ffrspNamePatch.Add((char)0xBF); break;
                            case ".": ffrspNamePatch.Add((char)0xC0); break;
                            case " ": ffrspNamePatch.Add((char)0xC1); break;
                            case "-": ffrspNamePatch.Add((char)0xC2); break;
                            case "!": ffrspNamePatch.Add((char)0xC4); break;
                            default: ffrspNamePatch.Add((char)0xC5); break; // i wonder what this does!
                                #endregion FF1CharMap
                        }
                        ffrPatchCount++;
                    }
                    ffrspNamePatch.Add((char)0x00);
                    if (ffrspreroll >= 5) { colors.echo(12, $"Rerolled {ffrspreroll} times! Ignored flags to avoid collisions."); }
                    quill.WriteLine($"{ffrspname}: {ffrsptype}{ffrspgfxcolor} {ffrspgfxshape}.");
                    colors.echo(0, $"Wrote \"{ffrspname}: {ffrsptype}{ffrspgfxcolor} {ffrspgfxshape}\" to {book}");
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
                            if (ffrsplevel == 8) // Array finished populating
                            {
/* Header for Spell Names */    etch.Write((byte)0x02); etch.Write((byte)0xBE); etch.Write((byte)0x13); etch.Write((byte)0x01); etch.Write((byte)0x3F);
                                var ffrEtchCount = 0;
                                while (ffrEtchCount <= 0x13E)
                                {
                                    etch.Write((byte)ffrspNamePatch[ffrEtchCount]);
                                    // if ((ffrEtchCount + 1) % 4 == 0 && ffrEtchCount != 0x13F) etch.Write((byte)0x00);
                                    ffrEtchCount++;
                                }
                                #region ItemMagic
                                // start going through array of spells at specific slots, forward and then backward
                                // deny that slot being assigned twice
                                // if (ffrspflags.IndexOf("i") != -1) {
                                // ffrLightAxe: TypeByte 1 or 2. TargByte 1. Array entries of (modulo 8 > 3) only. Start seeking at 17th entry.
                                // ffrHealGear: TypeByte 7, 8, or 15. TargByte 8. Array entries of (modulo 8 > 3) only. Start seeking at 17th entry.
                                // ffrDefense: TypeByte 9, 10, or 16. TargByte 4. Start seeking at 17th entry.
                                // ffrWhiteShirt: TypeByte 9, 10, or 16. TargByte 8. Start seeking at 17th entry.
                                // ffrPowerBonk: TypeByte 12 or 13. TargByte 4 or 8. Start seeking at 33rd entry.
                                // ffrThorZeus: ElemByte 64. TargByte 1. Start seeking at 21st entry.
                                // ffrBaneSword: ElemByte 2 or (TypeByte 3 && EffByte - 4 is a power of 2). TargByte 1. Array entires of (modulo 8 < 4) only. Start seeking at 37th entry.
                                // ffrMageStaff: TypeByte 1. TargByte 1. Array entries of (modulo 8 < 4) only. Start seeking at 21st entry.
                                // ffrBlackShirt: TypeByte 1. TargByte 1. Array entries of (modulo 8 < 4) only. Start seeking at 29th entry.
                                // ffrWizardStaff: TypeByte > 2. (TypeByte != 3 || EffByte > 3). TargByte 1. Starts seeking at 9th entry.
                                // }
                                // if all succeed, echo.colors(9,$"Item Magic verified!");
                                // else prompt user to proceed or retry
                                #endregion ItemMagic
                                #region BlackAoE
                                // if (ffrspflags.IndexOf("b") != -1) {
                                // Check for 6 of TypeByte 1 && TargByte 1 at (modulo 8 < 4) entries
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
                                // if: TypeByte 3 or 18. ElemByte 32. { ffrmsg76 = "Frozen" }
                                // else if: TypeByte 3 or 18. EffByte 128 || (EffByte - 4 is a power of 2). ElemByte 16. { ffrmsg76 = "Ablaze" }
                                // else if: TypeByte 12. TargByte 4. Array entries of (modulo 8 < 4) only. { ffrmsg76 = "Go mad" }
                                // Write down applicable battle messages and whether they were assigned
                                // (1, 2, 3, 5, 8, 10, 11, 12, 13, 15, 16, 18, 21, 22, 24, 25, 27, 28, 29, 30, 31, 43, 47, 74, 76, 77)
                                #endregion BattleMessages
/* Header for Spell Data */     etch.Write((byte)0x03); etch.Write((byte)0x01); etch.Write((byte)0xF0); etch.Write((byte)0x01); etch.Write((byte)0xFF);
                                ffrEtchCount = 0;
                                while (ffrEtchCount <= 0x1FE)
                                {
                                    etch.Write((byte)ffrspSpellPatch[ffrEtchCount]);
                                    ffrEtchCount++;
                                }
                                etch.Write(Encoding.ASCII.GetBytes("EOF"));
                                colors.echo(9, $"Generated 64 spells! Flags used: {ffrspflags}");
                                Process.Start("notepad.exe", $@"{ffrpath}{book}");
                            }
                            else if (ffrsplevel > 8) { colors.echo(4, $"ffrsplevel reached {ffrsplevel}, which exceeds 8!"); }
                            else
                            {
                                quill.WriteLine();
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