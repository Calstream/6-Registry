using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;
using Microsoft.Win32;

namespace _6_Registry
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;



            RegistryKey rk = null;
            string s = "";
            try
            {
                rk = Registry.CurrentUser.OpenSubKey(regkey);
                if (rk != null)
                {
                    Width = (int)rk.GetValue("FormWidth", Width);
                    Height = (int)rk.GetValue("FormHeight",  Height);
                    s = (string)rk.GetValue("FileName");
                    if (Directory.Exists(s))
                    {
                        this.Text = Path.GetFileName(s);
                        StreamReader sr = new StreamReader(filepath);
                        textBox1.Text = sr.ReadToEnd();
                        sr.Close();
                        textBox1.Enabled = true;
                    }
                    textBox1.SelectionStart = (int)rk.GetValue("CursorPosition");
                    comboBox1.SelectedIndex = (int)rk.GetValue("ConvertMode");
                    this.Location = (Point)rk.GetValue("FormLocation");
                }
            }
            finally
            {
                if (rk != null)
                    rk.Close();
            }



        }

        private static readonly Regex binary = new Regex("^[01]{1,32}$", RegexOptions.Compiled);

        private bool saved = true;
        private string filepath = "";
        private string regkey = "Software\\CSExamples\\reg-6";

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
            {
                string selected_text = this.textBox1.SelectedText.Trim();
                decimal d;
                int t;
                String result = selected_text;
                switch (this.comboBox1.SelectedIndex)
                {
                    //10 → 2
                    case 0:
                        if (decimal.TryParse(selected_text, out d))
                        {
                            result = Convert.ToString(Convert.ToInt32(selected_text, 10), 2);
                        }
                        break;

                    //10 → 16
                    case 1:
                        if (decimal.TryParse(selected_text, out d))
                        {
                            result = Convert.ToString(Convert.ToInt32(selected_text, 10), 16);
                        }
                        break;

                    //2 → 10
                    case 2:
                        if (binary.IsMatch(selected_text))
                        {
                            result = Convert.ToString(Convert.ToInt32(selected_text, 2), 10);
                        }
                        break;

                    //2 → 16
                    case 3:
                        if (binary.IsMatch(selected_text))
                        {
                            result = Convert.ToString(Convert.ToInt32(selected_text, 2), 16);
                        }
                        break;

                    //16 → 2
                    case 4:
                        if (int.TryParse(selected_text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out t))
                        {
                            result = Convert.ToString(Convert.ToInt32(selected_text, 16), 2);
                        }
                        break;

                    //16 → 10
                    case 5:
                        if (int.TryParse(selected_text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out t))
                        {
                            result = Convert.ToString(Convert.ToInt32(selected_text, 16), 10);
                        }
                        break;
                }
                if (!string.Equals(selected_text, result))
                {
                    textBox1.Text = textBox1.Text.Replace(textBox1.Text.Substring(textBox1.SelectionStart, textBox1.SelectionLength), result);
                }
            }
        }

        private void Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open...";
            ofd.Filter = "Text file|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filepath = ofd.FileName;
                StreamReader sr = new StreamReader(filepath);
                textBox1.Text = sr.ReadToEnd();
                sr.Close();
                this.Text = Path.GetFileName(filepath);
                this.textBox1.Enabled = true;
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            File.WriteAllText(filepath, textBox1.Text);
            this.Text = this.Text.Remove(this.Text.Length - 1, 1);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            if (saved)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = "Save yourself";
                saveFileDialog1.Filter = "Text file|*.txt";
                saveFileDialog1.ShowDialog();
            }
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!this.Text.Contains("*"))
                this.Text = this.Text + "*";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            RegistryKey rk = null;
            try
            {
                rk = Registry.CurrentUser.CreateSubKey(regkey);
                if (rk == null)
                    return;
                rk.SetValue("FileName", filepath);
                rk.SetValue("ConvertMode", comboBox1.SelectedIndex);
                rk.SetValue("FormHeight", this.Height);
                rk.SetValue("FormWidth", this.Width);
                rk.SetValue("FormLocation", this.Location);
                rk.SetValue("CursorPosition",textBox1.SelectionStart);
            }
            finally
            {
                if (rk != null)
                    rk.Close();
            }
        }
    }
}
