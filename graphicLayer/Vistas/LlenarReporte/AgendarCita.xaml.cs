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
using DataAccess.BussinesLogic.EntityRepository;
using graphicLayer.Utilidades;
using MySqlX.XDevAPI.Common;
using Sistema_De_Tutorias.Utility;
using Tutorias.Service.DatabaseContext;

namespace graphicLayer.Vistas.LlenarReporte
{
    /// <summary>
    /// Lógica de interacción para AgendarCita.xaml
    /// </summary>
    public partial class AgendarCita : Page
    {
        public Asistencia _Asistencia { get; set; } 

        public AgendarCita()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TbNombres.Text = String.Format("Nombres: {0} {1} Matricula: {2}",
                _Asistencia.Estudiante.Nombres, _Asistencia.Estudiante.Apellidos, _Asistencia.Estudiante.Matricula);
            TbSesionDeTutoria.Text = String.Format("Fecha de inicio de sesion: {0} Fecha de cierre de sesión {1}", _Asistencia.FechaDeTutoria.FechaDeInicionSesion, _Asistencia.FechaDeTutoria.FechaDeCierre);
            DpFecha.Text = _Asistencia.Horario.Date.ToString();
            TpHora.Text = _Asistencia.Horario.ToString("HH:mm");
        }


        private bool CamposVacios()
        {
            return TboxMensaje.Text == "" || DpFecha.Text == "" || TpHora.Text == "";
        }
        private void BtnEnviar_Click(object sender, RoutedEventArgs e)
        {
            AsistenciaRepository asistenciaRepository = new AsistenciaRepository(new TutoriasContext());
            if (!CamposVacios())
            {
                string correoDelTutorado = "z" + _Asistencia.Estudiante.Matricula + "@estudiantes.uv.mx";
                string mensaje = "Su tutoria sera el día: " + DpFecha.Text + "En la hora: " + TpHora.SelectedTime + "\n" + TboxMensaje.Text;
                string fechaDeTutoria = DpFecha.Text+" " + TpHora.Text;
                try
                {
                    _Asistencia.Horario = Convert.ToDateTime(fechaDeTutoria);
                    if (_Asistencia.Horario < _Asistencia.FechaDeTutoria.FechaDeCierre && _Asistencia.Horario > _Asistencia.FechaDeTutoria.FechaDeInicionSesion && _Asistencia.Horario > DateTime.Now)
                    {
                        asistenciaRepository.AddAsistencia(_Asistencia);
                        Comunicacion.EnviarCorreo(correoDelTutorado, mensaje);
                        MessageBox.Show("Hemos enviado un correo electronico a su tutorado ",
                            "Todo correcto",
                            MessageBoxButton.OK);
                        LlenarReporteDeTutorias firstPageTutorAcademico = new LlenarReporteDeTutorias();
                        this.NavigationService.Navigate(firstPageTutorAcademico);
                    }
                    else
                    {
                        MessageBox.Show("El horario de la cita de tutorias no se encuentra dentro del rango de la sesión de tutoria academica o se encuentra antes de la fecha actual",
                            "Vefique las fechas",
                            MessageBoxButton.OK);
                    }
                    

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message,
                        "Error en la conexión con la base de datos",
                        MessageBoxButton.OK);
                    LlenarReporteDeTutorias firstPageTutorAcademico = new LlenarReporteDeTutorias();
                    this.NavigationService.Navigate(firstPageTutorAcademico);
                }

            }
            else
            {
                MessageBox.Show("Campos vacios",
                    "Debe llenar los campos necesarios para poder enviar un mensaje",
                    MessageBoxButton.OK);
            }

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            LlenarReporteDeTutorias firstPageTutorAcademico = new LlenarReporteDeTutorias();
            this.NavigationService.Navigate(firstPageTutorAcademico);
        }
    }
}
