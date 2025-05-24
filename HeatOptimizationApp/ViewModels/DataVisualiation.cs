using Avalonia.Controls;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.Generic;
using SkiaSharp;
using LiveChartsCore.SkiaSharpView.Painting;
using HeatOptimizationApp.Models;
using System.Linq;
using LiveChartsCore.Measure;

namespace HeatOptimizationApp.ViewModels;

public class DataVisualization
{
    public ISeries[] WinterSeries { get; set; }
    public ISeries[] SummerSeries { get; set; }
    public ISeries[] WinterPriceSeries { get; set; }
    public ISeries[] SummerDemandSeries { get; set; }
    public string[] winterLabels;
    public string[] summerLabels;

    public DataVisualization()
    {
        var winterData = SDM.WinterData();
        var summerData = SDM.SummerData();

        // Get readable labels from TimeFrom
        winterLabels = winterData.Select(w => w.TimeFrom).ToArray();
        summerLabels = summerData.Select(s => s.TimeFrom).ToArray();

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
    }

}
