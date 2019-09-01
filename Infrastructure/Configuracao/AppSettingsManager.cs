using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seguradora.API.Infrastructure.Configuracao
{
    public static class AppSettingsManager
    {
        private static IConfiguration _config;

        public static void ConfigureSettings(IConfiguration config)
        {
            try
            {
                _config = config ?? throw new ArgumentException("config");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetServices => _config.GetConnectionString("Services");
    }
}
