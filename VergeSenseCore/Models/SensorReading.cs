using System;

namespace VergeSenseCore.Models
{
    public class SensorReading
    {
        public SensorReading(DateTime timestamp, int personCount)
        {
            this.TimeStamp = timestamp;
            this.PersonCount = personCount;
        }

        public DateTime TimeStamp { get; }
        public int PersonCount { get; }
    }
}