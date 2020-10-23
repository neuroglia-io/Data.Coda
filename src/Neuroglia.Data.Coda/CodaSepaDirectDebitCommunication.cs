using System;

namespace Neuroglia.Data.Coda
{
    public class CodaSepaDirectDebitCommunication
        : CodaCommunication
    {

        public CodaSepaDirectDebitCommunication(string communication, string creditorIdentificationCode, string mandateReference, int paidReason, int scheme, DateTime settlementDate, int sepaType) 
            : base(CodaCommunicationType.StructuredSepaDirectDebit, communication)
        {
            this.CreditorIdentificationCode = creditorIdentificationCode;
            this.MandateReference = mandateReference;
            this.PaidReason = paidReason;
            this.Scheme = scheme;
            this.SettlementDate = settlementDate;
            this.SepaType = sepaType;
        }

        public string CreditorIdentificationCode { get; }

        public string MandateReference { get; }

        public int PaidReason { get; }

        public int Scheme { get; }

        public DateTime SettlementDate { get; }

        public int SepaType { get; }

        public static CodaSepaDirectDebitCommunication Parse(string communication)
        {
            DateTime settlementDate = CodaDate.Parse(communication.Substring(0, 6));
            int sepaType = int.Parse(communication.Substring(6, 1));
            int scheme = int.Parse(communication.Substring(7, 1));
            int paidReason = int.Parse(communication.Substring(8, 1));
            string creditorIdentificationCode = communication.Substring(9, 35).Trim();
            string mandateReference = communication.Substring(44).Trim();
            return new CodaSepaDirectDebitCommunication(communication, creditorIdentificationCode, mandateReference, paidReason, scheme, settlementDate, sepaType);
        }

    }

}
