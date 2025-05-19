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

        var winterData = CSVData.WinterData(path);
        var summerData = CSVData.SummerData(path);

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

        // Assign series to charts
        WinterChart.Series = WinterSeries;
        WinterChart.XAxes = new[] { new Axis { Name = "Index" } };
        WinterChart.YAxes = new[] { new Axis { Name = "Heat Demand (MWh)" } };
        WinterChart.ZoomMode = ZoomAndPanMode.X;

        WinterPriceChart.Series = WinterPriceSeries;
        WinterPriceChart.XAxes = new[] { new Axis { Name = "Index" } };
        WinterPriceChart.YAxes = new[] { new Axis { Name = "Electricity Price (DKK/MWh)" } };
        WinterPriceChart.ZoomMode = ZoomAndPanMode.X;

        SummerChart.Series = SummerSeries;
        SummerChart.XAxes = new[] { new Axis { Name = "Index" } };
        SummerChart.YAxes = new[] { new Axis { Name = "Electricity Price (DKK/MWh)" } };
        SummerChart.ZoomMode = ZoomAndPanMode.X;

        SummerDemandChart.Series = SummerDemandSeries;
        SummerDemandChart.XAxes = new[] { new Axis { Name = "Index" } };
        SummerDemandChart.YAxes = new[] { new Axis { Name = "Heat Demand (MWh)" } };
        SummerDemandChart.ZoomMode = ZoomAndPanMode.X;
    }
}
