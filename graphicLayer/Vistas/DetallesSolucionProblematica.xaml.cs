using DataAccess.BussinesLogic.EntityRepository;
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
    /// Lógica de interacción para DetallesSolucionProblematica.xaml
    /// </summary>
    public partial class DetallesSolucionProblematica : Page
    {
        Problematica problematicaRecibida = new Problematica();

        public DetallesSolucionProblematica(Problematica fila)
        {
            InitializeComponent();
            fillData(fila);
        }

        private void fillData(Problematica fila)
        {
            problematicaRecibida = fila;
            Solucion objetoSolucion = problematicaRecibida.Solucion;
            tBoxDescripcionProblematica.Text = fila.Descripcion;
            lblIncidencias.Content = fila.NumAlumnos;
            lblEE.Content = fila.ExperienciaEducativa.Nombre;
            lblDocente.Content = fila.ExperienciaEducativa.Catedratico.NombreCompleto;
            lblNRC.Content = fila.ExperienciaEducativa.Nrc;
            tBoxSolucionProblematica.Text = objetoSolucion.Descripcion;
            lblTitulo.Content = objetoSolucion.Titulo;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
