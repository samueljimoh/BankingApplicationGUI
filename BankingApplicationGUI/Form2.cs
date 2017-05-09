using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankingApplicationGUI
{
    public partial class Form2 : Form
    {
        public Form1 FirstForm = Program.FirstForm;

        Dictionary<string, Account> accounts = new Dictionary<string, Account>();

        List<String> accountNumbers;
        List<String> userNames;
        public Form2()
        {
            //Set up form on load
            InitializeComponent();
            InitializeAccountList();
            InitializeGUITransactions();
            RefreshAccountList();
        }

        private void InitializeAccountList()
        {
            comboBType.DataSource = new List<string>() { "SV", "CK", "VS" };

            foreach (var a in Bank.accounts)
                accounts.Add(a.Key, a);

            RefreshAccountList();
            RefreshUserList();
        }

        /** Display account information */
        private void DisplayAccountInfo(string key)
        {
            Account a = accounts[key];
            lblAccountInfo.Text = a.ToString();
        }

        #region form open and close events
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Program.FirstForm.Show();

            this.Hide();
        }
        #endregion

        /** Display selected account information */
        private void lstAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayAccountInfo(lstAccounts.SelectedItem.ToString());
            lblSelectedAccount.Text = lstAccounts.SelectedItem.ToString();
        }

        #region Transactions click events
        private void btnDeposit_Click(object sender, EventArgs e)
        {
            PerformTransactions("deposit");
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            PerformTransactions("withdraw");
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            PerformTransactions("purchase");
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            PerformTransactions("payment");
        }

        #endregion

        #region Refresh account and user lists
        /** This function refeshes the bank account list */
        private void RefreshAccountList()
        {
            accountNumbers = new List<string>(accounts.Keys);
            lstAccounts.DataSource = accountNumbers;
        }

        /** This function refreshes the user list */
        private void RefreshUserList()
        {
            userNames = new List<string>();

            foreach (Person p in Bank.persons)
            {
                userNames.Add(p.Name);
            }

            lstUsers.DataSource = userNames;
        }
        #endregion

        /** This function performs all the transactions for account */
        private void PerformTransactions(string transaction)
        {
            string accountType = lstAccounts.SelectedItem.ToString();
            string checkAccountType = accountType.Substring(0, 2);

            try
            {
                switch (checkAccountType)
                {
                    case "SV":
                        SavingAccount s = (SavingAccount)Bank.GetAccount(accountType);
                        if (transaction == "deposit")
                            s.Deposit(Convert.ToDouble(txtAmount.Text), Bank.GetPerson(lblCurrentUser.Text));
                        else if (transaction == "withdraw")
                            s.Withdraw(Convert.ToDouble(txtAmount.Text), Bank.GetPerson(lblCurrentUser.Text));
                        else
                            MessageBox.Show("Transaction can not be performed for the selected account");
                        break;
                    case "CK":
                        CheckingAccount c = (CheckingAccount)Bank.GetAccount(accountType);
                        if (transaction == "deposit")
                            c.Deposit(Convert.ToDouble(txtAmount.Text), Bank.GetPerson(lblCurrentUser.Text));
                        else if (transaction == "withdraw")
                            c.Withdraw(Convert.ToDouble(txtAmount.Text), Bank.GetPerson(lblCurrentUser.Text));
                        else
                            MessageBox.Show("Transaction can not be performed for the selected account");
                        break;
                    case "VS":
                        VisaAccount v = (VisaAccount)Bank.GetAccount(accountType);
                        if (transaction == "payment")
                            v.DoPayment(Convert.ToDouble(txtAmount.Text), Bank.GetPerson(lblCurrentUser.Text));
                        else if (transaction == "purchase")
                            v.DoPurchase(Convert.ToDouble(txtAmount.Text), Bank.GetPerson(lblCurrentUser.Text));
                        else
                            MessageBox.Show("Transaction can not be performed for the selected account");
                        break;
                }
            }
            catch (AccountException msg)
            {
                MessageBox.Show(msg.Message);
            }
        }

        /** This function initializes the gui with transactions */
        private void InitializeGUITransactions()
        {
            Person p0, p1, p2, p3, p4, p5, p6, p7, p8;
            p0 = Bank.GetPerson("Narendra");
            p1 = Bank.GetPerson("Ilia");
            p2 = Bank.GetPerson("Tom");
            p3 = Bank.GetPerson("Syed");
            p4 = Bank.GetPerson("Arben");
            p5 = Bank.GetPerson("Patrick");
            p6 = Bank.GetPerson("Yin");
            p7 = Bank.GetPerson("Hao");
            p8 = Bank.GetPerson("Jake");

            p0.Login("123"); p1.Login("234");
            p2.Login("345"); p3.Login("456");
            p4.Login("567"); p5.Login("678");
            p6.Login("789"); p7.Login("890");

            //a visa account
            VisaAccount a = (VisaAccount)Bank.GetAccount("VS-100000");
            a.DoPayment(1500, p0);
            a.DoPurchase(200, p1);
            a.DoPurchase(25, p2);
            a.DoPurchase(15, p0);
            a.DoPurchase(39, p1);
            a.DoPayment(400, p0);

            a = (VisaAccount)Bank.GetAccount("VS-100001");
            a.DoPurchase(25, p3);
            a.DoPurchase(20, p4);
            a.DoPurchase(15, p2);
            a.DoPayment(400, p0);

            //a saving account
            SavingAccount b = (SavingAccount)Bank.GetAccount("SV-100002");
            b.Withdraw(300, p0);
            b.Withdraw(32.90, p6);
            b.Withdraw(50, p5);
            b.Withdraw(111.11, p5);
            //Console.WriteLine(b); 

            b = (SavingAccount)Bank.GetAccount("SV-100003");
            b.Deposit(300, p3);     //ok even though p3 is not a holder
            b.Deposit(32.90, p2);
            b.Deposit(50, p5);
            b.Withdraw(111.11, p5);

            //a checking account
            CheckingAccount c = (CheckingAccount)Bank.GetAccount("CK-100005");
            c.Deposit(33.33, p7);
            c.Deposit(40.44, p7);
            c.Withdraw(450, p5);
            c.Withdraw(500, p5);
            c.Withdraw(645, p5);
            c.Withdraw(850, p6);

            c = (CheckingAccount)Bank.GetAccount("CK-100004");
            c.Deposit(33.33, p7);
            c.Deposit(40.44, p7);
            c.Withdraw(150, p5);
            c.Withdraw(200, p7);
            c.Withdraw(645, p7);
            c.Withdraw(350, p5);

            p0.Logout(); p1.Logout();
            p2.Logout(); p3.Logout();
            p4.Logout(); p5.Logout();
            p6.Logout(); p7.Logout();
        }

        /** Add new user to account */
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            Person newUser = new Person(txtUserName.Text, txtSIN.Text);
            Bank.persons.Add(newUser);
            RefreshUserList();
        }

        /** Create Accounts */
        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            string accountType = comboBType.SelectedItem.ToString();

            switch (accountType)
            {
                case "SV":
                    SavingAccount s = new SavingAccount(Convert.ToDouble(txtBalance.Text));
                    accounts.Add(s.Key, s);
                    RefreshAccountList();
                    break;
                case "CK":
                    bool overdraft = false;
                    if (chckBoxOverdraft.Checked)
                        overdraft = true;
                    CheckingAccount c = new CheckingAccount(Convert.ToDouble(txtBalance.Text), overdraft);
                    accounts.Add(c.Key, c);
                    RefreshAccountList();
                    break;
                case "VS":
                    VisaAccount v = new VisaAccount(Convert.ToDouble(txtBalance.Text), Convert.ToDouble(txtCreditLimit.Text));
                    accounts.Add(v.Key, v);
                    RefreshAccountList();
                    break;
            }
        }

        /** Prepare Monthly Report */
        private void btnPrepareReport_Click(object sender, EventArgs e)
        {
            string accountType = lstAccounts.SelectedItem.ToString();
            string checkAccountType = accountType.Substring(0, 2);

            switch (checkAccountType)
            {
                case "SV":
                    SavingAccount s = (SavingAccount)Bank.GetAccount(accountType);
                    s.PrepareMonthlyReport();
                    DisplayAccountInfo(lstAccounts.SelectedItem.ToString());
                    break;
                case "CK":
                    CheckingAccount c = (CheckingAccount)Bank.GetAccount(accountType);
                    c.PrepareMonthlyReport();
                    DisplayAccountInfo(lstAccounts.SelectedItem.ToString());
                    break;
                case "VS":
                    VisaAccount v = (VisaAccount)Bank.GetAccount(accountType);
                    v.PrepareMonthlyReport();
                    DisplayAccountInfo(lstAccounts.SelectedItem.ToString());
                    break;
            }
        }

        /** Assign User to Account */
        private void btnAssign_Click(object sender, EventArgs e)
        {
            string accountType = lstAccounts.SelectedItem.ToString();
            Bank.GetAccount(accountType).AddUser(Bank.GetPerson(lstUsers.SelectedItem.ToString()));
            DisplayAccountInfo(lstAccounts.SelectedItem.ToString());
        }

        private void timerCurrTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
        }
    }
}
