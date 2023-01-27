using System;

namespace EventHubsConsume
{
    public class CoffeeData
    {
        public static readonly string[] AllCoffeeTypes = { "Sumatra", "Columbian", "French" };

        public double WaterTemperature { get; set; }
        public DateTime ReadingTime { get; set; }
        public string CoffeeType { get; set; }
    }
}