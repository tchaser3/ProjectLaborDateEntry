/* Title:           Project Labor Data Entry
 * Date:            2-8-18
 * Author:          Terry Holmes
 * 
 * Description:     This is the main window for the program */

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using NewEmployeeDLL;

namespace ProjectLaborDateEntry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EmployeeClass TheEmployeeClass = new EmployeeClass();
        WPFMessagesClass TheMessagesClass = new WPFMessagesClass();

        public static VerifyLogonDataSet TheVerifyLogonDataSet = new VerifyLogonDataSet();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            EmployeeLogon EmployeeLogon = new EmployeeLogon();
            EmployeeLogon.ShowDialog();
        }

        private void mitClose_Click(object sender, RoutedEventArgs e)
        {
            TheMessagesClass.CloseTheProgram();
        }
    }
}
