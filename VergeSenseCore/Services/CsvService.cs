using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VergeSenseCore.Models;

namespace VergeSenseCore.Services
{
    public class CsvService : ICsvService
    {
        public IEnumerable<SensorData> LoadFile(string filename)
        {
            // TODO: remove for prod   
            filename = "C:\\DevRoot\\data\\VS_Coding_Exercise_Data.csv";

            var ret = new List<SensorData>();

            const int dataPointMaxSize = 50; //maximum size of a data point reading, in this case, max size of one line in the file
            var AllLines = new string[dataPointMaxSize];
            using (StreamReader sr = File.OpenText(filename))
            {
                int x = 0;
                while (!sr.EndOfStream)
                {
                    AllLines[x] = sr.ReadLine();
                    x += 1;
                }
            }
            Parallel.For(0, AllLines.Length, x =>
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