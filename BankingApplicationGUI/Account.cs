using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplicationGUI
{
    /**
     * <summary>
     * This class creates an object that represents a Bank account
     * </summary>
     * 
     * @class Account
     */
    public abstract class Account
    {
        // Private instance variable
        private static int LAST_NUMBER = 100000;

        // Public instance variables
        public readonly string Number;
        public readonly List<Person> holders = new List<Person>();
        public readonly List<Transaction> transactions = new List<Transaction>();

        // Public properties
        public double Balance { get; protected set; }
        public double LowestBalance { get; private set; }


        /**
         * <summary>
         * This constructor takes two parameters: type and balance to instantiate an Account object
         * </summary>
         * 
         * @ constructor Account
         * @ param {string} type
         * @ param {double} balance
         */
        public Account(string type, double balance)
        {
            Balance = balance;
            LowestBalance = balance;
            Number = type + "-" + LAST_NUMBER++;
        }

        public void AddUser(Person person)
        {
            holders.Add(person);
        }

        public void Deposit(double amount, Person person)
        {
            Balance += amount;
            if (Balance < LowestBalance)
                LowestBalance = Balance;
            Transaction t = new Transaction(Number, amount, Balance, person, DateTime.Now);
            transactions.Add(t);
        }

        public bool IsHolder(string name)
        {
            foreach (Person p in holders)
                if (p.Name == name)
                    return true;

            return false;
        }

        public override string ToString()
        {
            string names = string.Empty;
            foreach (Person p in holders)
                names += p.Name + " ";
            string activities = "\nActivities: ";
            foreach (Transaction t in transactions)
                activities += "\n  " + t;

            return string.Format("\n{0} {1:c} {2}{3}", Number, Balance, names, activities);
        }

        public abstract void PrepareMonthlyReport();

        public string Key
        {
            get
            {
                return Number;
            }
        }
    }
}
