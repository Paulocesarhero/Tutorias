using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Sistema_De_Tutorias.Views;
using Tutorias.BussinesLogic.Management;
using Tutorias.Service.DatabaseContext;

namespace Sistema_De_Tutorias;

public partial class SesionesDeTutorias : Window
{
    
    

    public SesionesDeTutorias()
    {
        InitializeComponent();
        btn_consultar.IsEnabled = false;
        FillTable();

        
    }

    public void FillTable()
    {
        TutoriaManagement tutoriaManagement = new TutoriaManagement();
        List<Periodo_Escolar> periodosEscolares;
        try
        {
            periodosEscolares= tutoriaManagement.getPeriodosEscolares();
        }
        catch (Exception e)
        {
            Message message = new Message();
            message.block_contenido.Text = "Ocurrio un error al recuperar información de la base de datos por favor intente mas tarde";
            message.block_titulo.Text = "Error";
            throw;
        }
        
        dg_periodo_Escolar.Items = periodosEscolares;
    }

    

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        Periodo_Escolar periodoEscolarSelect = (Periodo_Escolar)dg_periodo_Escolar.SelectedItem;
        ReporteGeneralDeTutoriasAcademicas reporteDeTutoria = new ReporteGeneralDeTutoriasAcademicas(periodoEscolarSelect);
        reporteDeTutoria.block_fechaDeTutoriaAcademica.Text = "PERIODO ESCOLAR fecha de inicio: " 
                                                              + periodoEscolarSelect.fechaDeInicio.ToString()
                                                              + " fecha de fin: "
                                                              + periodoEscolarSelect.fechaDeFin.ToString();
        reporteDeTutoria.Show();
        this.Close();

    }
    

    private void Dg_periodo_Escolar_OnCellPointerPressed(object? sender, DataGridCellPointerPressedEventArgs e)
    {
        btn_consultar.IsEnabled = true;
    }
}