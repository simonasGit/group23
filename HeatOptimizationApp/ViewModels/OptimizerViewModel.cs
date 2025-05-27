
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
        public Unit winterGb {get; private set; } = new Unit();
        public Unit winterOb { get; private set; } = new Unit();
        public Unit winterGm {get; private set; } = new Unit();

        public Unit summerHp { get; private set; } = new Unit();
        public Unit summerGb { get; private set; } = new Unit();
        public Unit summerOb {get; private set; } = new Unit();
        public Unit summerGm { get; private set; } = new Unit();
        
        public class Unit
        {
            public List<double> demand;
            public double total;

            public Unit()
            {
                demand = new List<double>();
                total = 0;
            }
        }

        public Optimizer()
        {
         
            LoadScenario1();
            LoadScenario2();
        }

        public void LoadScenario1()
        {
            var winterData = SDM.WinterData();
            var summerData = SDM.SummerData();

            var winterTimestamps = winterData.Select(row => row.TimeFrom).ToList();
            var summerTimestamps = summerData.Select(row => row.TimeFrom).ToList();

         
            var winterGb1 = new Unit();
            var winterGb2 = new Unit();
            var winterOb1 = new Unit();

            var summerGb1 = new Unit();
            var summerGb2 = new Unit();
            var summerOb1 = new Unit();

            foreach (var row in winterData)
            {
                var demand = row.HeatDemand;
                winterGb1.demand.Add(Math.Min(demand, 4));
                winterGb1.total += Math.Min(demand, 4);
                winterGb2.demand.Add(Math.Min(Math.Max(demand - 4, 0), 3));
                winterGb2.total += Math.Min(Math.Max(demand - 4, 0), 3);
                winterOb1.demand.Add(Math.Max(demand - 7, 0));
                winterOb1.total += Math.Max(demand - 7, 0);
            }

            foreach (var row in summerData)
            {
                var demand = row.HeatDemand;
                summerGb1.demand.Add(Math.Min(demand, 4));
                summerGb1.total += Math.Min(demand, 4);
                summerGb2.demand.Add(Math.Min(Math.Max(demand - 4, 0), 3));
                summerGb2.total += Math.Min(Math.Max(demand - 4, 0), 3);
                summerOb1.demand.Add(Math.Max(demand - 7, 0));
                summerOb1.total += Math.Max(demand - 7, 0);
            }

            Console.WriteLine($"\nScenario1: Final Total heat produced across all data in winter: {winterGb1.total + winterGb2.total + winterOb1.total:F2} MW(th)");
            Console.WriteLine($"\nScenario1: Final Total heat produced across all data in summer: {summerGb1.total + summerGb2.total + summerOb1.total:F2} MW(th)");


            Series = new ISeries[]
            {
                new StackedColumnSeries<double>
                {
                    Values = winterGb1.demand,
                    Name = "GB1 (Winter)",
                    Fill = new SolidColorPaint(SKColors.Yellow)
                },
                new StackedColumnSeries<double>
                {
                    Values = winterGb2.demand,
                    Name = "GB2 (Winter)",
                    Fill = new SolidColorPaint(SKColors.Orange)
                },
                new StackedColumnSeries<double>
                {
                    Values = winterOb1.demand,
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
                    Values = summerGb1.demand,
                    Name = "GB1 (Summer)",
                    Fill = new SolidColorPaint(SKColors.Yellow)
                },
                new StackedColumnSeries<double>
                {
                    Values = summerGb2.demand,
                    Name = "GB2 (Summer)", 
                    Fill = new SolidColorPaint(SKColors.Orange)
                },
                new StackedColumnSeries<double>
                {
                    Values = summerOb1.demand,
                    Name = "OB1 (Summer)", 
                    Fill = new SolidColorPaint(SKColors.Gray)
                }
            };
            SummerXAxes = CreateXAxis(summerTimestamps); 
            SummerYAxes = CreateYAxis();
        }

        public void LoadScenario2()
        {
            var winterData = SDM.WinterData();
            var summerData = SDM.SummerData();
            var winterTimestamps = winterData.Select(row => row.TimeFrom).ToList();
            var summerTimestamps = summerData.Select(row => row.TimeFrom).ToList();


            double winterTotal = 0;
            double summerTotal = 0;

            foreach (var line in winterData)
            {
                var (hp, gb, ob, gm) = AllocateHeat(line.HeatDemand, line.ElectricityPrice);
                winterHp.demand.Add(hp); 
                winterGb.demand.Add(gb);
                winterOb.demand.Add(ob);
                winterGm.demand.Add(gm);
                winterHp.total += hp; 
                winterGb.total += gb;
                winterOb.total += ob;
                winterGm.total += gm;
                winterTotal = winterHp.total + winterGb.total + winterOb.total + winterGm.total;
            }
            Console.WriteLine($"\nScenario2: Final Total heat produced across all data in winter: {winterTotal:F2} MW(th)");

            foreach (var line in summerData)
            {
                var (hp, gb, ob, gm) = AllocateHeat(line.HeatDemand, line.ElectricityPrice);
                summerHp.demand.Add(hp); 
                summerGb.demand.Add(gb);
                summerOb.demand.Add(ob);
                summerGm.demand.Add(gm);
                summerHp.total += hp; 
                summerOb.total += ob;
                summerGm.total += gm;
                summerTotal = summerHp.total + summerGb.total + summerOb.total + summerGm.total;
            }
            Console.WriteLine($"\nScenario2: Final Total heat produced across all data in summer: {summerTotal:F2} MW(th)");
            Console.WriteLine($"\nScenario2: Final Total heat produced across all data (Total Winter + Total Summer): {winterTotal + summerTotal:F2} MW(th)");

            WinterSeries = CreateSeries(winterHp.demand, winterGb.demand, winterOb.demand, winterGm.demand);
            WinterXAxes = CreateXAxis(winterTimestamps);
            WinterYAxes = CreateYAxis();

            SummerSeries2 = CreateSeries(summerHp.demand, summerGb.demand, summerOb.demand, summerGm.demand);
            SummerXAxes2 = CreateXAxis(summerTimestamps);
            SummerYAxes2 = CreateYAxis();
        }

        private (double hp, double gb, double ob, double gm) AllocateHeat(double demand, double price)
        {
            var capacities = new Dictionary<string, double>
            {
                ["HP"] = 6.0,
                ["GB"] = 3.0,
                ["OB"] = 4.0,
                ["GM"] = 3.5
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
