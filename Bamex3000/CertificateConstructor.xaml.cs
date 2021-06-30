using System;
using System.Collections.Generic;
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
        double startPosX, startPosY, finPosX, finPosY;

        public CertificateConstructor()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            //this.Background = new ImageBrush(new BitmapImage(new Uri(@"C:\Users\theho\Desktop\bamex3000\Bamex3000\bin\Debug\inew.jpg")));
            //получить содержимое переменной bgPath из файла MainWindow.xaml.cs
        }

        private void WndConstruct_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tbName.Visibility = Visibility.Hidden;
            startPosX = Mouse.GetPosition(wndConstruct).X;
            startPosY = Mouse.GetPosition(wndConstruct).Y;
        }

        private void WndConstruct_MouseUp(object sender, MouseButtonEventArgs e)
        {
            finPosX = Mouse.GetPosition(wndConstruct).X;
            finPosY = Mouse.GetPosition(wndConstruct).Y;
            tbName.Margin = new Thickness(Math.Min(startPosX, finPosX), Math.Min(startPosY, finPosY), 0, 0);
            tbName.Width = Math.Abs(finPosX - startPosX);
            tbName.Height = Math.Abs(finPosY - startPosY);
            tbName.Visibility = Visibility.Visible;
        }
    }
}
