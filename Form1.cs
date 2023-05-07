using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32;

namespace Truncator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int charnumber = Convert.ToInt32(numericUpDown1.Value);
            string input = richTextBox1.Text;
            string output = input.Left(charnumber);
            richTextBox2.Text = output;
            button2.Enabled = true;
            if (checkBox1.Checked == true){
                Clipboard.SetText(richTextBox2.Text);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string registrykey = "HKEY_CURRENT_USER\\Software\\TextTruncator";
            int AutoCopyEnabled = 0;
            try
            {
                object regValue = Registry.GetValue(registrykey, "AutoCopy", null);
                if (regValue != null && regValue is int)
                {
                    AutoCopyEnabled = (int)regValue;
                }
            }
            catch (Exception)
            {
                AutoCopyEnabled=0;
            }
            if (AutoCopyEnabled == 1)
            {
                checkBox1.Checked = true;
            }
            int num = 0;
            try
            {
                object regValue = Registry.GetValue(registrykey, "Number", null);
                if (regValue != null && regValue is int)
                {
                    num = (int)regValue;
                }
            }
            catch (Exception)
            {
                num = 0;
            }
            if (num != 0)
            {
                numericUpDown1.Value = num;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string registrykey = "HKEY_CURRENT_USER\\Software\\TextTruncator";
            if (checkBox1.Checked == true)
            {
                Registry.SetValue(registrykey, "AutoCopy", "1", RegistryValueKind.DWord);
            }
            if (checkBox1.Checked == false)
            {
                Registry.SetValue(registrykey, "AutoCopy", "0", RegistryValueKind.DWord);
            }
            int num = Convert.ToInt32(numericUpDown1.Value);
            Registry.SetValue(registrykey, "Number", num, RegistryValueKind.DWord);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox2.Text);
        }
    }
    public static class StringExtensions
    {
        public static string Left(this string str, int length)
        {
            return str.Substring(0, Math.Min(length, str.Length));
        }
    }
}
