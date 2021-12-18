using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Management;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;


namespace Ost_RDP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form2 for2 = new Form2();
            for2.ShowDialog();
            this.KeyPreview = true;

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                button2.PerformClick();
            }
            if (e.KeyCode == Keys.F1)
            {
                button1.PerformClick();
            }
        }
    
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                rdp.AdvancedSettings9.NetworkConnectionType = 1;
                string filePath = @"C:\\Ost-RDP\\ipServer.txt";
                //var str = File.ReadAllText(filePath);
                string str = File.ReadLines(filePath).First();
                int index = str.LastIndexOf(":");
                if (index > 0)
                {
                    txtServerName.Text = str.Substring(0, index);
                }
                string outport = str.Substring(str.IndexOf(':') + 1);
                txtPort.Text = outport;
                // txtServerName.Text = str;
                rdp.Server = txtServerName.Text;
                rdp.UserName = txtUserName.Text;
                IMsTscNonScriptable secured = (IMsTscNonScriptable)rdp.GetOcx();
                secured.ClearTextPassword = txtPassword.Text;
                rdp.AdvancedSettings9.RDPPort = Convert.ToUInt16(txtPort.Text.Trim());
                rdp.AdvancedSettings9.PublicMode = false;
                //rdp.AdvancedSettings9.KeyboardFunctionKey = 1;
                //rdp.AdvancedSettings9.KeyboardType= 1;
               // rdp.SecuredSettings3.KeyboardHookMode = 9;


               // if (checkBox1.Checked == true)
              //  {
                  rdp.AdvancedSettings9.AuthenticationLevel = 2;
               // }
              //  if (checkBox1.Checked == false)
              //  {
              //      rdp.AdvancedSettings9.AuthenticationLevel = 0;
              //  }

                rdp.ConnectingText = "Please Wait Connecting to Remote Server";
                arpa.Connect();
            
            }
                
            catch (Exception Ex)
            {
                lblResult.Text = "ERROR";
                MessageBox.Show("Connecting Failed ", "ERR:  " + txtServerName.Text + Ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //ping Baxsina Xos Galibsiz
            Ping p = new Ping();
            PingReply r;
            string s;
            s = txtServerName.Text;
            r = p.Send(s);

            if (r.Status == IPStatus.Success)
            {
                lblResult.Text = "Ping to " + s.ToString() + "[" + r.Address.ToString() + "]" + " Successful"
                   + " Response delay = " + r.RoundtripTime.ToString() + " ms" + "\n";
            }
            else
            {
                lblResult.Text = " Response delay Timeout IP: " + s.ToString();
            }
        }
 
        private void  arpa(object sender ,EventArgs e ){

                    AppDomain.CreateDomain();
                    AppDomainInitializer[arpa]+arpa();
                }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = @"C:\\Ost-RDP\\ipServer.txt";
                var lines = File.ReadAllLines(filePath);
                File.WriteAllLines(filePath, lines.Skip(1).ToArray());
                if (rdp.Connected.ToString() == "1")
                {
                    rdp.RequestClose();
                    rdp.Disconnect();
                    lblResult.Text = "Please Press Conncet for Another Server Load";
                    this.Refresh();

                }
            }
            catch (Exception Ex)
            {
                lblResult.Text = "IP List is Empty";
                MessageBox.Show("Connecting Failed", "ERR:  " + txtServerName.Text + Ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            File.AppendAllText("C:\\Ost-RDP\\GoodOut.txt", txtServerName.Text.ToString() + ":" + txtPort.Text.ToString() + "  |u|  " + txtUserName.Text.ToString() + "   |p|  " + txtPassword.Text.ToString() + Environment.NewLine);
            lblResult.Text = "Saved";
        }

        private void Connecting_p1p(object sender, EventArgs e)
        {

          //PictureBox pb1 = new PictureBox();
           //pb1.Image = Image.FromFile("C:\\Users\\Admin\\Desktop\\Projects\\xam\\Ost-RDP10v\\Ost-RDP\\Ost-RDP\\bin\\Release\\12345.png");
            //pb1.Location = new Point(100, 100);
            //pb1.Size = new Size(500, 500);
            //this.Controls.Add(pb1);
        }


        private void Connected_A4A(object sender, EventArgs e)
        {
            SendKeys.Send("%"+"Y");
            lblResult.Text = "Connected succeeded " + txtServerName.Text.ToString();
            rdp.ConnectingText = "Connected succeeded " + txtServerName.Text.ToString();

        }
        private void Connected_BB(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
            button2.PerformClick();

        }

        private void Disconnected_procsess(object sender,AppDomainInitializer e)
        {
            string filePath = @"C:\\Ost-RDP\\ipServer.txt";
            var lines = File.ReadAllLines(filePath);
            File.WriteAllLines(filePath, lines.Skip(1).ToArray());
            button1.PerformClick();
        }      
    }
 }