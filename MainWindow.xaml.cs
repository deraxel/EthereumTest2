using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

using Ethereum_Test2.src;
using src;
using Contract = Nethereum.Contracts.Contract;

using static Ethereum_Test2.src.Ethernet;
using static src.Toaster;

namespace Ethereum_Test2 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private string v1 = "";
        private readonly Ethernet ether = new Ethernet(Crypto.LOCAL);
        private readonly Toaster toast = new Toaster();
        public MainWindow() {
            InitializeComponent();
            MainGrid.Children.Add(toast.GetToast());
            this.Combobox.ItemsSource = new string[] { "Local", "Test"};
            this.Combobox.SelectedValue = "Local";
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            _ = this.ether.GetAccountBalance(ether.EnvAccount);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            LabelOutput.Content = this.ether.AccBal;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            Contract cont = ether.GetContract(ether.EnvContractAccount);
            toast.PopToastie(cont.)
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            switch(((ComboBox)sender).SelectedValue.ToString()){
                case "Local":
                    ether.SetEnvironment(Crypto.LOCAL);
                    break;
                case "Test":
                    ether.SetEnvironment(Crypto.ROPSTEN);
                    break;
            }
        }
    }
}
