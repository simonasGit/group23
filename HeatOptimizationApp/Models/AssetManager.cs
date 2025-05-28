using System;
using System.Collections.Generic;

namespace HeatOptimizationApp
{

    public class Asset
    {
        public string Name { get; set; }
        public double MaxHeat { get; set; }
        public double ProductionCost { get; set; } // this is in DKK/MWh(th) but we can maybe change it late
        public double CO2Emissions { get; set; }   // kg/MWh(th)
    }


    public class GasBoiler : Asset
    {
        public double GasConsumption { get; set; } // MWh(gas)/MWh(th)
    }

    public class OilBoiler : Asset
    {
        public double OilConsumption { get; set; } // MWh(oil)/MWh(th) remember the units later beacuse converting might be a problem
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


    public class AssetManager //theyre all public so they can be acessed outside and from the other files
    {
        public List<Asset> Assets { get; } = new List<Asset>();

        public AssetManager()
        {
            LoadAssets();
        }

        public void LoadAssets()
        {
            Assets.Add(new GasBoiler
            {
                Name = "Gas Boiler 1",
                MaxHeat = 4.0,
                ProductionCost = 520,
                CO2Emissions = 175,
                GasConsumption = 0.9
            });

            Assets.Add(new GasBoiler
            {
                Name = "Gas Boiler 2",
                MaxHeat = 3.0,
                ProductionCost = 560,
                CO2Emissions = 130,
                GasConsumption = 0.7
            });

            Assets.Add(new OilBoiler
            {
                Name = "Oil Boiler",
                MaxHeat = 4.0,
                ProductionCost = 670,
                CO2Emissions = 330,
                OilConsumption = 1.5
            });

            Assets.Add(new GasMotor
            {
                Name = "Gas Motor",
                MaxHeat = 3.5,
                MaxElectricity = 2.6,
                ProductionCost = 990,
                CO2Emissions = 650,
                GasConsumption = 1.8
            });

            Assets.Add(new HeatPump
            {
                Name = "Heat Pump",
                MaxHeat = 6.0,
                MaxElectricity = -6.0,
                ProductionCost = 60,
                CO2Emissions = 0
            });
        }
    }



}
