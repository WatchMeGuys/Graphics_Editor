using System;
using System.Windows.Forms;

namespace CSL1
{
    [Serializable()]
    public partial class Form1 : Form
    {

          
        SaveFileDialog SavePic; //Переменная типа SaveFileDialog для сохранения файлов
        OpenFileDialog OpenPic; //Переменная типа OpenFileDialog для открытия файлов из системы
        public Form1()
        {
            InitializeComponent();
            //инициализация переменных
            SavePic = new SaveFileDialog();
            OpenPic = new OpenFileDialog();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Child = new Form2();
            Child.MdiParent = this;
            Child.Text = "Рисунок " + this.MdiChildren.Length.ToString();
            Child.Show();

        }

        private void windowToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


    }
}
