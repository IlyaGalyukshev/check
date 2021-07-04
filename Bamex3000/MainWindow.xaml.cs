using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;


namespace Bamex3000
{
    public partial class MainWindow : Window
    {
        public string bgPath, dbPath, finalPath;

        private List<CPosPoint> posText = new List<CPosPoint>();
        private List<string[]> userInfo = new List<string[]>();

      
        private string font;
        private int textSize;
        
        Decliner text = new Decliner();
        System.Drawing.Color color;
        public MainWindow()
        {
            InitializeComponent();
            FontDialog chooseFont = new FontDialog();

            font = "Verdana";
            textSize = 13;
            color = System.Drawing.Color.Black;
          
        }

        private void BtnOpenConstructor_Click(object sender, RoutedEventArgs e)
        {
            if (bgPath != null)
            {
                CertificateConstructor window = new CertificateConstructor(bgPath);
                window.Show();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("вы не выбрали фон"); 
            }
        }

        private void BtnOpenBG_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            dlg.Multiselect = false;
            if (dlg.ShowDialog() == true)
            {
                bgPath = dlg.FileName;
            }
        }

        private void BtnOpenDataBase_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Multiselect = false;
            if (dlg.ShowDialog() == true)
            {
                dbPath = dlg.FileName;
                _readTxtUser(dbPath);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            //if (dlg.ShowDialog() == true)
            //{
            //    finalPath = dlg.FileName;
              _save();
            //    /*реализация сохранения готовых сертификатов*/
            //}
         
           
        }

        private void CertSettings_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Multiselect = false;
            if (dlg.ShowDialog() == true)
            {
                dbPath = dlg.FileName;
                _readTxtCertSettings(dbPath);
            }
        }

        private void _readTxtUser(string url)
        {
            try
            {
                using (StreamReader sr = new StreamReader(url, System.Text.Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] words = line.Split(new char[] { '|' });
                        userInfo.Add(words);

                    }
                }
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show("что-то не так с файлом загрузки(попробуейте убрать все пробелы в конце)");
            }
        }
        private void _readTxtCertSettings(string url)
        {
            //StreamReader sr = new StreamReader(url);
            //while (!sr.EndOfStream)
            //{
            //    string line = sr.ReadLine();
            //    string[] words = line.Split('|');
            //    CPosPoint cpp = new CPosPoint(double.Parse(words[0]), double.Parse(words[1]), double.Parse(words[2]), double.Parse(words[3]), words[5]);
            //    posText.Add(cpp);
            //    System.Windows.Forms.MessageBox.Show("!!!!");
            //}
            using (StreamReader sr = new StreamReader(url, System.Text.Encoding.Default))
            {
                try
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {                        
                        string[] words = line.Split(new char[] { '|' });
                        if (words.Length > 1)
                        {
                            var cpp = new CPosPoint(double.Parse(words[0]), double.Parse(words[1]), double.Parse(words[2]), double.Parse(words[3]), words[5]);
                            posText.Add(cpp);
                            System.Windows.Forms.MessageBox.Show(posText.Count.ToString() + "+");
                        }
                    }
                }
                catch (Exception ee)
                {
                    System.Windows.Forms.MessageBox.Show("что-то не так с файлом загрузки(попробуейте убрать все пробелы в конце)");
                }
            }
        }
        private void _save()
        {
            int i = 0;
            foreach (var user in userInfo)
            {
                if (bgPath == null)
                {
                    System.Windows.Forms.MessageBox.Show("Выберете фон");
                    break;
                }
                System.Drawing.Image img = Bitmap.FromFile(bgPath); //путь к картинке
                foreach (var point in posText)
                {
                    Graphics g = Graphics.FromImage(img);
                    switch (int.Parse(point.name))
                    {
                        case 0:
                            if (user.Length >= 0)
                            {

                                //text = Decliner.Decline(user[0],"-","-");
                                g.DrawString(text.Decline(user[0], "-", "-")[0], new Font(font, (float)textSize),  //текст на картинке, шрифт и его размер
                                 new SolidBrush(color), (float)point.getX(), (float)point.getY()); //месторасположения текста


                            }

                            break;
                        case 1:
                            if (user.Length >= 1)
                            {
                                g.DrawString(text.Decline("-", user[1], "-")[1], new Font(font, (float)textSize), //текст на картинке, шрифт и его размер
                                new SolidBrush(color), (float)point.getX(), (float)point.getY()); //месторасположения текста

                            }
                            break;
                        case 2:
                            if (user.Length >= 2)
                            {
                                g.DrawString(text.Decline("-", "-", user[2])[2], new Font(font, (float)textSize), //текст на картинке, шрифт и его размер
                                 new SolidBrush(color), (float)point.getX(), (float)point.getY()); //месторасположения текста

                            }
                            break;
                        case 3:
                            if (user.Length >= 3)
                            {
                                g.DrawString(user[3], new Font(font, (float)textSize), //текст на картинке, шрифт и его размер
                                new SolidBrush(color), (float)point.getX(), (float)point.getY()); //месторасположения текста

                            }
                            break;
                        case 4:
                            if (user.Length >= 4)
                            {
                                g.DrawString(user[4], new Font(font, (float)textSize), //текст на картинке, шрифт и его размер
                                new SolidBrush(color), (float)point.getX(), (float)point.getY()); //месторасположения текста

                            }
                            break;
                        case 5:
                            if (user.Length >= 5)
                            {
                                g.DrawString(user[5], new Font(font, (float)textSize), //текст на картинке, шрифт и его размер
                                new SolidBrush(color), (float)point.getX(), (float)point.getY()); //месторасположения текста

                            }
                            break;
                        case 6:
                            if (user.Length >= 6)
                            {
                                g.DrawString(user[6], new Font(font, (float)textSize), //текст на картинке, шрифт и его размер
                                new SolidBrush(color), (float)point.getX(), (float)point.getY()); //месторасположения текста

                            }
                            break;
                        case 7:
                            if (user.Length >= 7)
                            {
                                g.DrawString(user[7], new Font(font, (float)textSize), //текст на картинке, шрифт и его размер
                                new SolidBrush(color), (float)point.getX(), (float)point.getY()); //месторасположения текста

                            }
                            break;
                        case 8:
                            if (user.Length >= 8)
                            {
                                g.DrawString(user[8], new Font(font, (float)textSize), //текст на картинке, шрифт и его размер
                                new SolidBrush(color), (float)point.getX(), (float)point.getY()); //месторасположения текста

                            }
                            break;

                        default:
                            Console.WriteLine("Default case");
                            break;

                    }
                }

                img.Save(@"certificate" + i + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg); //путь и имя сохранения файла
                i++;
            }
        }
    }
}
