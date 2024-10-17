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
using SysShopDAO.Implementation;
using SysShopDAO.Model;

namespace Navigation_Drawer_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
           // Set tooltip visibility

            if(Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_contacts.Visibility = Visibility.Collapsed;
                tt_maps.Visibility = Visibility.Collapsed;
               
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
                tt_maps.Visibility = Visibility.Visible;
               
            }
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 0.3;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void item_Contra_Selected(object sender, RoutedEventArgs e)
        {
            WinPassword contra = new WinPassword();
            contra.Show();
        }

        private void item_Gestion_Selected(object sender, RoutedEventArgs e)
        {
            WinAdmin gestion = new WinAdmin();
            gestion.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblIdentify.Content = "Bienvenido " + SessionClass.SessionFullName + " rol: " + SessionClass.SessionRol;
        }

        private void item_menu_Selected(object sender, RoutedEventArgs e)
        {
            MainWindow admenu = new MainWindow();
            admenu.Show();

        }
    }
}
