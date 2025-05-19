using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace HeatOptimizationApp.Models
{
    public static class CSVData
    {
        public class WinterDataRow
        {
            public string TimeFrom { get; set; }
            public double HeatDemand { get; set; }
            public double ElectricityPrice { get; set; }
        }

        public static List<WinterDataRow> WinterData(string path)
        {
            var winterRows = new List<WinterDataRow>();
            if (!File.Exists(path)) return winterRows;

            var lines = File.ReadAllLines(path).Skip(3);

            foreach (var line in lines)
            {
                var cols = line.Split(',');
                if (cols.Length > 4 &&
                    !string.IsNullOrWhiteSpace(cols[1]) &&
                    double.TryParse(cols[3], NumberStyles.Any, CultureInfo.InvariantCulture, out double heatDemand) &&
                    double.TryParse(cols[4], NumberStyles.Any, CultureInfo.InvariantCulture, out double electricityPrice))
                {
                    winterRows.Add(new WinterDataRow
                    {
                        TimeFrom = cols[1].Trim(),
                        HeatDemand = heatDemand,
                        ElectricityPrice = electricityPrice
                    });
                }
            }

            return winterRows;
        }

        public class SummerDataRow
        {
            public string TimeFrom { get; set; }
            public double HeatDemand { get; set; }
            public double ElectricityPrice { get; set; }
        }

        public static List<SummerDataRow> SummerData(string path)
        {
            var summerRows = new List<SummerDataRow>();
            if (!File.Exists(path)) return summerRows;

            var lines = File.ReadAllLines(path).Skip(4);

            foreach (var line in lines)
            {
                var cols = line.Split(',');
                if (cols.Length > 9 &&
                    !string.IsNullOrWhiteSpace(cols[6]) &&
                    double.TryParse(cols[8], NumberStyles.Any, CultureInfo.InvariantCulture, out double heatDemand) &&
                    double.TryParse(cols[9], NumberStyles.Any, CultureInfo.InvariantCulture, out double electricityPrice))
                {
                    summerRows.Add(new SummerDataRow
                    {
                        TimeFrom = cols[6].Trim(),
                        HeatDemand = heatDemand,
                        ElectricityPrice = electricityPrice
                    });
                }
            }

            return summerRows;
        }
    }
}
