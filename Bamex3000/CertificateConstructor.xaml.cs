using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace Bamex3000
{
    public partial class CertificateConstructor : Window
    {
        private double startPosX, startPosY, finPosX, finPosY;
        private int idText;

        private List<TextBox> areas = new List<TextBox>();
        private List<CPosPoint> posText = new List<CPosPoint>();//класс облостей 

        private string urlImage;//url сертификата



        public CertificateConstructor(string s_urlImage)
        {
            InitializeComponent();
            idText = 0;
            this.ResizeMode = ResizeMode.NoResize;
            urlImage = s_urlImage;
            _setImage();
            //this.Background = new ImageBrush(new BitmapImage(new Uri(@"C:\Users\theho\Desktop\bamex3000\Bamex3000\bin\Debug\inew.jpg")));
            //получить содержимое переменной bgPath из файла MainWindow.xaml.cs
        }

        private void WndConstruct_MouseDown(object sender, MouseButtonEventArgs e)
        {
            startPosX = Mouse.GetPosition(wndConstruct).X;
            startPosY = Mouse.GetPosition(wndConstruct).Y;
        }

        private void WndConstruct_MouseUp(object sender, MouseButtonEventArgs e)
        {
            finPosX = Mouse.GetPosition(wndConstruct).X;
            finPosY = Mouse.GetPosition(wndConstruct).Y;
            if (_proverkaXY())
            {
                TextBox tb = new TextBox()
                {
                    Name = "tbName" + idText,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Height = Math.Abs(finPosY - startPosY),
                    Margin = new Thickness(Math.Min(startPosX, finPosX), Math.Min(startPosY, finPosY), 0, 0),
                    TextWrapping = TextWrapping.Wrap,
                    Text = idText.ToString(),
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = Math.Abs(finPosX - startPosX),
                    TextAlignment = TextAlignment.Center,
                    FontSize = 15,
                    

                };
                idText++;
                areasGrid.Children.Add(tb);
                areas.Add(tb);

                posText.Add(new CPosPoint(startPosX, startPosY, finPosX, finPosY, "default"));//заполнение полей класса
            }
        }

        private void _save()//сохранит настройки
        {
            bool result = false;
            string text = "";
            string finalPath = "";
            int i = 0;
            foreach (var info in posText)
            {
                if (info.name.Equals(""))
                {
                    text = null;
                    result = false;
                    break;
                }
                else
                {
                    text += "" + info.startPosX + "|" + info.startPosY + "|" + info.finPosX + "|" + info.finPosY + "|" + info.name + "|" + i  + "\n";
                    i++;
                    result = true;
                }
            }
            if (result)
            {
                try
                {
                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                    if (dlg.ShowDialog() == true)
                    {
                        finalPath = dlg.FileName;
                        /*реализация сохранения готовых сертификатов*/
                    }
                    using (StreamWriter sw = new StreamWriter(finalPath+".txt", false, System.Text.Encoding.Default))
                    {
                        sw.WriteLine(text);
                    }
                    MessageBox.Show("все успешно сохранилось");
                }
                catch (Exception eq)
                {
                    Console.WriteLine(eq.Message);
                }
            }
            else
            {
                MessageBox.Show("скорее всего у вас не все точки текста правильно заполнены(или не заполнены).Мб Вы не загрузили изображение.");
            }
        }

        private void BtnSavePath_Click(object sender, RoutedEventArgs e)
        {
            _save();
        }

        private void _setImage()//загрузка изображения по данному url
        {
            ImageCertificate.Source = new BitmapImage(new Uri(urlImage));
            
            _changeSizeCC(ImageCertificate.Source.Width, ImageCertificate.Source.Height);
        }
        private void _changeSizeCC(double width, double height)//установка размера окна
        {
            //wndConstruct.Width = width + 180;
            
            if (height < 600)
            {
               // wndConstruct.Height = 600;
            }
            else
            {
                //wndConstruct.Height = height + 50;
            }
        }

        private bool _proverkaXY()//ограничение на выделение(можно тольок на изображении )
        {
            if (startPosX > ImageCertificate.ActualWidth || startPosY > ImageCertificate.ActualHeight)
            {
                MessageBox.Show("выделять поля можно только на шаблоне");
                return false;
            }
            if (finPosX > ImageCertificate.ActualWidth) finPosX = ImageCertificate.ActualWidth;
            if (finPosY > ImageCertificate.ActualHeight) finPosY = ImageCertificate.ActualHeight;

            return true;
        }

       
    }
}
