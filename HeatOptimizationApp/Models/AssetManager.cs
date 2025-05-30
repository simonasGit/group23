using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace HeatOptimizationApp
{
    public class Asset
    {
        public string Name { get; set; }
        public double MaxHeat { get; set; }
        public double ProductionCost { get; set; }
        public double CO2Emissions { get; set; }
    }

    public class GasBoiler : Asset
    {
        public double GasConsumption { get; set; }
    }

    public class OilBoiler : Asset
    {
        public double OilConsumption { get; set; }
    }

    public class GasMotor : Asset
    {
        public double MaxElectricity { get; set; }
        public double GasConsumption { get; set; }
    }

    public class HeatPump : Asset
    {
        public double MaxElectricity { get; set; }
    }

    public class AssetManager
    {
        public List<Asset> Assets { get; } = new List<Asset>();

        public AssetManager()
        {
            string path = "AssetManager.csv"; // colocar no /Assets/ folder

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"CSV file not found at path: {path}");
            }

            LoadAssetsFromCsv(path);
        }

        private void LoadAssetsFromCsv(string filePath) //lendo o .csv
        {
            var lines = File.ReadAllLines(filePath);

            if (lines.Length <= 1)
                throw new Exception("CSV file is empty or missing data.");

            foreach (var line in lines.Skip(1)) // skip header
            {
                var parts = line.Split(',');

                if (parts.Length < 7)
                    continue;

                string name = parts[0].Trim();
                string type = parts[1].Trim();
                double maxHeat = double.Parse(parts[2], CultureInfo.InvariantCulture);
                double productionCost = double.Parse(parts[3], CultureInfo.InvariantCulture);
                double co2 = double.Parse(parts[4], CultureInfo.InvariantCulture);
                double consumption = double.Parse(parts[5], CultureInfo.InvariantCulture);
                double maxElectricity = double.Parse(parts[6], CultureInfo.InvariantCulture);

                Asset asset = type switch
                {
                    "GasBoiler" => new GasBoiler
                    {
                        Name = name,
                        MaxHeat = maxHeat,
                        ProductionCost = productionCost,
                        CO2Emissions = co2,
                        GasConsumption = consumption
                    },
                    "OilBoiler" => new OilBoiler
                    {
                        Name = name,
                        MaxHeat = maxHeat,
                        ProductionCost = productionCost,
                        CO2Emissions = co2,
                        OilConsumption = consumption
                    },
                    "GasMotor" => new GasMotor
                    {
                        Name = name,
                        MaxHeat = maxHeat,
                        MaxElectricity = maxElectricity,
                        ProductionCost = productionCost, //opt does not change fr somethinkg
                        CO2Emissions = co2,
                        GasConsumption = consumption
                    },
                    "HeatPump" => new HeatPump
                    {
                        Name = name,
                        MaxHeat = maxHeat,
                        MaxElectricity = maxElectricity,
                        ProductionCost = productionCost,
                        CO2Emissions = co2
                    },
                    _ => null
                };

                if (asset != null)
                {
                    Assets.Add(asset);
                }
            }
        }
    }
}
