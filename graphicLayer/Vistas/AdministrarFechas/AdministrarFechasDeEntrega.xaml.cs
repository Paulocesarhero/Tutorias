using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
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
using Microsoft.EntityFrameworkCore;
using Tutorias.Service.DatabaseContext;

namespace graphicLayer.Vistas.AdministrarFechas
{
    /// <summary>
    /// Lógica de interacción para AdministrarFechasDeEntrega.xaml
    /// </summary>
    public partial class AdministrarFechasDeEntrega : Page
    {
        private List<Periodo_Escolar> PeriodoEscolares { get; set; } = new List<Periodo_Escolar>();
        private Fecha_De_Tutoria PrimeraFechaDeTutoria { get; set; } = new Fecha_De_Tutoria();
        private Fecha_De_Tutoria SegundaFechaDeTutoria { get; set; } = new Fecha_De_Tutoria();
        private Fecha_De_Tutoria TerceraFechaDEtutoria { get; set; } = new Fecha_De_Tutoria();

        
        public AdministrarFechasDeEntrega()
        {
            InitializeComponent();
        }

        private void BtnNewPeriodo_Click(object sender, RoutedEventArgs e)
        {
            AgregarPeriodoEscolar agregarPeriodoEscolar = new AgregarPeriodoEscolar();
            NavigationService.Navigate(agregarPeriodoEscolar);
        }

        private void BtnModPeriodo_Click(object sender, RoutedEventArgs e)
        {
            if (CbPeriodosEscolares.SelectedItem != null)
            {
                AgregarPeriodoEscolar agregarPeriodoEscolar = new AgregarPeriodoEscolar();
                agregarPeriodoEscolar._PeriodoEscolar = CbPeriodosEscolares.SelectedItem as Periodo_Escolar;
                NavigationService.Navigate(agregarPeriodoEscolar);
            }
           
        }

        public void FillPeriodosEscolares()
        {
            PeriodoEscolarRepository periodoEscolarRepository = new PeriodoEscolarRepository(new TutoriasContext());
            try
            {
                PeriodoEscolares = periodoEscolarRepository.GetAllPeriodosEscolar();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message,
                    "Error en la conexión con la base de datos",
                    MessageBoxButton.OK);
            }
            CbPeriodosEscolares.ItemsSource = PeriodoEscolares.OrderBy(x => x.FechaDeInicio).ToList();
        }

        private void CbPeriodosEscolares_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Periodo_Escolar periodoSeleccionado = CbPeriodosEscolares.SelectedItem as Periodo_Escolar;
            List<Fecha_De_Tutoria> fechasDeTutoriasDelPeriodo = periodoSeleccionado.FechasDeTutorias.ToList();
            if (fechasDeTutoriasDelPeriodo.Count != 0)
            {
                PrimeraFechaDeTutoria = fechasDeTutoriasDelPeriodo.First();
                SegundaFechaDeTutoria = fechasDeTutoriasDelPeriodo.ElementAt(1);
                TerceraFechaDEtutoria = fechasDeTutoriasDelPeriodo.ElementAt(2);

                DpFirst.Text = PrimeraFechaDeTutoria.FechaDeCierre.ToShortDateString();
                DpSecond.Text = SegundaFechaDeTutoria.FechaDeCierre.ToShortDateString();
                DpThird.Text = TerceraFechaDEtutoria.FechaDeCierre.ToShortDateString();

                DpFirstOpen.Text = PrimeraFechaDeTutoria.FechaDeInicionSesion.ToShortDateString();
                DpSecondOpen.Text = SegundaFechaDeTutoria.FechaDeInicionSesion.ToShortDateString();
                DpThirdOpen.Text = TerceraFechaDEtutoria.FechaDeInicionSesion.ToShortDateString();
            }
            else
            {
                DpFirst.Text = "";
                DpSecond.Text = "";
                DpThird.Text = "";

                DpFirstOpen.Text = "";
                DpSecondOpen.Text = "";
                DpThirdOpen.Text = "";
            }

            PrimeraFechaDeTutoria.PeriodoEscolar = periodoSeleccionado;
            SegundaFechaDeTutoria.PeriodoEscolar = periodoSeleccionado;
            TerceraFechaDEtutoria.PeriodoEscolar = periodoSeleccionado;

        }

        private void BtnSaveDates_Click(object sender, RoutedEventArgs e)
        {
            FechaDeTutoriaRepository fechaDeTutoriaRepository = new FechaDeTutoriaRepository(new TutoriasContext());
           
            if (AreValidateDates() && IsInPeriodos() && NotNullValues())
            {
                try
                {
                    PrimeraFechaDeTutoria.FechaDeCierre = Convert.ToDateTime(DpFirst.Text);
                    PrimeraFechaDeTutoria.FechaDeInicionSesion = Convert.ToDateTime(DpFirstOpen.Text);
                    PrimeraFechaDeTutoria.NumDeTutoria = 1;
                    SegundaFechaDeTutoria.FechaDeCierre = Convert.ToDateTime(DpSecond.Text);
                    SegundaFechaDeTutoria.FechaDeInicionSesion = Convert.ToDateTime(DpSecondOpen.Text);
                    SegundaFechaDeTutoria.NumDeTutoria = 2;
                    TerceraFechaDEtutoria.FechaDeCierre = Convert.ToDateTime(DpThird.Text);
                    TerceraFechaDEtutoria.FechaDeInicionSesion = Convert.ToDateTime(DpThirdOpen.Text);
                    TerceraFechaDEtutoria.NumDeTutoria = 3;
                    fechaDeTutoriaRepository.AddFechaDeTutoria(PrimeraFechaDeTutoria);
                    fechaDeTutoriaRepository.AddFechaDeTutoria(SegundaFechaDeTutoria);
                    fechaDeTutoriaRepository.AddFechaDeTutoria(TerceraFechaDEtutoria);
                    MessageBox.Show("Registro exitoso",
                        "Las fechas de tutorias se han registrado con exito",
                        MessageBoxButton.OK);

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message,
                        "Error en la conexión con la base de datos",
                        MessageBoxButton.OK);
                }

            }
            else
            {
               
                    MessageBox.Show("La primera fecha debe ser después del inicio del período escolar y la última antes de que termine" +
                                    " el período escolar. Además, las fechas de tutorías deben ser consecutivas y no superponerse entre sí.",
                        "Revise los campos",
                        MessageBoxButton.OK);
                

            }
        }

        private bool NotNullValues()
        {
            return DpFirst.Text != "" || DpSecond.Text != "" ||
                DpThird.Text != "" || DpFirstOpen.Text != "" || 
                DpSecondOpen.Text != "" || DpThird.Text != "" ;
        }

        private bool AreValidateDates()
        {
            if (PrimeraFechaDeTutoria.FechaDeCierre > SegundaFechaDeTutoria.FechaDeCierre
                || SegundaFechaDeTutoria.FechaDeCierre > TerceraFechaDEtutoria.FechaDeCierre)
            {
                return false;
            }

            if (PrimeraFechaDeTutoria.FechaDeCierre > PrimeraFechaDeTutoria.FechaDeInicionSesion
                || SegundaFechaDeTutoria.FechaDeCierre > SegundaFechaDeTutoria.FechaDeInicionSesion
                || TerceraFechaDEtutoria.FechaDeCierre > TerceraFechaDEtutoria.FechaDeInicionSesion)
            {
                return false;
            }
            return true;
        }

        private bool IsInPeriodos()
        {
            Periodo_Escolar periodoSeleccionado = CbPeriodosEscolares.SelectedItem as Periodo_Escolar;

            if (periodoSeleccionado == null)
            {
                return false;
            }
            if (PrimeraFechaDeTutoria.FechaDeCierre < periodoSeleccionado.FechaDeInicio || TerceraFechaDEtutoria.FechaDeCierre > periodoSeleccionado.FechaDeFin)
            {
                return false;
            }
            return true;
        }
    }
}
