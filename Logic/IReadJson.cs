using System;
using System.Collections.Generic;
using System.Text;
using static Logic.ReadJson;

namespace Logic
{
    public interface IReadJson
    {
        List<GeoLocations> LoadJson();
        bool GeoFenceParameter(List<Coordinates> geofenceCoordinates, Coordinates currentLocation);
    }
}
