namespace Neuroglia.Data.Coda
{

    public class CodaInformationPart1Line
        : CodaLine, ICodaTransactionComponentLine
    {

        public CodaInformationPart1Line(int sequenceNumber, int detailNumber, string reference, string transactionCode, CodaCommunication communication)
            : base(CodaLineType.InformationPart1)
        {
            this.SequenceNumber = sequenceNumber;
            this.DetailNumber = detailNumber;
            this.Reference = reference;
            this.TransactionCode = transactionCode;
            this.Communication = communication;
        }

        public int SequenceNumber { get; }

        public int DetailNumber { get; }

        public string Reference { get; }

        public string TransactionCode { get; }

        public CodaCommunication Communication { get; }

    }

}
