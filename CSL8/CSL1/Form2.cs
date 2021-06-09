using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace CSL1
{
    public partial class Form2 : Form
    {
        internal List<Figure> figures = new List<Figure>();
        //создаём List типа класса Figure
        Graphics g; //Создаём объект типа Graphics для отрисовки
        bool isMouseDown = false; // флаг рисования
        public string fileName = null; //имя файла
        public bool flagIzmen = false; //флаг изменения дочерней формы
        public int numFigure = 0;
        BufferedGraphics BuffGrapics;
        Figure cur;
        Form1 f1;
       
        public Form2()
        {
            InitializeComponent();

        }
        private void Form2_Load(object sender, System.EventArgs e)
        {
            g = CreateGraphics();
            //Устанавливаем максимальный размер буферной зоны
            BufferedGraphicsManager.Current.MaximumBuffer = SystemInformation.PrimaryMonitorMaximizedWindowSize;

            BuffGrapics = BufferedGraphicsManager.Current.Allocate(g, new Rectangle(0, 0,
                SystemInformation.PrimaryMonitorMaximizedWindowSize.Width,
                SystemInformation.PrimaryMonitorMaximizedWindowSize.Height));

            BuffGrapics.Graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, Size.Width, Size.Height);
            BuffGrapics.Render();
        }
        //Возвращение размеры рисунка
        public Size GetSize()
        {
            return Size;
        }
        //функция обработки события нажатия кнопки мыши
        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            //если нажата левая кнопка мыши
            if (e.Button == MouseButtons.Left && isMouseDown == false)           
            {      
                isMouseDown = true;
                numFigure = ((Form1)MdiParent).numFigure;
                switch (numFigure) // выбор фигуры для рисования

                {

                    case 0:
                        cur = new Rect(e.Location,e.Location, f1.brushSize, f1.colorLine, f1.colorFon);
                        
                        break;
                    case 1:
                        cur = new Ellipse(e.Location, e.Location, f1.brushSize, f1.colorLine, f1.colorFon);
                        
                        break;
                    case 2:
                        cur = new StraightLine(e.Location, e.Location, f1.brushSize, f1.colorLine, f1.colorFon);
                        break;
                    case 3:
                        cur = new CurveLine(e.Location, e.Location, f1.brushSize, f1.colorLine, f1.colorFon);
                        break;
                }
                cur.IsFilling = f1.IsFigureFilling;//Определяем состояние флага заливки
            }
        }
       
        //Функция обработки события перемещения мыши
        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {          
            f1 = (Form1)ParentForm;
            //В строке состояние записываем расположение курсора
            f1.GetStatusBar().Panels[0].Text = e.Location.X - AutoScrollPosition.X + ", " + (e.Location.Y - AutoScrollPosition.Y);
            g = CreateGraphics();
            if (isMouseDown)
            {
                BuffGrapics.Render(g);
                cur.MouseMove(e.Location);
                cur.DrawDash(g,AutoScrollPosition);
            }
        }

        //функция обработки события отпускания кнопки мыши
        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            f1 = (Form1)ParentForm;
            if (isMouseDown)
            {
                //Проверка на то, помещается ли фигура в область рисования
                if (cur.endPoint.X < Size.Width && cur.endPoint.Y < Size.Height
                && cur.startPoint.X > 0 && cur.startPoint.Y > 0)
                {
                    flagIzmen = true; //мы изменяли текущий файл
                    cur.Draw(BuffGrapics.Graphics, AutoScrollPosition);
                    figures.Add(cur); //Добавление объекта в List
                }             
                 else cur.Hide(g);
                BuffGrapics.Render();
                Invalidate();

                isMouseDown = false;
            }
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            BuffGrapics.Graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, this.Width, this.Height);
            //Перерисовка фигур           
            foreach (Figure MainFigure in figures)
            {
                MainFigure.Draw(BuffGrapics.Graphics, AutoScrollPosition);
            }   
            BuffGrapics.Render(e.Graphics);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flagIzmen) //если текущий файл изменен
            {
                DialogResult result;
                result = MessageBox.Show("Сохранить изменения в \"" + Text + "\"", "Графический редактор Никиты", MessageBoxButtons.YesNoCancel);
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

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Освобождение ресурсов
            BuffGrapics.Dispose();
        }

        private void Form2_Activated(object sender, System.EventArgs e)
        {
            f1 = (Form1)ParentForm;   //определение родительской формы
            f1.GetStatusBar().Panels[4].Text = Width.ToString() + " x " + Height.ToString();
        }
    }
}
