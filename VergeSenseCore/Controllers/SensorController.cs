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
        public IEnumerable<Sensor> SensorData(DateTime start, DateTime end)
        {
            if (start == null) start = DateTime.MinValue;
            if (end == null) end = DateTime.Now;
            var data = _sensorService.GetData(start, end);
            return data;
        }
    }
}