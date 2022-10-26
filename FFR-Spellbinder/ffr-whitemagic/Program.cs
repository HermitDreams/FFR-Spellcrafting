// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
namespace ffr_spellbinder
{
    internal class Program
    {
        static void Main()
        {
            ffr_whitemagic.WMag();
            Console.WriteLine("=");
            ffr_blackmagic.BMag();
        }
    }
}