using Squirrel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskWaiter;

namespace SteeroidPlatformInstaller
{
   public class SquirrelDownloadMngr
    {
        //return new UpdateManager(latestReleaseUrl, applicationName, rootDirectory, urlDownloader);

        public static string ApplicationName { get; set; }
        public static string RootDirectory { get; set; }

        public static IFileDownloader UrlDownloader { get; set; }

        private WebClient webClient = null;

        public bool IsDownloadDone { get; set; }

        public void DownLoadZip(string url)
        {

            IsDownloadDone = false;
            if(Directory.Exists(SquirrelFileEndPointManager.Temp))
                 System.IO.Directory.Delete(SquirrelFileEndPointManager.Temp, true);

            Directory.CreateDirectory(SquirrelFileEndPointManager.Temp);

            // Is file downloading yet?
            if (webClient != null)
                return;

            webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged; //new AsyncCompletedEventHandler(client_DownloadProgressChanged);


            webClient.Headers.Add("user-agent", "Anything");
            PercentDownload = 0;
            webClient.DownloadFileAsync(new Uri(url),Path.Combine( SquirrelFileEndPointManager.Temp,"Data.zip"));


        }

        public static int PercentDownload { get; set; }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            PercentDownload =(int)Math.Truncate(percentage);

            //label2.Text = "Downloaded " + e.BytesReceived + " of " + e.TotalBytesToReceive;
            //progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
        }





        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            webClient = null;
          var files =  Directory.GetFiles(SquirrelFileEndPointManager.Temp);

            if(files.Length>0)
            {
                // Extract the directory we just created.
                // ... Store the results in a new folder called "destination".
                // ... The new folder must not exist.
                //ZipFile.ExtractToDirectory("destination.zip", "destination");


                ZipFile.ExtractToDirectory(files.First(),Path.Combine(SquirrelFileEndPointManager.Temp, "Extracted"));

            }
            // Extract the directory we just created.
            // ... Store the results in a new folder called "destination".
            // ... The new folder must not exist.

            //ZipFile.ExtractToDirectory("destination.zip", "destination");

            //get release folder
            var root = Path.Combine(SquirrelFileEndPointManager.Temp, "Extracted");
            var releases =  Directory.GetDirectories(root,"Releases",SearchOption.AllDirectories);
           // var releases =  Directory.GetDirectories(dir, "Releases");

            if(releases.Length>0)
            {
                Console.WriteLine(releases.First());
            }


            #region DO the UPDATE

            //using (
            //     var squirrel = new UpdateManager(releases.First(), ApplicationName, RootDirectory, UrlDownloader))
            //{
            //    var result = AsyncHelper.RunSync<bool>(async () =>
            //    {
            //        await squirrel.UpdateApp();
            //        return true;
            //    });
            //}

            #endregion


            Console.WriteLine("Download completed!");
            IsDownloadDone = true;

        }

        public async Task<bool> IsDone()
        {
            //TIP: wait process
            Conditions cond = new Conditions("WaitDownloadDone");
            await cond.WaitUntil(() => this.IsDownloadDone == true).ContinueWith(x =>
            {
                // Console.WriteLine("Done.. ExcelRunAsUnit");
                return true;

            });
            return false;

        }
    }
}
