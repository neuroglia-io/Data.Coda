using System;

namespace Neuroglia.Data.Coda
{

    public class CodaHeaderLine
        : CodaLine
    {

        public CodaHeaderLine(DateTime date, string bankId, bool isDuplicate, string fileReference, string addressee, string bic, string accountId, 
            string transactionReference, string relatedReference, string externalReference) 
            : base(CodaLineType.Header)
        {
            this.Date = date;
            this.BankId = bankId;
            this.IsDuplicate = isDuplicate;
            this.FileReference = fileReference;
            this.Addressee = addressee;
            this.Bic = bic;
            this.AccountId = accountId;
            this.TransactionReference = transactionReference;
            this.RelatedReference = relatedReference;
            this.ExternalReference = externalReference;
        }

        public DateTime Date { get; }

        public string BankId { get; }

        public bool IsDuplicate { get; }

        public string FileReference { get; }

        public string Addressee { get; }

        public string Bic { get; }

        public string AccountId { get; }

        public string TransactionReference { get; }

        public string RelatedReference { get; }

        public string ExternalReference { get; }

    }

}
