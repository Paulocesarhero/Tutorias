using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Tutorias.Service.DatabaseContext;

namespace Sistema_De_Tutorias.Views;

public partial class AdministrarEEListado : Window
{
    public Catedratico _catedratico { get; set; }
    public AdministrarEEListado()
    {
        InitializeComponent();
    }

    public AdministrarEEListado(Catedratico catedraticoSeleccionado)
    {
        InitializeComponent();
        _catedratico = catedraticoSeleccionado;
        fillEE();
    }

    public void fillEE()
    {
        dg_EE.Items = _catedratico.experienciasEducativas;
    }
}