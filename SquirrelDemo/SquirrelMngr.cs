using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SquirrelDemo
{
   public class SquirrelMngr
    {
        private WebClient webClient = null;

        private void btnDownload_Click(object sender, EventArgs e)
        {
            // Is file downloading yet?
            if (webClient != null)
                return;

            webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadFileAsync(new Uri("http://---/file.zip"), @"c:\file.zip");


        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            webClient = null;
           Console.WriteLine("Download completed!");
        }
    }
}
