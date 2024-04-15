/* Title:           Employee Logon
 * Date:            2-8-18
 * Author:          Terry Holmes
 * 
 * Description:     This is the employee logon */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataValidationDLL;
using NewEventLogDLL;
using NewEmployeeDLL;

namespace ProjectLaborDateEntry
{
    /// <summary>
    /// Interaction logic for EmployeeLogon.xaml
    /// </summary>
    public partial class EmployeeLogon : Window
    {
        WPFMessagesClass TheMessagesClass = new WPFMessagesClass();
        EmployeeClass TheEmployeeClass = new EmployeeClass();
        EventLogClass TheEventLogClass = new EventLogClass();
        DataValidationClass TheDataValidationClass = new DataValidationClass();

        int gintNoOfMisses;

        public EmployeeLogon()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            TheMessagesClass.CloseTheProgram();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            //setting local variables
            string strValueForValidation;
            int intEmployeeID = 0;
            string strLastName;
            bool blnFatalError = false;
            int intRecordsReturned;
            string strErrorMessage = "";

            //beginning data validation
            strValueForValidation = pbxEmployeeID.Password;
            strLastName = txtLastName.Text;
            blnFatalError = TheDataValidationClass.VerifyIntegerData(strValueForValidation);
            if (blnFatalError == true)
            {
                strErrorMessage = "The Employee ID is not an Integer\n";
            }
            else
            {
                intEmployeeID = Convert.ToInt32(strValueForValidation);
            }
            if (strLastName == "")
            {
                blnFatalError = true;
                strErrorMessage += "The Last Name Was Not Entered\n";
            }
            if (blnFatalError == true)
            {
                TheMessagesClass.ErrorMessage(strErrorMessage);
                return;
            }

            //filling the data set
            MainWindow.TheVerifyLogonDataSet = TheEmployeeClass.VerifyLogon(intEmployeeID, strLastName);

            intRecordsReturned = MainWindow.TheVerifyLogonDataSet.VerifyLogon.Rows.Count;

            if (intRecordsReturned == 0)
            {
                LogonFailed();
            }
            else if(MainWindow.TheVerifyLogonDataSet.VerifyLogon[0].EmployeeGroup == "USERS")
            {
                LogonFailed();
            }
            else
            {
                Close();
            }
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gintNoOfMisses = 0;

            pbxEmployeeID.Focus();
        }
        private void LogonFailed()
        {
            gintNoOfMisses++;

            if (gintNoOfMisses == 3)
            {
                TheEventLogClass.InsertEventLogEntry(DateTime.Now, "There Have Been Three Attemps to Sign Into Project Labor Data Entry");

                TheMessagesClass.ErrorMessage("You Have Tried To Sign In Three Times\nThe Program Will Now Close");

                Application.Current.Shutdown();
            }
            else
            {
                TheMessagesClass.InformationMessage("You Have Failed The Sign In Process");
                return;
            }
        }
    }
}
