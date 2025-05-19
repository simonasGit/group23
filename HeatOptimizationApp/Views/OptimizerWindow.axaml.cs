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

public partial class OptimizerWindow : Window
{
    public ISeries[] Series { get; set; }

    public OptimizerWindow()
    {
        Series = new ISeries[]
        {
            new StackedAreaSeries<double>(new double[] { 3, 2, 3, 5, 3, 4, 6 }),
            new StackedAreaSeries<double>(new double[] { 6, 5, 6, 3, 8, 5, 2 }),
            new StackedAreaSeries<double>(new double[] { 4, 8, 2, 8, 9, 5, 3 })
        };

        InitializeComponent();
        DataContext = this;
        WinterChart.Series = Series;
    }
}
