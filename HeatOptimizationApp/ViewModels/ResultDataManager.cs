using Avalonia.Controls;
using System;

namespace HeatOptimizationApp.ViewModels;

public partial class ResultDataManager
{
    public Optimizer optimizer;

    public ResultDataManager()
    {
        optimizer = new Optimizer();
    }
}
