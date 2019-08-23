using System;
using System.Collections.Generic;
using VergeSenseCore.Models;

namespace VergeSenseCore.Services
{
    public interface ISensorService
    {
        IEnumerable<SensorData> GetData(DateTime start, DateTime end);
    }
}