using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplicationGUI
{
    /**
     * <summary>
     * This class defines a Person
     * </summary>
     * 
     * @class Person
     */
    public class Person
    {

        private string password;

        public bool IsAuthenticated { get; private set; }
        public string Name { get; private set; }
        public readonly string SIN;

        public Person(string name, string sin)
        {
            SIN = sin;
            password = sin.Substring(0, 3);
            Name = name;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} ({2}) {3}", SIN, Name, IsAuthenticated ? "logged in" : "not logged in", password);
        }
        public void Login(string password)
        {
            if (this.password != password)
                throw new AccountException(AccountException.PASSWORD_INCORRECT, Name);
            IsAuthenticated = true;
        }

        public void Logout()
        {
            IsAuthenticated = false;
        }
    }
}
