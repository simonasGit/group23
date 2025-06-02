using Avalonia.Controls;
using System;

namespace HeatOptimizationApp.Views;

public partial class MainWindow : Window
{
   
    public MainWindow()
    {
        InitializeComponent();
        
        DataVisualizationButton.Click += (_, _) =>
        {
            var dvWindow = new DataVisualizationWindow();
            dvWindow.Show();
        };
        AssetManagerButton.Click += (_, _) =>
        {

            var amWindow = new AssetManagerWindow();
            amWindow.Show();

        };
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
