using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace UtilitiesApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenAddressesList_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Window window =  new Addresses.ListWindow();
            window.Closed += (object? _, EventArgs _) => this.Show();
            window.Show();
        }

        private void OpenUtilitiesList_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Window window = new Utilities.ListWindow();
            window.Closed += (object? _, EventArgs _) => this.Show();
            window.Show();
        }

        private void OpenContractsList_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Window window = new Contracts.ListWindow();
            window.Closed += (object? _, EventArgs _) => this.Show();
            window.Show();
        }

        private void OpenBillsList_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Window window = new Consumptions.ListWindow();
            window.Closed += (object? _, EventArgs _) => this.Show();
            window.Show();
        }

        /*
                 private void OpenPaymentList_Click(object sender, RoutedEventArgs e)
                {

                }
         */
    }
}
