using System;
using System.Collections.Generic;
using System.Linq;

namespace Neuroglia.Data.Coda
{

    public class CodaStatement
    {

        public CodaStatement(string bankId, DateTime date, string fileReference, string transactionReference, string externalReference, int paperStatementSequenceNumber,
            BankAccount account, DateTime initialBalanceDate, decimal initialBalance, DateTime? finalBalanceDate, decimal? finalBalance, string message, bool isDuplicate, 
            int recordsCount, decimal totalDebited, decimal totalCredited, IEnumerable<CodaTransaction> transactions)
        {
            this.BankId = bankId;
            this.Date = date;
            this.FileReference = fileReference;
            this.TransactionReference = transactionReference;
            this.ExternalReference = externalReference;
            this.PaperStatementSequenceNumber = paperStatementSequenceNumber;
            this.Account = account;
            this.InitialBalanceDate = initialBalanceDate;
            this.InitialBalance = initialBalance;
            this.FinalBalanceDate = finalBalanceDate;
            this.FinalBalance = finalBalance;
            this.Communication = message;
            this.IsDuplicate = isDuplicate;
            this.RecordsCount = recordsCount;
            this.TotalDebited = totalDebited;
            this.TotalCredited = totalCredited;
            this.Transactions = transactions == null ? new List<CodaTransaction>().AsReadOnly() : transactions.ToList().AsReadOnly();
        }

        public string BankId { get; }

        public DateTime Date { get; }

        public string FileReference { get; }

        public string TransactionReference { get; }

        public string ExternalReference { get; }

        public int PaperStatementSequenceNumber { get; }

        public BankAccount Account { get; }

        public DateTime InitialBalanceDate { get; }

        public decimal InitialBalance { get; }

        public DateTime? FinalBalanceDate { get; }

        public decimal? FinalBalance { get; }

        public string Communication { get; }

        public bool IsDuplicate { get; }

        public int RecordsCount { get; }

        public decimal TotalDebited { get; }

        public decimal TotalCredited { get; }

        public IReadOnlyCollection<CodaTransaction> Transactions { get; }

    }

}
