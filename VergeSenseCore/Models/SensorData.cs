using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VergeSenseCore.Models
{
    public class SensorData
    {
        private string _sensorId; //ideally this would be and actual unique sensor ID, not a string
        private DateTime _timeStamp;
        private int _personCount;

        public SensorData(string Id, DateTime TimeStamp, int PersonCount)
        {
            _sensorId = Id;
            _timeStamp = TimeStamp;
            _personCount = PersonCount;
        }

        public string Id() => _sensorId;
        public DateTime TimeStamp() => _timeStamp;
        public int PersonCount() => _personCount;

        public static bool TryParseSensorData(string rawData, out SensorData sensorData)
        {
            const int MaxPersonCount = 100;
            sensorData = default;

            var parts = rawData.Split(',');
            if (parts.Length != 4) return false; //if there are an unexpected number of parts, then some piece of data is missing, return false
            if (string.IsNullOrWhiteSpace(parts[0])) return false; //if the data contains no timestamp, return false
            if (string.IsNullOrWhiteSpace(parts[1])) return false; //if the data contains no sensor name, return false
            if (string.IsNullOrWhiteSpace(parts[2])) return false; //if the data contains no person count, return false

            if (!DateTime.TryParse(parts[0], out DateTime timestamp)) return false; //if the timestamp cannot be parsed, return false
            if (!int.TryParse(parts[1], out int personCount)) return false; //if the person count cannot be parsed, return false
            if (personCount > MaxPersonCount) return false; //if the person count is over a realistic maximum person count, assume it is an error and ignore it, return false

            //if the function hasn't exited, assume all data is correct, and create a SensorData object
            sensorData = new SensorData(parts[1], timestamp, personCount);
            return true;
        }
    }
}