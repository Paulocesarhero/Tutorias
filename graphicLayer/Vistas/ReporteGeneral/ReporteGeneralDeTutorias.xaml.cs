using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using DataAccess.BussinesLogic.EntityRepository;
using Sistema_De_Tutorias.Utility;
using Tutorias.BussinesLogic.Management;
using Tutorias.Service.DatabaseContext;

namespace graphicLayer.Vistas
{
    /// <summary>
    /// Lógica de interacción para ReporteGeneralDeTutorias.xaml
    /// </summary>
    public partial class ReporteGeneralDeTutorias : Page
    {
        public ReporteGeneralDeTutorias()
        {
            InitializeComponent();
            ReporteGeneralViewModel dataContext = new ReporteGeneralViewModel();
            dataContext.ReporteGeneral = this;
            DataContext = dataContext;
            
        }
        private void BtnMod_Click(object sender, RoutedEventArgs e)
        {
            SolucionAProblematicaAcademica newWindow = new SolucionAProblematicaAcademica();
            Problematica problematica = DgProblematicas.SelectedItem as Problematica;
            newWindow.TbDescripcion.Text = problematica.Descripcion;
            newWindow.TbExperienciaEducativa.Text = problematica.ExperienciaEducativa.Nombre + " Nrc: " + problematica.ExperienciaEducativa.Nrc;
            newWindow.TbNumeroDeEstudiantes.Text = problematica.NumAlumnos.ToString();
            newWindow.TbProfesor.Text = problematica.ExperienciaEducativa.Catedratico.NombreCompleto;
            newWindow._Problematica = problematica;
            newWindow.TbFecha.Text = DateTime.Now.ToString();
            if (problematica.Solucion != null)
            {
                newWindow.TbTitulo.Text = problematica.Solucion.Titulo;
                newWindow.TbDescripcionSolucion.Text = problematica.Solucion.Descripcion;
            }

            this.NavigationService.Navigate(newWindow);

        }
    }

    public class ReporteGeneralViewModel : DependencyObject
    {
        public static readonly DependencyProperty TotalDeAsistenciasObservableProperty = DependencyProperty.Register(
            "TotalDeAsistenciasObservable", typeof(String), typeof(ReporteGeneralViewModel), new PropertyMetadata(default(String)));
        public ObservableCollection<Periodo_Escolar> PeriodosEscolaresObservableCollection { get; set; } =
            new ObservableCollection<Periodo_Escolar>();

        public ObservableCollection<Problematica> ProblematicasObservableCollection { get; set; } =
            new ObservableCollection<Problematica>();

        public String TutorAcademicoObservable { get; set; }

        public String ComentariosGeneralesObservable { get; set; }

        public String TotalDeAsistenciasObservable
        {
            get { return (string)GetValue(TotalDeAsistenciasObservableProperty); }
            set { SetValue(TotalDeAsistenciasObservableProperty, value); }
        }

        public ICommand SelectProblematicaCommand { get; set; }


        public ICommand BtnBuscarReporteCommand { get; }

        public ICommand BtnDatosProblematica { get; }

        public int NumTutoriaSeleccionada { get; set; }

        public Problematica ProblematicaSeleccionada { get; set; }

        public Periodo_Escolar PeriodoEscolarSeleccionado { get; set; }

        public ReporteGeneralDeTutorias ReporteGeneral { get; set; }


        public ReporteGeneralViewModel()
        {
            SelectProblematicaCommand = new RelayCommand<Problematica>(SelectProblematicaAction);
            BtnBuscarReporteCommand = new RelayCommand(ExecuteBuscarReporte);
            FillPeriodosEscolares();
        }

        

        private void ExecuteBuscarReporte()
        {
            ProblematicaRepository problematicaRepository = new ProblematicaRepository(new TutoriasContext());
            AsistenciaRepository asistenciaRepository = new AsistenciaRepository(new TutoriasContext());
            List<Problematica> problematicas = new List<Problematica>();
            List<Problematica> problematicasDelJefDeCarrera = new List<Problematica>();
            int totalAsistencias = 0;
            try
            {
                problematicas = problematicaRepository.GetProblematicas(PeriodoEscolarSeleccionado, NumTutoriaSeleccionada + 1);
                problematicasDelJefDeCarrera = problematicas.Where(x =>
                    x.ExperienciaEducativa.ProgramaEducativo.ProgramaEducativo ==
                    CredencialesUsuario.Instance.Usuario.ProgramaEducativo.ProgramaEducativo).ToList();
                 totalAsistencias =
                    asistenciaRepository.GetTotalDeAsistencias(PeriodoEscolarSeleccionado, NumTutoriaSeleccionada + 1);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,
                    "No hay conexión a la base de datos en estos momentos",
                    MessageBoxButton.OKCancel);
            }

            if (totalAsistencias > 0)
            {
                TotalDeAsistenciasObservable = totalAsistencias.ToString();
            }
            else
            {
                TotalDeAsistenciasObservable = "0";
            }
            ProblematicasObservableCollection.Clear();
            foreach (var problematica in problematicasDelJefDeCarrera) ProblematicasObservableCollection.Add(problematica);

        }

        private void SelectProblematicaAction(Problematica problematicaSeleccionada)
        {
            string nombreTutorAcademico = problematicaSeleccionada.ReporteDeTutoria.TutorAcademico.Nombres;
            string apellidosTutorAcademico = problematicaSeleccionada.ReporteDeTutoria.TutorAcademico.Apellidos;
            string ComentariosGeneralesDeTutoria = problematicaSeleccionada.ReporteDeTutoria.Comentarios;

            TutorAcademicoObservable = nombreTutorAcademico + apellidosTutorAcademico;
            ComentariosGeneralesObservable = ComentariosGeneralesDeTutoria;
        }

        public void FillPeriodosEscolares()
        {
            PeriodoEscolarRepository periodoEscolarRepository = new PeriodoEscolarRepository(new TutoriasContext());
            List<Periodo_Escolar> periodosEscolares = new List<Periodo_Escolar>();
            try
            {
                periodosEscolares = periodoEscolarRepository.GetAllPeriodosEscolar();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,
                    "No hay conexión a la base de datos en estos momentos",
                    MessageBoxButton.OKCancel);
            }
            List<Periodo_Escolar> periodosEscolarOrder = periodosEscolares.OrderBy(x => x.FechaDeInicio).ToList();

            PeriodosEscolaresObservableCollection = new ObservableCollection<Periodo_Escolar>(periodosEscolarOrder);
        }
    }
}