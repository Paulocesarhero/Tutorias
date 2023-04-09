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
        public ObservableCollection<Experiencia_Educativa> ExperienciasObservables { get; set; } =
            new ObservableCollection<Experiencia_Educativa>();

        public ObservableCollection<Catedratico> CatedraticosDisponiblesObservables { get; set; } =
            new ObservableCollection<Catedratico>();
        public AdministrarEE()
        {
            InitializeComponent();
            DataContext = this;
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

        public void FillCatedraticos()
        {
            TutoriaManagement tutoriaManagement = new TutoriaManagement();
            List<Catedratico> catedraticos = new List<Catedratico>();
            try
            {
                catedraticos = tutoriaManagement.GetCatedraticos();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            foreach (Catedratico catedratico in catedraticos) CatedraticosDisponiblesObservables.Add(catedratico);
        }
    }
}