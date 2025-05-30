using Avalonia.Controls;
using System;

namespace HeatOptimizationApp.ViewModels;

public partial class ResultDataManager
{
    public Optimizer optimizer;
    public AssetManager assetManager;

    public ResultDataManager()
    {
        optimizer = new Optimizer();
        assetManager = new AssetManager();
    }
}
