using System;
using System.Globalization;

namespace Neuroglia.Data.Coda
{

    public static class CodaDate
    {

        public static DateTime Parse(string codaDate)
        {
            return DateTime.Parse($"20{codaDate.Substring(4, 2)}/{codaDate.Substring(2, 2)}/{codaDate.Substring(0, 2)}", CultureInfo.InvariantCulture);
        }

    }

}
