using Avalonia.Controls;
using System;
using HeatOptimizationApp.ViewModels;
using Avalonia.Markup.Xaml;


namespace HeatOptimizationApp.Views;

public partial class ResultDataManagerWindow : Window
{
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
    //scenario 2

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

    public double winterGMtotal { get; set; }
    public double winterGMCO2 { get; set; }
    public double winterGMConsumption { get; set; }
    public double winterGMCosts { get; set; }
    public ResultDataManagerWindow()
    {
        InitializeComponent();

        var ResultDataManager = new ResultDataManager();
        var Assets = ResultDataManager.assetManager.Assets;
        var Optimizer = ResultDataManager.optimizer;

        //SCENARIO 1
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
        //el prod/cons
        summerOB1total = Optimizer.summerOb1.totalheat;
        summerOB1CO2 = Optimizer.summerOb1.CO2;
        summerOB1Consumption = Optimizer.summerOb1.Consumption;
        summerOB1Costs = Optimizer.summerOb1.totalcost;

        winterOB1total = Optimizer.winterOb1.totalheat;
        winterOB1CO2 = Optimizer.winterOb1.CO2;
        winterOB1Consumption = Optimizer.winterOb1.Consumption;
        winterOB1Costs = Optimizer.winterOb1.totalcost;

        //SCENARIO 2
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

        summerOBtotal = Optimizer.summerGb.totalheat;
        summerOBCO2 = Optimizer.summerGb.CO2;
        summerOBConsumption = Optimizer.summerGb.Consumption;
        summerOBCosts = Optimizer.summerGb.totalcost;

        winterOBtotal = Optimizer.winterGb.totalheat;
        winterOBCO2 = Optimizer.winterGb.CO2;
        winterOBConsumption = Optimizer.winterGb.Consumption;
        winterOBCosts = Optimizer.winterGb.totalcost;

        summerGMtotal = Optimizer.summerGb.totalheat;
        summerGMCO2 = Optimizer.summerGb.CO2;
        summerGMConsumption = Optimizer.summerGb.Consumption;
        summerGMCosts = Optimizer.summerGb.totalcost;

        winterGMtotal = Optimizer.winterGb.totalheat;
        winterGMCO2 = Optimizer.winterGb.CO2;
        winterGMConsumption = Optimizer.winterGb.Consumption;
        winterGMCosts = Optimizer.winterGb.totalcost;
        this.DataContext = this;
    }
}
