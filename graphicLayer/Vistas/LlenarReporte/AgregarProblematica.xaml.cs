using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataAccess.BussinesLogic.EntityRepository;
using Sistema_De_Tutorias.Utility;
using Tutorias.Service.DatabaseContext;

namespace graphicLayer.Vistas.LlenarReporte
{
    /// <summary>
    /// Lógica de interacción para AgregarProblematica.xaml
    /// </summary>
    public partial class AgregarProblematica : Page
    {
        public ObservableCollection<Experiencia_Educativa> ExperienciaEducativas { get; set; } =
            new ObservableCollection<Experiencia_Educativa>();

        public Reporte_De_Tutoria reporteDeTutoria { get; set; }

        public Tutor_Academico TutorAcademico { get; set; }
        public Fecha_De_Tutoria fecha { get; set; }

        public int NumTutorados { get; set; }

        public AgregarProblematica()
        {
            InitializeComponent();
            DataContext = new AgregarProblematicasViewModel();

        }

        public void FillData()
        {
            TbNumeroDeAlumnosAfectados.MaxLength = 3;
            TbNumeroDeAlumnosAfectados.SetValue(RangeBase.MinimumProperty, Convert.ToDouble(1));
            TbNumeroDeAlumnosAfectados.SetValue(RangeBase.MaximumProperty,
                Convert.ToDouble(TutorAcademico.Estudiantes.Count));
            EstudianteRepository estudianteRepository = new EstudianteRepository(new TutoriasContext());
            TutorAcademicoRepository tutorAcademicoRepository = new TutorAcademicoRepository(new TutoriasContext());
            Tutor_Academico tutorAcademico = new Tutor_Academico();
            try
            {
                tutorAcademico = tutorAcademicoRepository.GetTutorAcademicoByUser(CredencialesUsuario.Instance.Usuario);

                NumTutorados = estudianteRepository.GetEstudiantesByTutorAcademico(tutorAcademico).ToList().Count;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,
                    "Error en la conexión con la base de datos",
                    MessageBoxButton.OK);
            }


        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            TutoriasContext tutoriasContext = new TutoriasContext();
            ProblematicaRepository problematicaRepository = new ProblematicaRepository(tutoriasContext);
            ReporteDeTutoriaRepository reporteDeTutoriaRepository =
                new ReporteDeTutoriaRepository(tutoriasContext);
            Reporte_De_Tutoria reporteDeTutoria = reporteDeTutoriaRepository.GetReporteDeTutoria(TutorAcademico, fecha);
            Problematica problematicatoAdd = new Problematica()
            {
                Descripcion = TbDescripcion.Text,
                NumAlumnos = Convert.ToInt32(TbNumeroDeAlumnosAfectados.Text),
                ExperienciaEducativa = CbExperienciaEducativa.SelectedItem as Experiencia_Educativa,
                ReporteDeTutoria = reporteDeTutoria
            };
            try
            {
                problematicaRepository.AddProblematica(problematicatoAdd);
                MessageBox.Show("Problematica registrada",
                    "Problematica registrada",
                    MessageBoxButton.OK);

                LlenarProblematicas llenarProblematicas = new LlenarProblematicas();
                llenarProblematicas.FechaDeTutoria = fecha;
                llenarProblematicas.TutorAcademico = TutorAcademico;
                llenarProblematicas.FillProblematicas();
                this.NavigationService.Navigate(llenarProblematicas);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,
                    "Error en la conexión con la base de datos",
                    MessageBoxButton.OK);
            }

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^1-9]+");
            if (regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }

        private void txtNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = TbNumeroDeAlumnosAfectados.Text;

            if (text != "")
            {
                if (!int.TryParse(text, out int number) || number < 1 || number > NumTutorados)
                {
                    int caretIndex = TbNumeroDeAlumnosAfectados.CaretIndex;
                    TbNumeroDeAlumnosAfectados.Text = text.Substring(0, caretIndex - 1) + text.Substring(caretIndex);
                    TbNumeroDeAlumnosAfectados.CaretIndex = caretIndex - 1;
                }
            }
            
        }
    }

    public class AgregarProblematicasViewModel
        {
            public ObservableCollection<Experiencia_Educativa> ExperienciaEducativas { get; set; } =
                new ObservableCollection<Experiencia_Educativa>();

            public AgregarProblematicasViewModel()
            {
                FillExperienciasEducativas();
            }

            private void FillExperienciasEducativas()
            {
                ExperienciaEducativaRepository experienciaEducativaRepository =
                    new ExperienciaEducativaRepository(new TutoriasContext());
                List<Experiencia_Educativa> result = new List<Experiencia_Educativa>();
                try
                {
                    result = experienciaEducativaRepository.GetExperienciasEducativas();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message,
                        "Error en la conexión con la base de datos",
                        MessageBoxButton.OK);
                }

                foreach (Experiencia_Educativa exp in result) ExperienciaEducativas.Add(exp);
            }
        }
}
