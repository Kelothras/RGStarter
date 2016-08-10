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

            //WebRequest Request = WebRequest.Create("http://94.23.160.232:8085");
            //HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("http://94.23.160.232");
            //WebResponse Response = Request.GetResponse();
            //HttpWebResponse Respone = (HttpWebResponse)Request.GetResponse();
             return OnlinePlayer;
        }

        public void StartGame(string strPath)
        {
            // Spiel starten
            try
            {
                System.Diagnostics.Process.Start(strPath);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void ServerUpdate(string strURL)
        {
            string[,] strServerUpdate = new string[5 , 2];
            HtmlDocument website = new HtmlDocument();
            website.LoadHtml(new WebClient().DownloadString(strURL));
            var root = website.DocumentNode;
            var nodes = root.Descendants();
            var updatenode = root.Descendants()
                .Where(n => n.GetAttributeValue("class", "").Equals("ktopic-title km"))
                .ToArray();
            
            int iAnzahl = updatenode.Count();
            // Max letzte 5 Updates in Feed
            if (iAnzahl > 5)
            {
                iAnzahl = 5;
            }
            for (int i = 0; i < iAnzahl; i++)
            {
                strServerUpdate[i , 0] = updatenode[i].InnerText;
                strServerUpdate[i , 1] = updatenode[i].OuterHtml;

            }


        }
    }
}
