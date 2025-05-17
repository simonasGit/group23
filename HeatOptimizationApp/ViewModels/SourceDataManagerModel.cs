using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace HeatOptimizationApp.ViewModels
{
    public class SourceDataRow
    {
        public string TimeFrom { get; set; }
        public string HeatDemand { get; set; }
        public string ElectricityPrice { get; set; }
    }

    public class SourceDataManagerModel
    {
        public List<SourceDataRow> DataRows { get; private set; } = new List<SourceDataRow>();

        public void LoadCsv(string csvPath)
        {
            DataRows.Clear();
            if (!File.Exists(csvPath))
                throw new FileNotFoundException($"CSV file not found: {csvPath}");

            var lines = File.ReadAllLines(csvPath);
            // Start from row 4 (index 3)
            for (int i = 3; i < lines.Length; i++)
            {
                var line = lines[i];
                // Split by comma, but handle possible quoted values
                var columns = SplitCsvLine(line);

                // Defensive: check if there are enough columns
                if (columns.Length < 5)
                    continue;

                // 2nd, 4th, and 5th columns (index 1, 3, 4)
                var row = new SourceDataRow
                {
                    TimeFrom = columns[1].Trim(),
                    HeatDemand = columns[3].Trim(),
                    ElectricityPrice = columns[4].Trim()
                };
                DataRows.Add(row);
            }
        }

        // Helper to split CSV line, handling quoted commas
        private static string[] SplitCsvLine(string line)
        {
            var result = new List<string>();
            bool inQuotes = false;
            string current = "";
            foreach (char c in line)
            {
                if (c == '"')
                {
                    inQuotes = !inQuotes;
                    continue;
                }
                if (c == ',' && !inQuotes)
                {
                    result.Add(current);
                    current = "";
                }
                else
                {
                    current += c;
                }
            }
            result.Add(current);
            return result.ToArray();
        }
    }
}
