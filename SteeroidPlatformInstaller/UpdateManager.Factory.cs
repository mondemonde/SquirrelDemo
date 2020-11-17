using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Squirrel;
using Squirrel.Json;

namespace SteeroidPlatformInstaller
{
    public  class UpdateManagerPlatform
    {

        public class GitTag
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("zipball_url")]
            public Uri ZipballUrl { get; set; }

            [JsonProperty("tarball_url")]
            public Uri TarballUrl { get; set; }

            [JsonProperty("commit")]
            public Commit Commit { get; set; }

            [JsonProperty("node_id")]
            public string NodeId { get; set; }
        }

        public  class Commit
        {
            [JsonProperty("sha")]
            public string Sha { get; set; }

            [JsonProperty("url")]
            public Uri Url { get; set; }
        }







        [DataContract]
        public class Release
        {
            [DataMember(Name = "prerelease")]
            public bool Prerelease { get; set; }

            [DataMember(Name = "published_at")]
            public DateTime PublishedAt { get; set; }

            [DataMember(Name = "html_url")]
            public string HtmlUrl { get; set; }
        }

      


       public static async Task<SquirrelMngr> GitHubUpdateManager2(
       // public static async Task<bool> GitHubUpdateManager2(

           string repoUrl,
           string applicationName = null,
           string rootDirectory = null,
          // IFileDownloader urlDownloader = null,
           bool prerelease = false,
           string accessToken = null)
        {
            var repoUri = new Uri(repoUrl);
            var userAgent = new ProductInfoHeaderValue("Squirrel", Assembly.GetExecutingAssembly().GetName().Version.ToString());

            if (repoUri.Segments.Length != 3)
            {
                throw new Exception("Repo URL must be to the root URL of the repo e.g. https://github.com/myuser/myrepo");
            }

            var releasesApiBuilder = new StringBuilder("repos")
                .Append(repoUri.AbsolutePath)
                .Append("/releases");

            var tagsApiBuilder = new StringBuilder("repos")
    .Append(repoUri.AbsolutePath)
    .Append("/tags");


            if (!string.IsNullOrWhiteSpace(accessToken))
                releasesApiBuilder.Append("?access_token=").Append(accessToken);

            Uri baseAddress;

            if (repoUri.Host.EndsWith("github.com", StringComparison.OrdinalIgnoreCase))
            {
                baseAddress = new Uri("https://api.github.com/");
            }
            else
            {
                // if it's not github.com, it's probably an Enterprise server
                // now the problem with Enterprise is that the API doesn't come prefixed
                // it comes suffixed
                // so the API path of http://internal.github.server.local API location is
                // http://interal.github.server.local/api/v3. 
                baseAddress = new Uri(string.Format("{0}{1}{2}/api/v3/", repoUri.Scheme, Uri.SchemeDelimiter, repoUri.Host));
            }

            // above ^^ notice the end slashes for the baseAddress, explained here: http://stackoverflow.com/a/23438417/162694

            using (var client = new HttpClient() { BaseAddress = baseAddress })
            {
                client.DefaultRequestHeaders.UserAgent.Add(userAgent);
                var response = await client.GetAsync(tagsApiBuilder.ToString());

                response.EnsureSuccessStatusCode();

                //var releases = SimpleJson.DeserializeObject<List<Release>>(await response.Content.ReadAsStringAsync());
                var output = await response.Content.ReadAsStringAsync();
                var releases = JsonConvert.DeserializeObject<List<GitTag>>(output);

                var latestRelease = releases                    
                    //.OrderByDescending(x => x..PublishedAt)
                    .First();

                //Product deserializedProduct = JsonConvert.DeserializeObject<Product>(output);
                var latestReleaseUrl = latestRelease.ZipballUrl.ToString();//.HtmlUrl.Replace("/tag/", "/download/");


                //extract it now here..
                SquirrelMngr loader = new SquirrelMngr();
                SquirrelMngr.ApplicationName = applicationName;
                SquirrelMngr.RootDirectory = rootDirectory;
                SquirrelMngr.UrlDownloader =null;

                loader.DownLoadZip(latestReleaseUrl);


                //return new UpdateManager(latestReleaseUrl, applicationName, rootDirectory, urlDownloader);
                return loader;//new UpdateManager(latestReleaseUrl, applicationName, rootDirectory, urlDownloader);

            }
        }
    }
}