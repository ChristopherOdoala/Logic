using Logic.Web.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.Web.Services
{
    public class ConfigSettingsService : IConfigSettingsService
    {
        IConfiguration _configuration;
        public string ListOfExempted { get; private set; }
        public string NoOfExempted { get; private set; }

        public ConfigSettingsService(IConfiguration configuration)
        {
            this._configuration = configuration;
            ListOfExempted = _configuration.GetValue<string>("AppSettings:ListOfExempted");
            NoOfExempted = _configuration.GetValue<string>("AppSettings:NoOfExempted");
        }
    }
}
