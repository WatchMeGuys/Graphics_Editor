using System;
using System.Drawing;
using System.Windows.Forms;

namespace CSL1
{
    public partial class Form2 : Form
    {
        Point point1, point2;
        Point point3, point4;
        bool action = false;
        bool fst = true;
        public Form2()
        {
            InitializeComponent();
        }

        //функция обработки события нажатия кнопки мыши
        private void Form2_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && action == false)
            {
                point1 = new Point(e.X, e.Y);
                action = true;
            }
        }

        //Функция обработки события перемещения мыши
        private void Form2_MouseMove_1(object sender, MouseEventArgs e)
        {
            // иницилизируем объект для отображения графики
            Graphics g = CreateGraphics();
            Pen pen1 = new Pen(Color.Black, 1);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            //инициализируем переменную типа pen для пунктирной линии
            point2 = new Point(e.X, e.Y);
            //Записываем координаты правой нижней точки
            if (action) //Если флаг рисования установлен, то рисование возможно
            {
                // нормализация для рисования в любом направлении
                int xmin = Math.Min(point1.X, point2.X);
                int xmax = Math.Max(point1.X, point2.X);
                int ymin = Math.Min(point1.Y, point2.Y);
                int ymax = Math.Max(point1.Y, point2.Y);
                if (!fst)
                {
                    // отрисовка белого прямоугольника по предыдущим координатам
                    g.DrawRectangle(new Pen(Color.White, 1), Rectangle.FromLTRB(point3.X, point3.Y, point4.X, point4.Y));
                }
                // отрисовка прямоугольника чёрным пунктиром
                g.DrawRectangle(pen1, Rectangle.FromLTRB(xmin, ymin, xmax, ymax));
                fst = false;
                //переменные для записи предыдущих координат
                point3 = new Point(xmin, ymin);
                point4 = new Point(xmax, ymax);

            }
        }

        //функция обработки события отпускания кнопки мыши
        private void Form2_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (action)
            {
                Graphics g = CreateGraphics();
                g.DrawRectangle(new Pen(Color.Black, 1), Rectangle.FromLTRB(point3.X, point3.Y, point4.X, point4.Y));
                // Рисование сплошной чёрной линией
                action = false;
                fst = true;
            }
            point2 = new Point(0, 0);
        }
    }
}
