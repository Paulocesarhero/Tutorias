using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Sistema_De_Tutorias.Views;

public partial class MenuCoordinadoraDeTutorias : Window
{
    public MenuCoordinadoraDeTutorias()
    {
        InitializeComponent();

    }


    private void abrirAdminEE(object? sender, RoutedEventArgs e)
    {
        AdministrarEE administrarEe = new AdministrarEE();
        administrarEe.Show();
        this.Close();
        
    }
}