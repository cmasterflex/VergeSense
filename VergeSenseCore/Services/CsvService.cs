using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VergeSenseCore.Models;

namespace VergeSenseCore.Services
{
    public class CsvService : ICsvService
    {
        public IEnumerable<SensorData> LoadFile()
        {
            var filename = "C:\\DevRoot\\data\\VS_Coding_Exercise_Data.csv";

            var ret = new List<SensorData>();

            var AllLines = new List<string>();
            using (StreamReader sr = File.OpenText(filename))
            {
                while (!sr.EndOfStream)
                {
                    AllLines.Add(sr.ReadLine());
                }
            }
            Parallel.For(0, AllLines.Count, x =>
            {
                if (SensorData.TryParseSensorData(AllLines[x], out SensorData point))
                {
                    ret.Add(point);
                }
            });
            return ret;
        }
    }
}