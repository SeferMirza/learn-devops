# Kafka


Create a topic using following command

```cmd
/opt/kafka/bin/kafka-topics.sh --create --if-not-exists --topic <topic-name> --bootstrap-server localhost:9092 --partitions 3 --replication-factor 1
```