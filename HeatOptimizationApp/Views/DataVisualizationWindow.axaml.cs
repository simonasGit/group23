using Avalonia.Controls;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.Generic;
using SkiaSharp;
using LiveChartsCore.SkiaSharpView.Painting;
using HeatOptimizationApp.Models;
using System.Linq;
using LiveChartsCore.Measure;
using HeatOptimizationApp.ViewModels;

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
        var DataVisualization = new DataVisualization();
        WinterChart.Series = DataVisualization.WinterSeries;
        WinterChart.XAxes = new[]{
                new Axis
                {
                    Name = "Date",
                    Labels = DataVisualization.winterLabels,
                    LabelsRotation = 45,
                    TextSize = 10
                }
            };
        WinterChart.YAxes = new[] { new Axis { Name = "MWh" } };
        WinterChart.ZoomMode = ZoomAndPanMode.X;

        WinterPriceChart.Series = DataVisualization.WinterPriceSeries;
        WinterPriceChart.XAxes = new[]
        {
            new Axis
            {
                Name = "Date",
                Labels = DataVisualization.winterLabels,
                LabelsRotation = 45,
                TextSize = 10
            }
        };
        WinterPriceChart.YAxes = new[] { new Axis { Name = "DKK/MWh" } };
        WinterPriceChart.ZoomMode = ZoomAndPanMode.X;

        SummerChart.Series = DataVisualization.SummerSeries;
        SummerChart.XAxes = new[]
        {
            new Axis
            {
                Name = "Date",
                Labels = DataVisualization.summerLabels,
                LabelsRotation = 45,
                TextSize = 10
            }
        };
        SummerChart.YAxes = new[] { new Axis { Name = "DKK/MWh" } };
        SummerChart.ZoomMode = ZoomAndPanMode.X;

        SummerDemandChart.Series = DataVisualization.SummerDemandSeries;
        SummerDemandChart.XAxes = new[]
            {
            new Axis
            {
                Name = "Date",
                Labels = DataVisualization.summerLabels,
                LabelsRotation = 45,
                TextSize = 10
            }
        };
        SummerDemandChart.YAxes = new[] { new Axis { Name = "MWh" } };
        SummerDemandChart.ZoomMode = ZoomAndPanMode.X;
    }

}
