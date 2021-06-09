using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace CSL1
{
    public partial class Form2 : Form
    {
        internal List<Figure> figures = new List<Figure>();
        //создаём List типа класса Figure
        Rect rectangle; //Создаём переменную типа класса Rect
        Graphics g; //Создаём объект типа Graphics для отрисовки
        bool isMouseDown = false; // флаг рисования
        public string fileName = null; //имя файла
        public bool flagIzmen = false; //флаг изменения дочерней формы
        Form1 f1;
        public Form2()
        {
            InitializeComponent();
        }
        //функция обработки события нажатия кнопки мыши
        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isMouseDown == false)
            //если нажата левая кнопка мыши
            {
                isMouseDown = true;
                rectangle = new Rect(e.Location, e.Location); //Создаём новый объект класса Rect c начальными координатами в точке нажатия мыши
            
            }
        }
        private void Form2_Load(object sender, System.EventArgs e)
        {

        }
        //Функция обработки события перемещения мыши
        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            g = CreateGraphics();
            if (isMouseDown)
            {
                rectangle.Hide(g);
                rectangle.MouseMove(e.Location);
                rectangle.DrawDash(g);
            }
        }

        //функция обработки события отпускания кнопки мыши
        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                flagIzmen = true; //мы изменяли текущий файл
                rectangle.Draw(g);
                figures.Add(rectangle); //Добавление объекта в List
                Invalidate();
                isMouseDown = false;
            }
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            foreach (Figure f in figures)
            {
                f.Draw(g);
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flagIzmen) //если текущий файл изменен
            {
                DialogResult result;
                result = MessageBox.Show("Сохранить изменения в \"" + this.Text + "\"", "Графический редактор Никиты", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.Yes:
                        {
                            f1 = new Form1();
                            f1.saveFile(this); //сохранение файла (см. форму 1)
                            break;
                        }
                    case DialogResult.Cancel:
                        {
                            e.Cancel = true; //возвращение к файлу
                            return;
                        }
                }
            }
        }
    }
}
