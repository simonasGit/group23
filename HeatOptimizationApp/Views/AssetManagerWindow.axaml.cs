
using Avalonia.Controls;
using HeatOptimizationApp.ViewModels;

namespace HeatOptimizationApp.Views;

public partial class AssetManagerWindow : Window
{
    public AssetManagerWindow()
    {
        InitializeComponent();
        DataContext = new AssetManagerWindowViewModel();
    }
}