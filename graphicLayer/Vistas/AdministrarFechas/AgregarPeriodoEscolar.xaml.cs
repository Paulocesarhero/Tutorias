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
using Tutorias.Service.DatabaseContext;

namespace graphicLayer.Vistas.AdministrarFechas
{
    /// <summary>
    /// Lógica de interacción para AgregarPeriodoEscolar.xaml
    /// </summary>
    public partial class AgregarPeriodoEscolar : Page
    {
        public Periodo_Escolar _PeriodoEscolar { get; set; } = new Periodo_Escolar();
        public AgregarPeriodoEscolar()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            List<int> years = Enumerable.Range(2000, 31).ToList();
            CbYearInit.ItemsSource = years;
            CbLastYear.ItemsSource = years;

            if (_PeriodoEscolar.Id == 0)
            {
                CbYearInit.SelectedIndex = years.IndexOf(DateTime.Now.Year);
                CbLastYear.SelectedIndex = years.IndexOf(DateTime.Now.Year + 1);
                CbMesFin.SelectedIndex = 0;
            }
            else
            {
                CbYearInit.SelectedItem = years.IndexOf(_PeriodoEscolar.FechaDeInicio.Year);
                CbLastYear.SelectedItem = years.IndexOf(_PeriodoEscolar.FechaDeFin.Year);
                if (_PeriodoEscolar.FechaDeFin.Month == 1)
                {
                    CbMesFin.SelectedIndex = 1;
                }
                else
                {
                    CbMesFin.SelectedIndex = 0;
                }
                

            }

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            PeriodoEscolarRepository peroEscolarRepository = new PeriodoEscolarRepository(new TutoriasContext());

            _PeriodoEscolar.FechaDeInicio = new DateTime((int)CbYearInit.SelectedItem, GetMesDeFechaDeInicio(), 1);
            _PeriodoEscolar.FechaDeFin = new DateTime((int)CbLastYear.SelectedItem, GetMesDeFechaDeFin(), 1);
            try
            {
                peroEscolarRepository.AddPeriodoEscolar(_PeriodoEscolar);
                MessageBox.Show("Periodo escolar agregado",
                    "El periodo escolar ha sido agregado",
                    MessageBoxButton.OK);
                AdministrarFechasDeEntrega administrarFechasDeEntrega = new AdministrarFechasDeEntrega();
                administrarFechasDeEntrega.FillPeriodosEscolares();
                NavigationService.Navigate(administrarFechasDeEntrega);


            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,
                    "Error en la conexión con la base de datos",
                    MessageBoxButton.OK);
            }

        }

        private int GetMesDeFechaDeInicio()
        {
            return (CbMesInicio.SelectedItem == "Febrero") ? 2 : 8;

        }
        private int GetMesDeFechaDeFin()
        {
            return (CbMesFin.SelectedItem == "Enero") ? 1 : 7;

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            AdministrarFechasDeEntrega administrarfechas = new AdministrarFechasDeEntrega();
            administrarfechas.FillPeriodosEscolares();
            NavigationService.Navigate(administrarfechas);
        }

        private void CbMesInicio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbMesInicio.SelectedIndex == 0)
            {
                CbMesFin.SelectedIndex = 0;
                CbYearInit.SelectedItem = CbLastYear.SelectedItem;
                CbLastYear.SelectedItem = CbYearInit.SelectedItem;
            }
            else
            {
                CbMesFin.SelectedIndex = 1;
                int selectedYear = (int)CbYearInit.SelectedItem;
                CbLastYear.SelectedItem = selectedYear+1;
            }
        }

        private void CbMesFin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbMesFin.SelectedIndex == 0)
            {
                CbMesInicio.SelectedIndex = 0;
                CbYearInit.SelectedItem = CbLastYear.SelectedItem;
                CbLastYear.SelectedItem = CbYearInit.SelectedItem;
            }
            else
            {
                CbMesInicio.SelectedIndex = 1;
                int selectedYear = (int)CbYearInit.SelectedItem;
                CbLastYear.SelectedItem = selectedYear + 1;

            }
        }

        private void CbYearInit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbMesFin.SelectedIndex == 0)
            {
                CbLastYear.SelectedItem = CbYearInit.SelectedItem;
            }
            else
            {
                int selectedYear = (int)CbYearInit.SelectedItem;
                CbLastYear.SelectedItem = selectedYear + 1;
            }
           
        }

        private void CbLastYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbMesFin.SelectedIndex == 0)
            {
                CbYearInit.SelectedItem = CbLastYear.SelectedItem;
            }
            else
            {
                int selectedYear = (int)CbLastYear.SelectedItem;
                CbYearInit.SelectedItem = selectedYear - 1;
            }
           
        }
    }
}
