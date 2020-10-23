namespace Neuroglia.Data.Coda
{

    public class CodaTransactionPart2Line
        : CodaLine, ICodaTransactionComponentLine
    {

        public CodaTransactionPart2Line(int sequenceNumber, int detailNumber, string communication, string customerReference, 
            string counterPartyBic, CodaRTransactionType? rTransactionType, string rReasonCode, string categoryPurpose, string purpose)
            : base(CodaLineType.TransactionPart2)
        {
            this.SequenceNumber = sequenceNumber;
            this.DetailNumber = detailNumber;
            this.Communication = communication;
            this.CustomerReference = customerReference;
            this.CounterPartyBic = counterPartyBic;
            this.RTransactionType = rTransactionType;
            this.RReasonCode = rReasonCode;
            this.CategoryPurpose = categoryPurpose;
            this.Purpose = purpose;
        }

        public int SequenceNumber { get; }

        public int DetailNumber { get; }

        public string Communication { get; }

        public string CustomerReference { get; }

        public string CounterPartyBic { get; }

        public CodaRTransactionType? RTransactionType { get; }

        public string RReasonCode { get; }

        public string CategoryPurpose { get; }

        public string Purpose { get; }

    }

}
