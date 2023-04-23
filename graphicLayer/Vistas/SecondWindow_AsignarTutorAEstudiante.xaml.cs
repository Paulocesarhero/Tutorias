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
    /// Lógica de interacción para SecondWindow_AsignarTutorAEstudiante.xaml
    /// </summary>
    public partial class SecondWindow_AsignarTutorAEstudiante : Page
    {
        public SecondWindow_AsignarTutorAEstudiante(Tutor_Academico fila)
        {
            InitializeComponent();
            fillData(fila);
        }
        
        public void fillData(Tutor_Academico fila)
        {
            lblTutorName.Content = fila.Nombres;
            fillTable();
        }

        public void fillTable()
        {
            TutoriaManagement tutoriaManagement = new TutoriaManagement();
            //DgEstudiantes.ItemsSource = tutoriaManagement.GetEstudiantesWithoutTutorAcademico();
        }

    }
}
