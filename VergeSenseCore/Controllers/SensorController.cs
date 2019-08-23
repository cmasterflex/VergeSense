using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VergeSenseCore.Models;
using VergeSenseCore.Services;

namespace VergeSenseCore.Controllers
{
    [Route("api/[controller]")]
    public class SensorController : Controller
    {
        private ICsvService _csvService;
        public SensorController(ICsvService csvService)
        {
            _csvService = csvService;
        }
        //private static string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        //[HttpGet("[action]")]
        //public IEnumerable<WeatherForecast> WeatherForecasts()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    });
        //}

        [HttpGet("[action]")]
        public IEnumerable<SensorData> SensorData(DateTime start, DateTime end)
        {
            if (start == null) start = DateTime.MinValue;
            if (end == null) end = DateTime.Now;
            return _csvService.LoadFile()
        }

        //public class WeatherForecast
        //{
        //    public string DateFormatted { get; set; }
        //    public int TemperatureC { get; set; }
        //    public string Summary { get; set; }

        //    public int TemperatureF
        //    {
        //        get
        //        {
        //            return 32 + (int)(TemperatureC / 0.5556);
        //        }
        //    }
        //}
    }
}
