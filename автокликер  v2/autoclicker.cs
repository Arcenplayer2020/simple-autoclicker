using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace автокликер__v2
{
    public partial class autoclicker : Form
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        private const int leftup = 0x0004;
        private const int leftdown = 0x0002;
        public int intervals = 5;
        public bool clicked = false;
        public int parsedvalue;
        public autoclicker()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Thread ac = new Thread(autoclick);
            ac.Start();
            backgroundWorker1.RunWorkerAsync();
        }
        private void autoclick()
        {
            while (true)
            {
                if (clicked == true )
                {


                    mouse_event(dwFlags: leftup, dx: 0, dy: 0, cButtons: 0, dwExtraInfo: 0);
                    Thread.Sleep(1);
                    mouse_event(dwFlags: leftdown, dx: 0, dy: 0, cButtons: 0, dwExtraInfo: 0);
                    Thread.Sleep(intervals);
                }
                Thread.Sleep(2);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (checkBox1.Checked)
                {
                    if (GetAsyncKeyState(Keys.B) < 0)
                    {
                        clicked = false;
                    }
                    else if (GetAsyncKeyState(Keys.G) < 0)
                    {
                        clicked = true;
                    }
                    Thread.Sleep(1);

                }
                Thread.Sleep(1);
            }
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text,out parsedvalue))
            {
                MessageBox.Show("введи число");
            }
            else
            {
                intervals = int.Parse(textBox1.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("закрывать через диспечер задач т.к он будет работать в фоновом режиме");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}