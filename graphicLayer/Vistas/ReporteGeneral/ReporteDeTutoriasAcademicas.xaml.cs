using DataAccess.BussinesLogic.EntityRepository;
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
using Tutorias.Service.DatabaseContext;

namespace graphicLayer.Vistas.ReporteGeneral
{
    /// <summary>
    /// Lógica de interacción para ReporteDeTutoriasAcademicas.xaml
    /// </summary>
    public partial class ReporteDeTutoriasAcademicas : Page
    {
        List<Reporte_De_Tutoria> result = new List<Reporte_De_Tutoria>();
        public ReporteDeTutoriasAcademicas()
        {
            InitializeComponent();
            ReporteDeTutoriasAcademicasViewModel dataContext = new ReporteDeTutoriasAcademicasViewModel();
            DataContext = dataContext;
        }

        private void BtnBuscarReporte_Click(object sender, RoutedEventArgs e)
        {
            ReporteDeTutoriaRepository reporteDeTutoriaRepository =
                new ReporteDeTutoriaRepository(new TutoriasContext());
            try
            {
                result = reporteDeTutoriaRepository.GetReporteDeTutoriaByPeriodo(CbPeriodoEscolar.SelectionBoxItem as Periodo_Escolar, 
                    CbSesionDeTutoria.SelectedIndex + 1);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "No hay conexión a la base de datos en estos momentos",
                    MessageBoxButton.OKCancel);
            }

            DgReportes.ItemsSource = result;
        }

        private void BtnBuscarReporteByCriteria_Click(object sender, RoutedEventArgs e)
        {
            List<Reporte_De_Tutoria> searchByName = new List<Reporte_De_Tutoria>();
            if (CbSearchByCriteria.SelectedIndex == 0)
            {
                searchByName =
                    result.Where(x => 
                        x.TutorAcademico.Nombres.ToLower() == TbCriterio.Text.ToLower()
                        || x.TutorAcademico.Apellidos.ToLower() == TbCriterio.Text.ToLower()
                        || x.TutorAcademico.Nombres.ToLower() + " " +x.TutorAcademico.Apellidos.ToLower() == TbCriterio.Text.ToLower()).ToList();
            }

            DgReportes.ItemsSource = searchByName;
        }
    }

    public class ReporteDeTutoriasAcademicasViewModel
    {
        public ObservableCollection<Periodo_Escolar> PeriodosEscolaresObservableCollection { get; set; } =
            new ObservableCollection<Periodo_Escolar>();

        public ObservableCollection<Reporte_De_Tutoria> ReporteDeTutoriasObservable { get; set; }=
            new ObservableCollection<Reporte_De_Tutoria>();
        public Periodo_Escolar PeriodoEscolarSeleccionado { get; set; }

        public int NumTutoriaSeleccionada { get; set; }

        public ReporteDeTutoriasAcademicasViewModel()
        {
            FillPeriodosEscolares();
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
            foreach (Periodo_Escolar periodoEscolar in periodosEscolares) PeriodosEscolaresObservableCollection.Add(periodoEscolar);
        }

        private void BtnBuscarReporteCommand()
        {
            ReporteDeTutoriaRepository reporteDeTutoriaRepository =
                new ReporteDeTutoriaRepository(new TutoriasContext());
            List<Reporte_De_Tutoria> result = new List<Reporte_De_Tutoria>();
            try
            {
                 result = reporteDeTutoriaRepository.GetReporteDeTutoriaByPeriodo(PeriodoEscolarSeleccionado,
                    NumTutoriaSeleccionada + 1);
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,
                    "No hay conexión a la base de datos en estos momentos",
                    MessageBoxButton.OKCancel);
            }

            ReporteDeTutoriasObservable.Clear();
            foreach (Reporte_De_Tutoria reporteDeTutoria in result) ReporteDeTutoriasObservable.Add(reporteDeTutoria);

        }

    }
}
