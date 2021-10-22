using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using Ethereum_Test2.src;
using src;
using Contract = Nethereum.Contracts.Contract;

using static Ethereum_Test2.src.Ethernet;
using static src.Toaster;
using Ethereum_Test2.src.logger;
using System.Threading;

namespace Ethereum_Test2 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private readonly Ethernet ether = new Ethernet(Crypto.LOCAL);
        private readonly Toaster toast = new Toaster();
        private bool updaterRunning = true;
        private bool updaterBackground = true;
        private readonly SynchronizationContext synchronizationContext;
        private readonly IPFS_Interact ipfs = new IPFS_Interact();
        private string updateString = "";
        public MainWindow() {
            InitializeComponent();
            MainGrid.Children.Add(toast.GetToast());
            this.Combobox.ItemsSource = new string[] { "Local", "Test" };
            this.Combobox.SelectedValue = "Local";
            Log.StartLogger();

            synchronizationContext = SynchronizationContext.Current;
            TextBoxText.Text = Log.GetLog();
            Background.Content = "Background: " + updaterBackground;
            Task thread = new Task(Update);
            thread.Start();
        }
        private async void Update() {
            while (updaterRunning) {
                if (updaterBackground) {
                    await Task.Delay(100);
                    synchronizationContext.Post(new SendOrPostCallback((object o) => {
                        bool scrollDown = TextBoxText.VerticalOffset + TextBoxText.ViewportHeight >= TextBoxText.ExtentHeight;
                        TextBoxText.Text = Log.GetLog();
                        if (scrollDown) {
                            TextBoxText.ScrollToEnd();
                        }

                        if (updateString!=null && updateString.Length != 0) {
                            Log.InfoLog(updateString);
                            updateString = "";
                        }

                    }), null);
                }
            }
        }

        private void GetAccountBalanceButton(object sender, RoutedEventArgs e) {
            Log.InfoLog("test - GetAccountBalance (stores acct ballance doesn't display)");
            _ = this.ether.GetAccountBalance(ether.EnvAccount);
        }

        private void AccBalButton(object sender, RoutedEventArgs e) {
            Log.InfoLog("test - AccBal");
            Log.InfoLog(this.ether.AccBal);
        }

        private void GetContractButton(object sender, RoutedEventArgs e) {
            Log.InfoLog("test - GetContract");
            Contract cont = ether.GetContract(ether.EnvContractAccount);
            _ = ether.GetHashFromContract();
            Log.InfoLog(cont.ToString());
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            switch (((ComboBox)sender).SelectedValue.ToString()) {
                case "Local":
                    ether.SetEnvironment(Crypto.LOCAL);
                    break;
                case "Test":
                    ether.SetEnvironment(Crypto.ROPSTEN);
                    break;
            }
        }

        private void MemeHashButton(object sender, RoutedEventArgs e) {
            Log.InfoLog("test - MemeHash");
            Log.InfoLog(this.ether.MemeHash);
        }

        private void SetHashForContractButton(object sender, RoutedEventArgs e) {
            Log.InfoLog("Test - SetHashForContractButton");
            _ = ether.SetHashForContract("QmZHd1fbAsE4j281P69a9gR8UdoK3G8DsJ2G7oxVQ8osQ3");
        }

        private void ErrorTestButton(object sender, RoutedEventArgs e) {
            Log.ErrorLog("Error Test");
        }

        private void WarningTestButton(object sender, RoutedEventArgs e) {
            Log.WarningLog("Warning Test");
        }

        private void ClearLogsButton(object sender, RoutedEventArgs e) {
            Log.ClearLog();
        }

        private void InfoTestButton(object sender, RoutedEventArgs e) {
            Log.InfoLog("Info Test");
        }

        private void BackgroundButton(object sender, RoutedEventArgs e) {
            updaterBackground = !updaterBackground;
            Background.Content = "Background: " + updaterBackground;
        }

        private void GetIPFSFileButton(object sender, RoutedEventArgs e) {
            Log.InfoLog("Test - GetIPFSFile");
            _ = ipfs.GetIPFSFile();
        }

        private void SetFileToIPFSButton(object sender, RoutedEventArgs e) {
            Log.InfoLog("Test - SetFileToIPFS");
            Task.Run(sftipfs);
        }

        private async void sftipfs() {
            updateString = await ipfs.SetFileToIPFS(@"D:\Jason Howse\Pictures\Memes\atf dog meme.png");
        }
    }
}