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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DolbitManager.Views;
using DolbitManager.Models;
using DolbitManager.ViewModels;

namespace DolbitManager.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GenresViewModel gv = new GenresViewModel();


        LoginWindow loginWindow;
        // Recieving user
        private User authUser;

        public MainWindow()
        {
            InitializeComponent();
            loginWindow = new LoginWindow(this);
            loginWindow.ShowDialog();
            authUser = loginWindow.authUser;
            GenreCB.DataContext = gv;
        }
    }
}
