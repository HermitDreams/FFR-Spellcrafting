using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffr_spellbinder
{
    public class PowerOf2
    {
        public static bool Divide(int no)
        {
            // colors.echo(12, $"PowerOf2 determined the starting value was {no}");
            if (no == 4) { return false; }
            int remainder;
                while (no > 1)
                    {
                        remainder = no % 2;
                        if (remainder != 0)
                            break;
                        no /= 2;
                // colors.echo(12, $"PowerOf2 reduced the value to {no}");
            }
                if (no == 1)
                   return true;
                else
                   return false;
        }
    }
}
