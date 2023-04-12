using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Tutorias.BussinesLogic.Management;
using Tutorias.Service.DatabaseContext;

namespace graphicLayer.Vistas
{
    /// <summary>
    /// Lógica de interacción para AdministrarEE.xaml
    /// </summary>
    public partial class AdministrarEE : Page
    {

        public AdministrarEE()
        {
            InitializeComponent();
            AdministrarEEViewModel viewModel = new AdministrarEEViewModel();
            viewModel.AdministrarEe = this;
            DataContext = viewModel;
        }

        
      
        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
             AgregarEE newPage = new AgregarEE();
             newPage.FillData();
             this.NavigationService.Navigate(newPage);
        }

        private void BtnMod_Click(object sender, RoutedEventArgs e)
        {
            AgregarEE newPage = new AgregarEE();
            if (DgExperiencias.SelectedItem == null)
            {
                MessageBox.Show("Para modificar una experiencia educativa primero debe seleccionarla de la tabla",
                    "Seleccione una experiencia educativa",
                    MessageBoxButton.OK);
            }
            else
            {
                newPage.ExperienciaEducativaSelect = DgExperiencias.SelectedItem as Experiencia_Educativa;
                newPage.FillData();
                this.NavigationService.Navigate(newPage);
            }
            
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class AdministrarEEViewModel
    {
        public AdministrarEE AdministrarEe { get; set; }
        public Experiencia_Educativa ExperienciaEducativaSelect { get; set; }
        public ObservableCollection<Experiencia_Educativa> ExperienciasObservables { get; set; } =
            new ObservableCollection<Experiencia_Educativa>();

        public AdministrarEEViewModel()
        {
            FillExperiencias();   
        }
        public void FillExperiencias()
        {
            TutoriaManagement tutoriaManagement = new TutoriaManagement();
            List<Experiencia_Educativa> experiencisResult = new List<Experiencia_Educativa>();
            try
            {
                experiencisResult = tutoriaManagement.getExperienciasEducativas();

            }
            catch (Exception e)
            {
                MessageBox.Show("Error en la conexión con la base de datos",
                    "No hay conexión a la base de datos en estos momentos",
                    MessageBoxButton.OK);
            }
            foreach (Experiencia_Educativa experienciaEducativa in experiencisResult) ExperienciasObservables.Add(experienciaEducativa);
        }

    }
}