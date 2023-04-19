using ControlzEx.Standard;
using graphicLayer.Vistas;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace graphicLayer.Controles
{
    /// <summary>
    /// Lógica de interacción para MenuCoordinadora.xaml
    /// </summary>
    public partial class MenuCoordinadora : UserControl
    {
        public MenuCoordinadora()
        {
            InitializeComponent();
        }

        private void BtnAdministrarEE_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void BtnAdministrarTutoresAcademicos_Click(object sender, RoutedEventArgs e)
        {
            AdministrarTutorAcademico administrarTutor = new AdministrarTutorAcademico();
            Page pg = GetDependencyObjectFromVisualTree(this, typeof(Page)) as Page;
            pg.NavigationService.Navigate(administrarTutor);
        }
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            Page pg = GetDependencyObjectFromVisualTree(this, typeof(Page)) as Page;
            pg.NavigationService.Navigate(login);
        }
        private void BtnExperiencias_Click(object sender, RoutedEventArgs e)
        {
            AdministrarEE administrarEe = new AdministrarEE();
            Page pg = GetDependencyObjectFromVisualTree(this, typeof(Page)) as Page;
            pg.NavigationService.Navigate(administrarEe);

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