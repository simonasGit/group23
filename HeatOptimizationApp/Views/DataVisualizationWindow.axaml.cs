using Avalonia.Controls;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using SkiaSharp;
using LiveChartsCore.SkiaSharpView.Painting;


namespace HeatOptimizationApp.Views;

public partial class DataVisualizationWindow : Window
{
    public ISeries[] WinterSeries { get; set; }
    public ISeries[] SummerSeries { get; set; }

    public DataVisualizationWindow()
    {
        InitializeComponent();

        var winterValues = new List<double>();
        var summerValues = new List<double>();

        var path = "2025 Heat Production Optimization - Danfoss Deliveries - Source Data Manager - SDM.csv";

        var lines = File.ReadAllLines(path).Skip(4);

        foreach (var line in lines)
        {
            var cols = line.Split(',');

            if (cols.Length > 9 &&
                double.TryParse(cols[3], NumberStyles.Any, CultureInfo.InvariantCulture, out double winterHeat) &&
                double.TryParse(cols[9], NumberStyles.Any, CultureInfo.InvariantCulture, out double summerPrice)) //winter heat demand and summer price, but this logic works for any
            {
                winterValues.Add(winterHeat);
                summerValues.Add(summerPrice);
            }
        }

        WinterSeries = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = winterValues,
                Name = "Winter Heat Demand",
                Stroke = new SolidColorPaint(SKColors.Blue, 2),
                Fill = null
            }
        };

        SummerSeries = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = summerValues,
                Name = "Summer Electricity Price",
                Stroke = new SolidColorPaint(SKColors.Red, 2),
                Fill = null
            }
        };

        WinterChart.Series = WinterSeries;
        SummerChart.Series = SummerSeries;
    }
}
