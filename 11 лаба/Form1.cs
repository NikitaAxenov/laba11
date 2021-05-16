using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _11_лаба
{
    public partial class Form1 : Form
    {
        const double b = 4294967299;
        const double m = 9223372036854775808;
        double xNext = b;
        double average = 0;
        double average2 = 0;
        double average3 = 0;
        double variance = 0;
        double variance2 = 0;
        double variance3 = 0;
        double chi = 0;
        double xBefore, xNow;
        double[] stat = new double[5] { 0, 0, 0, 0, 0 };
        double[] prob = new double[5] { 0, 0, 0, 0, 0 };
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 20; i++)
            {
                xBefore = xNext;
                xNext = (b * xBefore) % m;
                xNow = xNext / m;
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            average = 0;
            average2 = 0;
            average3 = 0;
            variance = 0;
            variance2 = 0;
            variance3 = 0;
            chi = 0;
            chart1.Series[0].Points.Clear();
            if ((double)prob1.Value + (double)prob2.Value + (double)prob3.Value + (double)prob4.Value > 1)
                Error.Text = "Введённые данные некоректны";
            else
            {
                prob[0] = (double)prob1.Value;
                prob[1] = (double)prob2.Value;
                prob[2] = (double)prob3.Value;
                prob[3] = (double)prob4.Value;
                prob[4] = 1 - ((double)prob1.Value + (double)prob2.Value + (double)prob3.Value + (double)prob4.Value);
                for (int i = 0; i < numericUpDown1.Value; i++)
                {
                    xBefore = xNext;
                    xNext = (b * xBefore) % m;
                    xNow = xNext / m;
                    if (xNow < (double)prob1.Value)
                    {
                        stat[0]++;
                    }
                    else if (xNow < (double)prob1.Value + (double)prob2.Value)
                    {
                        stat[1]++;
                    }
                    else if (xNow < (double)prob1.Value + (double)prob2.Value + (double)prob3.Value)
                    {
                        stat[2]++;
                    }
                    else if (xNow < (double)prob1.Value + (double)prob2.Value + (double)prob3.Value + (double)prob4.Value)
                    {
                        stat[3]++;
                    }
                    else if (xNow <= 1)
                    {
                        stat[4]++;
                    }
                }
                for (int i = 0; i < 5; i++)
                {
                    average += prob[i] * (i + 1);
                    variance += prob[i] * (i + 1) * (i + 1);
                    if (stat[i] != 0)
                    {
                        average2 += stat[i] / (double)numericUpDown1.Value * (i + 1);
                        variance2 += stat[i] / (double)numericUpDown1.Value * (i + 1) * (i + 1);
                        chi += (stat[i] * stat[i]) / ((double)numericUpDown1.Value * prob[i]);
                    }
                }
                variance -= (average * average);
                variance2 -= (average2 * average2);
                chi -= (double)numericUpDown1.Value;
                for (int i = 0; i < 5; i++)
                {
                    chart1.Series[0].Points.AddXY(i + 1, stat[i] / (double)numericUpDown1.Value);
                    stat[i] = 0;
                }
                average3 = (Math.Abs(average2 - average) / Math.Abs(average)) * 100;
                variance3 = (Math.Abs(variance2 - variance) / Math.Abs(variance)) * 100;
                if (chi <= 11.07)
                {
                    label17.Text = "false";
                    label17.ForeColor = Color.Red;
                }
                else
                {
                    label17.Text = "true";
                    label17.ForeColor = Color.Green;
                }
                label13.Text = Math.Round(chi, 2) + " " + ">" + " " + "11.07" + " " + "?";
                label6.Text = Math.Round(average2, 2) + " " + "(error = " + Math.Round(average3, 0) + "%)";
                label12.Text = Math.Round(variance2, 2) + " " + "(error = " + Math.Round(variance3, 0) + "%)";
                Error.Text = "";
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
