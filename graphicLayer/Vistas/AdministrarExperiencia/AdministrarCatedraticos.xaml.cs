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
using DataAccess.BussinesLogic.EntityRepository;
using Tutorias.Service.DatabaseContext;

namespace graphicLayer.Vistas
{
    /// <summary>
    /// Lógica de interacción para AdministrarCatedraticos.xaml
    /// </summary>
    public partial class AdministrarCatedraticos : Page
    {
        public AdministrarCatedraticos()
        {
            InitializeComponent();
        }

        public void FillData()
        {
            CatedraticoRepository catedraticoRepository = new CatedraticoRepository(new TutoriasContext());
            List<Catedratico> result = new List<Catedratico>();
            try
            {
               result = catedraticoRepository.GetAllCatedraticos();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,
                    "Error en la conexión con la base de datos",
                    MessageBoxButton.OK);
            }

            foreach (Catedratico cat in result)
            {
                DgCatedraticos.Items.Add(cat);

            }
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            AgregarCatedratico agregarCatedratico = new AgregarCatedratico();
            NavigationService.Navigate(agregarCatedratico);
        }

        private void BtnMod_Click(object sender, RoutedEventArgs e)
        {
            AgregarCatedratico agregarCatedratico = new AgregarCatedratico();
            agregarCatedratico._catedratico = DgCatedraticos.SelectedItem as Catedratico;
            agregarCatedratico.FillData();
            NavigationService.Navigate(agregarCatedratico);
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            CatedraticoRepository catedraticoRepository = new CatedraticoRepository(new TutoriasContext());
            try
            {
                catedraticoRepository.DeleteCatedratico(DgCatedraticos.SelectedItem as Catedratico);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,
                    "Error en la conexión con la base de datos",
                    MessageBoxButton.OK);
            }
            AdministrarCatedraticos administrarCatedraticos = new AdministrarCatedraticos();
            administrarCatedraticos.FillData();
            NavigationService.Navigate(administrarCatedraticos);

        }
    }

   
}
