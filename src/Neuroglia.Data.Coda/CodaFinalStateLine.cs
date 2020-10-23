using System;

namespace Neuroglia.Data.Coda
{

    public class CodaFinalStateLine
        : CodaLine
    {

        public CodaFinalStateLine(int sequenceNumber, DateTime date, decimal balance)
            : base(CodaLineType.FinalState)
        {
            this.SequenceNumber = sequenceNumber;
            this.Date = date;
            this.Balance = balance;
        }

        public int SequenceNumber { get; }

        public DateTime Date { get; }

        public decimal Balance { get; }

    }

}
