using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using Tutorias.BussinesLogic.Management;
using Tutorias.Service.DatabaseContext;

namespace graphicLayer.Vistas
{
    /// <summary>
    /// Lógica de interacción para AsignarTutorAEstudiante.xaml
    /// </summary>
    public partial class AsignarTutorAEstudiante : Page
    {
        public AsignarTutorAEstudiante()
        {
            InitializeComponent();
            fillTutores();
        }

        private void fillTutores()
        {
            TutoriaManagement tutoriaManagement = new TutoriaManagement();
            DgTutores.ItemsSource = tutoriaManagement.GetTutorAcademicosWithTutoradosCount();            
        }

        private void AsignarTutor(object sender, RoutedEventArgs e)
        {
            var fila = (Tutor_Academico)DgTutores.SelectedItem;
            SecondWindow_AsignarTutorAEstudiante secondWindow = new SecondWindow_AsignarTutorAEstudiante(fila);
            this.NavigationService.Navigate(secondWindow);
        }

    }
}
