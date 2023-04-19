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

namespace graphicLayer.Vistas
{
    /// <summary>
    /// Lógica de interacción para DetallesTutorAcademico.xaml
    /// </summary>
    public partial class DetallesTutorAcademico : Page
    {
        public DetallesTutorAcademico()
        {
            InitializeComponent();
        }

        private void BtnMod_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public class DetallesTutorAcademicoViewModel
    {
        public ObservableCollection<Estudiante> EstudiantesObservables { get; set; } =
            new ObservableCollection<Estudiante>();
    }
}
