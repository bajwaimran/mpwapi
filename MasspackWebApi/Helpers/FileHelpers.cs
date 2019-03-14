using System;
using System.IO;

namespace BestellErfassung.FileHelpers
{
    class FileHelpers
    {
        internal static bool Exists(string FileName)
        {
            return File.Exists(FileName);
        }

       /* internal static void Delete(string p)
        {
            if (Exists(p))
                File.Delete(p);
        } */
    }
}
