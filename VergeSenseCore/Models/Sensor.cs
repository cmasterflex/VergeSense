using System;
using System.Collections.Generic;

namespace VergeSenseCore.Models
{
    public class Sensor
    {
        public Sensor(string Id, int Max, SensorReading[] data)
        {
            this.Id = Id;
            this.Max = Max;
            this.Data = data;

        }

        public string Id { get; }
        public int Max { get; }
        public SensorReading[] Data { get; }
    }
}