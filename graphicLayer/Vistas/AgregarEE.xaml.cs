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
using Tutorias.BussinesLogic.Management;
using Tutorias.Service.DatabaseContext;

namespace graphicLayer.Vistas
{
    /// <summary>
    /// Lógica de interacción para AgregarEE.xaml
    /// </summary>
    public partial class AgregarEE : Page
    {
        public Experiencia_Educativa ExperienciaEducativaSelect { get; set; }
        public AgregarEE()
        {
            InitializeComponent();
        }

        public void FillData()
        {
            if (ExperienciaEducativaSelect != null)
            {
                FillExperienciaEducativa();
            }
            FillAcademias();
            FillCatedraticos();
            FillProgramasEducativos();
        }

        private void FillExperienciaEducativa()
        {
            TbNombre.Text = ExperienciaEducativaSelect.Nombre;
            TbNrc.Text = ExperienciaEducativaSelect.Nrc;
            CbAcademia.Text = ExperienciaEducativaSelect.Academia.NombreAcademia;
            CbProgramaEducativo.Text = ExperienciaEducativaSelect.ProgramaEducativo.ProgramaEducativo;
            CbCatedratico.Text = ExperienciaEducativaSelect.Catedratico.NombreCompleto;

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            TutoriaManagement tutoriaManagement = new TutoriaManagement();
            InitExperienciaEducativa();
            try
            {
                if (tutoriaManagement.AddExperienciaEducativa(ExperienciaEducativaSelect))
                {
                    MessageBox.Show("Datos guardados con exito","La base de datos ha sido actualizada",
                        MessageBoxButton.OK);
                    AdministrarEE administrarEe = new AdministrarEE();
                    NavigationService.Navigate(administrarEe);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, 
                    "Error en la conexión con la base de datos",
                    MessageBoxButton.OK);
            }
            
            
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void InitExperienciaEducativa()
        {
            if (ExperienciaEducativaSelect == null)
            {
                ExperienciaEducativaSelect = new Experiencia_Educativa()
                {
                    Academia = CbAcademia.SelectionBoxItem as Academia,
                    Catedratico = CbCatedratico.SelectionBoxItem as Catedratico,
                    Nombre = TbNombre.Text,
                    Nrc = TbNrc.Text,
                    ProgramaEducativo = CbProgramaEducativo.SelectionBoxItem as Programa_Educativo
                };
            }
            else
            {
                ExperienciaEducativaSelect.Academia = CbAcademia.SelectionBoxItem as Academia;
                ExperienciaEducativaSelect.Catedratico = CbCatedratico.SelectionBoxItem as Catedratico;
                ExperienciaEducativaSelect.Nombre = TbNombre.Text;
                ExperienciaEducativaSelect.Nrc = TbNrc.Text;
                ExperienciaEducativaSelect.ProgramaEducativo = CbProgramaEducativo.SelectionBoxItem as Programa_Educativo;
            }
        }

        private void FillCatedraticos()
        {
            TutoriaManagement tutoriaManagement = new TutoriaManagement();
            try
            {
                CbCatedratico.ItemsSource = tutoriaManagement.GetCatedraticos();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error en la conexión con la base de datos",
                    "No hay conexión a la base de datos en estos momentos",
                    MessageBoxButton.OK);
            }
            tutoriaManagement.GetCatedraticos();
        }

        private void FillAcademias()
        {
            TutoriaManagement tutoriaManagement = new TutoriaManagement();
            try
            {
                CbAcademia.ItemsSource = tutoriaManagement.GetAcademias();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error en la conexión con la base de datos",
                    "No hay conexión a la base de datos en estos momentos",
                    MessageBoxButton.OK);
            }
        }

        private void FillProgramasEducativos()
        {
            TutoriaManagement tutoriaManagement = new TutoriaManagement();
            try
            {
                CbProgramaEducativo.ItemsSource = tutoriaManagement.GetProgramasEducativos();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error en la conexión con la base de datos",
                    "No hay conexión a la base de datos en estos momentos",
                    MessageBoxButton.OK);
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !AreAllValidNumericChars(e.Text);
        }
        private static bool AreAllValidNumericChars(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = textBox.Text.ToUpper();
            textBox.CaretIndex = textBox.Text.Length;
        }
    }
}
