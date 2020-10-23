using System;

namespace Neuroglia.Data.Coda
{

    public class CodaTransaction
    {

        public CodaTransaction(CodaTransactionType type, string family, string operation, string category, int statementSequenceNumber, int sequenceNumber, string reference, 
            DateTime entryDate, DateTime? effectiveDate, decimal amount, BankAccount counterPartyAccount, CodaCommunication communication, string message, int globalizationCode)
        {
            this.Type = type;
            this.Family = family;
            this.Operation = operation;
            this.Category = category;
            this.StatementSequenceNumber = statementSequenceNumber;
            this.SequenceNumber = sequenceNumber;
            this.Reference = reference;
            this.EntryDate = entryDate;
            this.EffectiveDate = effectiveDate;
            this.Amount = amount;
            this.CounterPartyAccount = counterPartyAccount;
            this.Communication = communication;
            this.Message = message;
            this.GlobalizationCode = globalizationCode;
        }

        public CodaTransactionType Type { get; }

        public string Family { get; }

        public string Operation { get; }

        public string Category { get; }

        public int StatementSequenceNumber { get; }

        public int SequenceNumber { get; }

        public string Reference { get; }

        public DateTime EntryDate { get; }

        public DateTime? EffectiveDate { get; }

        public decimal Amount { get; }

        public BankAccount CounterPartyAccount { get; }

        public CodaCommunication Communication { get; }

        public string Message { get; }

        public int GlobalizationCode { get; }

    }

}
