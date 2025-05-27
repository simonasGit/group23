using Avalonia.Controls;
using System;
using HeatOptimizationApp.ViewModels;


namespace HeatOptimizationApp.Views;

public partial class ResultDataManagerWindow : Window
{
  
    private Optimizer optimizer;

    public ResultDataManagerWindow()
    {
        InitializeComponent();


        optimizer = new Optimizer();



        Console.WriteLine($"winter GB Total: {optimizer.winterGb.total}");
    
     
        
    }
}
