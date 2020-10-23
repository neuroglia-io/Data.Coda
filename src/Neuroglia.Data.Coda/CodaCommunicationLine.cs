namespace Neuroglia.Data.Coda
{

    public class CodaCommunicationLine
        : CodaLine
    {

        public CodaCommunicationLine(int sequenceNumber, int detailNumber, string communication)
            : base(CodaLineType.Communication)
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
