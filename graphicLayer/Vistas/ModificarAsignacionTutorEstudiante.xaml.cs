using DataAccess.BussinesLogic.EntityRepository;
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
using Tutorias.Service.DatabaseContext;

namespace graphicLayer.Vistas
{
    public partial class ModificarAsignacionTutorEstudiante : Page
    {
        private object seleccionAnterior;

        public ModificarAsignacionTutorEstudiante()
        {
            InitializeComponent();
            fillTutores();
            btnReasignar.IsEnabled = false;
        }

        private void fillTutores()
        {            
            TutorAcademicoRepository tutorRepository = new TutorAcademicoRepository(new TutoriasContext());
            List<Tutor_Academico> tutores = tutorRepository.GetAllTutorAcademico();
            List<Tutor_Academico> tutoresAComboBox = new List<Tutor_Academico>();

            foreach (Tutor_Academico tutor in tutores)
            {
                tutor.Nombres = tutor.Nombres + " " + tutor.Apellidos;
                tutoresAComboBox.Add(tutor);
            }
            cbTutores.ItemsSource = tutoresAComboBox;
            cbTutoresAElegir.ItemsSource = tutoresAComboBox;
        }       

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {            
            EstudianteRepository estudianteRepository = new EstudianteRepository(new TutoriasContext());
            Tutor_Academico? tutorSelected = new Tutor_Academico();
            List<Estudiante> estudiantes = new List<Estudiante>();

            if (DgTutorados.ItemsSource == null || !DgTutorados.ItemsSource.OfType<Estudiante>().Any())
            {
                if (cbTutores.SelectedItem != null)
                {
                    tutorSelected = cbTutores.SelectedItem as Tutor_Academico;
                    estudiantes = estudianteRepository.getEstudiantesWithTutor(tutorSelected);

                    if (!(estudiantes.Count == 0))
                    {
                        DgTutorados.ItemsSource = estudiantes;
                        btnReasignar.IsEnabled = true;
                    }
                    else
                    {
                        MessageBox.Show("El tutor seleccionado no tiene aún tutorado por mostrar");
                        cbTutores.SelectedItem = null;
                    }

                }
                else
                {
                    MessageBox.Show("Selecciona un tutor de la lista. ");
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de buscar nuevos Tutorados? Los datos en la tabla se borrarán.", "Confirmar", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    if (cbTutores.SelectedItem != null)
                    {
                        tutorSelected = cbTutores.SelectedItem as Tutor_Academico;
                        estudiantes = estudianteRepository.getEstudiantesWithTutor(tutorSelected);

                        if (!(estudiantes.Count == 0))
                        {
                            DgTutorados.ItemsSource = estudiantes;
                            btnReasignar.IsEnabled = true;
                        }
                        else
                        {
                            MessageBox.Show("El tutor seleccionado no tiene aún tutorado por mostrar");
                            cbTutores.SelectedItem = null;
                            DgTutorados.ItemsSource = null;
                            btnReasignar.IsEnabled = false;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Selecciona un tutor de la lista. ");
                    }
                }
                else
                {
                    cbTutores.SelectedItem = seleccionAnterior;
                }
            }
        }

        private void btnReasignar_Click(object sender, RoutedEventArgs e)
        {
            List<Estudiante> estudiantesSeleccionados = new List<Estudiante>();
            EstudianteRepository estudianteRepository = new EstudianteRepository(new TutoriasContext());            
            bool result = false;
            int resultCount = 0;

            if (cbTutoresAElegir.SelectedItem != null && )
            {
                if ((cbTutoresAElegir.SelectedItem as Tutor_Academico).Id == (cbTutores.SelectedItem as Tutor_Academico).Id)
                {
                    MessageBox.Show("Elige un tutor diferente al que ya tiene asignado el Tutorado");
                }
                else
                {
                    int idNuevoTutorElegido = (cbTutoresAElegir.SelectedItem as Tutor_Academico).Id;

                    foreach (var item in DgTutorados.ItemsSource)
                    {
                        var checkbox = DgTutorados.Columns[0].GetCellContent(item) as CheckBox;

                        if (checkbox.GetValue != null)
                        {
                            if (checkbox.IsChecked == true)
                            {
                                (item as Estudiante).IdTutorAcademico = idNuevoTutorElegido;
                                estudiantesSeleccionados.Add(item as Estudiante);
                            }
                        }
                    }

                    for (int i = 0; i < estudiantesSeleccionados.Count(); i++)
                    {
                        result = estudianteRepository.UpdateTutorToEstudiante(estudiantesSeleccionados[i]);
                        if (result)
                        {
                            resultCount++;
                        }
                        result = false;
                    }

                    if (estudiantesSeleccionados.Count() == resultCount)
                    {
                        MessageBox.Show("Reasignación de Tutor exitosa");
                        btnReasignar.IsEnabled = false;
                        cbTutores.SelectedItem = null;
                        cbTutoresAElegir.SelectedItem = null;
                        DgTutorados.ItemsSource = null;
                    }
                    else if (resultCount == 0)
                    {
                        MessageBox.Show("Hubo un error en la base de datos");
                    }
                    else if (resultCount > 0 && resultCount != estudiantesSeleccionados.Count())
                    {
                        MessageBox.Show("Se lograron reasignar algunos Tutorados, pero no todos.");
                        btnReasignar.IsEnabled = false;
                    }
                }                
            }
            else
            {
                MessageBox.Show("Selecciona un nuevo Tutor a reasignar");
            }
        }

        private void cbTutores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            seleccionAnterior = e.RemovedItems.Count > 0 ? e.RemovedItems[0] : null;
        }
    }
}
