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

namespace Navigation_Drawer_App
{
    /// <summary>
    /// Lógica de interacción para WinPassword.xaml
    /// </summary>
    public partial class WinPassword : Window
    {
        public WinPassword()
        {
            InitializeComponent();
        }
        SessionImpl sessionImpl = new SessionImpl();
        UsuarioImpl usuarioImpl = new UsuarioImpl();

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
            
                DataTable table = usuarioImpl.VerificarContraseñaActual(SessionClass.SessionUserName, txtActual.Password);

                if (table.Rows.Count > 0)
                {
                    // Validar que las nuevas contraseñas coincidan
                    if (txtNuevo.Password == txtConfirm.Password)
                    {
                        // Verificar que la nueva contraseña cumpla con los requisitos de seguridad
                        string nuevaContrasenia = txtNuevo.Password;

                        if (usuarioImpl.validarContraseña(nuevaContrasenia))
                        {
                            // Cambiar la contraseña en la base de datos
                           if( usuarioImpl.CambiarContrasenia(SessionClass.SessionID, nuevaContrasenia) > 0);
                            {
                                MessageBox.Show("Contraseña cambiada con éxito. Por favor, inicie sesión nuevamente.");
                                sessionImpl.CerrarSesion();
                                winLogin login = new winLogin();
                                login.Show();
                                this.Close();
                               
                            }
                        }
                        else
                        {
                            MessageBox.Show("La nueva contraseña no cumple con los requisitos de seguridad.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Las nuevas contraseñas no coinciden.");
                    }
                }
                else
                {
                    MessageBox.Show("La contraseña actual es incorrecta.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      
       
        private void CerrarContra_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }
        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {

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

        private void item_home_Selected(object sender, RoutedEventArgs e)
        {
            WinHomeUser user = new WinHomeUser();
            user.Show();
        }

        private void item_Cambiar_Selected(object sender, RoutedEventArgs e)
        {
            WinPassword contra = new WinPassword();
            contra.Show();
        }

        private void item_Cerrar_Selected(object sender, RoutedEventArgs e)
        {
            
    
        }
    }
}
