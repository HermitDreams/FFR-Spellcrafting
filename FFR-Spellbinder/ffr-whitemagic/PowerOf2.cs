using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffr_spellbinder
{
    public class PowerOf2
    {
        /// <summary>
        /// Checks if "no" is a Power of 2 after "adjust" (default 0) is taken away from it
        /// </summary>
        /// <param name="no"></param>
        /// <param name="adjust"></param>
        /// <returns>bool</returns>
        public static bool Divide(int no, int adjust = 0)
        {
            // colors.echo(12, $"PowerOf2 determined the starting value was {no}");
            if (no - adjust == adjust) { Console.WriteLine("The input was cut in half immediately and did not need to be adjusted."); }
            else no = no - adjust;
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

        /// <summary>
        /// Checks if "bit" is on inside "wholeByte"
        /// </summary>
        /// <param name="wholeByte"></param>
        /// <param name="bit"></param>
        /// <returns>bool</returns>
        public static bool IsBitOn(int wholeByte, int bit)
        {
            var threshold = 128;
            while (wholeByte >= bit)
            {
                if (wholeByte >= threshold) wholeByte -= threshold;
                threshold /= 2;
            }
            if (wholeByte == bit) return true;
            else return false;
        }
    }
}
