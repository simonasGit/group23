using Avalonia.Controls;
using System;
using HeatOptimizationApp.ViewModels;


namespace HeatOptimizationApp.Views;

public partial class ResultDataManagerWindow : Window
{
    public ResultDataManagerWindow()
    {
        InitializeComponent();
        var ResultDataManager = new ResultDataManager();
        //check
        Console.WriteLine($"winter GB Total: {ResultDataManager.optimizer.winterGb.total}");
    }
}
