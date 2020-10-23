using System;

namespace Neuroglia.Data.Coda
{

    public class CodaTransactionPart1Line
        : CodaLine, ICodaTransactionComponentLine
    {

        public CodaTransactionPart1Line(int sequenceNumber, int statementSequenceNumber, int detailNumber, string reference, DateTime? effectiveDate, DateTime entryDate, 
            CodaTransactionType type, string code, string family, string operation, string category, CodaCommunication communication, decimal amount, int globalizationCode)
            : base(CodaLineType.TransactionPart1)
        {
            this.SequenceNumber = sequenceNumber;
            this.StatementSequenceNumber = statementSequenceNumber;
            this.DetailNumber = detailNumber;
            this.Reference = reference;
            this.EffectiveDate = effectiveDate;
            this.EntryDate = entryDate;
            this.Type = type;
            this.Code = code;
            this.Family = family;
            this.Operation = operation;
            this.Category = category;
            this.Communication = communication;
            this.Amount = amount;
            this.GlobalizationCode = globalizationCode;
        }

        public int SequenceNumber { get; }

        public int StatementSequenceNumber { get; }

        public int DetailNumber { get; }

        public string Reference { get; }

        public DateTime? EffectiveDate { get; }

        public DateTime EntryDate { get; }

        public CodaTransactionType Type { get; }

        public string Code { get; }

        public string Family { get; }

        public string Operation { get; }

        public string Category { get; }

        public CodaCommunication Communication { get; }

        public decimal Amount { get; }

        public int GlobalizationCode { get; }

    }

}
