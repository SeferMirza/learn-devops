using Consumer;

// Uncomment code below to use specific consumer setup
// var multipleConsumers = new MultipleConsumers();
// await multipleConsumers.RunAsync();

// Uncomment code below to use specific consumer setup
var batchConsumer = new BatchConsumer();
await batchConsumer.RunAsync();

// Uncomment code below to use specific consumer setup
// var exceedMaxPollIntervalConsumer = new ExceedMaxPollIntervalConsumer();
// await exceedMaxPollIntervalConsumer.RunAsync();