using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseClicker
{
    public partial class Form1 : Form
    {
        public string HeaderText { get => $"{timer1.Enabled}, interval {timer1.Interval}"; }
        
        public Form1()
        {
            InitializeComponent();
            lblStatus.Text = HeaderText;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var oldPos = MouseOperations.GetCursorPosition();
            
            MouseOperations.SetCursorPosition((int)xCoord.Value, (int)yCoord.Value);

            if (rbRight.Checked)
            {
                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightDown | MouseOperations.MouseEventFlags.RightUp);
            }
            if (rbLeft.Checked)
            {
                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown | MouseOperations.MouseEventFlags.LeftUp);
            }

            MouseOperations.SetCursorPosition(oldPos);

            lblStatus.Text = HeaderText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            lblStatus.Text = HeaderText;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            lblStatus.Text = HeaderText;
        }


        private void btnClickLocation_Click(object sender, EventArgs e)
        {
            IntPtr desktopPtr = MyInterop.GetDC(IntPtr.Zero);
            Graphics g = Graphics.FromHdc(desktopPtr);

            SolidBrush b = new SolidBrush(Color.Red);
            g.FillEllipse(b, new Rectangle((int)(xCoord.Value - 10), (int)(yCoord.Value - 10), 10, 10));

            g.Dispose();
            MyInterop.ReleaseDC(IntPtr.Zero, desktopPtr);
        }

        private volatile int _i = 0;
        private async void btnTest_Click(object sender, EventArgs e)
        {
            await DoSomeJob();
            MessageBox.Show($"the _i is {_i}");
        }

        async Task DoSomeJob()
        {
            await new TaskFactory().StartNew(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(10));
                _i++;
            });
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MyInterop.ReleaseCapture();
                MyInterop.SendMessage(Handle, MyInterop.WM_NCLBUTTONDOWN, MyInterop.HT_CAPTION, 0);
            }
        }
    }
}
