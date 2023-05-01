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
using Tutorias.Service.DatabaseContext;

namespace graphicLayer.Vistas
{
    /// <summary>
    /// Lógica de interacción para AgregarCatedratico.xaml
    /// </summary>
    public partial class AgregarCatedratico : Page
    {
        public Catedratico _catedratico { get; set; }
        private bool IsUpdate { get; set; } 
        public AgregarCatedratico()
        {
            InitializeComponent();
        }

        public void FillData()
        {
            TbNombre.Text = _catedratico.NombreCompleto;
            IsUpdate = true;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (TbNombre.Text != "")
            {
                CatedraticoRepository catedraticoRepository = new CatedraticoRepository(new TutoriasContext());
                if (IsUpdate)
                {
                    try
                    {
                        _catedratico.NombreCompleto = TbNombre.Text;
                        catedraticoRepository.UpdateCatedratico(_catedratico);
                        AdministrarCatedraticos administrarCatedraticos = new AdministrarCatedraticos();
                        administrarCatedraticos.FillData();
                        NavigationService.Navigate(administrarCatedraticos);
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
                    try
                    {
                        Catedratico catedraticoNew = new Catedratico()
                        {
                            NombreCompleto = TbNombre.Text
                        };
                        catedraticoRepository.AddCatedratico(catedraticoNew);
                        AdministrarCatedraticos administrarCatedraticos = new AdministrarCatedraticos();
                        administrarCatedraticos.FillData();
                        NavigationService.Navigate(administrarCatedraticos);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message,
                            "Error en la conexión con la base de datos",
                            MessageBoxButton.OK);
                    }
                }
            }
            else
            {
                
                    MessageBox.Show("Llene el campo de catedratico para poder continuar",
                        "Llene los campos necesarios",
                        MessageBoxButton.OK);
                

            }

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            AdministrarCatedraticos administrarCatedraticos = new AdministrarCatedraticos();
            administrarCatedraticos.FillData();
            NavigationService.Navigate(administrarCatedraticos);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.Text = textBox.Text.ToUpper();
            textBox.CaretIndex = textBox.Text.Length;
        }
    }
}
