using System.Linq;
using System.Text.RegularExpressions;

namespace Neuroglia.Data.Coda
{
    public class CodaCommunication
    {

        public CodaCommunication(CodaCommunicationType type, string communication)
        {
            this.Type = type;
            this.Communication = communication;
        }

        public CodaCommunicationType Type { get; }

        public string Communication { get; }

        public static CodaCommunication Parse(string communicationInfo, bool isSepaDirectDebit)
        {
            communicationInfo = Regex.Replace(communicationInfo, " {2,}", " ");
            if (string.IsNullOrWhiteSpace(communicationInfo.Substring(1)))
                return null;
            int communicationType = int.Parse(communicationInfo.First().ToString());
            if (communicationType == 0)
            {
                return new CodaCommunication(CodaCommunicationType.Unstructured, communicationInfo.Substring(1).Trim());
            }
            else
            {
                string structuredCommunicationType = communicationInfo.Substring(1, 3);
                if (structuredCommunicationType == "101")
                {
                    return new CodaCommunication(CodaCommunicationType.StructuredIso11649, communicationInfo.Substring(1, 12).Trim());
                }
                else if (structuredCommunicationType == "102")
                {
                    return new CodaCommunication(CodaCommunicationType.StructuredReconstituted, communicationInfo.Substring(1, 12).Trim());
                }
                else if (structuredCommunicationType == "103")
                {
                    return new CodaCommunication(CodaCommunicationType.StructuredNumber, communicationInfo.Substring(1, 12).Trim());
                }
                else if (structuredCommunicationType == "105")
                {
                    return new CodaCommunication(CodaCommunicationType.StructuredAmount, communicationInfo.Substring(1).Trim());
                }
                else if (structuredCommunicationType == "106")
                {
                    return new CodaCommunication(CodaCommunicationType.StructuredCalculation, communicationInfo.Substring(1).Trim());
                }
                else if (structuredCommunicationType == "107" && isSepaDirectDebit)
                {
                    return CodaSepaDirectDebitCommunication.Parse(communicationInfo.Substring(1).Trim());
                }
                else if (structuredCommunicationType == "108")
                {
                    return new CodaCommunication(CodaCommunicationType.StructuredClosing, communicationInfo.Substring(1).Trim());
                }
                else
                {
                    if(communicationInfo.Length >= 22)
                        return new CodaCommunication(CodaCommunicationType.Structured, communicationInfo.Substring(1, 21).Trim());
                    else
                        return new CodaCommunication(CodaCommunicationType.Structured, communicationInfo.Substring(1).Trim());
                }
            }
        }

    }

}
