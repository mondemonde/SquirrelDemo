using SteeroidPlatformInstaller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SquirrelDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string version = Assembly
                .GetExecutingAssembly().GetName().Version.ToString();
// returns 1.0.0.0
             this.richTextBox1.AppendText("file version:" + version);

          
        }

        async Task CheckForUpdates( string url)
        {
            try
            {
                    using (var mgr = UpdateManagerPlatform.GitHubUpdateManager2(url))
                    {
                      //await mgr.Result.UpdateApp();                     

                       await mgr.Result.IsDone();


                    }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);

                //throw;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            var url = textBox1.Text;


            //TIP Synch to Asynch
            var result = AsyncHelper.RunSync<string>(async () => {

               string remarks= "Updating";

                try
                {

                   await CheckForUpdates(url);
            }
                catch (Exception err)
                {

                   Console.WriteLine(err.Message);
                }


                return remarks;


            });
        }
    }
}
