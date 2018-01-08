using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

        }



        private void button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread thread = new System.Threading.Thread(start);
            thread.Start();

            //System.Threading.Thread thread1 = new System.Threading.Thread(start);
            //thread1.Start();
        }

        private void start()
        {
            int count = Convert.ToInt32(textBox1.Text);
            DateTime startTime = DateTime.Now;
            label1.Text = "开始时间：" + startTime.ToString("yyyy-MM-dd mm:HH:ss ff");
            int iii = 1;
            for (int i = 0; i < count; i++)
            {
                // DateTime time = DateTime.Now;
                // TimeSpan ts = time.Subtract(startTime);
                if (i==0)
                {
                    button1.Text = "执行中...";
                }
                if (i % 100 == 0)
                {
                    label2.Text = "总数" + "已执行"+ i;
                }
                //label2.Text = "执行时间：" + ts.Hours + "小时 " + ts.Minutes + "分钟 " + ts.Seconds + "秒 " + ts.Milliseconds + "毫秒";
               
                //System.Threading.Thread.Sleep(1000);
            }
            label3.Text = "结束时间：" + DateTime.Now.ToString("yyyy-MM-dd mm:HH:ss ff");
            button1.Text = "开始";
        }
    }
}
