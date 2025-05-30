using Avalonia.Controls;
using System;
using System.IO; // Required for file operations
using CsvHelper; // Required for CsvHelper
using System.Globalization; // Required for CultureInfo
using System.Collections.Generic; // Required for List<T>
using HeatOptimizationApp.ViewModels; // Assuming ResultDataManager is here
using Avalonia.Markup.Xaml; // Ensure this is present for InitializeComponent

namespace HeatOptimizationApp.Views;

public partial class ResultDataManagerWindow : Window
{
    // Scenario 1 Properties
    public double summerGB1total { get; set; }
    public double summerGB1CO2 { get; set; }
    public double summerGB1Consumption { get; set; }
    public double summerGB1Costs { get; set; }

    public double winterGB1total { get; set; }
    public double winterGB1CO2 { get; set; }
    public double winterGB1Consumption { get; set; }
    public double winterGB1Costs { get; set; }

    public double summerGB2total { get; set; }
    public double summerGB2CO2 { get; set; }
    public double summerGB2Consumption { get; set; }
    public double summerGB2Costs { get; set; }

    public double winterGB2total { get; set; }
    public double winterGB2CO2 { get; set; }
    public double winterGB2Consumption { get; set; }
    public double winterGB2Costs { get; set; }

    public double summerOB1total { get; set; }
    public double summerOB1CO2 { get; set; }
    public double summerOB1Consumption { get; set; }
    public double summerOB1Costs { get; set; }

    public double winterOB1total { get; set; }
    public double winterOB1CO2 { get; set; }
    public double winterOB1Consumption { get; set; }
    public double winterOB1Costs { get; set; }

    public double S1TotalHeat { get; set; }
    public double S1TotalElectricity { get; set; }
    public double S1TotalConsumption { get; set; }
    public double S1TotalCosts { get; set; }
    public double S1TotalCO2 { get; set; }

    // Scenario 2 Properties
    public double summerHPtotal { get; set; }
    public double summerHPCO2 { get; set; }
    public double summerHPConsumption { get; set; }
    public double summerHPCosts { get; set; }

    public double winterHPtotal { get; set; }
    public double winterHPCO2 { get; set; }
    public double winterHPConsumption { get; set; }
    public double winterHPCosts { get; set; }

    public double summerGBtotal { get; set; }
    public double summerGBCO2 { get; set; }
    public double summerGBConsumption { get; set; }
    public double summerGBCosts { get; set; }

    public double winterGBtotal { get; set; }
    public double winterGBCO2 { get; set; }
    public double winterGBConsumption { get; set; }
    public double winterGBCosts { get; set; }

    public double summerOBtotal { get; set; }
    public double summerOBCO2 { get; set; }
    public double summerOBConsumption { get; set; }
    public double summerOBCosts { get; set; }

    public double winterOBtotal { get; set; }
    public double winterOBCO2 { get; set; }
    public double winterOBConsumption { get; set; }
    public double winterOBCosts { get; set; }

    public double summerGMtotal { get; set; }
    public double summerGMCO2 { get; set; }
    public double summerGMConsumption { get; set; }
    public double summerGMCosts { get; set; }
    public double summerGMElectricity { get; set; }

    public double winterGMtotal { get; set; }
    public double winterGMCO2 { get; set; }
    public double winterGMConsumption { get; set; }
    public double winterGMCosts { get; set; }
    public double winterGMElectricity { get; set; }

    public double S2TotalHeat { get; set; }
    public double S2TotalElectricity { get; set; }
    public double S2TotalConsumption { get; set; }
    public double S2TotalCosts { get; set; }
    public double S2TotalCO2 { get; set; }

    public ResultDataManagerWindow()
    {
        InitializeComponent();

        var ResultDataManager = new ResultDataManager();
        // Assuming assetManager and optimizer are initialized within ResultDataManager's constructor
        // or are accessible public properties.
        var Assets = ResultDataManager.assetManager.Assets;
        var Optimizer = ResultDataManager.optimizer;

        // SCENARIO 1 Data Population
        summerGB1total = Optimizer.summerGb1.totalheat;
        summerGB1CO2 = Optimizer.summerGb1.CO2;
        summerGB1Consumption = Optimizer.summerGb1.Consumption;
        summerGB1Costs = Optimizer.summerGb1.totalcost;

        winterGB1total = Optimizer.winterGb1.totalheat;
        winterGB1CO2 = Optimizer.winterGb1.CO2;
        winterGB1Consumption = Optimizer.winterGb1.Consumption;
        winterGB1Costs = Optimizer.winterGb1.totalcost;

        summerGB2total = Optimizer.summerGb2.totalheat;
        summerGB2CO2 = Optimizer.summerGb2.CO2;
        summerGB2Consumption = Optimizer.summerGb2.Consumption;
        summerGB2Costs = Optimizer.summerGb2.totalcost;

        winterGB2total = Optimizer.winterGb2.totalheat;
        winterGB2CO2 = Optimizer.winterGb2.CO2;
        winterGB2Consumption = Optimizer.winterGb2.Consumption;
        winterGB2Costs = Optimizer.winterGb2.totalcost;
        
        summerOB1total = Optimizer.summerOb1.totalheat;
        summerOB1CO2 = Optimizer.summerOb1.CO2;
        summerOB1Consumption = Optimizer.summerOb1.Consumption;
        summerOB1Costs = Optimizer.summerOb1.totalcost;

        winterOB1total = Optimizer.winterOb1.totalheat;
        winterOB1CO2 = Optimizer.winterOb1.CO2;
        winterOB1Consumption = Optimizer.winterOb1.Consumption;
        winterOB1Costs = Optimizer.winterOb1.totalcost;

        // SCENARIO 2 Data Population
        summerHPtotal = Optimizer.summerHp.totalheat;
        summerHPCO2 = Optimizer.summerHp.CO2;
        summerHPConsumption = Optimizer.summerHp.Consumption;
        summerHPCosts = Optimizer.summerHp.totalcost;

        winterHPtotal = Optimizer.winterHp.totalheat;
        winterHPCO2 = Optimizer.winterHp.CO2;
        winterHPConsumption = Optimizer.winterHp.Consumption;
        winterHPCosts = Optimizer.winterHp.totalcost;

        summerGBtotal = Optimizer.summerGb.totalheat;
        summerGBCO2 = Optimizer.summerGb.CO2;
        summerGBConsumption = Optimizer.summerGb.Consumption;
        summerGBCosts = Optimizer.summerGb.totalcost;

        winterGBtotal = Optimizer.winterGb.totalheat;
        winterGBCO2 = Optimizer.winterGb.CO2;
        winterGBConsumption = Optimizer.winterGb.Consumption;
        winterGBCosts = Optimizer.winterGb.totalcost;

        summerOBtotal = Optimizer.summerOb.totalheat; // Corrected to use summerOb
        summerOBCO2 = Optimizer.summerOb.CO2;         // Corrected to use summerOb
        summerOBConsumption = Optimizer.summerOb.Consumption; // Corrected to use summerOb
        summerOBCosts = Optimizer.summerOb.totalcost;   // Corrected to use summerOb

        winterOBtotal = Optimizer.winterOb.totalheat; // Corrected to use winterOb
        winterOBCO2 = Optimizer.winterOb.CO2;         // Corrected to use winterOb
        winterOBConsumption = Optimizer.winterOb.Consumption; // Corrected to use winterOb
        winterOBCosts = Optimizer.winterOb.totalcost;   // Corrected to use winterOb

        summerGMtotal = Optimizer.summerGm.totalheat;
        summerGMCO2 = Optimizer.summerGm.CO2;
        summerGMConsumption = Optimizer.summerGm.Consumption;
        summerGMCosts = Optimizer.summerGm.totalcost;
        summerGMElectricity = Optimizer.summerGm.electricity;

        winterGMtotal = Optimizer.winterGm.totalheat;
        winterGMCO2 = Optimizer.winterGm.CO2;
        winterGMConsumption = Optimizer.winterGm.Consumption;
        winterGMCosts = Optimizer.winterGm.totalcost;
        winterGMElectricity = Optimizer.winterGm.electricity;

        // Totals
        S1TotalHeat = Optimizer.S1totalheat;
        S1TotalElectricity = Optimizer.S1totalelectricity;
        S1TotalCosts = Optimizer.S1totalcosts;
        S1TotalConsumption = Optimizer.S1totalconsumption;
        S1TotalCO2 = Optimizer.S1totalCO2;

        S2TotalHeat = Optimizer.S2totalheat;
        S2TotalElectricity = Optimizer.S2totalelectricity;
        S2TotalCosts = Optimizer.S2totalcosts;
        S2TotalConsumption = Optimizer.S2totalconsumption;
        S2TotalCO2 = Optimizer.S2totalCO2;

        this.DataContext = this;

       
        if (this.FindControl<Button>("SaveButtonScenario1") is Button saveButtonScenario1)
        {
            saveButtonScenario1.Click += SaveButton_Click;
        }

        
        if (this.FindControl<Button>("SaveButtonScenario2") is Button saveButtonScenario2)
        {
            saveButtonScenario2.Click += SaveButton_Click;
        }
       
    }

  
    private void SaveButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
       
        string filePath = "ResultDataManager.csv";

        try
        {
           
            var dataToSave = new List<object>
            {
                new
                {
                   
                    Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),

                    // Scenario 1 Data
                    Scenario = "Scenario 1",
                    SummerGB1TotalHeat = summerGB1total,
                    SummerGB1CO2 = summerGB1CO2,
                    SummerGB1Consumption = summerGB1Consumption,
                    SummerGB1Costs = summerGB1Costs,
                    WinterGB1TotalHeat = winterGB1total,
                    WinterGB1CO2 = winterGB1CO2,
                    WinterGB1Consumption = winterGB1Consumption,
                    WinterGB1Costs = winterGB1Costs,
                    SummerGB2TotalHeat = summerGB2total,
                    SummerGB2CO2 = summerGB2CO2,
                    SummerGB2Consumption = summerGB2Consumption,
                    SummerGB2Costs = summerGB2Costs,
                    WinterGB2TotalHeat = winterGB2total,
                    WinterGB2CO2 = winterGB2CO2,
                    WinterGB2Consumption = winterGB2Consumption,
                    WinterGB2Costs = winterGB2Costs,
                    SummerOB1TotalHeat = summerOB1total,
                    SummerOB1CO2 = summerOB1CO2,
                    SummerOB1Consumption = summerOB1Consumption,
                    SummerOB1Costs = summerOB1Costs,
                    WinterOB1TotalHeat = winterOB1total,
                    WinterOB1CO2 = winterOB1CO2,
                    WinterOB1Consumption = winterOB1Consumption,
                    WinterOB1Costs = winterOB1Costs,
                    S1_TotalHeat = S1TotalHeat,
                    S1_TotalElectricity = S1TotalElectricity,
                    S1_TotalConsumption = S1TotalConsumption,
                    S1_TotalCosts = S1TotalCosts,
                    S1_TotalCO2 = S1TotalCO2,

                    // Scenario 2 Data
                    
                   
                    Scenario2 = "Scenario 2", // Renamed to avoid duplicate 'Scenario' column
                    SummerHPTotalHeat = summerHPtotal,
                    SummerHPCO2 = summerHPCO2,
                    SummerHPConsumption = summerHPConsumption,
                    SummerHPCosts = summerHPCosts,
                    WinterHPTotalHeat = winterHPtotal,
                    WinterHPCO2 = winterHPCO2,
                    WinterHPConsumption = winterHPConsumption,
                    WinterHPCosts = winterHPCosts,
                    SummerGBTotalHeat = summerGBtotal,
                    SummerGBCO2 = summerGBCO2,
                    SummerGBConsumption = summerGBConsumption,
                    SummerGBCosts = summerGBCosts,
                    WinterGBTotalHeat = winterGBtotal,
                    WinterGBCO2 = winterGBCO2,
                    WinterGBConsumption = winterGBConsumption,
                    WinterGBCosts = winterGBCosts,
                    SummerOBTotalHeat = summerOBtotal,
                    SummerOBCO2 = summerOBCO2,
                    SummerOBConsumption = summerOBConsumption,
                    SummerOBCosts = summerOBCosts,
                    WinterOBTotalHeat = winterOBtotal,
                    WinterOBCO2 = winterOBCO2,
                    WinterOBConsumption = winterOBConsumption,
                    WinterOBCosts = winterOBCosts,
                    SummerGMTotalHeat = summerGMtotal,
                    SummerGMCO2 = summerGMCO2,
                    SummerGMConsumption = summerGMConsumption,
                    SummerGMCosts = summerGMCosts,
                    SummerGMElectricity = summerGMElectricity,
                    WinterGMTotalHeat = winterGMtotal,
                    WinterGMCO2 = winterGMCO2,
                    WinterGMConsumption = winterGMConsumption,
                    WinterGMCosts = winterGMCosts,
                    WinterGMElectricity = winterGMElectricity,
                    S2_TotalHeat = S2TotalHeat,
                    S2_TotalElectricity = S2TotalElectricity,
                    S2_TotalConsumption = S2TotalConsumption,
                    S2_TotalCosts = S2TotalCosts,
                    S2_TotalCO2 = S2TotalCO2
                }
            };

           
            bool fileExists = File.Exists(filePath);

            using (var stream = File.Open(filePath, FileMode.Append, FileAccess.Write))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                if (!fileExists)
                {
                    
                    csv.WriteHeader<object>(); 
                    csv.NextRecord(); 
                }

                csv.WriteRecords(dataToSave);
            }

            Console.WriteLine("aaaa");
           

        }
        catch (Exception ex)
        {
            Console.WriteLine("naodeu");
            
        }
    }
}