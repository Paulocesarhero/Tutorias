using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Tutorias.BussinesLogic.Management;
using Tutorias.Service.DatabaseContext;

namespace Sistema_De_Tutorias.Views;

public partial class AdministrarEE : Window
{

    ObservableCollection<Catedratico> catedraticos = new ObservableCollection<Catedratico>();

    public AdministrarEE()
    {
        InitializeComponent();
        fillCatedratico();
        btn_verExperienciasEducativas.IsEnabled = false;
    }

    private void Button_OnClick_AddCatedratico(object? sender, RoutedEventArgs e)
    {
        catedraticos.Add(new Catedratico
        {
            nombreCompleto = ""
        });
        dg_Catedraticos.Items = catedraticos;
        dg_Catedraticos.InvalidateVisual();
    }


    private void fillCatedratico()
    {
        TutoriaManagement tutoriaManagement = new TutoriaManagement();
        try
        {
            catedraticos = new ObservableCollection<Catedratico>(tutoriaManagement.getCatedraticos());
        }
        catch (Exception e)
        {
            Message message = new Message();
            message.block_contenido.Text = "Ha ocurrido un error al conectarse a la bd";
            message.block_titulo.Text = "ERROR";
            message.Show();
        }

        dg_Catedraticos.Items = catedraticos;

    }

    private void Button_OnClick_OpenExperienciasEducativas(object? sender, RoutedEventArgs e)
    {
        Catedratico catedraticoSeleccionado = (Catedratico)dg_Catedraticos.SelectedItem;
        AdministrarEEListado administrarEeListado = new AdministrarEEListado(catedraticoSeleccionado);
        administrarEeListado.Show();
        this.Close();
    }

    private void Dg_Catedraticos_OnCellPointerPressed(object? sender, DataGridCellPointerPressedEventArgs e)
    {
        btn_verExperienciasEducativas.IsEnabled = true;
    }

    private void Button_OnClick_Save(object? sender, RoutedEventArgs e)
    {
        bool hasNullValue = false;
        List<Catedratico> catedraticosList = dg_Catedraticos.Items.Cast<Catedratico>().ToList();
        foreach (var cat in catedraticosList)
        {
            if (cat.nombreCompleto == "")
            {
                hasNullValue = true;
            }
        }

        if (hasNullValue)
        {
            Message message = new Message();
            message.block_contenido.Text = "Campos vacios";
            message.block_titulo.Text = "llene todos los campos";
            message.Show();
        }
        else
        {
            updateCatedraticos(catedraticosList);
        }
    }

    public bool updateCatedraticos(List<Catedratico> catedraticos)
    {
        TutoriaManagement tutoriaManagement = new TutoriaManagement();
        bool resultado = false; 
        try
        {
            resultado = tutoriaManagement.AddCatedraticos(catedraticos);
        }
        catch (Exception exception)
        {
            Message message = new Message();
            message.block_contenido.Text = "Error en la conexión con la base de datos";
            message.block_titulo.Text = "Hubo un error inesperado";
            message.Show();
        }

        return resultado;
    }
}