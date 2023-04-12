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