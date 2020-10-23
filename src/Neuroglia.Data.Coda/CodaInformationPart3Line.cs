namespace Neuroglia.Data.Coda
{

    public class CodaInformationPart3Line
        : CodaLine, ICodaTransactionComponentLine
    {

        public CodaInformationPart3Line(int sequenceNumber, int detailNumber, string communication)
            : base(CodaLineType.InformationPart3)
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
