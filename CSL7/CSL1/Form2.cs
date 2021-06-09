using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace CSL1
{
    public partial class Form2 : Form
    {
        Figure MainFigure;//переменная для инициализации типа фигуры
        Form1.Figures Figure; //Переменная для хранения типа фигуры
        internal List<Figure> figures = new List<Figure>();
        //создаём List типа класса Figure
        Graphics g; //Создаём объект типа Graphics для отрисовки
        bool isMouseDown = false; // флаг рисования
        public string fileName = null; //имя файла
        public bool flagIzmen = false; //флаг изменения дочерней формы
        static Form1 f1;

        public Form2()
        {
            InitializeComponent();
        }
        //функция обработки события нажатия кнопки мыши
        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            Figure = f1.ChoosedFigure;
            if (e.Button == MouseButtons.Left && isMouseDown == false)
            //если нажата левая кнопка мыши
            {
                isMouseDown = true;
                switch (Figure)
                {
                    case Form1.Figures.Rectangle:
                        MainFigure = new Rect(e.Location, e.Location, f1.brushSize, f1.colorLine, f1.colorFon);
                        break;
                    case Form1.Figures.Ellipse:
                        MainFigure = new Ellipse(e.Location, e.Location, f1.brushSize, f1.colorLine, f1.colorFon);
                        break;                    
                }
            }
        }
        private void Form2_Load(object sender, System.EventArgs e)
        {
            
        }
        //Функция обработки события перемещения мыши
        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            f1 = (Form1)ParentForm;
            g = CreateGraphics();
            if (isMouseDown)
            {
                MainFigure.Hide(g);
                MainFigure.MouseMove(e.Location);
                MainFigure.DrawDash(g,AutoScrollPosition);
            }
        }

        //функция обработки события отпускания кнопки мыши
        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            f1 = (Form1)ParentForm;
            if (isMouseDown)
            {
                //Проверка на то, помещается ли фигура в область рисования
                if (MainFigure.endPoint.X < Size.Width && MainFigure.endPoint.Y < Size.Height
                && MainFigure.startPoint.X > 0 && MainFigure.startPoint.Y > 0)
                {
                    flagIzmen = true; //мы изменяли текущий файл
                    MainFigure.Draw(g,AutoScrollPosition);
                    figures.Add(MainFigure); //Добавление объекта в List
                }

                Invalidate();

                isMouseDown = false;
            }
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            //Заливка рабочей области при перерисовке
            g = CreateGraphics();
            g.FillRectangle(new SolidBrush(Color.White), Rectangle.FromLTRB(0, 0, Size.Width, Size.Height));           
            foreach (Figure f in figures)
            {
                f.Draw(g,AutoScrollPosition);
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
