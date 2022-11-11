namespace ffr_spellbinder
{
    public class FF1CharMap
    {
        /// <summary>
        /// Translates ASCII input to Final Fantasy (NES, USA)'s character index
        /// </summary>
        /// <param name="input"></param>
        /// <returns>char/byte</returns>
        public static char Transmute(string input)
        {
            switch (input)
            {
                case "0": return (char)0x80;
                case "1": return (char)0x81;
                case "2": return (char)0x82;
                case "3": return (char)0x83;
                case "4": return (char)0x84;
                case "5": return (char)0x85;
                case "6": return (char)0x86;
                case "7": return (char)0x87;
                case "8": return (char)0x88;
                case "9": return (char)0x89;
                case "A": return (char)0x8A;
                case "B": return (char)0x8B;
                case "C": return (char)0x8C;
                case "D": return (char)0x8D;
                case "E": return (char)0x8E;
                case "F": return (char)0x8F;
                case "G": return (char)0x90;
                case "H": return (char)0x91;
                case "I": return (char)0x92;
                case "J": return (char)0x93;
                case "K": return (char)0x94;
                case "L": return (char)0x95;
                case "M": return (char)0x96;
                case "N": return (char)0x97;
                case "O": return (char)0x98;
                case "P": return (char)0x99;
                case "Q": return (char)0x9A;
                case "R": return (char)0x9B;
                case "S": return (char)0x9C;
                case "T": return (char)0x9D;
                case "U": return (char)0x9E;
                case "V": return (char)0x9F;
                case "W": return (char)0xA0;
                case "X": return (char)0xA1;
                case "Y": return (char)0xA2;
                case "Z": return (char)0xA3;
                case "a": return (char)0xA4;
                case "b": return (char)0xA5;
                case "c": return (char)0xA6;
                case "d": return (char)0xA7;
                case "e": return (char)0xA8;
                case "f": return (char)0xA9;
                case "g": return (char)0xAA;
                case "h": return (char)0xAB;
                case "i": return (char)0xAC;
                case "j": return (char)0xAD;
                case "k": return (char)0xAE;
                case "l": return (char)0xAF;
                case "m": return (char)0xB0;
                case "n": return (char)0xB1;
                case "o": return (char)0xB2;
                case "p": return (char)0xB3;
                case "q": return (char)0xB4;
                case "r": return (char)0xB5;
                case "s": return (char)0xB6;
                case "t": return (char)0xB7;
                case "u": return (char)0xB8;
                case "v": return (char)0xB9;
                case "w": return (char)0xBA;
                case "x": return (char)0xBB;
                case "y": return (char)0xBC;
                case "z": return (char)0xBD;
                case "\"": return (char)0xBE;
                case ",": return (char)0xBF;
                case ".": return (char)0xC0;
                case " ": return (char)0xC1;
                case "-": return (char)0xC2;
                case "!": return (char)0xC4;
                default: return (char)0xC5; // i wonder what this does!
            }
        }
    }
}
