namespace Neuroglia.Data.Coda
{

    public class CodaTransactionPart3Line
        : CodaLine, ICodaTransactionComponentLine
    {

        public CodaTransactionPart3Line(int sequenceNumber, int detailNumber, BankAccount counterPartyAccount, string communication)
            : base(CodaLineType.TransactionPart3)
        {
            this.SequenceNumber = sequenceNumber;
            this.DetailNumber = detailNumber;
            this.CounterPartyAccount = counterPartyAccount;
            this.Communication = communication;
        }

        public int SequenceNumber { get; }

        public int DetailNumber { get; }

        public BankAccount CounterPartyAccount { get; }

        public string Communication { get; }

    }

}
