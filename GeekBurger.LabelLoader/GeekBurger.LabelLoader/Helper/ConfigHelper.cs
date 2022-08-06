using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GeekBurger.LabelLoader.Helper
{
    public class ConfigHelper
    {
        IConfiguration _config;
        public ConfigHelper()
        {
            _config = new ConfigurationBuilder()
               .SetBasePath(AppContext.BaseDirectory)
               .AddJsonFile($"appsettings.json", false, true)
               .Build();
        }

        public string GetConfigString(string configName)
        {
            return _config[configName];
        }

        public IConfiguration GetInstance()
        {
            return _config;
        }
    }
}
