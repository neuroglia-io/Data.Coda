namespace Neuroglia.Data.Coda
{

    public class CodaInformationPart2Line
        : CodaLine, ICodaTransactionComponentLine
    {

        public CodaInformationPart2Line(int sequenceNumber, int detailNumber, string communication)
            : base(CodaLineType.InformationPart2)
        {
            this.SequenceNumber = sequenceNumber;
            this.DetailNumber = detailNumber;
            this.Communication = communication;
        }

        public int SequenceNumber { get; }

        public int DetailNumber { get; }

        public string Communication { get; }

    }

}
