using Avalonia.Controls;
using HeatOptimizationApp.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using HeatOptimizationApp.ViewModels;

namespace HeatOptimizationApp.Views
{
    public partial class OptimizerWindow : Window
    {
        public ISeries[] Series { get; set; }
        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }

        public ISeries[] SummerSeries { get; set; }
        public Axis[] SummerXAxes { get; set; }
        public Axis[] SummerYAxes { get; set; }

        public ISeries[] WinterSeries { get; set; }
        public Axis[] WinterXAxes { get; set; }
        public Axis[] WinterYAxes { get; set; }

        public ISeries[] SummerSeries2 { get; set; }
        public Axis[] SummerXAxes2 { get; set; }
        public Axis[] SummerYAxes2 { get; set; }

        public OptimizerWindow()
        {
            InitializeComponent();

            var optimizer = new Optimizer();
            Series = optimizer.Series;
            XAxes = optimizer.XAxes;
            YAxes = optimizer.YAxes;

            SummerSeries = optimizer.SummerSeries;
            SummerXAxes = optimizer.SummerXAxes;
            SummerYAxes = optimizer.SummerYAxes;

            WinterSeries = optimizer.WinterSeries;
            WinterXAxes = optimizer.WinterXAxes;
            WinterYAxes = optimizer.WinterYAxes;

            SummerSeries2 = optimizer.SummerSeries2;
            SummerXAxes2 = optimizer.SummerXAxes2;
            SummerYAxes2 = optimizer.SummerYAxes2;
            DataContext = this;
        }
    }
}
