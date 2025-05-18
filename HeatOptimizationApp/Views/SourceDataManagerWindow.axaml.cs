using Avalonia.Controls;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Linq;
using HeatOptimizationApp.Models;

namespace HeatOptimizationApp.Views;

public partial class SourceDataManagerWindow : Window
{
    public ISeries[] WinterDemandSeries { get; set; }
    public ISeries[] SummerDemandSeries { get; set; }
    public ISeries[] WinterElectricitySeries { get; set; }
    public ISeries[] SummerElectricitySeries { get; set; }

    public SourceDataManagerWindow()
    {
        InitializeComponent();

        var path = "2025 Heat Production Optimization - Danfoss Deliveries - Source Data Manager - SDM.csv";
        var winterData = CSVData.WinterData(path);
        var summerData = CSVData.SummerData(path);

        WinterDemandSeries = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = winterData.Select(w => w.HeatDemand).ToList(),
                Name = "Winter Heat Demand",
                Stroke = new SolidColorPaint(SKColors.Blue, 2),
                Fill = null
            }
        };

        SummerDemandSeries = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = summerData.Select(s => s.HeatDemand).ToList(),
                Name = "Summer Heat Demand",
                Stroke = new SolidColorPaint(SKColors.Orange, 2),
                Fill = null
            }
        };

        WinterElectricitySeries = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = winterData.Select(w => w.ElectricityPrice).ToList(),
                Name = "Winter Electricity Price",
                Stroke = new SolidColorPaint(SKColors.DarkCyan, 2),
                Fill = null
            }
        };

        SummerElectricitySeries = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = summerData.Select(s => s.ElectricityPrice).ToList(),
                Name = "Summer Electricity Price",
                Stroke = new SolidColorPaint(SKColors.Red, 2),
                Fill = null
            }
        };
    }
}
