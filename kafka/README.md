# Kafka

The aim of this project is to demonstrate how to implement a `Consumer` for 
`Apache Kafka` in `.NET`. Project contains a sample producer and consumer 
implementations. `Confluent.Kafka` `.NET` library and official `Apache  Kafka` 
docker image is used.

Producer and Consumer samples are implemented in a way to demonstrate a topic
with multiple partitions, auto and manual offset committing.

View [Apache Kafka](0) for more information on `Kafka`

View [confluent-kafka-dotnet](1) for more information for consumer and producer 
implementations.

## Setup

- Run compose file to start a working `Kafka` server
- Create a topic using following command
```cmd
/opt/kafka/bin/kafka-topics.sh --create --if-not-exists --topic <topic-name> 
--bootstrap-server localhost:9092 --partitions 3 --replication-factor 1
```
- Start `Producer` app to send messages
- Start `Consumer` app to receive messages

> [!NOTE]
>
> We currently faced an issue when all applications run in docker, since 
> producer and consumer should wait for the server to be healthy but currently 
> a valid health check could not be performed so we decided to only run `Kafka` 
> server in docker 

For more information for docker setup, view [kafka](2) repository

## Producer

Producers are clients that send messages to Kafka topics. They create data and 
push to `Kafka` server. Producers can specify the partition a message belongs 
to, or it can be assigned by the Kafka server based on a given key or 
automatically.

Producers can send messages to multiple topics, and multiple producers can send 
messages to the same topic. If topic has multiple partitions, messages with the 
same key will always be in the same partition.

## Consumer

Consumers are clients that read messages from `Kafka` for given topic. Consumers
keep track of processed messages by committing an offset, which is stored in 
topic partitions.

Consumers can subscribe to multiple topics and can also be grouped. Each 
consumer group will have their individual message offsets so it enables multiple
groups to subscribe to same topic. 

### Notes

- Consumers have group id and offset is associated with that group id, if group 
  id is randomly generated every time, the offset will be reset for that group, 
  and all persisted messages will be read from the beginning

- One or more consumer groups can be subscribed to a topic, and each consumer
  group will have separate offsets.

- For a partitioned topic, each consumer will be assigned to an individual 
  partition if a topic has 3 partitions and 5 consumers, 2 of the consumers will 
  be left idle

- If number of partitions are more than consumer count in a group, a consumer 
  may have more than one partition assigned

- If a consumer of a group fails, the partitions will be assigned to remaining 
  consumers by `Kafka`

- `Kafka` moves the offset when committed, Auto-commit is set to true by default 
  but can be turned off and committed manually.

[0]: https://kafka.apache.org/
[1]: https://github.com/confluentinc/confluent-kafka-dotnet
[2]: https://github.com/apache/kafka/tree/trunk/docker/examples