using Avalonia.Controls;
using System;

namespace HeatOptimizationApp.Views;

public partial class MainWindow : Window
{
    //the order is messed up but no actual problem so far
    public MainWindow()
    {
        InitializeComponent();
        //commented because admin button doesnt need to work
        //!AdminButton.Click += (_, _) => 
      //  {
       //     var adminWindow = new AdminWindow();
      //  adminWindow.Show();
       // };
        DataVisualizationButton.Click += (_, _) =>
        {
            var dvWindow = new DataVisualizationWindow();
            dvWindow.Show();
        };
        AssetManagerButton.Click += (_, _) =>
        {//using try catch because it was catching, this is for debug and should be removed later hopefuly
        
            var amWindow = new AssetManagerWindow();
            amWindow.Show();
            
        };
        SourceDataManagerButton.Click += (_, _) => 
        {
            var sdmWindow = new SourceDataManagerWindow();
            sdmWindow.Show();
        };
        //the logic is the same if you want to add any other window, but I wanted to make tabs actually
        OptimizerButton.Click += (_, _) =>
        {
            var optWindow = new OptimizerWindow();
            optWindow.Show();
        };
        ResultDataManagerButton.Click += (_, _) =>
        {
            var rdmWindow = new ResultDataManagerWindow();
            rdmWindow.Show();
        };
        
        
        
    }
}
