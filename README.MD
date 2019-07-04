# MongoDBMigration
Sample project showcasing a migration to NServiceBus.Storage.MongoDB using RabbitMQ transport and MongoDB persistence.

## Setup infrastructure

### RabbitMQ

Start a RabbitMQ broker with enabled management plugin and expose ports locally:

`docker run -d --hostname my-rabbit --name rabbit -p 5672:5672 -p 15672:15672 rabbitmq:management`

### MongoDB

Start a MongoDB server with replication set enabled and expose ports locally:

`docker run -d -p 27017:27017 --name TestMongoDB mongo:latest --replSet tr0`
