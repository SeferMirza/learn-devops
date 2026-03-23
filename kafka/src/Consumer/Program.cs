// Uncomment to use specific consumer setup

using Consumer;

//var multipleConsumers = new MultipleConsumers();
//await multipleConsumers.RunAsync();

var batchConsumer = new BatchConsumer();
await batchConsumer.RunAsync();

//var exceedMaxPollIntervalConsumer = new ExceedMaxPollIntervalConsumer();
//await exceedMaxPollIntervalConsumer.RunAsync();