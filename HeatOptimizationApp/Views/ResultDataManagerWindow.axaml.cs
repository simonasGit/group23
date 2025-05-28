using Avalonia.Controls;
using System;
using HeatOptimizationApp.ViewModels;
using Avalonia.Markup.Xaml;


namespace HeatOptimizationApp.Views;

public partial class ResultDataManagerWindow : Window
{
    public ResultDataManagerWindow()
    {
        InitializeComponent();
        var ResultDataManager = new ResultDataManager();
        //check
        Console.WriteLine($"winter GB Total: {ResultDataManager.optimizer.winterGb.totalheat}");
        Console.WriteLine($"Assetmanager test: {ResultDataManager.assetManager.Assets[0].ProductionCost}");
    }
}
