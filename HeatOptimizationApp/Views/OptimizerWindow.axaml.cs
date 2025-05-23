using Avalonia.Controls;
using HeatOptimizationApp.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using HeatOptimizationApp.Models;

namespace HeatOptimizationApp.Views
{
    public partial class OptimizerWindow : Window
    {
        public ISeries[] Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }

        public ISeries[] SummerSeries { get; set; }
        public Axis[] SummerXAxes { get; set; }
        public Axis[] SummerYAxes { get; set; }

        public ISeries[] WinterSeries { get; set; }
        public Axis[] WinterXAxes { get; set; }
        public Axis[] WinterYAxes { get; set; }

        public ISeries[] SummerSeries2 { get; set; }
        public Axis[] SummerXAxes2 { get; set; }
        public Axis[] SummerYAxes2 { get; set; }

        public OptimizerWindow()
        {
            InitializeComponent();

            LoadScenario1();
            LoadScenario2();

            DataContext = this;
        }

        private void LoadScenario1()
        {
            var filePath = "2025 Heat Production Optimization - Danfoss Deliveries - Source Data Manager - SDM.csv";
            var winterData = CSVData.WinterData(filePath);

            var winterTimestamps = winterData.Select(row => row.TimeFrom).ToList();

            var winterGb1 = new List<double>();
            var winterGb2 = new List<double>();
            var winterOb1 = new List<double>();

        foreach (var row in winterData)
        {
        var demand = row.HeatDemand;
        winterGb1.Add(Math.Min(demand, 4));
        winterGb2.Add(Math.Min(Math.Max(demand - 4, 0), 3));
        winterOb1.Add(Math.Max(demand - 7, 0));
        }

        Series = new ISeries[]
        {
        new StackedColumnSeries<double>
        {
            Values = winterGb1,
            Name = "GB1 (Winter)",
            Fill = new SolidColorPaint(SKColors.Yellow)
        },
        new StackedColumnSeries<double>
        {
            Values = winterGb2,
            Name = "GB2 (Winter)",
            Fill = new SolidColorPaint(SKColors.Orange)
        },
        new StackedColumnSeries<double>
        {
            Values = winterOb1,
            Name = "OB1 (Winter)",
            Fill = new SolidColorPaint(SKColors.Gray)
        }
        };

    XAxes = CreateXAxis(winterTimestamps);
    YAxes = CreateYAxis();
        }

        private void LoadScenario2()
        {
            var filePath = "2025 Heat Production Optimization - Danfoss Deliveries - Source Data Manager - SDM.csv";
            var lines = File.ReadAllLines(filePath).Skip(4);

            var winterTimestamps = new List<string>();
            var winterHp = new List<double>();
            var winterGb = new List<double>();
            var winterOb = new List<double>();
            var winterGm = new List<double>();

            var summerTimestamps = new List<string>();
            var summerHp = new List<double>();
            var summerGb = new List<double>();
            var summerOb = new List<double>();
            var summerGm = new List<double>();

            foreach (var line in lines)
            {
                var columns = line.Split(',');
                if (columns.Length < 10)
                    continue;

                if (double.TryParse(columns[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var winterDemand) &&
                    double.TryParse(columns[4], NumberStyles.Any, CultureInfo.InvariantCulture, out var winterPrice))
                {
                    winterTimestamps.Add(columns[1]);
                    var (hp, gb, ob, gm) = AllocateByPriority(winterDemand, winterPrice);
                    winterHp.Add(hp);
                    winterGb.Add(gb);
                    winterOb.Add(ob);
                    winterGm.Add(gm);
                }

                if (double.TryParse(columns[8], NumberStyles.Any, CultureInfo.InvariantCulture, out var summerDemand) &&
                    double.TryParse(columns[9], NumberStyles.Any, CultureInfo.InvariantCulture, out var summerPrice))
                {
                    summerTimestamps.Add(columns[6]);
                    var (hp, gb, ob, gm) = AllocateByPriority(summerDemand, summerPrice);
                    summerHp.Add(hp);
                    summerGb.Add(gb);
                    summerOb.Add(ob);
                    summerGm.Add(gm);
                }
            }

            WinterSeries = CreateSeries(winterHp, winterGb, winterOb, winterGm);
            WinterXAxes = CreateXAxis(winterTimestamps);
            WinterYAxes = CreateYAxis();

            SummerSeries2 = CreateSeries(summerHp, summerGb, summerOb, summerGm);
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

        private (double hp, double gb, double ob, double gm) AllocateByPriority(double demand, double price)
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

            string[] order = GetPriorityOrderElectric(price);

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
            if (price <= 430.76) return new[] { "HP", "GB", "OB", "GM" };
            if (price <= 460) return new[] { "HP", "GB", "GM", "OB" };
            if (price <= 533.67) return new[] { "GB", "HP", "GM", "OB" };
            if (price <= 610.08) return new[] { "GB", "GM", "HP", "OB" };
            if (price <= 632.76) return new[] { "GB", "GM", "OB", "HP" };
            return new[] { "GM", "GB", "OB", "HP" };
        }

        private string[] GetPriorityOrderElectric(double price)
        {
            if (price <= 430.76) return new[] { "HP", "GB", "OB", "GM" };
            if (price <= 460) return new[] { "HP", "GB", "GM", "OB" };
            if (price <= 533.67) return new[] { "GB", "HP", "GM", "OB" };
            if (price <= 610.08) return new[] { "GB", "GM", "HP", "OB" };
            if (price <= 632.76) return new[] { "GB", "GM", "OB", "HP" };
            return new[] { "GM", "GB", "OB", "HP" };
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
                Name = "Heat Supplied (MWh)",
                MinLimit = 0
            }
        };
    }
}
