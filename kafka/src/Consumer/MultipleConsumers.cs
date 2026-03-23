using Confluent.Kafka;

namespace Consumer;

public class MultipleConsumers
{
    public async Task RunAsync()
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "1",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        var consumerBuilder = new ConsumerBuilder<string, string>(config);

        Task[] consumers = [
            Consume(consumerBuilder, 0),
            Consume(consumerBuilder, 1),
            Consume(consumerBuilder, 2)
        ];

        await Task.WhenAll(consumers);
    }

    Task Consume(ConsumerBuilder<string, string> builder, int consumerId) =>
        Task.Run(() =>
        {
            using var consumer = builder.Build();
            consumer.Subscribe("demo-topic");

            while (true)
            {
                var result = consumer.Consume(TimeSpan.FromSeconds(5));
                if (result == null) continue;

                Console.WriteLine(
                    $"Consumer: {consumerId}," +
                    $"Partition: {result.Partition.Value}," +
                    $"Offset: {result.Offset}," +
                    $"Key: {result.Message.Key}," +
                    $"Value: {result.Message.Value}"
                );
            }
        });
}
