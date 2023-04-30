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
using System.Windows.Controls;
using Tutorias.Service.DatabaseContext;
using DataAccess.BussinesLogic.EntityRepository;

namespace graphicLayer.Vistas
{
   
    public partial class SecondWindow_AsignarTutorAEstudiante : Page
    {
        
        Tutor_Academico objetoTutorRecibido = new Tutor_Academico();

        public SecondWindow_AsignarTutorAEstudiante(Tutor_Academico fila)
        {
            InitializeComponent();
            fillData(fila);
        }        

        public void fillData(Tutor_Academico fila)
        {
            objetoTutorRecibido = fila;
            lblTutorName.Content = objetoTutorRecibido.Nombres;
            fillTable();
        }

        public void fillTable()
        {
            EstudianteRepository estudianteRepository = new EstudianteRepository(new TutoriasContext());
            DgEstudiantes.ItemsSource = estudianteRepository.findEstudiantesWithOutTutor();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }        

        private void btnCreateAssignment_Click_1(object sender, RoutedEventArgs e)
        {
            int counterStudentsOnTutor = Int32.Parse(objetoTutorRecibido.Apellidos);
            bool resultAssignment = false;
            int counter = 0;

            List<Estudiante> estudiantesSeleccionados = new List<Estudiante>();

            foreach (var item in DgEstudiantes.ItemsSource)
            {
                var checkBox = DgEstudiantes.Columns[0].GetCellContent(item) as CheckBox;

                if (checkBox.GetValue != null)
                {
                    if (checkBox.IsChecked == true)
                    {
                        estudiantesSeleccionados.Add(item as Estudiante);
                    }
                }
            }

            if ((counterStudentsOnTutor+estudiantesSeleccionados.Count()) > 30)
            {
                MessageBox.Show("El tutor " + objetoTutorRecibido.Nombres + " tiene " + counterStudentsOnTutor 
                    + " tutorados. El límite de tutorados es de 6, no puedes exceder el límite (" 
                    + estudiantesSeleccionados.Count()+counterStudentsOnTutor + ").");
            }
            else
            {
                EstudianteRepository estudianteRepository = new EstudianteRepository(new TutoriasContext());
                for (int i = 0; i < estudiantesSeleccionados.Count(); i++)
                {
                    estudiantesSeleccionados[i].IdTutorAcademico = objetoTutorRecibido.Id;
                    resultAssignment = estudianteRepository.updateAssignmentTutorToStudent(estudiantesSeleccionados[i]);
                    if (resultAssignment == true)
                    {
                        counterStudentsOnTutor++;
                        counter++;
                    }
                }

                if (estudiantesSeleccionados.Count() == counter)
                {
                    MessageBox.Show("Asignación de estudiantes exitosa");
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al hacer la asignación de los tutores.");
                }
            }
        }


    }
}