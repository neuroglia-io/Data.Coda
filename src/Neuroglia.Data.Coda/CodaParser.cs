using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Neuroglia.Data.Coda
{

    public class CodaParser
        : ICodaParser
    {

        public virtual CodaDocument Parse(string coda)
        {
            string[] codaLines = coda.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            List<CodaLine> lines = new List<CodaLine>(codaLines.Length);
            foreach(string codaLine in codaLines)
            {
                lines.Add(this.ParseLine(codaLine));
            }
            CodaDocument document = new CodaDocument(lines, this.ParseStatements(lines));
            return document;
        }

        public virtual CodaLine ParseLine(string line)
        {
            CodaLine parsedLine = null;
            switch (line.First())
            {
                case '0':
                    parsedLine = this.ParseHeader(line);
                    break;
                case '1':
                    parsedLine = this.ParseInitialState(line);
                    break;
                case '2':
                    switch (line[1])
                    {
                        case '1':
                            parsedLine = this.ParseTransactionPart1(line);
                            break;
                        case '2':
                            parsedLine = this.ParseTransactionPart2(line);
                            break;
                        case '3':
                            parsedLine = this.ParseTransactionPart3(line);
                            break;
                        default:
                            throw new FormatException();
                    }
                    break;
                case '3':
                    switch (line[1])
                    {
                        case '1':
                            parsedLine = this.ParseInformationPart1(line);
                            break;
                        case '2':
                            parsedLine = this.ParseInformationPart2(line);
                            break;
                        case '3':
                            parsedLine = this.ParseInformationPart3(line);
                            break;
                        default:
                            throw new FormatException();
                    }
                    break;
                case '4':
                    parsedLine = this.ParseCommunication(line);
                    break;
                case '8':
                    parsedLine = this.ParseFinalState(line);
                    break;
                case '9':
                    parsedLine = this.ParseFooter(line);
                    break;
                default:
                    throw new FormatException();
            }
            return parsedLine;
        }

        protected virtual CodaLine ParseHeader(string line)
        {
            DateTime date = CodaDate.Parse(line.Substring(5, 6).Trim());
            string bankId = line.Substring(11, 3).Trim();
            bool isDuplicate = line[16] == 'D';
            string fileReference = line.Substring(24, 10).Trim();
            string addressee = line.Substring(34, 26).Trim();
            string bic = line.Substring(60, 11).Trim();
            string accountId = line.Substring(71, 11).Trim();
            string transactionReference = line.Substring(88, 16).Trim();
            string relatedReference = line.Substring(104, 16).Trim();
            string externalReference = line.Substring(83, 5).Trim();
            return new CodaHeaderLine(date, bankId, isDuplicate, fileReference, addressee, bic, accountId, transactionReference, relatedReference, externalReference);
        }

        protected virtual CodaLine ParseInitialState(string line)
        {
            AccountNumberType accountNumberType = (AccountNumberType)int.Parse(line[1].ToString());
            int statementSequenceNumber = int.Parse(line.Substring(2, 3).Trim());
            string accountInfo = line.Substring(5, 37).Trim();
            bool isPositive = line[42] == '0';
            decimal balance = decimal.Parse(line.Substring(43, 15).Trim()) / 1000;
            if (!isPositive)
                balance *= -1;
            DateTime date = CodaDate.Parse(line.Substring(58, 6).Trim());
            string accountHolderName = line.Substring(64, 26).Trim();
            string accountDescription = line.Substring(90, 35).Trim();
            int paperStatementSequenceNumber = int.Parse(line.Substring(125, 3).Trim());
            BankAccount account = BankAccount.Create(accountNumberType, accountInfo, accountHolderName, accountDescription);
            return new CodaInitialStateLine(statementSequenceNumber, account, balance, date, paperStatementSequenceNumber);
        }

        protected virtual CodaLine ParseTransactionPart1(string line)
        {
            int sequenceNumber = int.Parse(line.Substring(2, 4).Trim());
            int detailNumber = int.Parse(line.Substring(6, 4).Trim());
            string reference = line.Substring(10, 21).Trim();
            CodaTransactionType type = (CodaTransactionType)int.Parse(line[31].ToString());
            decimal amount = decimal.Parse(line.Substring(32, 15).Trim()) / 1000;
            if (type == CodaTransactionType.Debit)
                amount *= -1;
            string codaDate = line.Substring(47, 6).Trim();
            DateTime? effectiveDate = null;
            if(codaDate != "000000")
                effectiveDate = CodaDate.Parse(codaDate);
            string code = line.Substring(53, 8).Trim();
            string family = code.Substring(1, 2).Trim();
            string operation = code.Substring(3, 2).Trim();
            string category = code.Substring(5, 3).Trim();
            CodaCommunication communication = CodaCommunication.Parse(line.Substring(61, 54).Trim(), family == "05");
            DateTime entryDate = CodaDate.Parse(line.Substring(115, 6).Trim());
            int statementSequenceNumber = int.Parse(line.Substring(121, 3).Trim());
            int globalizationCode = int.Parse(line.Substring(124, 1).Trim());
            return new CodaTransactionPart1Line(sequenceNumber, statementSequenceNumber, detailNumber, reference, effectiveDate, 
                entryDate, type, code, family, operation, category, communication, amount, globalizationCode);
        }

        protected virtual CodaLine ParseTransactionPart2(string line)
        {
            int sequenceNumber = int.Parse(line.Substring(2, 4).Trim());
            int detailNumber = int.Parse(line.Substring(6, 4).Trim());
            string communication = line.Substring(10, 53).Trim();
            string customerReference = line.Substring(63, 35).Trim();
            string counterpartyBic = line.Substring(98, 11).Trim();
            string codaRTransactionType = line[112].ToString();
            CodaRTransactionType? rTransactionType = null;
            if(!string.IsNullOrWhiteSpace(codaRTransactionType))
                rTransactionType = (CodaRTransactionType)int.Parse(codaRTransactionType);
            string rReasonCode = line.Substring(113, 4).Trim();
            string categoryPurpose = line.Substring(117, 4).Trim();
            string purpose = line.Substring(121, 4).Trim();
            return new CodaTransactionPart2Line(sequenceNumber, detailNumber, communication, customerReference, counterpartyBic, rTransactionType, rReasonCode, categoryPurpose, purpose);
        }

        protected virtual CodaLine ParseTransactionPart3(string line)
        {
            int sequenceNumber = int.Parse(line.Substring(2, 4).Trim());
            int detailNumber = int.Parse(line.Substring(6, 4).Trim());
            string accountInfo = line.Substring(10, 37).Trim();
            string holderName = line.Substring(47, 35).Trim();
            string communication = line.Substring(82, 43).Trim();
            AccountNumberType accountNumberType;
            if(accountInfo.Trim().Length < 37 && accountInfo.Substring(accountInfo.Length - 2, 2) == "BE"
                || accountInfo.Substring(0, 2) == "BE")
                accountNumberType = AccountNumberType.Belgian;
            else if (accountInfo.Substring(accountInfo.Length - 2, 2) == "BE")
                accountNumberType = AccountNumberType.BelgianIban;
            else
                accountNumberType = AccountNumberType.Foreign;
            BankAccount counterPartyAccount = BankAccount.Create(accountNumberType, accountInfo, holderName, null);
            return new CodaTransactionPart3Line(sequenceNumber, detailNumber, counterPartyAccount, communication);
        }

        protected virtual CodaLine ParseInformationPart1(string line)
        {
            int sequenceNumber = int.Parse(line.Substring(2, 4).Trim());
            int detailNumber = int.Parse(line.Substring(6, 4).Trim());
            string reference = line.Substring(10, 31).Trim();
            string transactionCode = line.Substring(31, 8).Trim();
            CodaCommunication communication = CodaCommunication.Parse(line.Substring(39, 74).Trim(), false);
            return new CodaInformationPart1Line(sequenceNumber, detailNumber, reference, transactionCode, communication);
        }

        protected virtual CodaLine ParseInformationPart2(string line)
        {
            int sequenceNumber = int.Parse(line.Substring(2, 4).Trim());
            int detailNumber = int.Parse(line.Substring(6, 4).Trim());
            string communication = line.Substring(10, 105).Trim();
            return new CodaInformationPart2Line(sequenceNumber, detailNumber, communication);
        }

        protected virtual CodaLine ParseInformationPart3(string line)
        {
            int sequenceNumber = int.Parse(line.Substring(2, 4).Trim());
            int detailNumber = int.Parse(line.Substring(6, 4).Trim());
            string communication = line.Substring(10, 105).Trim();
            return new CodaInformationPart3Line(sequenceNumber, detailNumber, communication);
        }

        protected virtual CodaLine ParseCommunication(string line)
        {
            int sequenceNumber = int.Parse(line.Substring(2, 4).Trim());
            int detailNumber = int.Parse(line.Substring(6, 4).Trim());
            string communication = line.Substring(32, 80).Trim();
            return new CodaCommunicationLine(sequenceNumber, detailNumber, communication);
        }

        protected virtual CodaLine ParseFinalState(string line)
        {
            int sequenceNumber = int.Parse(line.Substring(1, 3).Trim());
            decimal balance = decimal.Parse(line.Substring(42, 15).Trim()) / 1000;
            bool isPositive = line[41] == '0';
            if (!isPositive)
                balance *= -1;
            DateTime date = CodaDate.Parse(line.Substring(57, 6).Trim());
            return new CodaFinalStateLine(sequenceNumber, date, balance);
        }

        protected virtual CodaLine ParseFooter(string line)
        {
            int recordsCount = int.Parse(line.Substring(16, 6));
            decimal totalDebited = -decimal.Parse(line.Substring(22, 15)) / 1000;
            decimal totalCredited = decimal.Parse(line.Substring(37, 15)) / 1000;
            return new CodaFooterLine(recordsCount, totalDebited, totalCredited);
        }

        protected virtual IEnumerable<CodaStatement> ParseStatements(IEnumerable<CodaLine> lines)
        {
            List<CodaStatement> statements = new List<CodaStatement>();
            foreach (IEnumerable<CodaLine> statementLines in this.GetStatementsLines(lines))
            {
                CodaStatement statement = this.ParseStatement(statementLines);
                statements.Add(statement);
            }
            return statements;
        }

        protected virtual CodaStatement ParseStatement(IEnumerable<CodaLine> lines)
        {
            CodaHeaderLine headerLine = lines.OfType<CodaHeaderLine>().Single();
            CodaInitialStateLine initialStateLine = lines.OfType<CodaInitialStateLine>().Single();
            IEnumerable<CodaTransaction> transactions = this.GetTransactionsLines(lines.OfType<ICodaTransactionComponentLine>()).Select(l => this.ParseTransaction(l));
            CodaFinalStateLine finalStateLine = lines.OfType<CodaFinalStateLine>().FirstOrDefault();
            CodaFooterLine footerLine = lines.OfType<CodaFooterLine>().Single();
            string message = this.ParseStatementMessage(lines.OfType<CodaCommunicationLine>());
            BankAccount bankAccount = BankAccount.CreateFrom(initialStateLine.Account, headerLine.AccountId, headerLine.Addressee, headerLine.Bic);
            return new CodaStatement(headerLine.BankId, headerLine.Date, headerLine.FileReference, headerLine.TransactionReference, headerLine.ExternalReference,
                initialStateLine.PaperStatementSequenceNumber, bankAccount, initialStateLine.Date, initialStateLine.Balance, finalStateLine?.Date, finalStateLine.Balance, 
                message, headerLine.IsDuplicate, footerLine.RecordsCount, footerLine.TotalDebited, footerLine.TotalCredited, transactions);
        }

        protected virtual CodaTransaction ParseTransaction(IEnumerable<ICodaTransactionComponentLine> lines)
        {
            CodaTransactionPart1Line transactionPart1Line = lines.OfType<CodaTransactionPart1Line>().Single();
            CodaTransactionPart2Line transactionPart2Line = lines.OfType<CodaTransactionPart2Line>().FirstOrDefault();
            CodaTransactionPart3Line transactionPart3Line = lines.OfType<CodaTransactionPart3Line>().FirstOrDefault();
            CodaInformationPart1Line informationPart1Line = lines.OfType<CodaInformationPart1Line>().FirstOrDefault();
            BankAccount counterPartyAccount = null;
            if(transactionPart3Line != null)
                counterPartyAccount = BankAccount.CreateFrom(transactionPart3Line.CounterPartyAccount, null, null, transactionPart2Line.CounterPartyBic);
            CodaCommunication communication = null;
            if (transactionPart1Line?.Communication == null)
                communication = transactionPart1Line.Communication;
            else if (informationPart1Line?.Communication == null)
                communication = informationPart1Line?.Communication;
            string message = Regex.Replace(this.ParseTransactionMessage(lines).Trim(), " {2,}", " ");
            return new CodaTransaction(transactionPart1Line.Type, transactionPart1Line.Family, transactionPart1Line.Operation, transactionPart1Line.Category,
                transactionPart1Line.StatementSequenceNumber, transactionPart1Line.SequenceNumber, transactionPart1Line.Reference, transactionPart1Line.EntryDate, transactionPart1Line.EffectiveDate,
                transactionPart1Line.Amount, counterPartyAccount, transactionPart1Line.Communication, message, transactionPart1Line.GlobalizationCode);
        }

        protected virtual string ParseStatementMessage(IEnumerable<CodaCommunicationLine> lines)
        {
            StringBuilder messageString = new StringBuilder();
            foreach (CodaCommunicationLine line in lines)
            {
                string message = line.Communication;
                if (message.Length > 0 && messageString.Length > 0)
                    messageString.Append(" ");
                messageString.Append(message);
            }
            return messageString.ToString();
        }

        protected virtual string ParseTransactionMessage(IEnumerable<ICodaTransactionComponentLine> lines)
        {
            return string.Join(" ", lines.Select(line =>
            {
                switch (line)
                {
                    case CodaTransactionPart1Line l:
                        return l.Communication?.Communication;
                    case CodaTransactionPart2Line l:
                        return l.Communication;
                    case CodaTransactionPart3Line l:
                        return l.Communication;
                    case CodaInformationPart1Line l:
                        return l.Communication?.Communication;
                    case CodaInformationPart2Line l:
                        return l.Communication;
                    case CodaInformationPart3Line l:
                        return l.Communication;
                    default:
                        throw new NotSupportedException();
                }
            }));
        }

        protected virtual IEnumerable<IEnumerable<CodaLine>> GetStatementsLines(IEnumerable<CodaLine> lines)
        {
            Dictionary<int, List<CodaLine>> linesPerStatement = new Dictionary<int, List<CodaLine>>();
            int statementCount = 0;
            foreach (CodaLine line in lines)
            {
                if (linesPerStatement.Count == 0
                    || line.LineType == CodaLineType.Header)
                {
                    statementCount += 1;
                    linesPerStatement[statementCount] = new List<CodaLine>();
                }
                linesPerStatement[statementCount].Add(line);
            }
            return linesPerStatement.Values;
        }

        protected virtual IEnumerable<IEnumerable<ICodaTransactionComponentLine>> GetTransactionsLines(IEnumerable<ICodaTransactionComponentLine> lines)
        {
            Dictionary<int, List<ICodaTransactionComponentLine>> linesPerTransaction = new Dictionary<int, List<ICodaTransactionComponentLine>>();
            var transactionCount = 0;
            var sequenceNumber = -1;
            foreach (ICodaTransactionComponentLine line in lines)
            {
                if (linesPerTransaction.Count == 0 || sequenceNumber != line.SequenceNumber)
                {
                    sequenceNumber = line.SequenceNumber;
                    transactionCount += 1;
                    linesPerTransaction[transactionCount] = new List<ICodaTransactionComponentLine>();
                }
                linesPerTransaction[transactionCount].Add(line);
            }
            return linesPerTransaction.Values;
        }

    }

}
