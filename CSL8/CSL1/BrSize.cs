using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSL1
{
    public partial class BrSize : Form
    {
        string vybor0;
        public int vybor;
        public BrSize()
        {
            InitializeComponent();
            int[] mass = new int[] { 1, 2, 5, 8, 10, 12, 15 };
            int i = 0;
            while (i < mass.Length)
            { comboBox1.Items.Add(mass[i]); i++; }      //Добавляем элемент в список позиций      
            comboBox1.SelectedIndex = 0;
        }

        private void BrSize_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            vybor0 = comboBox1.Text;
            vybor = Convert.ToInt32(vybor0);//Преобразовываем текстовое  значение этого поля в целочисленное
            
        }

        
    }
}
