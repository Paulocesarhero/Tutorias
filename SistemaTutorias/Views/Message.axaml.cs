using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Sistema_De_Tutorias.Views;

public partial class Message : Window
{
    public string cabezera { get; set; }
    public string contenido { get; set; }

    public Message()
    {
        InitializeComponent();
        fillContent();

    }

    public void fillContent()
    {
        block_titulo.Text = cabezera;
        block_contenido.Text = contenido;
    }

   
}