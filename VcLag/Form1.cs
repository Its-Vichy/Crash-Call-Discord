using System.Windows.Forms;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Threading;

namespace VcLag
{
    public partial class Form1 : Form
    {
        public Form1() => InitializeComponent();

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "" || guna2TextBox2.Text == "")
            {
                MessageBox.Show("Enter valid token / channel id");
                return;
            }

            MessageBox.Show($"{guna2TrackBar1.Value}");

            Thread T = new Thread(() => lag(guna2TextBox1.Text, guna2TextBox2.Text, guna2TrackBar1.Value));
            T.Start();
        }

        private static void lag(string token, string channelID, int delay)
        {
            List<string> regions = new List<string> { "japan", "europe", "brazil", "hongkong", "india", "russia", "singapore", "southafrica", "sydney", "us-central", "us-east", "us-south", "us-west" };

            while (true) {
                System.Threading.Thread.Sleep(delay * 1000);
                Random rnd = new Random();
                string country = regions[rnd.Next(regions.Count)];

                try
                {
                    var Request = (HttpWebRequest)WebRequest.Create($"https://discord.com/api/v9/channels/{channelID}/call");
                    var RequestData = Encoding.ASCII.GetBytes("{" + $"\"region\": \"{country}\"" + "}");

             
                    Request.Method = "PATCH";
                    Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) discord/1.0.9003 Chrome/91.0.4472.164 Electron/13.4.0 Safari/537.3";
                    Request.ContentLength = RequestData.Length;
                    Request.ContentType = "application/json";
                    Request.Headers.Add("authorization", token);

                    using (var stream = Request.GetRequestStream()) { stream.Write(RequestData, 0, RequestData.Length); }
                    var Response = new StreamReader(Request.GetResponse().GetResponseStream()).ReadToEnd();
                }
                catch { }
            }
        }
    }
}