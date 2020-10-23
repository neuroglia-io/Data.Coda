namespace Neuroglia.Data.Coda
{

    public class CodaFooterLine
        : CodaLine
    {

        public CodaFooterLine(int recordsCount, decimal totalDebited, decimal totalCredited)
            : base(CodaLineType.Footer)
        {
            this.RecordsCount = recordsCount;
            this.TotalDebited = totalDebited;
            this.TotalCredited = totalCredited;
        }

        public int RecordsCount { get; }

        public decimal TotalDebited { get; }

        public decimal TotalCredited { get; }

    }

}
