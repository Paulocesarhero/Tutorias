using System;
using System.Collections.Generic;
using Sistema_De_Tutorias.Utility;
using System.Windows;
using System.Windows.Controls;
using DataAccess.BussinesLogic.EntityRepository;
using Tutorias.BussinesLogic.Management;
using Tutorias.Service.DatabaseContext;

namespace graphicLayer.Vistas
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidaUser())
            {
                NavigateToUserWindow();
            }
        }

        private bool IsValidaUser()
        {
            string username = TbUsername.Text;
            string password = PbPassword.Password;
            bool flag = false;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Llene todos los campos",
                            "Campos vacios");
            }
            else
            {
                flag = true;
            }
            return flag;
        }

        private void NavigateToUserWindow()
        {
            Usuario result = null;
            UsuarioRepository usuarioRepository = new UsuarioRepository(new TutoriasContext());
            FechaDeTutoriaRepository fechaDeTutoriaRepository = new FechaDeTutoriaRepository(new TutoriasContext());
            string username = TbUsername.Text;
            string password = PbPassword.Password.ToString();
            try
            {
                result = usuarioRepository.Login(username, password);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error en la conexión con la base de datos",
                    e.Message,
                    MessageBoxButton.OKCancel);
            }

            if (result != null)
            {
                switch (result.TipoUsuario.Tipo)
                {
                    case "Jefe de carrera":
                        ReporteGeneralDeTutorias firstPageJefeDeCarrera = new ReporteGeneralDeTutorias();
                        CredencialesUsuario.Instance.Usuario = result;
                        this.NavigationService.Navigate(firstPageJefeDeCarrera);
                        break;

                    case "Coordinadora":
                        AdministrarEE firstPageCoordinadora = new AdministrarEE();
                        CredencialesUsuario.Instance.Usuario = result;
                        this.NavigationService.Navigate(firstPageCoordinadora);
                        break;

                    case "Tutor academico":
                        if (fechaDeTutoriaRepository.GetFechaDeTutoriaActual(DateTime.Now) == null)
                        {
                            MessageBox.Show("Actualmente no hay ninguna fecha de tutoria abierta",
                                "Podria comunicarse con la coordinadora para solicitar una prorroga");
                        }
                        else
                        {
                            CredencialesUsuario.Instance.Usuario = result;
                            LlenarReporteDeTutorias firstPageTutorAcademico = new LlenarReporteDeTutorias();

                            this.NavigationService.Navigate(firstPageTutorAcademico);
                        }

                        break;
                    default:
                        MessageBox.Show("El usuario no tiene un tipo de usuario asignado",
                            "Contacte al administrador para concederle los permisos adecuados");
                        break;
                }
            }
            else
            {
                MessageBox.Show("El usuario no se encuentra",
                    "Verifique su usuario y contraseña");
            }
        }
    }
}