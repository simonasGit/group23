using Avalonia.Controls;
using System;
using HeatOptimizationApp.ViewModels;
using Avalonia.Markup.Xaml;


namespace HeatOptimizationApp.Views;

public partial class ResultDataManagerWindow : Window
{
    public double summerGB1total { get; set; }
    public double summerGB1CO2 { get; set; }
    public double summerGB1Consumption { get; set; }
    public double summerGB1Costs { get; set; }

    public ResultDataManagerWindow()
    {
        InitializeComponent();
        var ResultDataManager = new ResultDataManager();
        var Assets = ResultDataManager.assetManager.Assets;
        var Optimizer = ResultDataManager.optimizer;
        summerGB1total = Optimizer.summerGb1.totalheat;
        summerGB1CO2 = Assets[0].CO2Emissions * Optimizer.summerGb1.totalheat;
        summerGB1Consumption = ((GasBoiler)Assets[0]).GasConsumption * Optimizer.summerGb1.totalheat;
        summerGB1Costs = Assets[0].ProductionCost * Optimizer.summerGb1.totalheat;
        //el prod/cons
        //prod costs
        this.DataContext = this;
    }
}
