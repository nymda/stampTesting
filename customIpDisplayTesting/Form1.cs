using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace customIpDisplayTesting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Bitmap canvas = new Bitmap(400, 150);
        public Graphics g;
        public Random rnd = new Random();
        private void Form1_Load(object sender, EventArgs e)
        {
            g = Graphics.FromImage(canvas);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = generateStamp("111.111.111.111:8080", "admin:admin");
        }

        public Bitmap generateStamp(string ip, string userpass)
        {
            Font luc = new Font("Lucida Console", 15);

            Bitmap canvas = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(canvas);

            SizeF _ipSize = g.MeasureString(ip, luc);
            SizeF _upSize = g.MeasureString(userpass, luc);
            int setWidth = 0;

            if(_ipSize.Width > _upSize.Width)
            {
                setWidth = (int)_ipSize.Width;
            }
            else
            {
                setWidth = (int)_upSize.Width;
            }

            //leave space for a 2px buffer
            setWidth = setWidth + 4;
            int setHeight = (int)_ipSize.Height + (int)_upSize.Height + 6;

            canvas = new Bitmap(setWidth, setHeight);
            Graphics g = Graphics.FromImage(canvas);

            Color midGray = Color.FromArgb(128, 128, 128);
            Color vDarkGray = Color.FromArgb(64, 64, 64);
            Pen FourPxBlack = new Pen(Brushes.Black, 4);
            Pen TwoPxLGray = new Pen(midGray, 2);
            Pen OnePxVDarkGray = new Pen(vDarkGray, 1);
            g.DrawRectangle(FourPxBlack, 2, 2, setWidth - 4, setHeight - 4);
            g.DrawRectangle(TwoPxLGray, 1, 1, setWidth - 2, setHeight - 2);
            g.DrawLine(OnePxVDarkGray, 0, 0, 0, setHeight);
            g.DrawLine(OnePxVDarkGray, 1, 1, 1, setHeight);
            g.DrawLine(OnePxVDarkGray, 0, setHeight - 1, setWidth - 2, setHeight - 1);
            g.DrawLine(OnePxVDarkGray, 0, setHeight - 2, setWidth - 3, setHeight - 2);

            return canvas;
        }

        public void snap()
        {
            string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            string fname = path + "/" + RandomString(10) + ".png";
            pictureBox1.Image.Save(fname);
            Process.Start(fname);
        }

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            snap();
        }
    }
}