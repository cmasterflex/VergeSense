using System;
using System.Collections.Generic;
using System.Linq;
using VergeSenseCore.Models;

namespace VergeSenseCore.Services
{
    public class SensorService : ISensorService
    {
        private ICsvService _csvService;
        public SensorService(ICsvService csvService)
        {
            _csvService = csvService;
        }
        public IEnumerable<Sensor> GetData(DateTime start, DateTime end)
        {
            var ret = new List<Sensor>();
            //in a production application, I assume this would be replaced by a call to a database service 
            //which would make sorting and filtering more efficient, instead of having to load up 
            //the entire dataset
            var data = _csvService.LoadFile();
            var sensors = data.GroupBy(point => point.Id);
            foreach(var sensor in sensors)
            {
                sensor.OrderBy(x => x.TimeStamp);
                ret.Add(new Sensor(sensor.Key, sensor.Select(x => new SensorReading(x.TimeStamp, x.PersonCount)).ToArray()));
            }
            //return data.Where(x => x.TimeStamp() > start && x.TimeStamp() < end);
            return ret;
        }
    }
}