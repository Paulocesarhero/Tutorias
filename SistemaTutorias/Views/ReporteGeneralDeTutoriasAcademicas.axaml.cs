using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Sistema_De_Tutorias.Views;
using Tutorias.BussinesLogic.Management;
using Tutorias.Service.DatabaseContext;

namespace Sistema_De_Tutorias;

public partial class ReporteGeneralDeTutoriasAcademicas : Window
{
    public Periodo_Escolar periodoEscolarSeleccionado { get; set; }
    private List<Problematica> _problematicas { get; set; }
    
    private List<Reporte_De_Tutoria> _reporteDeTutorias { get; set; }
    public ReporteGeneralDeTutoriasAcademicas()
    {
        InitializeComponent();
        fillData();
    }

    public ReporteGeneralDeTutoriasAcademicas(Periodo_Escolar periodoEscolar)
    {
        periodoEscolarSeleccionado = periodoEscolar;
        InitializeComponent();
        fillData();
    }

    private void fillData()
    {
        fillProblematicasAcademicas();
        fillReportesDeTutorias();
    }

    private void fillProblematicasAcademicas()
    {
        int numDeSesion = cb_numSesionDeTutoriaAcademica.SelectedIndex+1; 
        TutoriaManagement tutoriaManagement = new TutoriaManagement();
        
        _problematicas = tutoriaManagement.findProblematicasAcademicas(periodoEscolarSeleccionado, numDeSesion);
        
        dg_ProblematicaAcademica.Items = _problematicas;
        
    }

    private void ClickSolucionarProblematica(object? sender, RoutedEventArgs e)
    {
        Message message = new Message();
        message.block_contenido.Text = _problematicas.First().experienciaEducativa.nombre; 
        message.Show();
    }

    private void fillReportesDeTutorias()
    {
        int numDeSesion = cb_numSesionDeTutoriaAcademica.SelectedIndex+1; 
        TutoriaManagement tutoriaManagement = new TutoriaManagement();
        _reporteDeTutorias = tutoriaManagement.findReportesDeTutorias(periodoEscolarSeleccionado, numDeSesion);

        dg_reporteDeTutoria.Items = _reporteDeTutorias;
    }
}