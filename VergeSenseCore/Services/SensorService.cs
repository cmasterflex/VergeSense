using System;
using System.Collections.Generic;
using System.Linq;
using VergeSenseCore.Models;

namespace VergeSenseCore.Services
{
    public class SensorService : ISensorService
    {
        private readonly ICsvService _csvService;
        private SensorData[] _dataCache;
        public SensorService(ICsvService csvService)
        {
            _csvService = csvService;
        }

        public SensorData[] DataCache => _dataCache ?? (_dataCache = _csvService.LoadFile().ToArray())
;

        public IEnumerable<Sensor> GetData(DateTime start, DateTime end)
        {
            var ret = new List<Sensor>();
            //in a production application, I assume this would be replaced by a call to a database service
            //which would handle sorting, filtering, and cache
            var sensors = DataCache.GroupBy(point => point.Id);
            foreach(var sensor in sensors)
            {
                var data = sensor.Select(x => new SensorReading(x.TimeStamp, x.PersonCount))
                    .OrderBy(x => x.TimeStamp)
                    .Where(x => x.TimeStamp >= start && x.TimeStamp <= end);
                if (data.Any()) ret.Add(new Sensor(sensor.Key, data.ToArray()));
            }
            return ret;
        }
    }
}