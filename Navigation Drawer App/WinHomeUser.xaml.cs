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
using SysShopDAO.Implementation;
using SysShopDAO.Model;
using System.Data;
using MySql.Data.MySqlClient;
using Navigation_Drawer_App;
namespace Navigation_Drawer_App
{
    /// <summary>
    /// Lógica de interacción para WinHomeUser.xaml
    /// </summary>
    public partial class WinHomeUser : Window
    {
        public WinHomeUser()
        {
            InitializeComponent();
        }
        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            if (Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_contacts.Visibility = Visibility.Collapsed;
                
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
              
            }
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            lblIdentify.Content = "Bienvenido " + SessionClass.SessionFullName + " rol: " + SessionClass.SessionRol;

        }

        private void CerrarUsr_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void item_cambiar_Selected(object sender, RoutedEventArgs e)
        {
            WinPassword contra = new WinPassword();
            contra.Show();
        }

        private void item_menu_Selected(object sender, RoutedEventArgs e)
        {
            MainWindow menu = new MainWindow();
            menu.Show();
        }
    }
}
