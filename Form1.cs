using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            Text = HeaderText;
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

            Text = HeaderText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            Text = HeaderText;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Text = HeaderText;
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
    }
}
