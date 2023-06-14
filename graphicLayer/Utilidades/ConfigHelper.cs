using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphicLayer.Utilidades
{
    public static class ConfigHelper
    {
        public static IConfiguration Configuration { get; set; }
        static ConfigHelper()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public static string GetConnectionString()
        {
            string connectionString = Configuration.GetConnectionString("NombreConexion");
            return connectionString;
        }

        public static string GetCorreoElectronicoRemitente()
        {
            return Configuration.GetConnectionString("CorreoElectronicoRemitente");
        }

        public static string GetRemitentePassword()
        {
            return Configuration.GetConnectionString("RemitentePassword");
        }
    }
}
