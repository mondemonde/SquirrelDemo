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
    }
}
