using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1l
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = CreateGraphics();
            if (e.Button == MouseButtons.Left)
            {
                string s = "(" + e.X.ToString() + ";" + e.Y.ToString() + ")";
                g.DrawString(s, new Font("Algerian", 14),
                new SolidBrush(Color.Black), new Point(e.X, e.Y));
            }
            if (e.Button == MouseButtons.Right)
            {
                MessageBox.Show("Нажата правая кнопка мыши", "Hello");
                g.Clear(Color.Red);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}