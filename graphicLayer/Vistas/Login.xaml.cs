using Azure.Messaging;
using MahApps.Metro.Controls.Dialogs;
using MySqlX.XDevAPI.Common;
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
using Tutorias.BussinesLogic.Interface;
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
            Usuario result;
            TutoriaManagement tutoriaManagement = new TutoriaManagement();
            string username = TbUsername.Text;
            string password = PbPassword.Password.ToString();
            result = tutoriaManagement.Login(username, password);
            switch (result.tipoUsuario.tipo)
            {
                case "Jefe De Carrera":
                    ReporteGeneralDeTutorias firstPageJefeDeCarrera = new ReporteGeneralDeTutorias();
                    this.NavigationService.Navigate(firstPageJefeDeCarrera);
                    break;
                case "Coordinadora":
                    AdministrarEE firstPageCoordinadora = new AdministrarEE();
                    this.NavigationService.Navigate(firstPageCoordinadora);
                    break;
                case "Tutor academico":
                    LlenarReporteDeTutorias firstPageTutorAcademico = new LlenarReporteDeTutorias();
                    this.NavigationService.Navigate(firstPageTutorAcademico);
                    break;


            }
        }
    }
}
