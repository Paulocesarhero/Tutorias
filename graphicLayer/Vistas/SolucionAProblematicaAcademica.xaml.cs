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
    /// Lógica de interacción para SolucionAProblematicaAcademica.xaml
    /// </summary>
    public partial class SolucionAProblematicaAcademica : Page
    {
        public Problematica _Problematica { get; set; }
        public SolucionAProblematicaAcademica()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (TbTitulo.Text != "" && TbDescripcionSolucion.Text != "")
            {
                Solucion sol = new Solucion()
                {
                    Problematica = _Problematica,
                    Descripcion = TbDescripcionSolucion.Text,
                    Fecha = Convert.ToDateTime(TbFecha.Text),
                    Titulo = TbTitulo.Text
                };
                SaveSolucion(sol);
                
            }
            else
            {
                MessageBox.Show("Campos vacios", "tiene que llenar los campos de titulo y descripción antes",
                    MessageBoxButton.OK);
            }
        }

        private void SaveSolucion(Solucion solucion)
        {
            bool result;
            TutoriaManagement tutoria = new TutoriaManagement();
            try
            {
               result = tutoria.AddSolucion(solucion);
               if (result)
               {
                   this.NavigationService.GoBack();
               }
               else
               {
                   MessageBox.Show("Ha ocurrido un error",
                       "No se ha podido guardar la solución a la problematica",
                       MessageBoxButton.OK);
               }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error en la conexión con la base de datos",
                    "No hay conexión a la base de datos en estos momentos",
                    MessageBoxButton.OK);
                Console.WriteLine(e.StackTrace);
            }

        }
    }
}
