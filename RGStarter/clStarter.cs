using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Threading;

namespace RGStarter
{
    class clStarter
    {
        public int StatusCheck(string strURL)
        {
            // Status der Webadresse ermitteln
            // RG Server 94.23.160.232:8085 / 185.60.115.21:80 (Blizzard)
            // wotlk.rising-gods.de
            try
            {
                HtmlDocument website = new HtmlDocument();
                website.LoadHtml(new WebClient().DownloadString(strURL));
                var root = website.DocumentNode;
                var nodes = root.Descendants();
                var servernode = root.Descendants()
                    .Where(n => n.GetAttributeValue("id", "").Equals("serverstatus"))
                    .Single();
                var serveronline = servernode.Descendants()
                    .Where(n => n.GetAttributeValue("class", "").Equals("pvetd alignright"))
                    .Single();
                string[] strInnerText = Regex.Split(serveronline.InnerText.ToString(), "\n\t");
                string[] strPlayer = Regex.Split(strInnerText[1].ToString(), "\t");
                int OnlinePlayer = Convert.ToInt32(strPlayer[2].ToString());

                return OnlinePlayer;
            }
            catch (Exception) // Error Handling
            {
                throw;
            }
        }

        public void StartGame(string strPath)
        {
            // Start Game
            try
            {
                System.Diagnostics.Process.Start(strPath);
            }
            catch (Exception) // Error Handling
            {
                throw;
            }
            
        }

        public void ServerUpdate(string strURL)
        {
            string[,] strServerUpdate = new string[5 , 2];
            try
            {
                HtmlDocument website = new HtmlDocument();
                website.LoadHtml(new WebClient().DownloadString(strURL));
                var root = website.DocumentNode;
                var nodes = root.Descendants();
                var updatenode = root.Descendants()
                    .Where(n => n.GetAttributeValue("class", "").Equals("ktopic-title km"))
                    .ToArray();

                // Count Updates in feed
                int iAnzahl = updatenode.Count();
                // Show max last 5 Updates in feed
                if (iAnzahl > 5)
                {
                    iAnzahl = 5;
                }
                for (int i = 0; i < iAnzahl; i++)
                {
                    strServerUpdate[i, 0] = updatenode[i].InnerText;
                    strServerUpdate[i, 1] = updatenode[i].OuterHtml;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
