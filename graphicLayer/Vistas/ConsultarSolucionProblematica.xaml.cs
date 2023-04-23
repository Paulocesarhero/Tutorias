using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Tutorias.BussinesLogic.Management;
using Tutorias.Service.DatabaseContext;

namespace graphicLayer.Vistas
{
    /// <summary>
    /// Lógica de interacción para ConsultarSolucionProblematica.xaml
    /// </summary>
    /// 

    public partial class ConsultarSolucionProblematica : Page
    {
        public ConsultarSolucionProblematica()
        {
            InitializeComponent();
            DataContext = new ConsultarSolucionProblematicaViewModel();
        }

        public void VerDetalles(object sender, RoutedEventArgs e)
        {
            Problematica fila = DgProblematicas.SelectedItem as Problematica;

            MessageBox.Show(fila.Descripcion,
                    "No hay conexión a la base de datos en estos momentos",
                    MessageBoxButton.OKCancel);
        }

    }

    public partial class ConsultarSolucionProblematicaViewModel : DependencyObject
    {                   
        public ConsultarSolucionProblematicaViewModel()
        {
            SelectProblematicaCommand = new RelayCommand<Problematica>(SelectProblematicaAction);
            BtnBuscarReporteCommand = new RelayCommand(ExecuteBuscarReporte);
            FillPeriodosEscolares();
        }

        public ObservableCollection<Periodo_Escolar> PeriodosEscolaresObservableCollection { get; set; } =
            new ObservableCollection<Periodo_Escolar>();

        public ObservableCollection<Problematica> ProblematicasObservableCollection { get; set; } =
            new ObservableCollection<Problematica>();

        public static readonly DependencyProperty TotalDeAsistenciasObservableProperty = DependencyProperty.Register(
            "TotalDeAsistenciasObservable", typeof(String), typeof(ReporteGeneralViewModel), new PropertyMetadata(default(String)));

        public Problematica ProblematicaSeleccionada { get; set; } = new Problematica();

        public String TutorAcademicoObservable { get; set; }

        public String ComentariosGeneralesObservable { get; set; }

        public Periodo_Escolar PeriodoEscolarSeleccionado { get; set; }
        public int NumTutoriaSeleccionada { get; set; }
        public ICommand SelectProblematicaCommand { get; set; }
        public ICommand BtnBuscarReporteCommand { get; }

        public String TotalDeAsistenciasObservable
        {
            get { return (string)GetValue(TotalDeAsistenciasObservableProperty); }
            set { SetValue(TotalDeAsistenciasObservableProperty, value); }
        }

        private void ExecuteBuscarReporte()
        {
            TutoriaManagement tutoriaManagement = new TutoriaManagement();
            List<Problematica> problematicas = new List<Problematica>();
            int totalAsistencias = 0;
            try
            {
                problematicas = tutoriaManagement.FindProblematicasAcademicas(PeriodoEscolarSeleccionado, NumTutoriaSeleccionada + 1);
                totalAsistencias =
                    tutoriaManagement.FindTotalAssistants(PeriodoEscolarSeleccionado, NumTutoriaSeleccionada + 1);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error en la conexión con la base de datos",
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
            foreach (var problematica in problematicas) ProblematicasObservableCollection.Add(problematica);

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
            TutoriaManagement tutoriaManagement = new TutoriaManagement();
            List<Periodo_Escolar> periodosEscolares = new List<Periodo_Escolar>();
            try
            {
                periodosEscolares = tutoriaManagement.GetPeriodosEscolares();
            }

            catch (Exception e)
            {
                MessageBox.Show("Error en la conexión con la base de datos",
                    "No hay conexión a la base de datos en estos momentos",
                    MessageBoxButton.OKCancel);
            }

            PeriodosEscolaresObservableCollection = new ObservableCollection<Periodo_Escolar>(periodosEscolares);
        }        
    }
}
