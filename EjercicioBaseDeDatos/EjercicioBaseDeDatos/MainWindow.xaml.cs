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
using System.Data.OleDb;
using System.Data;

namespace EjercicioBaseDeDatos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OleDbConnection con;
        DataTable dt;
        public MainWindow()
        {
            InitializeComponent();
            con = new OleDbConnection();
            con.ConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\AlumnosBD.mdb";
            MostrarDatos();
        }
        private void MostrarDatos()
        {
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            cmd.Connection = con;
             cmd.CommandText = "Select * form Progra";
                OleDbDataAdapter da = new OleDbDataAdapter();
                dt = new DataTable();
                da.Fill(dt);
                gvDatos.ItemsSource = dt.AsDataView();
                if (dt.Rows.Count > 0)
                {
                    lbContenido.Visibility = System.Windows.Visibility.Hidden;
                    gvDatos.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    lbContenido.Visibility = System.Windows.Visibility.Visible;
                    gvDatos.Visibility = System.Windows.Visibility.Hidden;
                }
       
        }
        private void LimpiarTodo()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            cbGenero.SelectedIndex = 0;
            txtTelefono.Text = "";
            btnNuevo.Content = "Nuevo";
            txtId.IsEnabled = true;
        }
        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            cmd.Connection = con;
            if (txtId.Text!="")
            {
                if (cbGenero.Text!= "Selecciona Genero")
                {
                    cmd.CommandText = "insert into Progra(Id,Nombre,Genero,Telefono,Direccion)" +
                        "Values(" + txtId.Text + ",'" + txtNombre.Text + ",'" + cbGenero.Text + ",'" + txtTelefono.Text + ",'" + txtDireccion + ")'";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Alumnos agregado correcto...");
                    LimpiarTodo();
                }
                else
                {
                    MessageBox.Show("Favor de seleccionar el genero");
                }
            }
            else
            {
                cmd.CommandText = "update Progra set Nombre='" + txtNombre.Text + "',Genero='" + cbGenero.Text + "',Telefono=" + txtTelefono.Text + ",Direccion='" + txtDireccion.Text + "'where Id=" + txtId.Text;
                MostrarDatos();
                MessageBox.Show("Datos del alumno Actualizado...");
                LimpiarTodo();
            }
            
           
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
