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
        public IEnumerable<SensorData> GetData(DateTime start, DateTime end)
        {
            //in a production application, I assume this would be replaced by a call to a database service 
            //which would make sorting and filtering more efficient, instead of having to load up 
            //the entire dataset
            var data = _csvService.LoadFile();
            //return data.Where(x => x.TimeStamp() > start && x.TimeStamp() < end);
            return data;
        }
    }
}