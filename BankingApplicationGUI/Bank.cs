using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplicationGUI
{
    /**
     * <summary>
     * This class simulates activities that take place at a bank
     * </summary>
     * 
     * @class Bank
     */
    public static class Bank
    {
        // List of accounts and persons
        public static List<Account> accounts;
        public static List<Person> persons;

        static Bank()
        {
            InitializeAccounts();
        }

        /**
         * <summary>
         * This method creates multiple accounts and persons, and 
         * adds the created persons to the created accounts
         * </summary>
         * 
         * @ method InitializeAccounts
         */
        static void InitializeAccounts()
        {
            CreateAccounts();
            CreatePersons();
            accounts[0].AddUser(persons[0]);
            accounts[0].AddUser(persons[1]);
            accounts[0].AddUser(persons[2]);

            accounts[1].AddUser(persons[3]);
            accounts[1].AddUser(persons[4]);
            accounts[1].AddUser(persons[2]);

            accounts[2].AddUser(persons[0]);
            accounts[2].AddUser(persons[5]);
            accounts[2].AddUser(persons[6]);

            accounts[3].AddUser(persons[5]);
            accounts[3].AddUser(persons[6]);

            accounts[4].AddUser(persons[5]);
            accounts[4].AddUser(persons[7]);
            accounts[4].AddUser(persons[8]);

            accounts[5].AddUser(persons[5]);
            accounts[5].AddUser(persons[6]);
        }

        /**
         * <summary>
         * This method initializes different persons and adds them 
         * to the Person list
         * </summary>
         * 
         * @ method CreatePersons
         */
        private static void CreatePersons()
        {
            persons = new List<Person>(){
                new Person("Narendra", "1234-5678"),
                new Person("Ilia", "2345-6789"),
                new Person("Tom", "3456-7890"),
                new Person("Syed", "4567-8901"),
                new Person("Arben", "5678-9012"),
                new Person("Patrick", "6789-0123"),
                new Person("Yin", "7890-1234"),
                new Person("Hao", "8901-2345"),
                new Person("Jake", "9012-3456")
            };
        }

        /**
         * <summary>
         * This method initializes different accounts, and adds them 
         * to the Account list
         * </summary>
         * 
         * @ method CreateAccounts
         */
        private static void CreateAccounts()
        {
            accounts = new List<Account>{
                new VisaAccount(),              //VS-100000
                new VisaAccount(550, -500),     //VS-100001
                new SavingAccount(5000),        //SV-100002
                new SavingAccount(),            //SV-100003
                new CheckingAccount(2000),      //CK-100004
                new CheckingAccount(1500, true) //CK-100005
            };
        }

        /**
         * <summary>
         * This method returns a Person object based on the 
         * specified name
         * </summary>
         * 
         * @ method GetPerson
         * @ param {string} name
         * 
         * <exception>
         * Thrown when Person object with the specified name
         * cannot be found
         * </exception>
         */
        public static Person GetPerson(string name)
        {

            foreach (var p in persons)
            {
                if (p.Name == name)
                    return p;
            }
            throw new AccountException(AccountException.USER_DOES_NOT_EXIST, name);
        }

        /**
         * <summary>
         * This method returns an Account object based on the 
         * specified number
         * </summary>
         * 
         * @ method GetAccount
         * @ param {string} number
         * 
         * <exception>
         * Thrown when Account object with the specified number
         * cannot be found
         * </exception>
         */
        public static Account GetAccount(string number)
        {
            foreach (var a in accounts)
            {
                if (a.Number == number)
                    return a;
            }
            throw new AccountException(AccountException.ACCOUNT_DOES_NOT_EXIST, number);
        }

        /**
         * <summary>
         * This method displays the list of Accounts to the console
         * </summary>
         * 
         * @ method PrintAccounts
         */
        public static void PrintAccounts()
        {
            foreach (var a in accounts)
                Console.WriteLine(a);
        }

        /**
         * <summary>
         * This method displays the list of Persons to the console
         * </summary>
         * 
         * @ method PrintPersons
         */
        public static void PrintPersons()
        {
            foreach (var p in persons)
                Console.WriteLine(p);
        }

    }
}
