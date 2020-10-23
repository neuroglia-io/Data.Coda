using System;

namespace Neuroglia.Data.Coda
{

    public class BankAccount
    {

        public BankAccount(AccountNumberType numberType, string number, string holderName, string description, string currency, string country)
        {
            this.NumberType = numberType;
            this.Number = number;
            this.HolderName = holderName;
            this.Description = description;
            this.Currency = currency;
            this.Country = country;
        }

        public BankAccount(string id, string name, string holderName, string description, AccountNumberType numberType, string number, string bic, string currency, string country)
        {
            this.Id = id;
            this.Name = name;
            this.HolderName = holderName;
            this.Description = description;
            this.NumberType = numberType;
            this.Number = number;
            this.Bic = bic;
            this.Currency = currency;
            this.Country = country;
        }

        public string Id { get; }

        public string Name { get; }

        public string HolderName { get; }

        public string Description { get; }

        public AccountNumberType NumberType { get; }

        public string Number { get; }

        public string Bic { get; }

        public string Currency { get; }

        public string Country { get; }

        public bool IsIban
        {
            get
            {
                return this.NumberType == AccountNumberType.BelgianIban || this.NumberType == AccountNumberType.ForeignIban;
            }
        }

        public bool IsBelgian
        {
            get
            {
                return this.NumberType == AccountNumberType.Belgian || this.NumberType == AccountNumberType.BelgianIban;
            }
        }

        public static BankAccount Create(AccountNumberType numberType, string accountInfo, string holderName, string description)
        {
            string number = null;
            string currency = null;
            string country = null;
            switch (numberType)
            {
                case AccountNumberType.Belgian:
                    number = accountInfo.Substring(0, 12).Trim();
                    currency = accountInfo.Substring(13, 3).Trim();
                    country = "BE";
                    break;
                case AccountNumberType.Foreign:
                    if(accountInfo.Length >= 37)
                    {
                        number = accountInfo.Substring(0, 34).Trim();
                        currency = accountInfo.Substring(34, 3).Trim();
                    }
                    else
                    {
                        number = accountInfo.Trim();
                        currency = "EUR";
                    }
                    country = accountInfo.Substring(0, 2);
                    if (!country.IsAlphabetic())
                        country = null;
                    break;
                case AccountNumberType.BelgianIban:
                    if (accountInfo.Length >= 37)
                    {
                        number = accountInfo.Substring(0, 31).Trim();
                        currency = accountInfo.Substring(34, 3).Trim();
                    }
                    else
                    {
                        number = accountInfo.Trim();
                        currency = "EUR";
                    }
                    country = "BE";
                    break;
                case AccountNumberType.ForeignIban:
                    if (accountInfo.Length >= 37)
                    {
                        number = accountInfo.Substring(0, 34).Trim();
                        currency = accountInfo.Substring(34, 3).Trim();
                    }
                    else
                    {
                        number = accountInfo.Trim();
                        currency = "EUR";
                    }
                    country = accountInfo.Substring(0, 2);
                    if (!country.IsAlphabetic())
                        country = null;
                    break;
                default:
                    throw new NotSupportedException($"The specified account number type '{numberType}' is not supported");
            }
            return new BankAccount(numberType, number, holderName, description, currency, country);
        }

        public static BankAccount CreateFrom(BankAccount account, string id, string name, string bic)
        {
            return new BankAccount(id, name, account.HolderName, account.Description, account.NumberType, account.Number, bic, account.Currency, account.Country);
        }

    }

}
