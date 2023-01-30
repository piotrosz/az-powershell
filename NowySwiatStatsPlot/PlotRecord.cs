using CsvHelper.Configuration.Attributes;

namespace NowySwiatStatsPlot
{
    public class PlotRecord
    {
        [Name(("RowKey"))]
        public DateTime Date { get; set; }

        public int NoOfPatrons { get; set; }

        public int? MonthlyAmount { get; set;}

        public int? TotalAmount { get; set;}
    }
}
