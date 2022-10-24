using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffr_spellbinder
{
    internal class colors
    {
        public static void echo(int colorId, string text)
        {
            ConsoleColor colorName;
            switch (colorId)
            {
                case 0: colorName = ConsoleColor.White; break;
                case 1: colorName = ConsoleColor.Black; break;
                case 2: colorName = ConsoleColor.DarkBlue; break;
                case 3: colorName = ConsoleColor.DarkGreen; break;
                case 4: colorName = ConsoleColor.Red; break;
                case 5: colorName = ConsoleColor.DarkRed; break;
                case 6: colorName = ConsoleColor.DarkMagenta; break;
                case 7: colorName = ConsoleColor.DarkYellow; break;
                case 8: colorName = ConsoleColor.Yellow; break;
                case 9: colorName = ConsoleColor.Green; break;
                case 10: colorName = ConsoleColor.DarkCyan; break;
                case 11: colorName = ConsoleColor.Cyan; break;
                case 12: colorName = ConsoleColor.Blue; break;
                case 13: colorName = ConsoleColor.Magenta; break;
                case 14: colorName = ConsoleColor.DarkGray; break;
                case 15: colorName = ConsoleColor.Gray; break;
                default: colorName = ConsoleColor.Gray; break;
            }
            Console.ForegroundColor = colorName;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
