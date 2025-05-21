
using Avalonia.Collections;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia.Data.Converters;

using HeatOptimizationApp;


namespace HeatOptimizationApp.ViewModels
{
    public class AssetManagerWindowViewModel : INotifyPropertyChanged
    {
        private readonly AssetManager _assetManager;

        public AvaloniaList<ProductionUnitViewModel> ProductionUnits { get; set; }

        public ICommand EditUnitCommand { get; }
        public ICommand SaveUnitCommand { get; }
        public ICommand CancelEditCommand { get; }

        public AssetManagerWindowViewModel()
        {
            _assetManager = new AssetManager();

            
            ProductionUnits = new AvaloniaList<ProductionUnitViewModel>();
            foreach (var asset in _assetManager.Assets)
            {
                ProductionUnits.Add(new ProductionUnitViewModel(asset, this)); // 
            }

            EditUnitCommand = new RelayCommand(EditUnit);
            SaveUnitCommand = new RelayCommand(SaveUnit);
            CancelEditCommand = new RelayCommand(CancelEdit);
        }

        private void EditUnit(object? parameter)
        {
            if (parameter is ProductionUnitViewModel unit)
            {
                unit.IsEditing = true;
            }
        }

        private void SaveUnit(object? parameter)
        {
            if (parameter is ProductionUnitViewModel unit)
            {
                unit.IsEditing = false;
            }
        }

        private void CancelEdit(object? parameter)
        {
            if (parameter is ProductionUnitViewModel unit)
            {
                unit.RevertChanges();
                unit.IsEditing = false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, newValue)) return false;
            field = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    public class ProductionUnitViewModel : INotifyPropertyChanged
    {
        internal Asset _asset;
        public Asset AssetModel => _asset; // Public property for XAML

        //
        private AssetManagerWindowViewModel _parentViewModel;


        private bool _isEditing;

        private double _originalMaxHeat;
        private double _originalProductionCost;
        private double _originalCO2Emissions;
        private double _originalGasConsumption;
        private double _originalOilConsumption;
        private double _originalMaxElectricity;


        
        public ProductionUnitViewModel(Asset asset, AssetManagerWindowViewModel parentViewModel)
        {
            _asset = asset ?? throw new ArgumentNullException(nameof(asset));
            _parentViewModel = parentViewModel ?? throw new ArgumentNullException(nameof(parentViewModel)); 

            UnitName = asset.Name;
            MaxHeat = asset.MaxHeat;
            ProductionCosts = asset.ProductionCost;
            CO2Emissions = asset.CO2Emissions;

            if (asset is GasBoiler gasBoiler)
            {
                Consumption = gasBoiler.GasConsumption;
            }
            else if (asset is OilBoiler oilBoiler)
            {
                Consumption = oilBoiler.OilConsumption;
            }
            else if (asset is GasMotor gasMotor)
            {
                Consumption = gasMotor.GasConsumption;
                MaxElectricity = gasMotor.MaxElectricity;
            }
            else if (asset is HeatPump heatPump)
            {
                Consumption = heatPump.MaxElectricity;
                MaxElectricity = heatPump.MaxElectricity;
            }

            IconSource = GetIconSourceForAsset(asset);
        }

        
        public ICommand EditUnitCommand => _parentViewModel.EditUnitCommand; 
        public ICommand SaveUnitCommand => _parentViewModel.SaveUnitCommand; 
        public ICommand CancelEditCommand => _parentViewModel.CancelEditCommand; 


        public string UnitName { get; }

        public double MaxHeat
        {
            get => _asset.MaxHeat;
            set
            {
                if (_asset.MaxHeat != value) { _asset.MaxHeat = value; OnPropertyChanged(); }
            }
        }

        public double ProductionCosts
        {
            get => _asset.ProductionCost;
            set
            {
                if (_asset.ProductionCost != value) { _asset.ProductionCost = value; OnPropertyChanged(); }
            }
        }

        public double CO2Emissions
        {
            get => _asset.CO2Emissions;
            set
            {
                if (_asset.CO2Emissions != value) { _asset.CO2Emissions = value; OnPropertyChanged(); }
            }
        }

        public double Consumption
        {
            get
            {
                if (_asset is GasBoiler gb) return gb.GasConsumption;
                if (_asset is OilBoiler ob) return ob.OilConsumption;
                if (_asset is GasMotor gm) return gm.GasConsumption;
                if (_asset is HeatPump hp) return hp.MaxElectricity;
                return 0.0;
            }
            set
            {
                if (_asset is GasBoiler gb && gb.GasConsumption != value) { gb.GasConsumption = value; OnPropertyChanged(); }
                else if (_asset is OilBoiler ob && ob.OilConsumption != value) { ob.OilConsumption = value; OnPropertyChanged(); }
                else if (_asset is GasMotor gm && gm.GasConsumption != value) { gm.GasConsumption = value; OnPropertyChanged(); }
                else if (_asset is HeatPump hp && hp.MaxElectricity != value) { hp.MaxElectricity = value; OnPropertyChanged(); }
            }
        }

        public double MaxElectricity
        {
            get
            {
                if (_asset is GasMotor gm) return gm.MaxElectricity;
                if (_asset is HeatPump hp) return hp.MaxElectricity;
                return 0.0;
            }
            set
            {
                if (_asset is GasMotor gm && gm.MaxElectricity != value) { gm.MaxElectricity = value; OnPropertyChanged(); }
                else if (_asset is HeatPump hp && hp.MaxElectricity != value) { hp.MaxElectricity = value; OnPropertyChanged(); }
            }
        }

        public string IconSource { get; }

        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                if (SetProperty(ref _isEditing, value))
                {
                    if (value)
                    {
                        _originalMaxHeat = _asset.MaxHeat;
                        _originalProductionCost = _asset.ProductionCost;
                        _originalCO2Emissions = _asset.CO2Emissions;

                        if (_asset is GasBoiler gb) _originalGasConsumption = gb.GasConsumption;
                        else if (_asset is OilBoiler ob) _originalOilConsumption = ob.OilConsumption;
                        else if (_asset is GasMotor gm) { _originalGasConsumption = gm.GasConsumption; _originalMaxElectricity = gm.MaxElectricity; }
                        else if (_asset is HeatPump hp) { _originalMaxElectricity = hp.MaxElectricity; }
                    }
                }
            }
        }

        public void RevertChanges()
        {
            _asset.MaxHeat = _originalMaxHeat;
            _asset.ProductionCost = _originalProductionCost;
            _asset.CO2Emissions = _originalCO2Emissions;

            if (_asset is GasBoiler gb) gb.GasConsumption = _originalGasConsumption;
            else if (_asset is OilBoiler ob) ob.OilConsumption = _originalOilConsumption;
            else if (_asset is GasMotor gm) { gm.GasConsumption = _originalGasConsumption; gm.MaxElectricity = _originalMaxElectricity; }
            else if (_asset is HeatPump hp) { hp.MaxElectricity = _originalMaxElectricity; }

            OnPropertyChanged(nameof(MaxHeat));
            OnPropertyChanged(nameof(ProductionCosts));
            OnPropertyChanged(nameof(CO2Emissions));
            OnPropertyChanged(nameof(Consumption));
            OnPropertyChanged(nameof(MaxElectricity));
        }

        private string GetIconSourceForAsset(Asset asset)
        {
            return asset switch
            {
                GasBoiler => "/Assets/boilers.png",
                OilBoiler => "/Assets/oilboiler.png",
                GasMotor => "/Assets/gasmotor.png",
                HeatPump => "/Assets/heatpump.png",
                
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, newValue)) return false;
            field = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    internal class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Predicate<object?>? _canExecute;

        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object? parameter) => _execute(parameter);

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class AssetTypeConverter : IValueConverter
    {
        public static readonly AssetTypeConverter Instance = new();
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is GasBoiler) return "Gas consumption";
            if (value is OilBoiler) return "Oil consumption";
            if (value is GasMotor) return "Gas consumption";
            if (value is HeatPump) return "Max electricity";
            return "Consumption";
        }
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) { throw new NotImplementedException(); }
    }

    public class UnitSuffixConverter : IValueConverter
    {
        public static readonly UnitSuffixConverter Instance = new();
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is GasBoiler) return "MWh(gas)/MWh(th))";
            if (value is OilBoiler) return "MWh(oil)/MWh(th))";
            if (value is GasMotor) return "MWh(gas)/MWh(th))";
            if (value is HeatPump) return "MW";
            return "";
        }
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) { throw new NotImplementedException(); }
    }

    public class AssetIsTypeConverter : IValueConverter
    {
        public static readonly AssetIsTypeConverter Instance = new();
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null || !(parameter is Type targetAssetType)) { return false; }
            return targetAssetType.IsInstanceOfType(value);
        }
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) { throw new NotImplementedException(); }
    }

    public class AssetHasMaxElectricityConverter : IValueConverter
    {
        public static readonly AssetHasMaxElectricityConverter Instance = new();
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is GasMotor || value is HeatPump;
        }
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) { throw new NotImplementedException(); }
    }
}