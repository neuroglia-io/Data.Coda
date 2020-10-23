using System;

namespace Neuroglia.Data.Coda
{

    public class CodaInitialStateLine
        : CodaLine
    {

        public CodaInitialStateLine(int statementSequenceNumber, BankAccount account, decimal balance, DateTime date, int paperStatementSequenceNumber)
            : base(CodaLineType.InitialState)
        {
            this.StatementSequenceNumber = statementSequenceNumber;
            this.Account = account;
            this.Balance = balance;
            this.Date = date;
            this.PaperStatementSequenceNumber = paperStatementSequenceNumber;
        }

        public int StatementSequenceNumber { get; }

        public BankAccount Account { get; }

        public decimal Balance { get; }

        public DateTime Date { get; }

        public int PaperStatementSequenceNumber { get; }

    }

}
