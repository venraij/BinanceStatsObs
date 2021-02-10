using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using View;

namespace BinanceStatsStream
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Thread getStatsThread;

        public MainWindow()
        {
            InitializeComponent();

            BinanceEndpoints.DirectoryCheck();

            if (File.Exists(@"C:\BinanceStats\apiKey.txt"))
            {
                string apiKey = File.ReadAllText(@"C:\BinanceStats\apiKey.txt");

                BinanceEndpoints.apiKey = apiKey;
                apiKeyText.Password = apiKey;
            }

            if (File.Exists(@"C:\BinanceStats\secretKey.txt"))
            {
                string secretKey = File.ReadAllText(@"C:\BinanceStats\secretKey.txt");

                BinanceEndpoints.secretKey = secretKey;
                apiSecretText.Password = secretKey;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                File.WriteAllText(@"C:\BinanceStats\apiKey.txt", apiKeyText.Password);
                File.WriteAllText(@"C:\BinanceStats\secretKey.txt", apiSecretText.Password);

                BinanceEndpoints.apiKey = apiKeyText.Password;
                BinanceEndpoints.secretKey = apiSecretText.Password;
                SaveMessage.Content = "Saved keys";
            } catch (Exception error)
            {
                SaveMessage.Content = "Error with saving keys";
            }

            
        }

        private void getStats()
        {
            while(true)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    BinanceEndpoints.FuturesAccountInformation();
                }));
                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(Convert.ToString(EnableButton.Content) == "Enable")
            {
                EnableButton.Content = "Disable";
                getStatsThread = new Thread(getStats);
                getStatsThread.Start();
            } else
            {
                EnableButton.Content = "Enable";
                if (getStatsThread != null)
                {
                    getStatsThread.Abort();
                }
            }
        }
    }
}
