using System;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;

using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

namespace EventHubsPublish;

class Program
{  
    static readonly Random Rand = new();

    private const string ConnectionString = "";
    private const string EventHubName = "piotreventhub-eh-hub";
         
    static async Task Main(string[] args)
    {
        //await SendToRandomPartition();
        await SendToSamePartition();                   
    }

    static async Task SendToRandomPartition()
    {            
        await using var producerClient = 
            new EventHubProducerClient(ConnectionString, EventHubName);

        using EventDataBatch eventBatch = 
            await producerClient.CreateBatchAsync();
                        
        for (int i = 0; i < 100; i++)
        {
            double waterTemp = (Rand.NextDouble()) * 100;
            int coffeeTypeIndex = Rand.Next(2);

            var coffeeMachineData = new CoffeeData
            { 
                WaterTemperature = waterTemp, 
                ReadingTime = DateTime.Now, 
                CoffeeType = CoffeeData.AllCoffeeTypes[coffeeTypeIndex]
            };

            var coffeeMachineDataBytes = 
                JsonSerializer.SerializeToUtf8Bytes(coffeeMachineData);

            var eventData = new EventData(coffeeMachineDataBytes);

            if (!eventBatch.TryAdd(eventData))
            {
                throw new Exception("Cannot add coffee machine data to random batch");
            }
        }

        await producerClient.SendAsync(eventBatch); 

        Console.WriteLine("Wrote events to random partitions");           
    }
    
    static async Task SendToSamePartition()
    {            
        await using var producerClient = 
            new EventHubProducerClient(ConnectionString, EventHubName);
        
        // can also do this with EventDataBatch - but showing another way
        
        List<EventData> eventBatch = new List<EventData>();
            
        for (int i = 0; i < 100; i++)
        {
            double waterTemp = (Rand.NextDouble()) * 100;
            int coffeeTypeIndex = Rand.Next(2);

            var coffeeMachineData = new CoffeeData
            { 
                WaterTemperature = waterTemp, 
                ReadingTime = DateTime.Now, 
                CoffeeType = CoffeeData.AllCoffeeTypes[coffeeTypeIndex]
            };
        
            var coffeeMachineDataBytes = 
                JsonSerializer.SerializeToUtf8Bytes(coffeeMachineData);

            var eventData = new EventData(coffeeMachineDataBytes);
                         
            eventBatch.Add(eventData);                
        }

        var options = new SendEventOptions
        {
            PartitionKey = "device1"
        };

        await producerClient.SendAsync(eventBatch, options);

        Console.WriteLine("Wrote events to single partition");
    }

}    
