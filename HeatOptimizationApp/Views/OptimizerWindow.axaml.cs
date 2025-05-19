using Avalonia.Controls;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Avalonia.Interactivity;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace HeatOptimizationApp.Views
{
    public partial class OptimizerWindow : Window
    {
        public ISeries[] Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }

        public OptimizerWindow()
        {
            InitializeComponent();

            var filePath = "2025 Heat Production Optimization - Danfoss Deliveries - Source Data Manager - SDM.csv";
            var lines = File.ReadAllLines(filePath).Skip(4).ToList(); // Skip headers

            var timestamps = new List<string>();
            var gb1 = new List<double>();
            var gb2 = new List<double>();
            var ob1 = new List<double>();

            foreach (var line in lines)
            {
                var columns = line.Split(',');

                if (columns.Length < 4 || !double.TryParse(columns[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var demand))
                    continue;

                timestamps.Add(columns[1]); // Time from
                gb1.Add(Math.Min(demand, 4));
                gb2.Add(Math.Min(Math.Max(demand - 4, 0), 3));
                ob1.Add(Math.Max(demand - 7, 0));
            }

            Series = new ISeries[]
            {
                new StackedColumnSeries<double>
                {
                    Values = gb1,
                    Name = "GB1",
                    Fill = new SolidColorPaint(SKColors.Yellow)
                },
                new StackedColumnSeries<double>
                {
                    Values = gb2,
                    Name = "GB2",
                    Fill = new SolidColorPaint(SKColors.Orange)
                },
                new StackedColumnSeries<double>
                {
                    Values = ob1,
                    Name = "OB1",
                    Fill = new SolidColorPaint(SKColors.Gray)
                }
            };

            XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = timestamps,
                    LabelsRotation = 45,
                    TextSize = 12,
                    Name = "Time"
                }
            };

            YAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Heat Demand (MWh)",
                    TextSize = 14
                }
            };

            DataContext = this;
        }
    }
}
