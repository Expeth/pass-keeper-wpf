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

namespace PassKeeper_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window, IWindow
    {
        public MainWindowView()
        {
            InitializeComponent();
        }

        public void CloseWindow()
        {
            this.Close();
        }

        public void MaximizeWindow()
        {
            this.WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        public void MinimizeWindow()
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
