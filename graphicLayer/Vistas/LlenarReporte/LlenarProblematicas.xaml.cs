using System;
using System.Collections;
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
using DataAccess.BussinesLogic.EntityRepository;
using graphicLayer.Vistas;
using graphicLayer.Vistas.LlenarReporte;
using Tutorias.Service.DatabaseContext;

namespace graphicLayer
{
    /// <summary>
    /// Lógica de interacción para LlenarProblematicas.xaml
    /// </summary>
    public partial class LlenarProblematicas : Page
    {
        public Tutor_Academico TutorAcademico { get; set; }
        public Fecha_De_Tutoria FechaDeTutoria { get; set; }
        public LlenarProblematicas()
        {
            InitializeComponent();
            DataContext = new LlenarProblematicasViewModel();
        }

        public void FillProblematicas()
        {
            List<Problematica> result = new List<Problematica>();
            ProblematicaRepository problematicaRepository = new ProblematicaRepository(new TutoriasContext());
            try
            {
                result = problematicaRepository.GetProblematicasByFechaDeTutoria(FechaDeTutoria, TutorAcademico);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,
                    "Error en la conexión con la base de datos",
                    MessageBoxButton.OK);
            }
            DgProblematicas.ItemsSource = result;
        }


        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            AgregarProblematica agregarProblematica = new AgregarProblematica();
            agregarProblematica.TutorAcademico = TutorAcademico;
            agregarProblematica.fecha = FechaDeTutoria;
            agregarProblematica.FillData();
            this.NavigationService.Navigate(agregarProblematica);
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            ProblematicaRepository problematicaRepository = new ProblematicaRepository(new TutoriasContext());
            Problematica problematicaSeleccionada = DgProblematicas.SelectedItem as Problematica;
            try
            {
                problematicaRepository.DeleteProblematica(problematicaSeleccionada);
                LlenarProblematicas llenarProblematicas = new LlenarProblematicas();
                llenarProblematicas.FechaDeTutoria = this.FechaDeTutoria;
                llenarProblematicas.TutorAcademico = this.TutorAcademico;
                llenarProblematicas.FillProblematicas();
                this.NavigationService.Navigate(llenarProblematicas);

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,
                    "Error en la conexión con la base de datos",
                    MessageBoxButton.OK);
            }

            
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            LlenarReporteDeTutorias firstPageTutorAcademico = new LlenarReporteDeTutorias();
            this.NavigationService.Navigate(firstPageTutorAcademico);
        }
    }

    public class LlenarProblematicasViewModel
    {
        public ObservableCollection<Problematica> problematicasObserbObservableCollection { get; set; } =
            new ObservableCollection<Problematica>();

        public LlenarProblematicasViewModel()
        {
        }
    }
}
