using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Sistema_De_Tutorias.Views;

public partial class ConsultarSolucionProblematicaAcademica : Window
{
    public ConsultarSolucionProblematicaAcademica()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}