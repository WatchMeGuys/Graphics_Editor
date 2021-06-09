using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Rect= CSL1.Rect;
namespace CSL1
{
    public partial class Form2 : Form
    {
        List<Figure> figures = new List<Figure>();
        //создаём List типа класса Figure
        Rect rectangle; //Создаём переменную типа класса Rect
        Graphics g; //Создаём объект типа Graphics для отрисовки
        bool isMouseDown = false; // флаг рисования
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

    }
}
