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
using DataAccess.BussinesLogic.EntityRepository;
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
            
            FillAcademias();
            FillCatedraticos();
            FillProgramasEducativos();
            if (ExperienciaEducativaSelect != null)
            {
                FillExperienciaEducativa();
            }
        }

        private void FillExperienciaEducativa()
        {
            TbNrc.IsReadOnly = true;
            TbNombre.Text = ExperienciaEducativaSelect.Nombre;
            TbNrc.Text = ExperienciaEducativaSelect.Nrc;
            CbAcademia.Text = ExperienciaEducativaSelect.Academia.NombreAcademia;
            CbProgramaEducativo.Text = ExperienciaEducativaSelect.ProgramaEducativo.ProgramaEducativo;
            CbCatedratico.Text = ExperienciaEducativaSelect.Catedratico.NombreCompleto;

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            ExperienciaEducativaRepository experienciaEducativaRepository = new ExperienciaEducativaRepository(new TutoriasContext()) ;
            InitExperienciaEducativa();
            try
            {
                if (experienciaEducativaRepository.AddExperienciaEducativa(ExperienciaEducativaSelect))
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
            CatedraticoRepository catedraticoRepository = new CatedraticoRepository(new TutoriasContext());
            try
            {
                CbCatedratico.ItemsSource = catedraticoRepository.GetAllCatedraticosWithEE();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error en la conexión con la base de datos",
                    "No hay conexión a la base de datos en estos momentos",
                    MessageBoxButton.OK);
            }
        }

        private void FillAcademias()
        {
            AcademiaRepository academiaRepository = new AcademiaRepository(new TutoriasContext());
            try
            {
                CbAcademia.ItemsSource = academiaRepository.getAllAcademias();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,
                    "No hay conexión a la base de datos en estos momentos",
                    MessageBoxButton.OK);
            }
        }

        private void FillProgramasEducativos()
        {
            ProgramaEducativoRepository programaEducativoRepository =
                new ProgramaEducativoRepository(new TutoriasContext());
            try
            {
                CbProgramaEducativo.ItemsSource = programaEducativoRepository.GetProgramasEducativos();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,
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
