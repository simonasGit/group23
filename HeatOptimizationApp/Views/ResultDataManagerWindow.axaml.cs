using Avalonia.Controls;
using System;
using HeatOptimizationApp.ViewModels;
using Avalonia.Markup.Xaml;


namespace HeatOptimizationApp.Views;

public partial class ResultDataManagerWindow : Window
{
  public double GB1total { get; set; }
    public ResultDataManagerWindow()
    {

        InitializeComponent();
        var ResultDataManager = new ResultDataManager();
        GB1total = ResultDataManager.assetManager.Assets[0].ProductionCost;

        Console.WriteLine(GB1total);
        this.DataContext = this;
    }
}
