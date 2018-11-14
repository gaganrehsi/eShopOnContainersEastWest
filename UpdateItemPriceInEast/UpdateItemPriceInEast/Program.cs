using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace UpdateItemPriceInEast
{
    //Write 2 East 



    class Program
    {
        const string TopicName = "eshop_event_bus";
        const string ServiceBusWriteConnectionString = "Endpoint=sb://eshopsbcdcbguvwwotha.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=rD6FwEQDuXe6ZutG942tHrhBYohenPknDiu7H0mxi/k=";

        static ITopicClient topicClient;

        static void Main(string[] args)
        {

            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        { 
            topicClient = new TopicClient(ServiceBusWriteConnectionString, TopicName);

            var @event = new ProductPriceChangedIntegrationEvent(Convert.ToInt32(args[0]),Convert.ToDecimal(args[1]),0);

            var jsonMessage = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            var message = new Message
            {
                MessageId = Guid.NewGuid().ToString(),
                Body = body,
                Label = "ProductPriceChanged",
            };

            await topicClient.SendAsync(message);

            Console.WriteLine("message sent");
        }
    }
}
