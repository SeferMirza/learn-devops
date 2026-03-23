using Confluent.Kafka;

namespace Consumer;

public class ExceedMaxPollIntervalConsumer
{
    public async Task RunAsync()
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "99",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false,
            MaxPollIntervalMs = 11000,
            SessionTimeoutMs = 10000,
            HeartbeatIntervalMs = 3000
        };
        var consumerBuilder = new ConsumerBuilder<string, string>(config).SetLogHandler((_, __) => { });

        using var consumer = consumerBuilder.Build();
        consumer.Subscribe("demo-topic");

        while (true)
        {
            try
            {
                var result = consumer.Consume(TimeSpan.FromMilliseconds(100));
                if (result == null) continue;

                Console.WriteLine("Consuming Message");
                Console.WriteLine(
                    $"Partition: {result.Partition.Value}," +
                    $"Offset: {result.Offset}," +
                    $"Key: {result.Message.Key}," +
                    $"Value: {result.Message.Value}"
                );

                await Task.Delay((int)(result.Offset * 1000));

                consumer.Commit(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
            }
        }
    }
}
