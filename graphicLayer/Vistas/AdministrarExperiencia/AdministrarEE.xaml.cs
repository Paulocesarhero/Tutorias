using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using DataAccess.BussinesLogic.EntityRepository;
using Microsoft.EntityFrameworkCore;
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
            if (IsExperienciaSelectNull())
            {
                newPage.ExperienciaEducativaSelect = DgExperiencias.SelectedItem as Experiencia_Educativa;
                newPage.FillData();
                this.NavigationService.Navigate(newPage);
            }
            
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (IsExperienciaSelectNull())
            {
                ExperienciaEducativaRepository experienciaEducativaRepository =
                    new ExperienciaEducativaRepository(new TutoriasContext());
                Experiencia_Educativa experienciSeleccionada = DgExperiencias.SelectedItem as Experiencia_Educativa;
                try
                {
                    if (experienciaEducativaRepository.DeleteExperienciaEducativa(experienciSeleccionada))
                    {
                        MessageBox.Show("La experiencia educativa " + experienciSeleccionada.Nombre + " " + experienciSeleccionada.Nrc + " ha sido eliminada",
                            "Datos eliminados con exito",
                            MessageBoxButton.OK);
                    }

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message,
                        "Error en la conexión con la base de datos",
                        MessageBoxButton.OK);
                }
                
                AdministrarEE administrarEE = new AdministrarEE();
                NavigationService.Navigate(administrarEE);
            }
        }

        private bool IsExperienciaSelectNull()
        {
            bool result = false;
            if (DgExperiencias.SelectedItem == null)
            {
                MessageBox.Show("Para modificar o eliminar una experiencia educativa primero debe seleccionarla de la tabla",
                    "Seleccione una experiencia educativa",
                    MessageBoxButton.OK);
            }
            else
            {
                result = true;
            }
            return result;  
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
            ExperienciaEducativaRepository experienciaEducativaRepository =
                new ExperienciaEducativaRepository(new TutoriasContext());
            List<Experiencia_Educativa> experiencisResult = new List<Experiencia_Educativa>();
            try
            {
                experiencisResult = experienciaEducativaRepository.GetExperienciasEducativas();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,
                    "No hay conexión a la base de datos en estos momentos",
                    MessageBoxButton.OK);
            }
            foreach (Experiencia_Educativa experienciaEducativa in experiencisResult) ExperienciasObservables.Add(experienciaEducativa);
        }

    }
}