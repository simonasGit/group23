using Avalonia; 
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq; 
using Avalonia.Markup.Xaml;
using HeatOptimizationApp.ViewModels; 
using HeatOptimizationApp.Views;    
using System.Globalization; 
using System.Threading;     

namespace HeatOptimizationApp
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;




            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
                
            }


            base.OnFrameworkInitializationCompleted();
        }
    }
}