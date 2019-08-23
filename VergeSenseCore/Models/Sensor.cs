using System;
using System.Collections.Generic;

namespace VergeSenseCore.Models
{
    public class Sensor
    {
        public Sensor(string Id, SensorReading[] data)
        {
            this.Id = Id;
            this.Data = data;
        }

        public string Id { get; }
        public SensorReading[] Data { get; }
    }
}