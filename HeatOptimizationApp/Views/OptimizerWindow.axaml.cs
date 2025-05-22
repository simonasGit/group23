using Avalonia.Controls;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Interactivity;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using HeatOptimizationApp.Models;

namespace HeatOptimizationApp.Views
{
    public partial class OptimizerWindow : Window
    {
        public ISeries[] Series { get; set; } = Array.Empty<ISeries>();
        public Axis[] XAxes { get; set; } = Array.Empty<Axis>();
        public Axis[] YAxes { get; set; } = Array.Empty<Axis>();

        public ISeries[] SummerSeries { get; set; } = Array.Empty<ISeries>();
        public Axis[] SummerXAxes { get; set; } = Array.Empty<Axis>();
        public Axis[] SummerYAxes { get; set; } = Array.Empty<Axis>();

        public OptimizerWindow()
        {
            InitializeComponent();

            var filePath = "2025 Heat Production Optimization - Danfoss Deliveries - Source Data Manager - SDM.csv";
            Scenario1(filePath);
            DataContext = this;

            var tabControl = this.FindControl<TabControl>("TabControl");
            if (tabControl != null)
            {
                tabControl.SelectionChanged += TabControl_SelectionChanged;
            }
        }

        private void TabControl_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var selectedTab = (sender as TabControl)?.SelectedIndex;
            var filePath = "2025 Heat Production Optimization - Danfoss Deliveries - Source Data Manager - SDM.csv";

            if (selectedTab == 0)
            {
                Scenario1(filePath);
            }
            else if (selectedTab == 1)
            {
                Scenario2(filePath);
            }
        }

        public void Scenario1(string filePath)
        {
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

            XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = winterTimestamps,
                    LabelsRotation = 45,
                    TextSize = 12,
                    Name = "Time (Winter)",
                    MinStep = 1,
                    UnitWidth = 1,
                    Labeler = value => winterTimestamps.ElementAtOrDefault((int)value) ?? string.Empty
                }
            };

            YAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Heat Demand (MWh)",
                    TextSize = 14,
                    MinLimit = 0
                }
            };

            var summerData = CSVData.SummerData(filePath);
            var summerTimestamps = summerData.Select(row => row.TimeFrom).ToList();
            var summerGb1 = new List<double>();
            var summerGb2 = new List<double>();
            var summerOb1 = new List<double>();

            foreach (var row in summerData)
            {
                var demand = row.HeatDemand;
                summerGb1.Add(Math.Min(demand, 4));
                summerGb2.Add(Math.Min(Math.Max(demand - 4, 0), 3));
                summerOb1.Add(Math.Max(demand - 7, 0));
            }

            SummerSeries = new ISeries[]
            {
                new StackedColumnSeries<double>
                {
                    Values = summerGb1,
                    Name = "GB1 (Summer)",
                    Fill = new SolidColorPaint(SKColors.Yellow)
                },
                new StackedColumnSeries<double>
                {
                    Values = summerGb2,
                    Name = "GB2 (Summer)",
                    Fill = new SolidColorPaint(SKColors.Orange)
                },
                new StackedColumnSeries<double>
                {
                    Values = summerOb1,
                    Name = "OB1 (Summer)",
                    Fill = new SolidColorPaint(SKColors.Gray)
                }
            };

            SummerXAxes = new Axis[]
            {
                new Axis
                {
                    Labels = summerTimestamps,
                    LabelsRotation = 45,
                    TextSize = 12,
                    Name = "Time (Summer)",
                    MinStep = 1,
                    UnitWidth = 1,
                    Labeler = value => summerTimestamps.ElementAtOrDefault((int)value) ?? string.Empty
                }
            };

            SummerYAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Heat Demand (MWh)",
                    TextSize = 14,
                    MinLimit = 0
                }
            };

            DataContext = null;
            DataContext = this;
        }

        public void Scenario2(string filePath)
        {
            // Placeholder for Scenario 2
        }
    }
}
