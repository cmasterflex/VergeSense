using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VergeSenseCore.Models;
using VergeSenseCore.Services;

namespace VergeSenseCore.Controllers
{
    [Route("api/[controller]")]
    public class SensorController : Controller
    {
        private ISensorService _sensorService;
        public SensorController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        [HttpGet("[action]")]
        public IEnumerable<Sensor> SensorData(string start, string end)
        {
            var startDate = start == null ? DateTime.MinValue : DateTime.Parse(start) ;
            var endDate = end == null ? DateTime.Now : DateTime.Parse(end);
            var data = _sensorService.GetData(startDate, endDate);
            return data;
        }
    }
}