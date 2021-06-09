using System;
using System.Drawing;

namespace CSL1
{

    [Serializable()]
    public abstract class Figure //Абстрактный класс для фигуры

    {
        protected Point point1, point2;
        // переменные для хранения координат точек прямоугольника
        public Figure(Point point1, Point point2) //Конструктор класса Figure для инициализации координат точек
        {
            this.point1 = point1;
            this.point2 = point2;
        }
        //Зададим абстрактные методы класса
        public abstract void Draw(Graphics g);
        public abstract void DrawDash(Graphics g);
        public abstract void Hide(Graphics g);
    }

    public class Rect : Figure // Создадим производный класс для прямоугльника от класса Figure
    {

        public Point startPoint, endPoint; //Переменные для новых координат точек
        public Rect(Point point1, Point point2) : base(point1, point2) // Конструктор класс Rect производный от класса Figure
        {

        }
        public void MouseMove(Point point2) //Функция для изменения второй координаты прямоугольника
        {
            this.point2 = point2;
        }
        //Виртуальная функция нормализации координат
        public void norm(ref Point startPoint, ref Point endPoint)
        {
            int xmin = Math.Min(startPoint.X, endPoint.X);
            int xmax = Math.Max(startPoint.X, endPoint.X);
            int ymin = Math.Min(startPoint.Y, endPoint.Y);
            int ymax = Math.Max(startPoint.Y, endPoint.Y);
            startPoint = new Point(xmin, ymin);
            endPoint = new Point(xmax, ymax);
        }
        //Виртуальная функция для рисования конечного сплошного контура
        public override void Draw(Graphics g)
        {
            norm(ref point1, ref point2); //Применяем нормализацию координат
            g.DrawRectangle(new Pen(Color.Black, 1), Rectangle.FromLTRB(point1.X, point1.Y, point2.X, point2.Y));
            // - рисование прямоугольника по конечным координатам
        }
        //Виртуальная функция рисования пунктирных контуров прямоугольника
        public override void DrawDash(Graphics g)
        {
            startPoint = point1;
            endPoint = point2;
            //Используем другие переменные, чтобы не изменить начальные координаты
            norm(ref startPoint, ref endPoint);
            Pen pen1 = new Pen(Color.Black, 1);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            g.DrawRectangle(pen1, Rectangle.FromLTRB(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y));
            // Рисуем прямоугольник чёрный пунктиром
        }
        //Виртуальная функция для стирания старых контуров(рисования белого прямоугольника поверх пунктира)
        public override void Hide(Graphics g)
        {
            g.DrawRectangle(new Pen(Color.White, 1), Rectangle.FromLTRB(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y));
        }
    }
}
