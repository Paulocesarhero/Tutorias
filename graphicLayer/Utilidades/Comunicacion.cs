using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace graphicLayer.Utilidades
{
    public class Comunicacion
    {
       

        public static void EnviarCorreo(string destinatario, string mensaje)
        {
            string remitente = ConfigHelper.GetCorreoElectronicoRemitente();
            string password = ConfigHelper.GetRemitentePassword();
            string asunto = "¡Hola desde el sistema de tutorias institucional de la uv!";

            // Configurar el cliente SMTP
            var smtpClient = new SmtpClient("smtp.office365.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(remitente, password),
                EnableSsl = true
            };

            try
            {
                MailMessage correo = new MailMessage(remitente, destinatario, asunto, mensaje);

                smtpClient.Send(correo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar correo electronico", ex);
            }
        }
    }
}
