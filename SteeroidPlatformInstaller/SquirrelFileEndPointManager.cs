using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Squirrel.UpdateManager;
using static SteeroidPlatformInstaller.UpdateManagerPlatform;

namespace SteeroidPlatformInstaller
{
   
    public static class SquirrelFileEndPointManager
    {



        static string _installFolder;
        public static string InstallFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_installFolder))
                {                    

                 if (string.IsNullOrEmpty(_installFolder))
                    {
                        // _installFolder = @"C:\BlastAsia\Steeroid";
                       var local = Environment.GetEnvironmentVariable("LocalAppData");

                        //C:\Users\rgalvez\AppData\Local\SteeroidAutomation
                        //var app = "SteeroidAutomation";

                        string app = ConfigurationManager.AppSettings["AppName"];
                        //var app = "SteeroidAutomation";


                        _installFolder = Path.Combine(local, app);
                    }


                }
                return _installFolder;
            }
        }


        static string _installFolderPlatform;
        public static string InstallFolderPlatform
        {
            get
            {
                if (string.IsNullOrEmpty(_installFolder))
                {

                    if (string.IsNullOrEmpty(_installFolder))
                    {
                        // _installFolder = @"C:\BlastAsia\Steeroid";
                        var local = @"C:\BlastAsia\Steeroid";//




                        _installFolderPlatform = local;//Path.Combine(local, app);
                    }


                }
                return _installFolderPlatform;
            }
        }





        static string _myCommon;
        public static String MyCommon
        {
            get
            {
                //C:\BlastAsia\Common
                if (string.IsNullOrEmpty(_myCommon))
                    _myCommon = Path.Combine(InstallFolderPlatform, "Common");

                return _myCommon;
            }
        }


        static string _myTemp;
        public static String Temp
        {
            get
            {
                //C:\BlastAsia\Common
                if (string.IsNullOrEmpty(_myTemp))
                    _myTemp = Path.Combine(InstallFolderPlatform, "Temp");

                return _myTemp;
            }
        }















        public static String MyTasktDirectory
        {
            get
            {
                //C:\BlastAsia\DevnoteTaskt
                if (string.IsNullOrEmpty(myTasktDirectory))
                    myTasktDirectory = InstallFolder; //Path.Combine(InstallFolder, "DevnoteTaskt");

                return myTasktDirectory;
            }
        }



        //public static string MyCodeceptTestTemplate
        //{
        //    get
        //    {
        //        ConfigManager config = new ConfigManager();
        //        string template = config.GetValue("CodeceptTestTemplate");

        //        if (string.IsNullOrEmpty(template))
        //        {
        //            //get default directory
        //            //D:\_MY_PROJECTS\_DEVNOTE\_DevNote4\DevNote.Web.Recorder\Chrome\chrome-win\chrome.exe
        //            var currentDir = LogApplication.Agent.GetCurrentDir();
        //            currentDir = currentDir.Replace("file:\\", string.Empty);


        //            var dir = string.Format("{0}\\CodeCeptJS\\Project2", currentDir);
        //            template = System.IO.Path.Combine(dir, "template_test.txt");
        //        }
        //        return template;
        //    }

        //}
        static string _myCommonExeDirectory;
        public static string MyCommonExeDirectory
        {

            get
            {

                if (string.IsNullOrEmpty(_myCommonExeDirectory))
                {

                    var dir = string.Format("{0}\\Bat", SquirrelFileEndPointManager.MyTasktDirectory);
                    var dirExe = System.IO.Path.Combine(dir, "Exe");

                    _myCommonExeDirectory = dirExe;

                }
                return _myCommonExeDirectory;


            }




        }
        public static bool IsEventBusy
        {
            get
            {
                var files = Directory.GetFiles(MyWaitOneDirectory, "*.eve", SearchOption.TopDirectoryOnly);

                return files.Length > 0;
            }
        }

        public static String MyWaitOneDirectory
        {
            get
            {
                //STEP_.EVENT MyWaitOneDirectory
                if (string.IsNullOrEmpty(myWaitOneDirectory))
                {

                    var dir = string.Format("{0}\\Bat", SquirrelFileEndPointManager.MyTasktDirectory);
                    var dirWaitOne = System.IO.Path.Combine(dir, "WaitOne");

                    myWaitOneDirectory = dirWaitOne;
                }
                return myWaitOneDirectory;
            }
        }
        static string _myOutcomeFolder;
        public static String MyOutcomeFolder
        {
            get
            {
                //STEP_.EVENT MyWaitOneDirectory
                if (string.IsNullOrEmpty(_myOutcomeFolder))
                {

                    var dir = string.Format("{0}\\Bat", SquirrelFileEndPointManager.MyTasktDirectory);
                    var dirWaitOne = System.IO.Path.Combine(dir, "Outcome");

                    _myOutcomeFolder = dirWaitOne;
                }
                return _myOutcomeFolder;
            }
        }

        static string myEventDirectory;

        //TODO handle Event files
        public static String MyEventDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(myEventDirectory))
                {

                    var dir = string.Format("{0}\\Bat", SquirrelFileEndPointManager.MyTasktDirectory);
                    var dirEvents = System.IO.Path.Combine(dir, "Events");

                    myEventDirectory = dirEvents;
                }
                return myEventDirectory;
            }
        }





        static string _myChromiumExe;








        //must be 1 reference only
        //only used by OutputManager!! 


        //must be 1 reference only





        //public static void ClearOutputWF()
        //{
        //    // var stringContent = JsonConvert.SerializeObject(cmd); //new StringContent(JsonConvert.SerializeObject(cmd), Encoding.UTF8, "application/json");
        //    var file = Path.Combine (SquirrelFileEndPointManager.MyWaitOneDirectory, EnumFiles.WFOutput);
        //    // File.WriteAllText(file, stringContent);
        //    if (File.Exists(file))
        //        File.Delete(file);

        //}



        public static GitTag ReadCmdJsonFile(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var cmd = JsonConvert.DeserializeObject<GitTag>(json);
            return cmd;

        }

        public static string InputWFFilePath
        {
            get
            {
                return Path.Combine(SquirrelFileEndPointManager.MyWaitOneDirectory, "Input");

            }

        }
        public static string OutputWFFilePath
        {
            get
            {
                return Path.Combine(SquirrelFileEndPointManager.MyWaitOneDirectory, "Output");

            }

        }



        public static bool IsWFBusy
        {
            get
            {
                var files = Directory.GetFiles(MyWaitOneDirectory, "Output", SearchOption.TopDirectoryOnly);
                bool isOuput = files.Length > 0;

                var filesIn = Directory.GetFiles(MyWaitOneDirectory, "Input", SearchOption.TopDirectoryOnly);

                bool isInput = filesIn.Length > 0;

                if (isInput && isOuput == false)
                {
                    return true;
                }
                else
                    return false;


            }
        }


        public static bool IsTasktClear
        {
            get
            {
                var files = Directory.GetFiles(MyWaitOneDirectory, "*.*", SearchOption.TopDirectoryOnly);
                bool isOuput = files.Length == 0;
                return isOuput;

            }
        }
        public static void ClearTasktOutput()
        {
            var files = Directory.GetFiles(MyWaitOneDirectory, "*.*", SearchOption.TopDirectoryOnly).ToList();

            foreach (var item in files)
            {
                File.Delete(item);
            }

        }



      

   


        static string _defaultLatestTasktXML;
        public static string DefaultLatestTasktXML
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultLatestTasktXML))
                {

                    _defaultLatestTasktXML = Path.Combine(SquirrelFileEndPointManager.MyCommon, "latestTaskt.xml");

                }
                return _defaultLatestTasktXML;
            }
        }

        static string _katalonLog;
        public static string KatalonLog
        {
            get
            {
                if (string.IsNullOrEmpty(_katalonLog))
                {

                    _katalonLog = Path.Combine (SquirrelFileEndPointManager.MyCommon, "katalonLog.txt");

                }
                return _katalonLog;
            }
        }



        static string _defaultLatestHtmlFile;
        public static string DefaultLatestHtmlFile
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultLatestHtmlFile))
                {
                    var dir = Path.Combine (SquirrelFileEndPointManager.MyCommon, "Temp");

                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    _defaultLatestHtmlFile = Path.Combine (SquirrelFileEndPointManager.MyCommon, @"Temp\latest.html");

                }
                return _defaultLatestHtmlFile;
            }
        }


        static string _defaultTempHtmlFile;
        public static string DefaultTempHtmlFile
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultTempHtmlFile))
                {
                    var dir = Path.Combine (SquirrelFileEndPointManager.MyCommon, "Temp");
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    // _defaultLatestHtmlFile = Path.Combine(Project2Folder, "latest.html");
                    _defaultTempHtmlFile = Path.Combine (SquirrelFileEndPointManager.MyCommon, @"Temp\temp.html");
                }
                return _defaultTempHtmlFile;
            }
        }

        static string _defaultTempTasktFile;
        public static string DefaultTempTasktFile
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultTempTasktFile))
                {
                    var dir = Path.Combine (SquirrelFileEndPointManager.MyCommon, "Temp");
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    // _defaultLatestHtmlFile = Path.Combine(Project2Folder, "latest.html");
                    _defaultTempTasktFile = Path.Combine (SquirrelFileEndPointManager.MyCommon, @"Temp\temp.xml");
                }
                return _defaultTempTasktFile;
            }
        }



        static string _defaultLoader;
        public static string DefaultLoader
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultLoader))
                {
                    var dir = Path.Combine (SquirrelFileEndPointManager.MyTasktDirectory, "Assets");
                    //if (!Directory.Exists(dir))
                    //    Directory.CreateDirectory(dir);
                    _defaultLoader = Path.Combine(dir, @"Loading.html");


                }
                return _defaultLoader;
            }
        }


        static string _defaultLatestResultFile;
        public static string DefaultLatestResultFile
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultLatestResultFile))
                {
                    var dir = Path.Combine (SquirrelFileEndPointManager.MyCommon, "Temp");

                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);


                    // _defaultLatestHtmlFile = Path.Combine(Project2Folder, "latest.html");
                    return Path.Combine (SquirrelFileEndPointManager.MyCommon, @"Temp\result.txt");

                }
                return _defaultLatestResultFile;
            }
        }

        static string _defaultCSV;
        public static string DefaultCSV
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultCSV))
                {
                    var dir = Path.Combine (SquirrelFileEndPointManager.MyCommon, "Temp");

                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);


                    // _defaultLatestHtmlFile = Path.Combine(Project2Folder, "latest.html");
                    return Path.Combine (SquirrelFileEndPointManager.MyCommon, @"Temp\scrape.xlsx");

                }
                return _defaultCSV;
            }
        }

        static string _defaultExcelScrapeXLSM;
        public static string DefaultExcelScrapeXLSM
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultExcelScrapeXLSM))
                {
                    var dir = Path.Combine(MyTasktDirectory, "Templates");

                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);


                    // _defaultLatestHtmlFile = Path.Combine(Project2Folder, "latest.html");
                    return Path.Combine(dir, "scrape.xlsm");

                }
                return _defaultExcelScrapeXLSM;
            }
        }



        static string _defaultScriptFolder;
        public static string DefaultScriptFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultScriptFolder))
                {
                    //ConfigManager config = new ConfigManager();
                    //var file = config.GetValue("DefaultXMLFile");

                    var file = Path.Combine(MyTasktDirectory, "Scripts");
                    _defaultScriptFolder = file;
                }
                return _defaultScriptFolder;
            }

        }

        static string _defaultSnippetFolder;
        public static string DefaulSnippetFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultSnippetFolder))
                {
                    //ConfigManager config = new ConfigManager();
                    //var file = config.GetValue("DefaultXMLFile");

                    var file = Path.Combine(MyTasktDirectory, "Snippets");
                    _defaultSnippetFolder = file;
                }
                return _defaultSnippetFolder;
            }

        }
        static string _defaultXAMLFolder;
        public static string DefaultXAMLFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultXAMLFolder))
                {
                    //ConfigManager config = new ConfigManager();
                    //var file = config.GetValue("DefaultXMLFile");

                    var file = Path.Combine(MyTasktDirectory, "XAML");
                    _defaultXAMLFolder = file;
                }
                return _defaultXAMLFolder;
            }

        }



        #region ROOT Dynmic

        //    <add key="Project2EndPointFolder" 
        //value="D:\_MY_PROJECTS\_DEVNOTE\_DevNote4\DevNote.Web.Recorder\bin\Debug\CodeCeptJS\Project2\output\endpoint" />

        static string _project2EndPointFolder;

        [Obsolete]
        public static String Project2EndPointFolder
        {
            get
            {

                return DefaultLatestResultFile;
                //_project2EndPointFolder = Path.Combine(Project2Folder, "output");
                //var dirOutput = System.IO.Path.Combine(_project2EndPointFolder, "endpoint");

                //return dirOutput; //_project2EndPointFolder;
            }
        }

        static string _project2Folder;


        [Obsolete]
        public static string MyPlayerExe
        {
            get
            {
                var exe = string.Format("{0}\\_EXE\\Player\\DevNote.Web.Recorder.exe", MyTasktDirectory);

                return exe;

            }
        }




        public static string DefaultTasktTemplate
        {
            get
            {
                var exe = string.Format("{0}\\Bat\\DefaultTasktTemplate.xml", MyTasktDirectory);
                return exe;

            }
        }


        //static string _chromeDownLoad;
        //public static String ChromeDownLoad
        //{
        //    get
        //    {
        //        ConfigManager config = new ConfigManager();
        //        _chromeDownLoad= config.GetValue("ChromeDownloadFolder");

        //        if (string.IsNullOrWhiteSpace(_chromeDownLoad))
        //            return Project2Folder;


        //        return _chromeDownLoad; //_project2EndPointFolder;
        //    }
        //}



   

        public static string ReadMyResultValueFile()
        {
            var @result = string.Empty;
            var file = DefaultLatestResultFile;//Project2EndPointFolder;
            //var file = Path.Combine(endPointFolder, EnumFiles.MyGrabValue);

            if (File.Exists(file))
            {
                result = File.ReadAllText(file);

            }

            return result;

        }


        #endregion



        #region OUTPU

        public static void CreateLatestResultFile(string latestContent)
        {
            //string latestXML = SquirrelFileEndPointManager.DefaultLatestXMLFile;
            string latest = DefaultLatestResultFile;//.DefaultLatestHtmlFile;        


            if (File.Exists(latest))
                File.Delete(latest);

            //File.WriteAllText(latestXML, xmlContent);
            File.WriteAllText(latest, latestContent);
        }

        #endregion



        private static string myTasktDirectory;
        private static string myWaitOneDirectory;
        // private static string myOutputWFDirectory;

        #region SYNC CustomConfig

        //public static void SyncCustomConfig()
        //{
        //    //update config
        //    //STEP_.INIT UPdate ALL COnfig Sync Custom.Config

        //    var dir = LogApplication.Agent.GetCurrentDir();
        //    myTasktDirectory = dir.Replace("file:\\", string.Empty);

        //    //set root folder for all
        //    ConfigManager config = new ConfigManager();
        //    config.SetValue(MyConfig.MyMainFolder.ToString(), myTasktDirectory);



        //    //Project2Folder
        //    config.SetValue(MyConfig.Project2Folder.ToString(), Project2Folder);



          

        //    var exeFolder4 = string.Format("{0}\\_EXE\\Player2", myTasktDirectory);
        //    // var exePath = Path.Combine(exeFolder4, "DevNote.Web.Recorder.exe");
        //    var exePath = Path.Combine(exeFolder4, "DevNotePlay2.exe");

        //    config.SetValue(MyConfig.DevNotePlayerExe.ToString(), exePath);



       



        //    //copy to all Concern Exe
        //    var configFolder = myTasktDirectory;
        //    var source = Path.Combine(configFolder, "Custom.config");

     
        //    config.Save();
        //    var dest4 = Path.Combine(exeFolder4, "Custom.config");
        //    File.Copy(source, dest4, true);

        //}

        //public static void SyncDatabaseConfig()
        //{
        //    //D:\_MY_PROJECTS\_DEVNOTE\_DevNote4\DevNote.Main\bin\Debug2
        //    var dir = LogApplication.Agent.GetCurrentDir();
        //    myTasktDirectory = dir.Replace("file:\\", string.Empty);

        //    //FileEndPointManager.MyCommon

        //    var templatePath = Path.Combine(myTasktDirectory, "Common.config.txt");
        //    var templateConfig = File.ReadAllText(templatePath);

        //    //make it permanent
        //    var actualPath = Path.Combine(MyCommon, "MyDBContext.sdf");// @"C:\Blastasia\Common\MyDbContext.sdf"; 



        //    var finalTxt = templateConfig.Replace("##DbFullPath##", actualPath);

        //    var myCommonConfig = Path.Combine(myTasktDirectory, "common.config");
        //    File.WriteAllText(myCommonConfig, finalTxt.Trim());

        //    var source = myCommonConfig;

        //    //var exeFolder1 = string.Format("{0}\\_EXE\\Receiver", myMainDirectory);
        //    //var commonPath = Path.Combine(exeFolder1, "common.config");
        //    //File.Copy(source, commonPath, true);

        //    //var exeFolder2 = string.Format("{0}\\_EXE\\Sender", myMainDirectory);
        //    //commonPath = Path.Combine(exeFolder2, "common.config");
        //    //File.Copy(source, commonPath, true);


        //    //var exeFolder3 = string.Format("{0}\\_EXE\\Designer", myMainDirectory);
        //    //commonPath = Path.Combine(exeFolder3, "common.config");
        //    //File.Copy(source, commonPath, true);


        //    var exeFolder4 = string.Format("{0}\\_EXE\\Player2", myTasktDirectory);
        //    var commonPath = Path.Combine(exeFolder4, "common.config");
        //    File.Copy(source, commonPath, true);

        //}

        //public static void SyncLogConfig()
        //{

        //    //initializeData="Logs\{ApplicationName}-{DateTime:yyyy-MM-dd}.log"
        //    var keyWord = @"{ApplicationName}-{DateTime:yyyy-MM-dd}.log";
        //    var dir = LogApplication.Agent.GetCurrentDir();
        //    myTasktDirectory = dir.Replace("file:\\", string.Empty);
        //    var logFolder = Path.Combine(myTasktDirectory, "Logs");
        //    var replaceWord = Path.Combine(logFolder, keyWord);


        //    var configTemplate = Path.Combine(myTasktDirectory, "CommonLog.config.txt");
        //    var templateConfig = File.ReadAllText(configTemplate);

        //    var finalTxt = templateConfig.Replace(@"##LogPath##", replaceWord);

        //    var myCommonConfig = Path.Combine(myTasktDirectory, "CommonLog.config");
        //    File.WriteAllText(myCommonConfig, finalTxt.Trim());

        //    var source = myCommonConfig;

        //    //var exeFolder1 = string.Format("{0}\\_EXE\\Receiver", myMainDirectory);
        //    //var commonPath = Path.Combine(exeFolder1, "CommonLog.config");
        //    //File.Copy(source, commonPath, true);

        //    //var exeFolder2 = string.Format("{0}\\_EXE\\Sender", myMainDirectory);
        //    //commonPath = Path.Combine(exeFolder2, "CommonLog.config");
        //    //File.Copy(source, commonPath, true);


        //    //var exeFolder3 = string.Format("{0}\\_EXE\\Designer", myMainDirectory);
        //    //commonPath = Path.Combine(exeFolder3, "CommonLog.config");
        //    //File.Copy(source, commonPath, true);


        //    var exeFolder4 = string.Format("{0}\\_EXE\\Player2", myTasktDirectory);
        //    var commonPath = Path.Combine(exeFolder4, "CommonLog.config");
        //    File.Copy(source, commonPath, true);





        //}



        #endregion



    }

}
