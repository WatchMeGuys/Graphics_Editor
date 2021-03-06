using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO; //сериализация
using System.Runtime.Serialization.Formatters.Binary; //сериализация
using System.Windows.Forms;

namespace CSL1
{
    [Serializable()]
    public partial class Form1 : Form
    {
        // текущие настройки  цвета линии, цвета фона фигуры и толщины линии
        public int brushSize; //толщина линии
        public Color colorLine, colorFon; //цвет линии и фона
        int keyopen;
        public bool IsFigureFilling { get; set; } // флаг заливки
        public int numFigure = 3; // номер выбранной фигуры
        Form2 f2;
        public Form1()
        {
            InitializeComponent();
            colorLine = Color.Black;//по умолчанию цвет линии - чёрный    
            colorFon = Color.White;//по умолчанию цвет фона - белый    
            brushSize = 1;//по умолчанию толщина линии - 1
            IsFigureFilling = false;
            заливкаToolStripMenuItem.Checked = false; //изначальное состояние кнопки заливки
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void NewtoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
                Form Child = new Form2();
                Child.MdiParent = this;
                Child.Text = "Рисунок " + this.MdiChildren.Length.ToString();
                Child.Show();
            
        }
        public void proverkaOpen()
        {
            keyopen = MdiChildren.Length; // открыта ли в настоящее время дочерняя форма?
            if (keyopen == 0)
            {
                SavetoolStripMenuItem2.Enabled = false; //блокировка "Сохранить"
                SaveAsStripMenuItem3.Enabled = false; //блокировка "Сохранить как"
            }

            if (keyopen > 0)
            {
                SavetoolStripMenuItem2.Enabled = true; //разблокировка  "Сохранить"
                SaveAsStripMenuItem3.Enabled = true; //разблокировка  "Сохранить как"
                f2 = (Form2)ActiveMdiChild; // возвращает объект Form, который представляет дочернее окно текущего активного интерфейса
                if (f2.flagIzmen == false) //если дочерняя форма не изменялась с момента открытия
                {
                    SavetoolStripMenuItem2.Enabled = false; //блокировка меню "Сохранить"          
                }
            }
        }
        public void saveFile(Form2 f2) // функция сохранения файла
        {
            string fileName;
            BinaryFormatter formatter1 = new BinaryFormatter(); //Сохранение объекта obj некоторого класса X в файле с именем fileName 
            if (f2.fileName == null) //Имя файла, выбранное в диалоговом окне файла - если раньше этот файл не был сохранен
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = Environment.CurrentDirectory; //При инициализации файловых диалогов указываем в качестве стартового каталога текущий каталог программы
                saveFileDialog1.Filter = "Графический редактор(*.alx)|*.alx|All files (*.*)|*.*"; //Задаем текущую строку фильтра имен файлов
                saveFileDialog1.FilterIndex = 1; //Задаем индекс фильтра, выбранного в настоящий момент в диалоговом окне файла.
                if (saveFileDialog1.ShowDialog() == DialogResult.OK) //если нажали OK
                {
                    fileName = saveFileDialog1.FileName;
                    Stream myStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                    formatter1.Serialize(myStream, f2.figures);
                    myStream.Close();
                    f2.flagIzmen = false;
                    f2.fileName = fileName;
                    f2.Text = Path.GetFileName(saveFileDialog1.FileName);
                }
            }
            else // если раньше этот файл был сохранен
            {
                fileName = f2.fileName;
                Stream myStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter1.Serialize(myStream, f2.figures);
                myStream.Close();
                f2.flagIzmen = false;
            }

        }

        private void LineColToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog myDialog = new ColorDialog();  //открытие диалогового окна                              
            DialogResult result = myDialog.ShowDialog();
            if (result == DialogResult.OK) colorLine = myDialog.Color; //проверка и установление выбранного цвета с помощью свойства Color класса ColorDialog.  
            statusBar1.Refresh();
        }

        private void BackColToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog myDialog = new ColorDialog(); //открытие диалогового окна 
            DialogResult result = myDialog.ShowDialog();
            if (result == DialogResult.OK) colorFon = myDialog.Color;    // проверка и установление выбранного цвета с помощью свойства Color класса ColorDialog. 
            statusBar1.Refresh();
        }

        private void BrsizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BrSize myDialog = new BrSize(); //открытие диалогового окна                           
            DialogResult result = myDialog.ShowDialog(this);
            if (result == DialogResult.OK) brushSize = myDialog.vybor;  //проверка и установление выбранной толщины линии 
            statusBar1.Refresh();
        }

        private void SavetoolStripMenuItem2_Click(object sender, EventArgs e)
        {
            saveFile((Form2)ActiveMdiChild);
        }

        private void OpenStripMenuItem_Click(object sender, EventArgs e)
        {

            {
                string fileName;
                OpenFileDialog OpenPic = new OpenFileDialog();
                OpenPic.InitialDirectory = Environment.CurrentDirectory;
                OpenPic.Filter = "Графический редактор(*.alx)|*.alx|All files (*.*)|*.*";
                OpenPic.FilterIndex = 1;
                if (OpenPic.ShowDialog() == DialogResult.OK)
                {
                    fileName = OpenPic.FileName;
                    BinaryFormatter formatter1 = new BinaryFormatter(); // Восстановление сохранённого объекта из файла:                           
                    Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    List<Figure> array = (List<Figure>)formatter1.Deserialize(stream);
                    stream.Close();
                    f2 = new Form2();
                    f2.MdiParent = this;
                    f2.fileName = fileName;
                    f2.Text = OpenPic.SafeFileName;
                    f2.figures = array;
                    f2.Show();
                    f2.flagIzmen = false;
                }
            }
        }

        private void SaveAsStripMenuItem3_Click(object sender, EventArgs e)
        {
            f2 = (Form2)ActiveMdiChild;
            f2.fileName = null;
            saveFile((Form2)ActiveMdiChild);
        }

        private void размерРисункаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SizeChoice MySize = new SizeChoice();
            DialogResult result = MySize.ShowDialog(this);
            if (result == DialogResult.OK) //
            {
                Form Child = new Form2();
                Child.MdiParent = this;
                Child.Text = "Рисунок " + this.MdiChildren.Length.ToString();
                Child.Height = MySize.height; //
                Child.Width = MySize.width; //
                Child.Show();
            }
        }

        private void прямоугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //для кнопок
            toolStripButton8.Checked = false;
            toolStripButton9.Checked = false;
            toolStripButton10.Checked = false;
            toolStripButton11.Checked = true;

            эллипсToolStripMenuItem.Checked = false;
            прямаяЛинияToolStripMenuItem.Checked = false;
            криваяToolStripMenuItem.Checked = false;
            прямоугольникToolStripMenuItem.Checked = true;
            numFigure = 0;
        }

        private void эллипсToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //для кнопок
            toolStripButton8.Checked = false;
            toolStripButton9.Checked = false;
            toolStripButton10.Checked = true;
            toolStripButton11.Checked = false;

            эллипсToolStripMenuItem.Checked = true;
            прямаяЛинияToolStripMenuItem.Checked = false;
            криваяToolStripMenuItem.Checked = false;
            прямоугольникToolStripMenuItem.Checked = false;
            numFigure = 1;
        }

        private void прямаяЛинияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //для кнопок
            toolStripButton8.Checked = true;
            toolStripButton9.Checked = false;
            toolStripButton10.Checked = false;
            toolStripButton11.Checked = false;

            эллипсToolStripMenuItem.Checked = false;
            прямаяЛинияToolStripMenuItem.Checked = true;
            криваяToolStripMenuItem.Checked = false;
            прямоугольникToolStripMenuItem.Checked = false;
            numFigure = 2;
        }

        private void криваяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //для кнопок
            toolStripButton8.Checked = false;
            toolStripButton9.Checked = true;
            toolStripButton10.Checked = false;
            toolStripButton11.Checked = false;

            эллипсToolStripMenuItem.Checked = false;
            прямаяЛинияToolStripMenuItem.Checked = false;
            криваяToolStripMenuItem.Checked = true;
            прямоугольникToolStripMenuItem.Checked = false;
            numFigure = 3;
        }

        private void заливкаToolStripMenuItem_Click(object sender, EventArgs e)
        {          
                if (заливкаToolStripMenuItem.Checked || toolStripButton12.Checked)
                    IsFigureFilling = true; //флаг заливки для функции в Form2-> Class Figure
                else IsFigureFilling = false;
        }

        private void окноToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            proverkaOpen();
        }

        private void statusBar1_DrawItem(object sender, StatusBarDrawItemEventArgs sbdevent)
        {
            Graphics g = statusBar1.CreateGraphics();        //Объект для рисования прямоугольников в строке состояния

            //вывод толщины линии
            statusBarPanelPenSize.Text = brushSize.ToString();

            //Подписи 
            g.DrawString("Цвет 1:", new Font(Font, FontStyle.Regular),
                new SolidBrush(Color.Black), statusBarPanelCursor.Width + 5, 7);
            g.DrawString("Цвет 2:", new Font(Font, FontStyle.Regular),
             new SolidBrush(Color.Black), statusBarPanelCursor.Width + 5 + statusBarPanelLineColor.Width, 7);

            int RectDist = 50; //Перемення для расположения прямоугольников относитель начала панели

            //Рисование индикаторов цвета
            g.FillRectangle(new SolidBrush(colorLine), Rectangle.FromLTRB(statusBar1.Panels[0].Width + RectDist, 2,
                statusBar1.Panels[0].Width + RectDist + 20, 22));
            g.DrawRectangle(new Pen(Color.Black, 1), Rectangle.FromLTRB(statusBar1.Panels[0].Width + RectDist, 2,
                statusBar1.Panels[0].Width + RectDist + 20, 22));

            g.FillRectangle(new SolidBrush(colorFon),
                Rectangle.FromLTRB(statusBar1.Panels[0].Width + statusBar1.Panels[1].Width + RectDist, 2,
                statusBar1.Panels[0].Width + statusBar1.Panels[1].Width + RectDist + 20, 22));
            g.DrawRectangle(new Pen(Color.Black, 1),
                Rectangle.FromLTRB(statusBar1.Panels[0].Width + statusBar1.Panels[1].Width + RectDist, 2,
                statusBar1.Panels[0].Width + statusBar1.Panels[1].Width + RectDist + 20, 22));           
        }

        public StatusBar GetStatusBar()
        {
            return statusBar1;
        }

    }
}
