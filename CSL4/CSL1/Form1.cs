using System;
using System.Collections.Generic;
using System.IO; //сериализация
using System.Runtime.Serialization.Formatters.Binary; //сериализация
using System.Windows.Forms;

namespace CSL1
{
    [Serializable()]
    public partial class Form1 : Form
    {

        int keyopen;
        Form2 f2;
        public Form1()
        {
            InitializeComponent();
            //инициализация переменных
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void proverkaOpen()
        {
            keyopen = MdiChildren.Length; // открыта ли в настоящее время дочерняя форма?
            if (keyopen == 0)
            {
                SaveToolStripMenuItem.Enabled = false; //блокировка "Сохранить"
                SaveAsToolStripMenuItem.Enabled = false; //блокировка "Сохранить как"
            }

            if (keyopen > 0)
            {
                SaveToolStripMenuItem.Enabled = true; //разблокировка  "Сохранить"
                SaveAsToolStripMenuItem.Enabled = true; //разблокировка  "Сохранить как"
                f2 = (Form2)ActiveMdiChild; // возвращает объект Form, который представляет дочернее окно текущего активного интерфейса
                if (f2.flagIzmen == false) //если дочерняя форма не изменялась с момента открытия
                {
                    SaveToolStripMenuItem.Enabled = false; //блокировка меню "Сохранить"          
                }
            }
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Child = new Form2();
            Child.MdiParent = this;
            Child.Text = "Рисунок " + this.MdiChildren.Length.ToString();
            Child.Show();

        }

       

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile((Form2)ActiveMdiChild);
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f2 = (Form2)ActiveMdiChild;
            f2.fileName = null;
            saveFile((Form2)ActiveMdiChild);
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void windowToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            proverkaOpen();
        }
    }
}
