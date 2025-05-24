using Avalonia.Controls;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.Generic;
using SkiaSharp;
using LiveChartsCore.SkiaSharpView.Painting;
using HeatOptimizationApp.Models;
using System.Linq;
using LiveChartsCore.Measure;

namespace HeatOptimizationApp.Views;

public partial class DataVisualizationWindow : Window
{
    public ISeries[] WinterSeries { get; set; }
    public ISeries[] SummerSeries { get; set; }
    public ISeries[] WinterPriceSeries { get; set; }
    public ISeries[] SummerDemandSeries { get; set; }

    public DataVisualizationWindow()
    {
        InitializeComponent();


        var path = "2025 Heat Production Optimization - Danfoss Deliveries - Source Data Manager - SDM.csv";

        var winterData = SDM.WinterData(path);
        var summerData = SDM.SummerData(path);

        // Get readable labels from TimeFrom
        var winterLabels = winterData.Select(w => w.TimeFrom).ToArray();
        var summerLabels = summerData.Select(s => s.TimeFrom).ToArray();

        WinterSeries = new ISeries[]
        {
        new LineSeries<double>
        {
            Values = winterData.Select(w => w.HeatDemand).ToList(),
            Name = "Winter Heat Demand",
            Stroke = new SolidColorPaint(SKColors.Blue, 2),
            Fill = null,
            GeometrySize = 0
        }
        };

        WinterPriceSeries = new ISeries[]
        {
        new LineSeries<double>
        {
            Values = winterData.Select(w => w.ElectricityPrice).ToList(),
            Name = "Winter Electricity Price",
            Stroke = new SolidColorPaint(SKColors.Green, 2),
            Fill = null,
            GeometrySize = 0
        }
        };

        SummerSeries = new ISeries[]
        {
        new LineSeries<double>
        {
            Values = summerData.Select(s => s.ElectricityPrice).ToList(),
            Name = "Summer Electricity Price",
            Stroke = new SolidColorPaint(SKColors.Red, 2),
            Fill = null,
            GeometrySize = 0
        }
        };

        SummerDemandSeries = new ISeries[]
        {
        new LineSeries<double>
        {
            Values = summerData.Select(s => s.HeatDemand).ToList(),
            Name = "Summer Heat Demand",
            Stroke = new SolidColorPaint(SKColors.Orange, 2),
            Fill = null,
            GeometrySize = 0
        }
        };

        WinterChart.Series = WinterSeries;
        WinterChart.XAxes = new[]
        {
        new Axis
        {
            Name = "Date",
            Labels = winterLabels,
            LabelsRotation = 45,
            TextSize = 10
        }
    };
        WinterChart.YAxes = new[] { new Axis { Name = "MWh" } };
        WinterChart.ZoomMode = ZoomAndPanMode.X;

        WinterPriceChart.Series = WinterPriceSeries;
        WinterPriceChart.XAxes = new[]
        {
        new Axis
        {
            Name = "Date",
            Labels = winterLabels,
            LabelsRotation = 45,
            TextSize = 10
        }
    };
        WinterPriceChart.YAxes = new[] { new Axis { Name = "DKK/MWh" } };
        WinterPriceChart.ZoomMode = ZoomAndPanMode.X;

        SummerChart.Series = SummerSeries;
        SummerChart.XAxes = new[]
        {
        new Axis
        {
            Name = "Date",
            Labels = summerLabels,
            LabelsRotation = 45,
            TextSize = 10
        }
    };
        SummerChart.YAxes = new[] { new Axis { Name = "DKK/MWh" } };
        SummerChart.ZoomMode = ZoomAndPanMode.X;

        SummerDemandChart.Series = SummerDemandSeries;
        SummerDemandChart.XAxes = new[]
        {
        new Axis
        {
            Name = "Date",
            Labels = summerLabels,
            LabelsRotation = 45,
            TextSize = 10
        }
    };
        SummerDemandChart.YAxes = new[] { new Axis { Name = "MWh" } };
        SummerDemandChart.ZoomMode = ZoomAndPanMode.X;
    }

}
