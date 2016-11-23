using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6_Registry
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //this.comboBox1.SelectedItem = this.comboBox1.Items[0];
            comboBox1.SelectedIndex = 0;
        }

        private int convert_2_to_10(int n)
        {
            return 0;
        }

        private int convert_2_to_16(int n)
        {
            return 0;
        }

        private int convert_16_to_10(int n)
        {
            return 0;
        }

        private int convert_16_to_2(int n)
        {
            return 0;
        }

        private int convert_10_to_2(int n)
        {
            return 0;
        }

        private int convert_10_to_16(int n)
        {
            return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selected_text = this.textBox1.SelectedText;
            decimal d;
            String result = "";
            //String result = Convert.ToString(Convert.ToInt32(selected_text, f), t);

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
                    if (true)
                    {
                        result = Convert.ToString(Convert.ToInt32(selected_text, 10), 16);
                    }
                    break;

                //2 → 10
                case 2:
                    if (true)
                    {
                        result = Convert.ToString(Convert.ToInt32(selected_text, 2), 10);
                    }
                    break;

                //2 → 16
                case 3:
                    if (true)
                    {
                        result = Convert.ToString(Convert.ToInt32(selected_text, 2), 16);
                    }
                    break;

                //16 → 2
                case 4:
                    if (true)
                    {
                        result = Convert.ToString(Convert.ToInt32(selected_text, 16), 2);
                    }
                    break;

                //16 → 10
                case 5:
                    if (true)
                    {
                        result = Convert.ToString(Convert.ToInt32(selected_text, 16), 2);
                    }
                    break;

            }
        }
    }
}
