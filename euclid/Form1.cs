using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace euclid
{
    public partial class Form1 : Form
    {

        ulong lfsr;
        ulong[] key;
        byte[] index, opentext;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            textBox1.Text = System.IO.File.ReadAllText(openFileDialog1.FileName);
            lfsr = Convert.ToUInt64(textBox2.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lfsr = Convert.ToUInt64(textBox1.Text);
            index = new byte[] { 12, 7, 3, 1 };
            //ulong x = 0;
            textBox2.Text = "";
            for (int i = 0; i < text.Text.Length * 8; ++i)
            {
                //if (i != 0 && (i % 16 == 0)) textBox1.Text += Environment.NewLine;
                lfsr = ((((lfsr >> 11) ^ (lfsr >> 5) ^ (lfsr >> 2)) & 1) << 15) | (lfsr >> 1);
                textBox2.Text += (lfsr & 1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            key = new ulong[text.Text.Length];
            Random rand = new Random();
            for (int i = 0; i < key.Length; i++)
            {
                key[i] = (ulong)rand.Next(key.Length);
                textBox3.Text += key[i];
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            for (int i = 0; i < textBox2.Text.Length / 8; ++i)
            {
                ulong otext = Convert.ToByte(text.Text[i]);

                var b = Convert.ToByte(textBox2.Text.Substring(i * 8, 8), 2);
                otext = otext ^ b;
                textBox4.Text += (char)otext;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            for (int i = 0; i < text.Text.Length; ++i)
            {
                ulong otext = Convert.ToByte(text.Text[i]);
                ulong b = Convert.ToByte(textBox3.Text[i]);
                otext = otext ^ b;
                textBox4.Text += (char)otext;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "key.txt";
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                    sw.WriteLine(textBox3.Text);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "output.txt";
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                    sw.WriteLine(textBox4.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            text.Text = System.IO.File.ReadAllText(openFileDialog1.FileName);
            using (FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read))
            {
                opentext = new byte[fs.Length];
                fs.Read(opentext, 0, (int)fs.Length);
            }
        }

    }
}
