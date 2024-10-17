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
    /// Lógica de interacción para WinAdmin.xaml
    /// </summary>
    public partial class WinAdmin : Window
    {
        byte op = 0;
        UsuarioImpl usuario;
        Usuario t;
        public WinAdmin()
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
                tt_maps.Visibility = Visibility.Collapsed;
              
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
                tt_maps.Visibility = Visibility.Visible;
             
            }
        }

      

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


      
        void Select()

        {
            try
            {
                usuario = new UsuarioImpl();
                dgDatos.ItemsSource = null;
                dgDatos.ItemsSource = usuario.Select().DefaultView;
                dgDatos.Columns[0].Visibility = Visibility.Collapsed;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        void Habilitar()
        {
            btnInsertar.IsEnabled = false;
            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;


            btnGuardar.IsEnabled = true;
            btnCancelar.IsEnabled = true;

            txtNombres.IsEnabled = true;
            txtCi.IsEnabled = true;
            txtFechaNacimiento.IsEnabled = true;
            txtPrimerApellido.IsEnabled = true;
            txtSegundoApellido.IsEnabled = true;
            txtRol.IsEnabled = true;
            txtSexo.IsEnabled = true;
            txtId.IsEnabled = true;

            txtNombres.Focus();
        }
        void Deshabilitar()
        {
            btnInsertar.IsEnabled = true;
            btnModificar.IsEnabled = true;
            btnEliminar.IsEnabled = true;


            btnGuardar.IsEnabled = false;
            btnCancelar.IsEnabled = false;

            txtNombres.IsEnabled = false;
            txtCi.IsEnabled = false;
            txtFechaNacimiento.IsEnabled = false;
            txtPrimerApellido.IsEnabled = false;
            txtSegundoApellido.IsEnabled = false;
            txtRol.IsEnabled = false;
            txtSexo.IsEnabled = false;
            txtId.IsEnabled = false;



            txtNombres.Text = "";
            txtPrimerApellido.Text = "";
            txtSegundoApellido.Text = "";
            txtSexo.Text = "";
            txtUsuario.Text = "";
            txtContra.Password = "";
            txtCi.Text = "";
            txtFechaNacimiento.Text = "";
            txtRol.Text = "";
            txtId.Text = "";
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Select();
        }
        private void btnInsertar_Click(object sender, RoutedEventArgs e)
        {
            Habilitar();
            this.op = 1;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Habilitar();
            this.op = 2;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

            if (t != null)
            {
                if (MessageBox.Show("¿Está seguro de eliminar el registro?", "Eliminar",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    try
                    {
                        usuario = new UsuarioImpl();
                        if (usuario.Delete(t) > 0)
                        {
                            MessageBox.Show("Registro eliminado");
                            Select();
                        }
                        else
                        {
                            MessageBox.Show("No se eliminaron registros");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro");
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            switch (this.op)
            {
                case 1:
                    try
                    {
                        usuario = new UsuarioImpl();

                        // Generar el nombre de usuario automáticamente
                        string nombreUsuario = usuario.generarUsuario(txtNombres.Text, txtPrimerApellido.Text, txtSegundoApellido.Text);

                        // Verificar que el nombre de usuario no exista en la base de datos
                        while (usuario.ExisteNombreUsuario(nombreUsuario))
                        {
                            // Si el nombre de usuario ya existe, añadir un número al final para hacerlo único
                            Random rnd = new Random();
                            int randomNumber = rnd.Next(1, 1000); // Número aleatorio para hacer único el nombre de usuario
                            nombreUsuario = usuario.generarUsuario(txtNombres.Text, txtPrimerApellido.Text, txtSegundoApellido.Text) + randomNumber;
                        }

                        // Generar una contraseña inicial automáticamente
                        string contraseniaInicial = usuario.generarContra();

                        // Creación de un nuevo objeto Usuario con todos los campos
                        t = new Usuario(
                            txtCi.Text,
                            txtNombres.Text,
                            txtPrimerApellido.Text,
                            txtSegundoApellido.Text,
                            DateTime.Parse(txtFechaNacimiento.Text),
                            char.Parse(txtSexo.Text),
                            txtRol.Text,
                            nombreUsuario,  // Nombre de usuario generado automáticamente
                            contraseniaInicial,
                            byte.Parse(txtId.Text)// Contraseña generada automáticamente
                        );

                        int idGenerado = usuario.Insert(t); // Aquí llamas al método Insert

                        if (idGenerado > 0)
                        {
                            MessageBox.Show($"Registro insertado con éxito. ID del usuario: {idGenerado}");
                            Select(); // Actualiza la vista después de la inserción
                            Deshabilitar();
                        }
                        else
                        {
                            MessageBox.Show("No se insertaron registros");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;


                case 2:
                    if (t != null)
                    {
                        t.Ci = txtCi.Text;
                        t.Nombres = txtNombres.Text;
                        t.PrimerApellido = txtPrimerApellido.Text;
                        t.SegundoApellido = txtSegundoApellido.Text;
                        t.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                        t.Sexo = char.Parse(txtSexo.Text);
                        t.Rol = txtRol.Text;


                        try
                        {
                            usuario = new UsuarioImpl();
                            if (usuario.Update(t) > 0)
                            {
                                MessageBox.Show("Registro modificado con éxito");
                                Select();
                               // Deshabilitar();
                            }
                            else
                            {
                                MessageBox.Show("No se modificaron registros");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    break;
            }




        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Deshabilitar();
        }

    
        private void CerrarAdm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
   
        }

        private void dgDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDatos.SelectedItem != null && dgDatos.Items.Count > 0)
            {
                // Castear para obtener la fila seleccionada
                DataRowView d = (DataRowView)dgDatos.SelectedItem;
                // Obtener el ID del usuario
                byte id = byte.Parse(d.Row.ItemArray[0].ToString()); // Aquí está el ID
                t = null;

                try
                {
                    usuario = new UsuarioImpl();

                    // Obtener el usuario por su ID
                    t = usuario.Get(id);

                    if (t != null)
                    {
                        // Asignar los datos obtenidos a los controles (TextBox) en tu ventana WPF
                        txtCi.Text = t.Ci;  // CI del usuario
                        txtNombres.Text = t.Nombres;  // Nombres del usuario
                        txtPrimerApellido.Text = t.PrimerApellido;  // Primer Apellido
                        txtSegundoApellido.Text = t.SegundoApellido;  // Segundo Apellido
                        txtFechaNacimiento.Text = t.FechaNacimiento.ToShortDateString();  // Fecha de Nacimiento
                        txtSexo.Text = t.Sexo.ToString();  // Sexo (asumimos que es char)
                        txtRol.Text = t.Rol;  // Rol
                        txtUsuario.Text = t.NombreUsuario;  
                        txtContra.Password = t.Contrasenia;
                        txtId.Text = t.idUsuario.ToString();

                       
                    }
                }
                catch (Exception ex)
                {
                  
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void item_contra_Selected(object sender, RoutedEventArgs e)
        {
            WinPassword contra = new WinPassword();
            contra.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void item_menu_Selected(object sender, RoutedEventArgs e)
        {
            MainWindow menu = new MainWindow();
            menu.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
           
        }

        private void item_gestion_Selected(object sender, RoutedEventArgs e)
        {
            WinAdmin gestion = new WinAdmin();
            gestion.Show();
        }
    }
}
