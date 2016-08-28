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
            // Update Server Stats
            //updateServerStats();
            // Initialize Timer 
            DispatcherTimer disTimer = new DispatcherTimer();
            disTimer.Tick += new EventHandler(disTimer_Tick);
            disTimer.Interval = new TimeSpan(0, 5, 0);
            disTimer.Start();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            clStarter ProcessStarter = new clStarter();
            if (WoWStarter.Default.WoWPath.Length > 0)
            {
                ProcessStarter.StartGame(WoWStarter.Default.WoWPath);
            }
        }


        private void disTimer_Tick(object sender, EventArgs e)
        {
            //updateServerStats();
        }

        public void updateServerStats()
        {
            // Init
            clStarter ProcessStarter = new clStarter();
            int OnlinePlayer = ProcessStarter.StatusCheck(RGStarter.WoWStarter.Default.ServerURL);
            string[,] strServerUpdate;

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

            // Read Server Updates from URL
            ProcessStarter.ServerUpdate("https://www.rising-gods.de/forum/95-serverupdates.html");
        }

    }
}
