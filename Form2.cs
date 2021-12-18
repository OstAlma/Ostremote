using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Ost_RDP
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process ExP = new Process();
            ExP.StartInfo.FileName = "List Tools.exe";
            ExP.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            ExP.Start();
            ExP.WaitForExit();
          Close();
        }
    }
}
