using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace AenSidhe.Kafka.Test.Producer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: .. brokerList topicName");
                return;
            }

            var brokerList = args[0];
            var topicName = args[1];

            MainAsync(brokerList, topicName).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string brokerList, string topicName)
        {
            var config = new Dictionary<string, object> {{"bootstrap.servers", brokerList}};

            using (var producer = new Producer<string, string>(
                config,
                new StringSerializer(Encoding.UTF8),
                new StringSerializer(Encoding.UTF8)))
            {
                Console.WriteLine();
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine($"Producer {producer.Name} producing on topic {topicName}.");
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine("To create a kafka message with UTF-8 encoded key/value message:");
                Console.WriteLine("> key value<Enter>");
                Console.WriteLine("To create a kafka message with empty key and UTF-8 encoded value:");
                Console.WriteLine("> value<enter>");
                Console.WriteLine("Ctrl-C to quit.");
                Console.WriteLine();

                var cancelled = false;
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true; // prevent the process from terminating.
                    cancelled = true;
                };

                while (!cancelled)
                {
                    Console.Write("> ");

                    string text;
                    try
                    {
                        text = Console.ReadLine();
                    }
                    catch (IOException)
                    {
                        // IO exception is thrown when ConsoleCancelEventArgs.Cancel == true.
                        break;
                    }

                    if (text == null)
                    {
                        // Console returned null before
                        // the CancelKeyPress was treated
                        break;
                    }

                    var key = "";
                    var val = text;

                    // split line if both key and value specified.
                    var index = text.IndexOf(" ", StringComparison.InvariantCulture);
                    if (index != -1)
                    {
                        key = text.Substring(0, index);
                        val = text.Substring(index + 1);
                    }

                    var result = await producer.ProduceAsync(topicName, key, val);
                    Console.WriteLine($"Partition: {result.Partition}, Offset: {result.Offset}");
                }
            }
        }
    }
}
