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
using Tutorias.BussinesLogic.Management;
using Tutorias.Service.DatabaseContext;

namespace graphicLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            fillList();
            DataContext = new MyViewModel();
        }
        public void fillList()
        {
        }
        public class MyViewModel
        {
            public ObservableCollection<Periodo_Escolar> MyItems { get; set; }

            public MyViewModel()
            {
                TutoriaManagement tu
                = new TutoriaManagement();
                MyItems = new ObservableCollection<Periodo_Escolar>(tu.getPeriodosEscolares());
            }
        }
    }
}
