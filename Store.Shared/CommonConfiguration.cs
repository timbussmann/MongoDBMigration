using System;
using System.Text;
using MongoDB.Driver;
using NServiceBus;
using NServiceBus.Encryption.MessageProperty;
using NServiceBus.MessageMutator;
//using NServiceBus.Persistence.MongoDB;

public static class CommonConfiguration
{
    public static void ApplyCommonConfiguration(this EndpointConfiguration endpointConfiguration,
        Action<TransportExtensions<RabbitMQTransport>> messageEndpointMappings = null)
    {
        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString("host=localhost");
        transport.UseConventionalRoutingTopology();

        messageEndpointMappings?.Invoke(transport);

        var persistence = endpointConfiguration.UsePersistence<MongoPersistence>();
        persistence.MongoClient(new MongoClient("mongodb://localhost"));
        persistence.DatabaseName("showcase");
        var compatibility = persistence.CommunityPersistenceCompatibility();
        compatibility.VersionElementName("Version");


        var defaultKey = "2015-10";
        var ascii = Encoding.ASCII;
        var encryptionService = new RijndaelEncryptionService(
            encryptionKeyIdentifier: defaultKey,
            key: ascii.GetBytes("gdDbqRpqdRbTs3mhdZh9qCaDaxJXl+e6"));
        endpointConfiguration.EnableMessagePropertyEncryption(encryptionService);
        endpointConfiguration.RegisterMessageMutator(new DebugFlagMutator());
    }
}