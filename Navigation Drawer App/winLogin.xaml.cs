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
using SysShopDAO.Model;
using SysShopDAO.Implementation;
using System.Data;

namespace Navigation_Drawer_App
{
    /// <summary>
    /// Lógica de interacción para winLogin.xaml
    /// </summary>
    public partial class winLogin : Window
    {
        public winLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               UsuarioImpl usuarioImpl = new UsuarioImpl();
               DataTable table =  usuarioImpl.Login(txtNombreUsuario.Text,txtContraseña.Password);
                if (table.Rows.Count > 0)
                {
                    
                    //valores de las variables de sesion
                    SessionClass.SessionID = byte.Parse(table.Rows[0][0].ToString());
                    SessionClass.SessionFullName = table.Rows[0][1].ToString();
                    SessionClass.SessionRol = table.Rows[0][2].ToString();
                    SessionClass.SessionUserName = table.Rows[0][3].ToString();


                    if (SessionClass.SessionRol == "admin")
                    {
                        MainWindow admin = new MainWindow();
                        admin.Show();
                        this.Visibility = Visibility.Hidden;
                    }
                    else if(SessionClass.SessionRol == "cajero")
                    {
                        WinHomeUser user = new WinHomeUser();
                        user.Show();
                        this.Visibility = Visibility.Hidden;
                    }

                    
                }
                else
                {
                    lblInfo.Content = "Nombre de usuario y/o Contraseña incorrectos";
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
