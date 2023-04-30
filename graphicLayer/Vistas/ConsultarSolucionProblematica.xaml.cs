using DataAccess.BussinesLogic.EntityRepository;
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

            DetallesSolucionProblematica secondWindow = new DetallesSolucionProblematica(fila);
            this.NavigationService.Navigate(secondWindow);
        }        

    }

    public partial class ConsultarSolucionProblematicaViewModel : DependencyObject
    {
        public Periodo_Escolar PeriodoEscolarSeleccionado { get; set; }
        public int NumTutoriaSeleccionada { get; set; }
        public ObservableCollection<Periodo_Escolar> PeriodosEscolaresObservableCollection { get; private set; }
        public ObservableCollection<Problematica> ProblematicasObservableCollection { get; set; } =
            new ObservableCollection<Problematica>();
        public ICommand BtnBuscarReporteCommand { get; }

        public ConsultarSolucionProblematicaViewModel()
        {            
            fillPeriodos();
            BtnBuscarReporteCommand = new RelayCommand(BuscarReporteCommand);
        }        

        private void fillPeriodos()
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

            periodosEscolares.FirstOrDefault().FechaDeInicio.GetDateTimeFormats();
            PeriodosEscolaresObservableCollection = new ObservableCollection<Periodo_Escolar>(periodosEscolares);
        }

        private void BuscarReporteCommand()
        {
            List<Problematica> problematicas = new List<Problematica>();
            ProblematicaRepository problematicaRepository = new ProblematicaRepository(new TutoriasContext());
            problematicas = problematicaRepository.GetProblematicas(PeriodoEscolarSeleccionado, NumTutoriaSeleccionada + 1);
            ProblematicasObservableCollection.Clear();
            foreach (var problematica in problematicas) ProblematicasObservableCollection.Add(problematica);
        }

    }
}