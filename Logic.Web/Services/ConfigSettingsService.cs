using Logic.Web.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.Web.Services
{
    public class ConfigSettingsService : IConfigSettingsService
    {
        private readonly AppSettings _appSettings;
        public string ListOfExempted { get; private set; }
        public string NoOfExempted { get; private set; }

        public ConfigSettingsService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            ListOfExempted = _appSettings.ListOfExempted;
            NoOfExempted = _appSettings.NoOfExempted;
        }
    }
}
