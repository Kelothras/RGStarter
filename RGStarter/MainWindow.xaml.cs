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
using System.IO;
using System.Windows.Threading;

namespace RGStarter
{
    public partial class MainWindow : Window
    {
        public string[,] strUpdate;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow windowSettings = new SettingsWindow();
            windowSettings.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            updateServerStats();
            getServerUpdate();
            // Initialize Timer 
            DispatcherTimer disTimer = new DispatcherTimer();
            disTimer.Tick += new EventHandler(disTimer_Tick);
            disTimer.Interval = new TimeSpan(0, 5, 0);
            disTimer.Start();
        }

        private void getServerUpdate()
        {
            string[,] strServerUpdate = new string[,]
            { 
                { "Test", "intranet" },
                { "Test2", "intranet2"}
            };
            strUpdate = strServerUpdate;
            //clStarter ProcessStarter = new clStarter();
            //string[] strServerUpdate = ProcessStarter.getServerUpdate(WoWStarter.Default.UpdateUrl);
            foreach (var item in strServerUpdate)
            {
                lsbServerUpdate.Items.Add(item);  
            }

        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clStarter ProcessStarter = new clStarter();
                if (WoWStarter.Default.WoWPath.Length > 0)
                {
                    ProcessStarter.StartGame(WoWStarter.Default.WoWPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler beim Starten aufgetreten");
            }

        }

        private void disTimer_Tick(object sender, EventArgs e)
        {
            updateServerStats();
        }

        public void updateServerStats()
        {
            try
            {
                // Init
                clStarter ProcessStarter = new clStarter();
                int OnlinePlayer = ProcessStarter.StatusCheck(WoWStarter.Default.ServerUrl);

                // Serverstatus
                lblPlayer.Content = OnlinePlayer;
                if (OnlinePlayer > 0)
                {
                    // Server Online
                    lblStatus.Content = "Online";
                    lblStatus.Foreground = Brushes.GreenYellow;
                }
                else
                {
                    // Server Offline
                    lblStatus.Content = "Offline";
                    lblStatus.Foreground = Brushes.Red;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Content = "Error";
                lblStatus.Foreground = Brushes.Yellow;
                MessageBox.Show(ex.Message, "Keine Verbindung möglich");
            }
            
        }

        private void lsbServerUpdate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {


            MessageBox.Show(lsbServerUpdate.SelectedItem.ToString());
        }

    }
}
