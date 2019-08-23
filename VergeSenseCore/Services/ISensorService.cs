using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VergeSenseCore.Models;

namespace VergeSenseCore.Services
{
    interface ISensorService
    {
        bool ParseSensorData(string rawData, out SensorData sensorData);
    }
}
