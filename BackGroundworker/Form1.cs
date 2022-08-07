using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
namespace BackGroundworker
{
    public partial class Form1 : Form
    {
        List<int> temp;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.WorkerReportsProgress = true;
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int time = (int)e.Argument;
            temp = new List<int>();
            for (int i = 0; i <= 10; i++)
            {
                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                backgroundWorker1.ReportProgress(i * 10);
                Thread.Sleep(time);
                temp.Add(i);
            }
            e.Result = temp;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                MessageBox.Show("You Cancelled The BackGroundWorker!");
            else
            {
                temp.AddRange((List<int>)(e.Result));
                MessageBox.Show("Done!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync(300);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
    }
}
