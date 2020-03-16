using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Data;
using Logic.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Logic.ReadJson;

namespace Logic.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CoordinateController : BaseApiController
    {
        private readonly IReadJson _readJson;
        public CoordinateController(IRepository repo, IReadJson readJson) :base(repo)
        {
            _readJson = readJson;
        }

        [HttpGet]
        public List<GeoLocations> GetCoordinates()
        {
            var res = _readJson.LoadJson();
            return res;
        }

        [HttpPost]
        public bool GetGeofencingInfractionResult([FromBody]GeofenceInfrationViewModel model)
        {
            var res = _readJson.GeoFenceParameter(model.GeofencingCoordinates, model.CurrentLocation);

            return res;
        }
    }
}