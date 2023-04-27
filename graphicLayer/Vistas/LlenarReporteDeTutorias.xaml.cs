using System.Collections.ObjectModel;
using System.Windows.Controls;
using Tutorias.Service.DatabaseContext;

namespace graphicLayer.Vistas
{
    /// <summary>
    /// Lógica de interacción para LlenarReporteDeTutorias.xaml
    /// </summary>
    public partial class LlenarReporteDeTutorias : Page
    {
        public LlenarReporteDeTutorias()
        {
            InitializeComponent();
            LlenarReporteDeTutoriasViewModel viewModel = new LlenarReporteDeTutoriasViewModel();
            DataContext = viewModel;

        }
    }

    public class LlenarReporteDeTutoriasViewModel
    {
        public ObservableCollection<Asistencia> AsistenciaObservable { get; set; } =
            new ObservableCollection<Asistencia>();

    }
}