using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplicationGUI
{
    /**
     * <summary>
     * This is a generic Visa Account class and it is derived from the 
     * account class
     * </summary>
     */
    public class VisaAccount : Account
    {
        // Private instance variables
        private static double INTEREST_RATE = 19.99;
        private double creditLimit;

        /**
         * <summary>
         * This constructor method takes two parameters: balance and creditLimit
         * </summary>
         * 
         * @constructor VisaAccount
         * @param {double} balance
         * @param {double} creditLimit
         */
        public VisaAccount(double balance = 0, double creditLimit = -1200)
            : base("VS", balance)
        {
            this.creditLimit = creditLimit;
        }

        /**
         * <summary>
         * This method increases the balance property based on the Person and
         * amount specified 
         * </summary>
         * 
         * @method DoPayment
         * @param {double} balance
         * @param {Person} person
         * @return {void}
         */
        public void DoPayment(double amount, Person person)
        {
            base.Deposit(amount, person);
        }

        /**
         * <summary>
         * This method decreases the balance property based on the Person and
         * amount specified 
         * </summary>
         * 
         * @method DoPurchase
         * @return {void}
         */
        public void DoPurchase(double amount, Person person)
        {
            if (!IsHolder(person.Name))
                throw new AccountException(AccountException.NAME_NOT_ASSOCIATED_WITH_ACCOUNT, person.Name);
            if (!person.IsAuthenticated)
                throw new AccountException(AccountException.USER_NOT_LOGGED_IN, person.Name);
            if (Balance - amount < creditLimit)
                throw new AccountException(AccountException.CREDIT_LIMIT_HAS_BEEN_EXCEEDED, Number);

            base.Deposit(-amount, person);
        }

        /**
         * <summary>
         * This method clears the list of transactions and updates the Balance
         * property
         * </summary>
         * 
         * @method PrepareMonthlyReport
         * @return {void}
         */
        public override void PrepareMonthlyReport()
        {
            double interest = LowestBalance * INTEREST_RATE / 12;
            Balance = Balance - interest;
            transactions.Clear();
        }
    }
}
