using System;
using System.Collections.Generic;
using System.Text;
using static Logic.ReadJson;

namespace Logic.Model.ViewModel
{
    public class GeofenceInfrationViewModel
    {
        public List<Coordinates> GeofencingCoordinates { get; set; }
        public Coordinates CurrentLocation { get; set; }
    }
}
