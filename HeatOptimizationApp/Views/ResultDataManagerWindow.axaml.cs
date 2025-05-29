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

    public double winterGB1total { get; set; }
    public double winterGB1CO2 { get; set; }
    public double winterGB1Consumption { get; set; }
    public double winterGB1Costs { get; set; }

    public double summerGB2total { get; set; }
    public double summerGB2CO2 { get; set; }
    public double summerGB2Consumption { get; set; }
    public double summerGB2Costs { get; set; }

    public double winterGB2total { get; set; }
    public double winterGB2CO2 { get; set; }
    public double winterGB2Consumption { get; set; }
    public double winterGB2Costs { get; set; }
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

        winterGB1total = Optimizer.winterGb1.totalheat;
        winterGB1CO2 = Assets[0].CO2Emissions * Optimizer.winterGb1.totalheat;
        winterGB1Consumption = ((GasBoiler)Assets[0]).GasConsumption * Optimizer.winterGb1.totalheat;
        winterGB1Costs = Assets[0].ProductionCost * Optimizer.winterGb1.totalheat;

        summerGB2total = Optimizer.summerGb2.totalheat;
        summerGB2CO2 = Assets[1].CO2Emissions * Optimizer.summerGb2.totalheat;
        summerGB2Consumption = ((GasBoiler)Assets[1]).GasConsumption * Optimizer.summerGb2.totalheat;
        summerGB2Costs = Assets[1].ProductionCost * Optimizer.summerGb2.totalheat;

        winterGB2total = Optimizer.winterGb2.totalheat;
        winterGB2CO2 = Assets[1].CO2Emissions * Optimizer.winterGb2.totalheat;
        winterGB2Consumption = ((GasBoiler)Assets[1]).GasConsumption * Optimizer.winterGb2.totalheat;
        winterGB2Costs = Assets[1].ProductionCost * Optimizer.winterGb2.totalheat;
        //el prod/cons
        this.DataContext = this;
    }
}
