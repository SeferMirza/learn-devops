# Kafka

We used `Confluent.Kafka` `.NET` library and `apache/kafka` docker image in this
demo project

## Setup

Create a topic using following command

```cmd
/opt/kafka/bin/kafka-topics.sh --create --if-not-exists --topic <topic-name> --bootstrap-server localhost:9092 --partitions 3 --replication-factor 1
```

## Consumer


### Notes

- Consumers have group id and offset is associated with the group id, if group 
  id is randomly generated everytime, the offset will be reset for that group, 
  and all persisted messages will be read from the beginning

- One ore more consumer groups can be subscribed to a topic, and each conmsumer
  group will have seperate offsets.

- For a partioned topic, each consumer will be assigned to an individual 
  partition if a topic has 3 partitions and 5 consumers, 2 of the consumers will 
  be left idle

- If number of partitions are more than consumer count in a group, a consumer may 
  have more than one partitions assigned

- If a consumer of a group fails, the partitions will be assigned to remaining 
  consumers by kafka

- Messages with same key will exists in the same partition

- Kafka moves the offset when commited, Auto commit is set true by default, can 
  be turned of an manually committed.
