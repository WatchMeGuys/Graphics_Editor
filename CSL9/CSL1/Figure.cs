using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Point = System.Drawing.Point;
//NEW
namespace CSL1
{

    [Serializable()]
    public abstract class Figure //Абстрактный класс для фигуры

    {
        protected Point point1, point2;
        protected int BS1;
        protected Color LC1, BC1;
        protected bool IsScrolling = false;
        public bool IsFilling { get; set; }
        [NonSerialized] public Point startPoint, endPoint;//Переменные для новых координат точек
        // переменные для хранения координат точек прямоугольника
        public Figure(Point point1, Point point2, int BS, Color LC, Color BC) //Конструктор класса Figure для инициализации координат точек
        {
            this.point1 = point1;
            this.point2 = point2;
            BS1 = BS; LC1 = LC; BC1 = BC;//присваиваем выбранные значения параметов рисования
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
        public void MouseMove(Point point2) //Функция для изменения второй координаты прямоугольника
        {
            this.point2 = point2;
        }
        protected void ScrollCalibaration(Point ScrollShift)  // Для нормальной прорисовки фигур при прокрутке
        {
            if (!IsScrolling)
            {
                point1.X -= ScrollShift.X;
                point1.Y -= ScrollShift.Y;
                point2.X -= ScrollShift.X;
                point2.Y -= ScrollShift.Y;

                IsScrolling = true;
            }
        }
        //Зададим абстрактные методы класса
        public abstract void Draw(Graphics g, Point ScrollShift);
        public abstract void DrawDash(Graphics g, Point ScrollShift);
        public abstract void Hide(Graphics g);
    }

    [Serializable()]
    public class Rect : Figure // Создадим производный класс для прямоугльника от класса Figure
    {

        public Rect(Point point1, Point point2, int BS, Color LC, Color BC) : base(point1, point2, BS, LC, BC) // Конструктор класс Rect производный от класса Figure
        {

        }
        //Виртуальная функция для рисования конечного сплошного контура
        public override void Draw(Graphics g, Point ScrollShift)
        {
            ScrollCalibaration(ScrollShift);
            norm(ref point1, ref point2); //Применяем нормализацию координат
            Pen P1 = new Pen(LC1, BS1);
            Rectangle r = Rectangle.FromLTRB(point1.X + ScrollShift.X, point1.Y + ScrollShift.Y, point2.X + ScrollShift.X, point2.Y + ScrollShift.Y); //создаем структуру прмоугольника
            SolidBrush SB = new SolidBrush(BC1);//для заливки
            if (IsFilling)
            {
                g.FillRectangle(SB, r);//Заполняет внутреннюю часть прямоугольника, определяемого структурой Rectangle.
            }         
            g.DrawRectangle(P1, r);// - рисование прямоугольника по конечным координатам
        }
        //Виртуальная функция рисования пунктирных контуров прямоугольника
        public override void DrawDash(Graphics g, Point ScrollShift)
        {
            startPoint = point1;
            endPoint = point2;
            //Используем другие переменные, чтобы не изменить начальные координаты
            norm(ref startPoint, ref endPoint);
            Pen P2 = new Pen(LC1, BS1);
            P2.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            Rectangle r = Rectangle.FromLTRB(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
            g.DrawRectangle(P2, r);   // Рисуем прямоугольник чёрный пунктиром
        }
        //Виртуальная функция для стирания старых контуров(рисования белого прямоугольника поверх пунктира)
        public override void Hide(Graphics g)
        {
            Pen P3 = new Pen(Color.White, BS1);
            Rectangle r = Rectangle.FromLTRB(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
            g.DrawRectangle(P3, r);
        }
    }
    //Класс эллипса
    [Serializable]
    public class Ellipse : Figure
    {
        public Ellipse(Point point1, Point point2, int BS, Color LC, Color BC)
           : base(point1, point2, BS, LC, BC)
        { }

        public override void Draw(Graphics g, Point ScrollShift)
        {
            ScrollCalibaration(ScrollShift);
            norm(ref point1, ref point2);
            if (IsFilling)
            {
                g.FillEllipse(new SolidBrush(BC1),
                Rectangle.FromLTRB(point1.X + ScrollShift.X, point1.Y + ScrollShift.Y, point2.X + ScrollShift.X, point2.Y + ScrollShift.Y));
            }
            g.DrawEllipse(new Pen(LC1, BS1),
            Rectangle.FromLTRB(point1.X + ScrollShift.X, point1.Y + ScrollShift.Y, point2.X + ScrollShift.X, point2.Y + ScrollShift.Y));
        }

        public override void DrawDash(Graphics g, Point ScrollShift)
        {
            startPoint = point1;
            endPoint = point2;
            norm(ref startPoint, ref endPoint);

            Pen P2 = new Pen(LC1, BS1)
            {
                DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
            };
            g.DrawEllipse(P2, Rectangle.FromLTRB(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y));
        }

        public override void Hide(Graphics g)
        {
            g.DrawEllipse(new Pen(Color.White, BS1), Rectangle.FromLTRB(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y));
        }
    }
    //Класс для прямой линии
    [Serializable]
    public class StraightLine : Figure
    {
        public StraightLine(Point point1, Point point2, int BS, Color LC, Color BC)
            : base(point1, point2, BS, LC, BC)
        { }

        public override void DrawDash(Graphics g, Point ScrollShift)
        {
            startPoint = point1;
            endPoint = point2;

            Pen pen = new Pen(LC1, BS1)
            {
                DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
            };
            g.DrawLine(pen, startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
        }
        public override void Draw(Graphics g, Point ScrollShift)
        {
            ScrollCalibaration(ScrollShift);
            g.DrawLine(new Pen(LC1, BS1), point1.X + ScrollShift.X, point1.Y + ScrollShift.Y, point2.X + ScrollShift.X, point2.Y + ScrollShift.Y);
        }
        //Стирание предыдущих контуров
        public override void Hide(Graphics g)
        {
            g.DrawLine(new Pen(Color.White, BS1), startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
        }
    }
    [Serializable]
    public class CurveLine : Figure
    {
        List<Point> Points = new List<Point>();

        public CurveLine(Point point1, Point point2, int BS, Color LC, Color BC)
            : base(point1, point2, BS, LC, BC)
        {
            Points.Add(point1);
            Points.Add(point2);
        }
        //Добавление новых точек для кривой
        public void AddPoint(Point NewPoint)
        {
            Points.Add(NewPoint);
        }
        //Пунктир
        public override void DrawDash(Graphics g, Point ScrollShift)
        {
            startPoint = point1;
            endPoint = point2;

            Points.Add(endPoint);
            g.DrawCurve(new Pen(LC1, BS1), Points.ToArray());

        }
        //Рисование сплошной линией
        public override void Draw(Graphics g, Point ScrollShift)
        {
            if (!IsScrolling)
            {
                for (int i = 0; i < Points.Count; i++)
                    Points[i] = new Point(Points[i].X - ScrollShift.X, Points[i].Y - ScrollShift.Y);
                IsScrolling = true;
            }
            //Новый массив для изменения координат точек
            Point[] Points_With_Scroll = new Point[Points.Count];
            for (int i = 0; i < Points.Count; i++)
            {
                Points_With_Scroll[i] = new Point(Points[i].X + ScrollShift.X, Points[i].Y + ScrollShift.Y);
            }
            g.DrawCurve(new Pen(LC1, BS1), Points_With_Scroll);
        }
        //Стирание предыдущих контуров
        public override void Hide(Graphics g)
        {

        }
    }
    [Serializable()]
    public class TextLable : Figure //Окошко для текста
    {
        public TextLable(Point point1, Point point2, int BS, Color LC, Color BC)
            : base(point1, point2, BS, LC, BC)
        {
            editAction = true;
        }
        [NonSerialized()]
        public Form2 f2;
        public Font tFont;
        [NonSerialized()]
        TextBox tb;
        private bool editAction;
        private string text;
        
           
        
        public override void DrawDash( Graphics g,Point ScrollShift)
        {
            startPoint = point1;
            endPoint = point2;
            //Используем другие переменные, чтобы не изменить начальные координаты
            norm(ref startPoint, ref endPoint);
            Pen P2 = new Pen(Color.Black, 1);
            P2.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            Rectangle r = Rectangle.FromLTRB(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
            g.DrawRectangle(P2, r);   // Рисуем прямоугольник чёрный пунктиром
        }
        public override void Draw( Graphics g, Point ScrollShift)
        {
            norm(ref point1, ref point2);
            
                //Создать Текстовое окошко

                tb = new TextBox();
                tb.Parent = f2;
                tb.Location = point1;
                tb.Size = new Size(200,120);
                tb.Multiline = true;
                tb.Font = tFont;
                tb.ForeColor = LC1;
                tb.Text = "hhh";
                tb.KeyDown += textKeyDown;
                tb.Show();
                g.DrawString(text, tb.Font, new SolidBrush(LC1), point1.X, point1.Y);


        }

        private void textKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {                
               tb.Hide();
               text = tb.Text;
               tb.Dispose();
            }
        }

        public override void Hide(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}
