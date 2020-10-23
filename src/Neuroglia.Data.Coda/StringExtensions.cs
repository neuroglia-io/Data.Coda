using System;

namespace Neuroglia.Data.Coda
{

    public static class StringExtensions
    {

        public static bool IsAlphabetic(this string text)
        {
            foreach(char c in text)
            {
                if (!Char.IsLetter(c))
                    return false;
            }
            return true;
        }

    }

}
