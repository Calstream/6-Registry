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

namespace _6_Registry
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private static readonly Regex binary = new Regex("^[01]{1,32}$", RegexOptions.Compiled);

        private bool saved = true;
        private string filepath = "";

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
    }
}
