using Confluent.Kafka;
using System.Diagnostics;

namespace Consumer;

public class BatchConsumer
{
    public async Task RunAsync()
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "12",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };
        var consumerBuilder = new ConsumerBuilder<string, string>(config);

        using var consumer = consumerBuilder.Build();
        consumer.Subscribe("demo-topic");

        var batch = new List<ConsumeResult<string, string>>();

        var stopWatch = new Stopwatch();
        stopWatch.Start();

        while (true)
        {

            var result = consumer.Consume(TimeSpan.FromSeconds(5));
            if (result == null) continue;

            batch.Add(result);
            if (batch.Count < 5 && stopWatch.ElapsedMilliseconds <= TimeSpan.FromSeconds(1).TotalMilliseconds)
            {
                continue;
            }

            foreach (var item in batch)
            {
                Console.WriteLine(
                    $"Partition: {item.Partition.Value}," +
                    $"Offset: {item.Offset}," +
                    $"Key: {item.Message.Key}," +
                    $"Value: {item.Message.Value}"
                );
            }

            batch.Clear();
            consumer.Commit();

            Console.WriteLine("awating next batch");
            await Task.Delay(1000);
            stopWatch.Restart();
        }
    }
}
