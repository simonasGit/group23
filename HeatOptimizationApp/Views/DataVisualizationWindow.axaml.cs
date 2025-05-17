using Avalonia.Controls;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.Generic;
using SkiaSharp;
using LiveChartsCore.SkiaSharpView.Painting;
using HeatOptimizationApp.Models;
using System.Linq;


namespace HeatOptimizationApp.Views;

public partial class DataVisualizationWindow : Window
{
    public ISeries[] WinterSeries { get; set; }
    public ISeries[] SummerSeries { get; set; }

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
                Fill = null
            }
        };

        SummerSeries = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = summerData.Select(s => s.ElectricityPrice).ToList(),
                Name = "Summer Electricity Price",
                Stroke = new SolidColorPaint(SKColors.Red, 2),
                Fill = null
            }
        };

        WinterChart.Series = WinterSeries;
        SummerChart.Series = SummerSeries;
    }
}
