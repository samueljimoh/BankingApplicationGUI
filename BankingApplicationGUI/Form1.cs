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
    public partial class Form1 : Form
    {
        //Alias for second Form
        public Form2 SecondForm = Program.SecondForm;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string userName;
            string password;
            userName = txtName.Text;
            password = txtPassword.Text;

            try
            {
                Bank.GetPerson(userName);
                try
                {
                    Bank.GetPerson(userName).Login(password);

                    // Display the current logged in user
                    SecondForm.lblCurrentUser.Text = txtName.Text;

                    /*
                     * Show second form that was defined
                     * as a static object in main program
                     */
                    SecondForm.Show();

                    // hide first form after moving to second
                    this.Hide();
                }
                catch (AccountException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //lblUserInfoContent.Text = Bank.GetPerson(userName).ToString();
            }
            catch (AccountException msg)
            {
                MessageBox.Show(msg.Message);
            }

        }
    }
}
