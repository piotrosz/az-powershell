using CsvHelper;
using System.Globalization;
using NowySwiatStatsPlot;
using ScottPlot;

var outDir = "C:\\temp";

using var reader = new StreamReader(@$"{outDir}\stats.csv");
using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
var records = csv.GetRecords<PlotRecord>().ToList();
Console.WriteLine(records.Count());

double[] noOfPatrons = records.Select(x => (double)x.NoOfPatrons).ToArray();
double[] dates = records.Select(x => x.Date.ToOADate()).ToArray();
var plotNoOfPatrons = new Plot(900, 500);

plotNoOfPatrons.AddScatter(dates, noOfPatrons);
plotNoOfPatrons.XAxis.DateTimeFormat(true);

plotNoOfPatrons.Title("Number of patrons in 2023");
plotNoOfPatrons.YAxis.Label("Number of patrons");

plotNoOfPatrons.SaveFig(@$"{outDir}\no-of-patrons.png");

double[] monthlyAmount = records.Where(x => x.MonthlyAmount.HasValue).Select(x => (double)x.MonthlyAmount.Value).ToArray();
double[] monthlyAmountDates = records.Where(x => x.MonthlyAmount.HasValue).Select(x => x.Date.ToOADate()).ToArray();

var plotMonthlyAmount = new Plot(900, 500);

plotMonthlyAmount.AddScatter(monthlyAmountDates, monthlyAmount);
plotMonthlyAmount.XAxis.DateTimeFormat(true);

plotMonthlyAmount.Title("Monthly amount in 2023");
plotMonthlyAmount.YAxis.Label("Monthly amount");

plotMonthlyAmount.SaveFig(@"C:\temp\monthly-amount.png");

double[] totalAmount = records.Where(x => x.TotalAmount.HasValue).Select(x => (double)x.TotalAmount.Value).ToArray();
double[] totalAmountDates = records.Where(x => x.TotalAmount.HasValue).Select(x => x.Date.ToOADate()).ToArray();

var plotTotalAmount = new Plot(900, 500);

plotTotalAmount.AddScatter(totalAmountDates, totalAmount);
plotTotalAmount.XAxis.DateTimeFormat(true);

plotTotalAmount.Title("Total amount in 2023");
plotTotalAmount.YAxis.Label("Total amount");

plotTotalAmount.SaveFig(@$"{outDir}\total-amount.png");
