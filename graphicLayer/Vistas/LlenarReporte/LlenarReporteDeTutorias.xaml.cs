using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DataAccess.BussinesLogic.EntityRepository;
using Sistema_De_Tutorias.Utility;
using Tutorias.Service.DatabaseContext;

namespace graphicLayer.Vistas
{
    /// <summary>
    /// Lógica de interacción para LlenarReporteDeTutorias.xaml
    /// </summary>
    public partial class LlenarReporteDeTutorias : Page
    {
        static FechaDeTutoriaRepository fechaDeTutoriaRepository = new FechaDeTutoriaRepository(new TutoriasContext());

        private static TutorAcademicoRepository tutorAcademicoRepository =
            new TutorAcademicoRepository(new TutoriasContext());
        private Fecha_De_Tutoria fechaDeTutoria { get; set; }
        private Tutor_Academico tutorAcademico { get; set; }

        public LlenarReporteDeTutorias()
        {
            InitializeComponent();
            LlenarReporteDeTutoriasViewModel viewModel = new LlenarReporteDeTutoriasViewModel();
            DataContext = viewModel;
            fechaDeTutoria = fechaDeTutoriaRepository.GetFechaDeTutoriaActual(DateTime.Now);
            tutorAcademico = tutorAcademicoRepository.GetTutorAcademicoByUser(CredencialesUsuario.Instance.Usuario);
            FillComentario();

        }

        private void FillComentario()
        {
           
                ReporteDeTutoriaRepository reporteDeTutoriaRepository =
                    new ReporteDeTutoriaRepository(new TutoriasContext());
                Reporte_De_Tutoria reporteDe = reporteDeTutoriaRepository.GetReporteDeTutoria(tutorAcademico, fechaDeTutoria);
                if (reporteDe !=null)
                {
                    TbComentariosGenerales.Text = reporteDe.Comentarios; 
                }



            
        }

        private void BtnAddProblematica_Click(object sender, RoutedEventArgs e)
        {
            ReporteDeTutoriaRepository reporteDeTutoriaRepository = new ReporteDeTutoriaRepository(new TutoriasContext());
            Reporte_De_Tutoria findReporteDeTutoria = new Reporte_De_Tutoria();
            try
            {
                 findReporteDeTutoria = reporteDeTutoriaRepository.GetReporteDeTutoria(tutorAcademico, fechaDeTutoria);

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,
                    "Error en la conexión con la base de datos",
                    MessageBoxButton.OK);
            }

            if (findReporteDeTutoria != null)
            {
                LlenarProblematicas llenarProblematicas = new LlenarProblematicas();
                llenarProblematicas.FechaDeTutoria = fechaDeTutoria;
                llenarProblematicas.TutorAcademico = tutorAcademico;
                llenarProblematicas.FillProblematicas();
                this.NavigationService.Navigate(llenarProblematicas);
            }
            else
            {
                MessageBox.Show("Primero tiene que guardar el reporte de tutoria antes de agregar las problematicas",
                    "Guarde su reporte de tutoria academica ",
                    MessageBoxButton.OK);
            }

            
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            TutoriasContext tutoriasContext = new TutoriasContext();
            ReporteDeTutoriaRepository reporteDeTutoriaRepository = new ReporteDeTutoriaRepository(tutoriasContext);
            AsistenciaRepository asistenciaRepository = new AsistenciaRepository(tutoriasContext);
            FechaDeTutoriaRepository fechaDeTutoriaRepository = new FechaDeTutoriaRepository(tutoriasContext);
            TutorAcademicoRepository tutorAcademicoRepository = new TutorAcademicoRepository(tutoriasContext);
            try
            {
                Reporte_De_Tutoria reporteDeTutoria = new Reporte_De_Tutoria()
                {
                    Comentarios = TbComentariosGenerales.Text,
                    FechaDeTutoria = fechaDeTutoriaRepository.GetFechaDeTutoriaActual(DateTime.Now),
                    Fecha = DateTime.Now,
                    TutorAcademico = tutorAcademicoRepository.GetTutorAcademicoByUser(CredencialesUsuario.Instance.Usuario)

                };
                reporteDeTutoriaRepository.AddReporteDeTutoria(reporteDeTutoria);
                List<Asistencia> asitencias = DgAsistencias.Items.Cast<Asistencia>().ToList();
                foreach (Asistencia asis in asitencias)
                {
                    asistenciaRepository.AddAsistencia(asis);

                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,
                    "No hay conexión a la base de datos en estos momentos",
                    MessageBoxButton.OK);
            }

            
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            NavigationService.Navigate(login);
        }
    }

    public class LlenarReporteDeTutoriasViewModel
    {
        private TutoriasContext tutoriasContext = new TutoriasContext();
        private Tutor_Academico tutorAcademico = new Tutor_Academico();
        private List<Estudiante> estudiantes = new List<Estudiante>();
        public Fecha_De_Tutoria fechaDeTutoria { get; set; } = new Fecha_De_Tutoria();
        public ObservableCollection<Asistencia> AsistenciaObservable { get; set; } =
            new ObservableCollection<Asistencia>();

    
        public DateTime fechaDeHoy { get; set; } = DateTime.Now;

        public LlenarReporteDeTutoriasViewModel()
        {
            FillEstudiantes();
            FillAsistencias();
        }

        

        public void FillEstudiantes()
        {
            EstudianteRepository estudianteRepository = new EstudianteRepository(tutoriasContext);
            TutorAcademicoRepository tutorAcademicoRepository = new TutorAcademicoRepository(tutoriasContext);
            try
            {
                tutorAcademico = tutorAcademicoRepository.GetTutorAcademicoByUser(CredencialesUsuario.Instance.Usuario);
                estudiantes = estudianteRepository.GetEstudiantesByTutorAcademico(tutorAcademico);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,
                    "No hay conexión a la base de datos en estos momentos",
                    MessageBoxButton.OK);
            }
        }

        public void FillAsistencias()
        {
            FechaDeTutoriaRepository fechaDeTutoriaRepository = new FechaDeTutoriaRepository(tutoriasContext);
            AsistenciaRepository asistenciaRepository = new AsistenciaRepository(tutoriasContext);
            TutorAcademicoRepository tutorAcademicoRepository = new TutorAcademicoRepository(tutoriasContext);
            fechaDeTutoria = fechaDeTutoriaRepository.GetFechaDeTutoriaActual(DateTime.Now);
            List<Asistencia> listaDeAsistencias = new List<Asistencia>();

            tutorAcademico = tutorAcademicoRepository.GetTutorAcademicoByUser(CredencialesUsuario.Instance.Usuario);
            fechaDeTutoria = fechaDeTutoriaRepository.GetFechaDeTutoriaActual(DateTime.Now);
            listaDeAsistencias = asistenciaRepository.GetAsistencias(tutorAcademico, fechaDeTutoria
                );
            if (listaDeAsistencias == null)
            {
                foreach (Estudiante estudiante in estudiantes)
                {
                    Asistencia asistencias = new Asistencia()
                    {
                        Estudiante = estudiante,
                        Asiste = true,
                        FechaDeTutoria = fechaDeTutoria,
                        Horario = DateTime.Now

                    };
                    asistenciaRepository.AddAsistencia(asistencias);
                    listaDeAsistencias.Add(asistencias);
                }
                foreach (Asistencia asi in listaDeAsistencias) AsistenciaObservable.Add(asi);
            }
            else
            {
                foreach (Asistencia asi in listaDeAsistencias) AsistenciaObservable.Add(asi);

            }

        }


    }
}