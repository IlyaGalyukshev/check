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
        public MainWindow()
        {
            InitializeComponent();
            FontDialog chooseFont = new FontDialog();
        }

        private void BtnOpenConstructor_Click(object sender, RoutedEventArgs e)
        {
            CertificateConstructor window = new CertificateConstructor();
            window.Height = 700;
            window.Width = 700 / Math.Sqrt(2);
            window.Show();
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
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            if (dlg.ShowDialog() == true) {
                finalPath = dlg.FileName;
                /*реализация сохранения готовых сертификатов*/
            }
        }
    }
}
