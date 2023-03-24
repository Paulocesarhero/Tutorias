using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Remote.Protocol.Viewport;
using Sistema_De_Tutorias.Utility;
using Sistema_De_Tutorias.Views;
using Tutorias.BussinesLogic.Management;
using Tutorias.Service.DatabaseContext;


namespace Sistema_De_Tutorias;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
    }

    private void BTNLogin_OnClick(object? sender, RoutedEventArgs e)
    {
        TutoriaManagement tutoriaManagement = new TutoriaManagement();
        Usuario usuario;
        if (!string.IsNullOrEmpty(TB_Username.Text) && !string.IsNullOrEmpty(TB_Password.Text))
        {
            usuario = tutoriaManagement.Login(TB_Username.Text,TB_Password.Text);
            if (usuario.tipoUsuario.tipo == "Jefe de carrera")
            {
                SesionesDeTutorias sesionesDeTutorias = new SesionesDeTutorias();
                sesionesDeTutorias.Show();
                CredencialesUsuario.Instance.Usuario = usuario;
                this.Close();
            }
            if (usuario.tipoUsuario.tipo == "Tutor academico")
            {
                MenuTutorAcademico menuTutorAcademico = new MenuTutorAcademico();
                menuTutorAcademico.Show();
                CredencialesUsuario.Instance.Usuario = usuario;
                this.Close();
            }
            if (usuario.tipoUsuario.tipo == "Coordinadora")
            {
                MenuCoordinadoraDeTutorias menuCoordinadora = new MenuCoordinadoraDeTutorias();
                menuCoordinadora.Show();
                CredencialesUsuario.Instance.Usuario = usuario;
                this.Close();
            }
            else
            {
                Message message = new Message();
                message.block_contenido.Text = "El usuario que usted ingreso no se encuentra registrado en la base de datos";
                message.block_titulo.Text = "Error";
                message.Show();
            }
        }
        else
        {
            Message message = new Message();
            message.block_contenido.Text = "Debe de llenar los campos primero";
            message.block_titulo.Text = "Error";
            message.Show();
        }
       



    }
}