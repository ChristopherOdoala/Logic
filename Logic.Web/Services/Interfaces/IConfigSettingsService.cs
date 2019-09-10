using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.Web.Services.Interfaces
{
    public interface IConfigSettingsService
    {
        string ListOfExempted
        {
            get;
        }
        string NoOfExempted
        {
            get;
        }
    }
}
