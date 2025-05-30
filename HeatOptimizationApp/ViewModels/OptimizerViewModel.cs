
using System;
using System.Collections.Generic;
using System.Linq;
using HeatOptimizationApp.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace HeatOptimizationApp.ViewModels
{
    public class Optimizer
    {
        public AssetManager assetManager = new AssetManager();

        public ISeries[] Series { get; private set; }
        public Axis[] XAxes { get; private set; }
        public Axis[] YAxes { get; private set; }

        public ISeries[] SummerSeries { get; private set; }
        public Axis[] SummerXAxes { get; private set; }
        public Axis[] SummerYAxes { get; private set; }


        public ISeries[] WinterSeries { get; private set; }
        public Axis[] WinterXAxes { get; private set; }
        public Axis[] WinterYAxes { get; private set; }

        public ISeries[] SummerSeries2 { get; private set; }
        public Axis[] SummerXAxes2 { get; private set; }
        public Axis[] SummerYAxes2 { get; private set; }

        public Unit winterHp { get; private set; } = new Unit();
        public Unit winterGb { get; private set; } = new Unit();
        public Unit winterOb { get; private set; } = new Unit();
        public Unit winterGm { get; private set; } = new Unit();

        public Unit summerHp { get; private set; } = new Unit();
        public Unit summerGb { get; private set; } = new Unit();
        public Unit summerOb { get; private set; } = new Unit();
        public Unit summerGm { get; private set; } = new Unit();

        public Unit winterGb1 { get; private set; } = new Unit();
        public Unit winterGb2 { get; private set; } = new Unit();
        public Unit winterOb1 { get; private set; } = new Unit();

        public Unit summerGb1 { get; private set; } = new Unit();
        public Unit summerGb2 { get; private set; } = new Unit();
        public Unit summerOb1 { get; private set; } = new Unit();

        public double S1totalheat { get; set; }
        public double S1totalelectricity { get; set; }
        public double S1totalconsumption { get; set; }
        public double S1totalcosts { get; set; }
        public double S1totalCO2 { get; set; }

        public double S2totalheat { get; set; }
        public double S2totalelectricity { get; set; }
        public double S2totalconsumption { get; set; }
        public double S2totalcosts { get; set; }
        public double S2totalCO2 { get; set; }

        public class Unit
        {
            public List<double> currentheat;
            public double totalheat;
            public double totalcost;
            public double CO2;
            public double Consumption;
            public double electricity;
            public Unit()
            {
                currentheat = new List<double>();
                totalheat = 0;
                CO2 = 0;
                Consumption = 0;
                electricity = 0;
            }
        }

        public Optimizer()
        {
            LoadScenario1();
            LoadScenario2();
        }

        public void LoadScenario1()
        {
            var Assets = assetManager.Assets;
            var winterData = SDM.WinterData();
            var summerData = SDM.SummerData();

            var winterTimestamps = winterData.Select(row => row.TimeFrom).ToList();
            var summerTimestamps = summerData.Select(row => row.TimeFrom).ToList();

            foreach (var row in winterData)
            {
                var demand = row.HeatDemand;
                winterGb1.currentheat.Add(Math.Min(demand, Assets[0].MaxHeat)); //gb1 instead of 4
                winterGb1.totalheat += Math.Min(demand, Assets[0].MaxHeat);//gb1 instead of 4
                winterGb2.currentheat.Add(Math.Min(Math.Max(demand - Assets[0].MaxHeat, 0), Assets[1].MaxHeat));//gb1 instead of 4 and gb2 for 3
                winterGb2.totalheat += Math.Min(Math.Max(demand - Assets[0].MaxHeat, 0), Assets[1].MaxHeat);//gb1 instead of 4 and gb2 for 3
                winterOb1.currentheat.Add(Math.Max(demand - (Assets[0].MaxHeat + Assets[1].MaxHeat), 0)); //gb + gb2 instead of 7
                winterOb1.totalheat += Math.Max(demand - (Assets[0].MaxHeat + Assets[1].MaxHeat), 0); // gb + gb2 instead of 7
            }

            winterGb1.totalcost = Assets[0].ProductionCost * winterGb1.totalheat;
            winterGb1.CO2 = Assets[0].CO2Emissions * winterGb1.totalheat;
            winterGb1.Consumption = ((GasBoiler)Assets[0]).GasConsumption * winterGb1.totalheat;
            winterGb2.totalcost = Assets[1].ProductionCost * winterGb2.totalheat;
            winterGb2.CO2 = Assets[1].CO2Emissions * winterGb2.totalheat;
            winterGb2.Consumption = ((GasBoiler)Assets[1]).GasConsumption * winterGb2.totalheat;
            winterOb1.totalcost = Assets[2].ProductionCost * winterOb1.totalheat;
            winterOb1.CO2 = Assets[2].CO2Emissions * winterOb1.totalheat;
            winterOb1.Consumption = ((OilBoiler)Assets[2]).OilConsumption * winterOb1.totalheat;

            foreach (var row in summerData)
            {
                var demand = row.HeatDemand;
                summerGb1.currentheat.Add(Math.Min(demand, 4));
                summerGb1.totalheat += Math.Min(demand, 4);
                summerGb2.currentheat.Add(Math.Min(Math.Max(demand - 4, 0), 3));
                summerGb2.totalheat += Math.Min(Math.Max(demand - 4, 0), 3);
                summerOb1.currentheat.Add(Math.Max(demand - 7, 0));
                summerOb1.totalheat += Math.Max(demand - 7, 0);
            }

            summerGb1.totalcost = Assets[0].ProductionCost * summerGb1.totalheat;
            summerGb1.CO2 = Assets[0].CO2Emissions * summerGb1.totalheat;
            summerGb1.Consumption = ((GasBoiler)Assets[0]).GasConsumption * summerGb1.totalheat;
            summerGb2.totalcost = Assets[1].ProductionCost * summerGb2.totalheat;
            summerGb2.CO2 = Assets[1].CO2Emissions * summerGb2.totalheat;
            summerGb2.Consumption = ((GasBoiler)Assets[1]).GasConsumption * summerGb2.totalheat;
            summerOb1.totalcost = Assets[2].ProductionCost * summerOb1.totalheat;
            summerOb1.CO2 = Assets[2].CO2Emissions * summerOb1.totalheat;
            summerOb1.Consumption = ((OilBoiler)Assets[2]).OilConsumption * summerOb1.totalheat;

            S1totalheat = summerGb1.totalheat + winterGb1.totalheat + summerGb2.totalheat + winterGb2.totalheat + summerOb1.totalheat + winterOb1.totalheat;
            S1totalelectricity = summerGb1.electricity + winterGb1.electricity + summerGb2.electricity + winterGb2.electricity + summerOb1.electricity + winterOb1.electricity;
            S1totalcosts = summerGb1.totalcost + winterGb1.totalcost + summerGb2.totalcost + winterGb2.totalcost + summerOb1.totalcost + winterOb1.totalcost;
            S1totalconsumption = summerGb1.Consumption + winterGb1.Consumption + summerGb2.Consumption + winterGb2.Consumption + summerOb1.Consumption + winterOb1.Consumption;
            S1totalCO2 = summerGb1.CO2 + winterGb1.CO2 + summerGb2.CO2 + winterGb2.CO2 + summerOb1.CO2 + winterOb1.CO2;
            //Console.WriteLine($"\nScenario1: Final Total heat produced across all data in winter: {winterGb1.totalheat + winterGb2.totalheat + winterOb1.totalheat:F2} MW(th)");
            //Console.WriteLine($"\nScenario1: Final Total heat produced across all data in summer: {summerGb1.totalheat + summerGb2.totalheat + summerOb1.totalheat:F2} MW(th)");


            Series = new ISeries[]
            {
                new StackedColumnSeries<double>
                {
                    Values = winterGb1.currentheat,
                    Name = "GB1 (Winter)",
                    Fill = new SolidColorPaint(SKColors.Yellow)
                },
                new StackedColumnSeries<double>
                {
                    Values = winterGb2.currentheat,
                    Name = "GB2 (Winter)",
                    Fill = new SolidColorPaint(SKColors.Orange)
                },
                new StackedColumnSeries<double>
                {
                    Values = winterOb1.currentheat,
                    Name = "OB1 (Winter)",
                    Fill = new SolidColorPaint(SKColors.Gray)
                }
            };

            XAxes = CreateXAxis(winterTimestamps);
            YAxes = CreateYAxis();

            SummerSeries = new ISeries[]
            {
                new StackedColumnSeries<double>
                {
                    Values = summerGb1.currentheat,
                    Name = "GB1 (Summer)",
                    Fill = new SolidColorPaint(SKColors.Yellow)
                },
                new StackedColumnSeries<double>
                {
                    Values = summerGb2.currentheat,
                    Name = "GB2 (Summer)",
                    Fill = new SolidColorPaint(SKColors.Orange)
                },
                new StackedColumnSeries<double>
                {
                    Values = summerOb1.currentheat,
                    Name = "OB1 (Summer)",
                    Fill = new SolidColorPaint(SKColors.Gray)
                }
            };
            SummerXAxes = CreateXAxis(summerTimestamps);
            SummerYAxes = CreateYAxis();
        }

        public void LoadScenario2()
        {
            var Assets = assetManager.Assets;
            var winterData = SDM.WinterData();
            var summerData = SDM.SummerData();
            var winterTimestamps = winterData.Select(row => row.TimeFrom).ToList();
            var summerTimestamps = summerData.Select(row => row.TimeFrom).ToList();

            double winterTotal = 0;
            double summerTotal = 0;

            foreach (var line in winterData)
            {
                var (hp, gb, ob, gm) = AllocateHeat(line.HeatDemand, line.ElectricityPrice);
                winterHp.currentheat.Add(hp);
                winterHp.totalheat += hp;
                winterHp.totalcost += Assets[4].ProductionCost * hp - ((HeatPump)Assets[4]).MaxElectricity / Assets[4].MaxHeat * line.ElectricityPrice * hp;
                winterGb.currentheat.Add(gb);
                winterGb.totalheat += gb;
                winterGb.totalcost += Assets[0].ProductionCost * winterGb.totalheat;
                winterOb.currentheat.Add(ob);
                winterOb.totalheat += ob;
                winterOb.totalcost += Assets[2].ProductionCost * winterOb.totalheat;
                winterGm.currentheat.Add(gm);
                winterGm.totalheat += gm;
                winterGm.totalcost += Assets[3].ProductionCost * gm - (((GasMotor)Assets[3]).MaxElectricity) / (Assets[3].MaxHeat) * gm * line.ElectricityPrice;
                winterGm.electricity += (((GasMotor)Assets[3]).MaxElectricity / Assets[3].MaxHeat) * winterGm.currentheat[winterGm.currentheat.Count - 1];
                winterTotal = winterHp.totalheat + winterGb.totalheat + winterOb.totalheat + winterGm.totalheat;
            }
            winterHp.Consumption = -((HeatPump)Assets[4]).MaxElectricity * winterHp.totalheat;
            winterGb.CO2 = Assets[0].CO2Emissions * winterGb.totalheat;
            winterGb.Consumption = ((GasBoiler)Assets[0]).GasConsumption * winterGb.totalheat;
            winterOb.CO2 = Assets[2].CO2Emissions * winterOb.totalheat;
            winterOb.Consumption = ((OilBoiler)Assets[2]).OilConsumption * winterOb.totalheat;
            winterGm.CO2 = Assets[3].CO2Emissions * winterGm.totalheat;
            winterGm.Consumption = ((GasMotor)Assets[3]).GasConsumption * winterGm.totalheat;
            //Console.WriteLine($"\nScenario2: Final Total heat produced across all data in winter: {winterTotal:F2} MW(th)");

            foreach (var line in summerData)
            {
                var (hp, gb, ob, gm) = AllocateHeat(line.HeatDemand, line.ElectricityPrice);
                summerHp.currentheat.Add(hp);
                summerHp.totalheat += hp;
                summerHp.totalcost += Assets[4].ProductionCost * hp - ((HeatPump)Assets[4]).MaxElectricity / Assets[4].MaxHeat * line.ElectricityPrice * hp;
                summerGb.currentheat.Add(gb);
                summerGb.totalheat += gb;
                summerGb.totalcost += Assets[0].ProductionCost * summerGb.totalheat;
                summerOb.currentheat.Add(ob);
                summerOb.totalheat += ob;
                summerOb.totalcost += Assets[2].ProductionCost * summerOb.totalheat;
                summerGm.currentheat.Add(gm);
                summerGm.totalheat += gm;
                summerGm.totalcost += Assets[3].ProductionCost * gm - (((GasMotor)Assets[3]).MaxElectricity) / (Assets[3].MaxHeat) * gm * line.ElectricityPrice;
                summerGm.electricity += (((GasMotor)Assets[3]).MaxElectricity / Assets[3].MaxHeat) * summerGm.currentheat[summerGm.currentheat.Count - 1];
                summerTotal = summerHp.totalheat + summerGb.totalheat + summerOb.totalheat + summerGm.totalheat;
            }

            summerHp.Consumption = -((HeatPump)Assets[4]).MaxElectricity * summerHp.totalheat;
            summerGb.CO2 = Assets[0].CO2Emissions * summerGb.totalheat;
            summerGb.Consumption = ((GasBoiler)Assets[0]).GasConsumption * summerGb.totalheat;
            summerOb.CO2 = Assets[2].CO2Emissions * summerOb.totalheat;
            summerOb.Consumption = ((OilBoiler)Assets[2]).OilConsumption * summerOb.totalheat;
            summerGm.CO2 = Assets[3].CO2Emissions * summerGm.totalheat;
            summerGm.Consumption = ((GasMotor)Assets[3]).GasConsumption * summerGm.totalheat;

            S2totalheat = summerGb.totalheat + winterGb.totalheat + summerHp.totalheat + winterHp.totalheat + summerOb.totalheat + winterOb.totalheat + summerGm.totalheat + winterGm.totalheat;
            S2totalelectricity = summerGm.electricity + winterGm.electricity;
            S2totalcosts = summerGb.totalcost + winterGb.totalcost + summerHp.totalcost + winterHp.totalcost + summerOb.totalcost + winterOb.totalcost + summerGm.totalcost + winterGm.totalcost;
            S2totalconsumption = summerGb.Consumption + winterGb.Consumption + summerHp.Consumption + winterHp.Consumption + summerOb.Consumption + winterOb.Consumption + summerGm.Consumption + winterGm.Consumption;
            S2totalCO2 = summerGb.CO2 + winterGb.CO2 + summerHp.CO2 + winterHp.CO2 + summerOb.CO2 + winterOb.CO2 + summerGm.CO2 + winterGm.CO2;


            Console.WriteLine("summer gm total s2s" + summerGm.totalcost);



            WinterSeries = CreateSeries(winterHp.currentheat, winterGb.currentheat, winterOb.currentheat, winterGm.currentheat);
            WinterXAxes = CreateXAxis(winterTimestamps);
            WinterYAxes = CreateYAxis();

            SummerSeries2 = CreateSeries(summerHp.currentheat, summerGb.currentheat, summerOb.currentheat, summerGm.currentheat);
            SummerXAxes2 = CreateXAxis(summerTimestamps);
            SummerYAxes2 = CreateYAxis();
        }

        private (double hp, double gb, double ob, double gm) AllocateHeat(double demand, double price)
        {
            var Assets = assetManager.Assets;
            var capacities = new Dictionary<string, double>
            {
                ["HP"] = Assets[4].MaxHeat,
                ["GB"] = Assets[0].MaxHeat,
                ["OB"] = Assets[2].MaxHeat,
                ["GM"] = Assets[3].MaxHeat,
            };

            var allocation = new Dictionary<string, double>
            {
                ["HP"] = 0,
                ["GB"] = 0,
                ["OB"] = 0,
                ["GM"] = 0
            };

            string[] order = GetPriorityOrder(price);

            foreach (var unit in order)
            {
                if (demand <= 0) break;
                double toUse = Math.Min(capacities[unit], demand);
                allocation[unit] = toUse;
                demand -= toUse;
            }

            return (allocation["HP"], allocation["GB"], allocation["OB"], allocation["GM"]);
        }

        private string[] GetPriorityOrder(double price)
        {
            return price switch
            {
                <= 430.76 => new[] { "HP", "GB", "OB", "GM" },
                <= 460 => new[] { "HP", "GB", "GM", "OB" },
                <= 533.67 => new[] { "GB", "HP", "GM", "OB" },
                <= 610.08 => new[] { "GB", "GM", "HP", "OB" },
                <= 632.76 => new[] { "GB", "GM", "OB", "HP" },
                _ => new[] { "GM", "GB", "OB", "HP" }
            };
        }

        private ISeries[] CreateSeries(List<double> hp, List<double> gb, List<double> ob, List<double> gm) => new ISeries[]
        {
            new StackedColumnSeries<double> { Values = hp, Name = "HP", Fill = new SolidColorPaint(SKColors.Green) },
            new StackedColumnSeries<double> { Values = gb, Name = "GB", Fill = new SolidColorPaint(SKColors.Yellow) },
            new StackedColumnSeries<double> { Values = ob, Name = "OB", Fill = new SolidColorPaint(SKColors.Gray) },
            new StackedColumnSeries<double> { Values = gm, Name = "GM", Fill = new SolidColorPaint(SKColors.Blue) }
        };

        private Axis[] CreateXAxis(List<string> timestamps) => new Axis[]
        {
            new Axis
            {
                Labels = timestamps,
                LabelsRotation = 45,
                Name = "Time",
                Labeler = value => timestamps.ElementAtOrDefault((int)value) ?? ""
            }
        };

        private Axis[] CreateYAxis() => new Axis[]
        {
            new Axis
            {
                Name = "Heat demand (MWh)",
                MinLimit = 0
            }
        };
    }
}
