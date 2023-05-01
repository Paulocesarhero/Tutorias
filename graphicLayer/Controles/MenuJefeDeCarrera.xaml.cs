using graphicLayer.Vistas;
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
using graphicLayer.Vistas.ReporteGeneral;
using ControlzEx.Standard;

namespace graphicLayer.Controles
{
    /// <summary>
    /// Lógica de interacción para MenuJefeDeCarrera.xaml
    /// </summary>
    public partial class MenuJefeDeCarrera : UserControl
    {
        public MenuJefeDeCarrera()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            Page pg = GetDependencyObjectFromVisualTree(this, typeof(Page)) as Page;
            pg.NavigationService.Navigate(login);
        }

        private void BtnReportes_Click(object sender, RoutedEventArgs e)
        {
            ReporteDeTutoriasAcademicas reporteDeTutoriasAcademicas = new ReporteDeTutoriasAcademicas();
            Page pg = GetDependencyObjectFromVisualTree(this, typeof(Page)) as Page;
            pg.NavigationService.Navigate(reporteDeTutoriasAcademicas);
        }

        private void BtnProblematicas_Click(object sender, RoutedEventArgs e)
        {
            ReporteGeneralDeTutorias reporteGeneralDeTutorias = new ReporteGeneralDeTutorias();
            Page pg = GetDependencyObjectFromVisualTree(this, typeof(Page)) as Page;
            pg.NavigationService.Navigate(reporteGeneralDeTutorias);
        }
        /// <summary>

        /// Walk visual tree to find the first DependencyObject  of the specific type.

        /// </summary>

        private DependencyObject

            GetDependencyObjectFromVisualTree(DependencyObject startObject, Type type)
        {
            //Walk the visual tree to get the parent(ItemsControl)

            //of this control

            DependencyObject parent = startObject;

            while (parent != null)

            {
                if (type.IsInstanceOfType(parent))

                    break;
                else

                    parent = VisualTreeHelper.GetParent(parent);
            }

            return parent;
        }


    }
}

