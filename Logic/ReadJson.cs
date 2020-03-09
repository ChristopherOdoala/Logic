using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static Logic.ReadJson;

namespace Logic
{
    public class ReadJson : IReadJson
    {
        private readonly ISearchGeolocation _searchGeolocation;
        const double height = 4.0872875;
        const int Radius = 6370;

        public ReadJson(ISearchGeolocation searchGeolocation)
        {
            _searchGeolocation = searchGeolocation;
        }
        public List<GeoLocations> LoadJson()
        {
            using (StreamReader r = new StreamReader("Coordinates.json"))
            {
                string json = r.ReadToEnd();
                List<GeoLocations> items = JsonConvert.DeserializeObject<List<GeoLocations>>(json);
                return items;
            }
        }

        public bool IsGeofenceInfraction(List<Coordinates> Geofence, List<GeoLocations> GeoLocation, Coordinates currentLocation)
        {
            var currentGeoFence = _searchGeolocation.Compare(Geofence, GeoLocation);
            var instance = new GeoLocations
            {
                Coordinates = new List<Coordinates>
                    {
                        new Coordinates{ lat = currentLocation.lat, lng = currentLocation.lng}
                    }
            };

            var res = currentGeoFence.Where(z => (z.Coordinates.First().lat >= currentLocation.lat || z.Coordinates.Last().lat <= currentLocation.lat) && (z.Coordinates[0].lng <= currentLocation.lng || z.Coordinates[1].lng >= currentLocation.lng)).FirstOrDefault();

            if (res == null)
                return true;

            return false;
        }


        public List<Coordinates> GeoFenceParameter(List<Coordinates> geofenceCoordinates)
        {
            List<Coordinates> Geofence = new List<Coordinates> { };

            for (int i = 0; i < geofenceCoordinates.Count(); i++)
            {
                if ((i + 1) == geofenceCoordinates.Count())
                    break;
                //take first and second points
                var pointA = geofenceCoordinates[i];
                var pointB = geofenceCoordinates[i+1];
                if (pointA.lat < pointB.lat)
                    ComputeCoordinateTypeA(pointA, pointB, Geofence);
                
                else if (pointA.lat > pointB.lat)
                    ComputeCoordinateTypeB(pointA, pointB, Geofence);

            }

            return Geofence;
        }

        public void ComputeCoordinateTypeB(Coordinates pointA, Coordinates pointB, List<Coordinates> Geofence)
        {
            var pointC = new Coordinates { lat = pointB.lat, lng = pointA.lng };
            var initialAdj = Distance(pointC.lat, pointC.lng, pointB.lat, pointB.lng, 'K');
            var initialHyp = Distance(pointA.lat, pointA.lng, pointB.lat, pointB.lng, 'K');
            var initialOpp = Distance(pointA.lat, pointA.lng, pointC.lat, pointC.lng, 'K');

            //find angle between Adj and Hyp
            var radianAngleAdjHyp = Math.Acos(initialAdj / initialHyp);
            var angleAdjHyp = rad2deg(radianAngleAdjHyp);

            //find angle between opp and hyp
            var radianAngleOppHyp = Math.Asin(initialOpp / initialHyp);
            var angleOppHyp = rad2deg(radianAngleOppHyp);

            //find the number of points of intersection using fixed interval
            var noOfPoints = Math.Round(initialOpp / height, 0, MidpointRounding.AwayFromZero);

            for (int j = 0; j < noOfPoints; j++)
            {
                TriangleNodes triangleNodes = new TriangleNodes
                {
                    NodeA = pointA,
                    NodeB = pointB,
                    NodeC = pointC
                };

                //finding the new latitude of point in noOfPoints
                var newLat = (((height * (j + 1)) * 360) / (2 * Math.PI * Radius)) + triangleNodes.NodeA.lat;

                //find the new length of the hyp
                var newHyp = (initialOpp - (height * (j + 1)) / Math.Sin(angleOppHyp));
                //find the new length of the adj
                var newAdj = newHyp * Math.Cos(angleAdjHyp);

                //find the new longitude of point in noOfPoints
                var newLong = ((newAdj * 360) / (2 * Math.PI * Radius * Math.Cos(newLat))) + triangleNodes.NodeA.lng;
                var coordinate = new Coordinates { lat = newLat, lng = newLong };
                Geofence.Add(coordinate);
            }
        }

        public void ComputeCoordinateTypeA(Coordinates pointA, Coordinates pointB, List<Coordinates> Geofence)
        {
            var pointC = new Coordinates { lat = pointA.lat, lng = pointB.lng }; 
             var initialAdj = Distance(pointA.lat, pointA.lng, pointC.lat, pointC.lng, 'K');
            var initialHyp = Distance(pointA.lat, pointA.lng, pointB.lat, pointB.lng, 'K');
            var initialOpp = Distance(pointC.lat, pointC.lng, pointB.lat, pointB.lng, 'K');

            //find angle between Adj and Hyp
            var radianAngleAdjHyp = Math.Acos(initialAdj / initialHyp);
            var angleAdjHyp = rad2deg(radianAngleAdjHyp);

            //find angle between opp and hyp
            var radianAngleOppHyp = Math.Asin(initialOpp / initialHyp);
            var angleOppHyp = rad2deg(radianAngleOppHyp);

            //find the number of points of intersection using fixed interval
            var noOfPoints = Math.Round(initialOpp / height, 0, MidpointRounding.AwayFromZero);

            for (int j = 0; j < noOfPoints; j++)
            {
                TriangleNodes triangleNodes = new TriangleNodes
                {
                    NodeA = pointA,
                    NodeB = pointB,
                    NodeC = pointC
                };

                //finding the new latitude of point in noOfPoints
                var newLat = (((height * (j + 1)) * 360) / (2 * Math.PI * Radius)) + triangleNodes.NodeA.lat;

                //find the new length of the hyp
                var newHyp = (initialOpp - (height * (j + 1)) / Math.Sin(angleOppHyp));
                //find the new length of the adj
                var newAdj = newHyp * Math.Cos(angleAdjHyp);

                //find the new longitude of point in noOfPoints
                var newLong = ((newAdj * 360) / (2 * Math.PI * Radius * Math.Cos(newLat))) + triangleNodes.NodeA.lng;
                var coordinate = new Coordinates { lat = newLat, lng = newLong };
                Geofence.Add(coordinate);
            }
        }
        
        private double Distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;
                if (unit == 'K')
                {
                    dist = dist * 1.609344;
                }
                else if (unit == 'N')
                {
                    dist = dist * 0.8684;
                }
                return (dist);
            }
        }

        // This function converts decimal degrees to radians             
        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        // This function converts radians to decimal degrees             
        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }


        public class Coordinates
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class GeoLocations
        {
            public List<Coordinates> Coordinates { get; set; }
            public int Index { get; set; }
        }
    }

    public class SearchGeolocation : ISearchGeolocation
    {
        public List<GeoLocations> Compare(object x, object y)
        {
            List<GeoLocations> NewGeoLocation = new List<GeoLocations> { };
            List<Coordinates> coordinates = (List<Coordinates>)x;
            List<GeoLocations> geoLocations = (List<GeoLocations>)y;
            foreach (var coordinate in coordinates)
            {
                var instance = new GeoLocations
                {
                    Coordinates = new List<Coordinates>
                    {
                        new Coordinates{ lat = coordinate.lat, lng = coordinate.lng}
                    }
                };
                NewGeoLocation.AddRange(geoLocations.Where(z => (z.Coordinates.First().lat >= coordinate.lat || z.Coordinates.Last().lat <= coordinate.lat) && (z.Coordinates[0].lng <= coordinate.lng || z.Coordinates[1].lng >= coordinate.lng)));

            }

            return NewGeoLocation;
        }
    }

    public interface ISearchGeolocation
    {
        List<GeoLocations> Compare(object x, object y);
    }
}