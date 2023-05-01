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

namespace graphicLayer.Vistas.AdministrarFechas
{
    /// <summary>
    /// Lógica de interacción para AgregarPeriodoEscolar.xaml
    /// </summary>
    public partial class AgregarPeriodoEscolar : Page
    {
        public AgregarPeriodoEscolar()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            List<int> years = Enumerable.Range(2000, 31).ToList();
            CbYearInit.ItemsSource = years;
            CbYearInit.SelectedIndex = years.IndexOf(DateTime.Now.Year);
            CbLastYear.ItemsSource = years;
            CbLastYear.SelectedIndex = years.IndexOf(DateTime.Now.Year + 1);
            CbMesFin.SelectedIndex = 0;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
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
