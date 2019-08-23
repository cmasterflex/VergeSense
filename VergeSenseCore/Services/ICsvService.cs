using System.Collections.Generic;
using VergeSenseCore.Models;

namespace VergeSenseCore.Services
{
    public interface ICsvService
    {
        IEnumerable<SensorData> LoadFile();
    }
}