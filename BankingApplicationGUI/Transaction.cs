using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplicationGUI
{
    /**
     * <summary>
     * This class keeps track of transactions performed by
     * a user
     * </summary>
     * 
     * @class Transaction
     */
    public sealed class Transaction
    {
        // Public instance variables
        public readonly string AccountNumber;
        public readonly double Amount;
        public readonly double EndBalance;
        public readonly Person Originator;
        public readonly DateTime Time;

        /**
        * <summary>
        * This constructor method takes five parameters: accountNumber, amount, 
        * endBalance, person and time
        * </summary>
        * 
        * @constructor VisaAccount
        * @param {string} accountNumber
        * @param {double} amount
        * @param {double} endBalance
        * @param {Person} person
        * @param {DateTime} time
        */
        public Transaction(string accountNumber, double amount, double
            endBalance, Person person, DateTime time)
        {
            AccountNumber = accountNumber;
            Amount = amount;
            EndBalance = endBalance;
            Originator = person;
            Time = time;
        }

        public override string ToString()
        {
            if (Amount < 0)
                return string.Format("{0} withdraw {1:c} on {2}", Originator.Name, -Amount, Time.ToShortTimeString());
            return string.Format("{0} deposit {1:c} on {2}", Originator.Name, Amount, Time.ToShortTimeString());
        }
    }
}
