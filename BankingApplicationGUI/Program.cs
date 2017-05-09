using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankingApplicationGUI
{
    static class Program
    {
        // Make form accessble
        public static Form1 FirstForm;
        public static Form2 SecondForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Instantiate both forms
            SecondForm = new Form2();
            FirstForm = new Form1();


            Application.Run(FirstForm);
        }
    }
}
