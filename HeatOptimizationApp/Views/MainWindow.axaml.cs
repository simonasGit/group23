using Avalonia.Controls;

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
    }
}
