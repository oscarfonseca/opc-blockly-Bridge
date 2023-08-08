using Opc.Ua;
using Opc.Ua.Client;
using StatusCodes = Opc.Ua.StatusCodes;

namespace OpcServer.OpcClient;

public class OpcClient : IOpcClient
{
    const string EndpointUrl = "opc.tcp://172.16.1:4840";
    const string NodeId = "ns=4;i=10";
    
    public async Task<bool> Read()
    {
        var session = OpenSession();
        
        var root = session.NodeCache.Find(Objects.RootFolder);
       
        var readValue = await session.ReadValueAsync(NodeId);
        
        if(readValue.StatusCode != StatusCodes.Good)
            Console.WriteLine($"Could not read {NodeId}");
        
        Console.WriteLine($"Read successful, Value: {readValue.Value}");

        await session.CloseAsync();

        return (bool) readValue.Value;
    }


    public async Task<bool> Write(bool b)
    {
        var session = OpenSession();
        
        var root = session.NodeCache.Find(Objects.RootFolder);
        
        var writeValue = new WriteValue {
            NodeId = NodeId,
            AttributeId = Attributes.Value,
            Value = new DataValue(new Variant(true))
        };

        var result = await session.WriteAsync(null, new[] { writeValue }, default);
        
        if (result.Results.All(StatusCode.IsGood))
            return true;
        
        Console.WriteLine("Error while writing data");
        return false;
    }

    private static Session OpenSession()
    {
        var endpoint = new ConfiguredEndpoint(null, new EndpointDescription(EndpointUrl));
        var applicationConfiguration = new ApplicationConfiguration();
        var session = Session.Create(applicationConfiguration, endpoint, true, "YourSessionName", 60000, null, null)
            .Result;

        // Connect to the server
        session.Open(null, null);

        return session;
    }
}