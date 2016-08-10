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
using System.IO;
using Microsoft.Win32;
using System.Configuration;
using Shell32;

namespace RGStarter
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public string strWoWPath = "";
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void btnPath_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog ofdPath = new OpenFileDialog();
            ofdPath.Title = "WoW.exe auswählen";
            ofdPath.Filter = "Windows-Anwendung (.exe)|*.exe";
            ofdPath.DefaultExt = ".exe";

            // Einstellung für WoWPfad bereits gesetzt
            if (strWoWPath != "")
            {
                ofdPath.InitialDirectory = strWoWPath;
            }

            // Bool kann auch Wert Null zugewiesen werden --> auch möglich: Bool? hat dann gleichen Effekt
            Nullable <bool> result = ofdPath.ShowDialog();
            if (result == true)
            {
                if (ofdPath.FileName.ToString() != "WoW.exe")
                {
                   // Fehlerbehandlung
                }
                strWoWPath = ofdPath.FileName.ToString();
                // Maximal soviel vom Pfad ausgeben wie im Textfeld angegeben
                txtPath.Text = strWoWPath;
                
                
                WoWStarter.Default.WoWPath = strWoWPath.ToString();
            }

        }

        private void windowSettings_Initialized(object sender, EventArgs e)
        {
            strWoWPath = getAppSetting("WoWPath");
            if (strWoWPath.Length > 0)
            {
                txtPath.Text = strWoWPath;
                readWoWFileVersion(strWoWPath);
            }
        }

        private void readWoWFileVersion(string strPath)
        {
            FileInfo fileInfoWoW = new FileInfo(strWoWPath);
            FileAttributes fileAttributeWoW = fileInfoWoW.Attributes;

        }

        // liefert die Einstellungen der Übergabe zurück
        public string getAppSetting(string strSetting) 
        {
            if (WoWStarter.Default[strSetting] != null)
            {
                return WoWStarter.Default[strSetting].ToString();
            }
            else
            {
                return "";
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Close();
            WoWStarter.Default.Save();
        }
    }
}
