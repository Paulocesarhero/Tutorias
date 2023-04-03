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
            BtnBuscarReporte.IsEnabled = false;
            DataContext = new ReporteGeneralViewModel();
        }

        private void BtnBuscarReporte_Click(object sender, RoutedEventArgs e)
        {
        }

        private void CbPeriodoEscolar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnBuscarReporte.IsEnabled = true;
        }

        private void CbSesionDeTutoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnBuscarReporte.IsEnabled = true;
        }

        public void FillProblematicas()
        {
            int sesionSelect = 1 + CbPeriodoEscolar.SelectedIndex;
            TutoriaManagement tutoriaManagement = new TutoriaManagement();
            Periodo_Escolar periodoEscolarSelect = (Periodo_Escolar)CbPeriodoEscolar.SelectedItem;
            try
            {
                tutoriaManagement.FindProblematicasAcademicas(periodoEscolarSelect, sesionSelect);

            }
            catch (Exception e)
            {
                MessageBox.Show("Error en la conexión con la base de datos",
                    "No hay conexión a la base de datos en estos momentos",
                    MessageBoxButton.OKCancel);
            }
        }
        
        
    }

    public class ReporteGeneralViewModel
    {
        public ObservableCollection<Periodo_Escolar> PeriodosEscolaresObservableCollection { get; set; }

        public ReporteGeneralViewModel()
        {
            FillPeriodosEscolares();
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
