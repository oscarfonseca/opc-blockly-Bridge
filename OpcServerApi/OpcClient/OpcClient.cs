using Opc.Ua;
using Opc.Ua.Client;
using OpcServerApi.DTO;
using StatusCodes = Opc.Ua.StatusCodes;

namespace OpcServerApi.OpcClient;

public class OpcClient : IOpcClient
{
    const string EndpointUrl = "opc.tcp://172.16.1:4840";
    
    public async Task<bool> Read(ReadValueDto readValueDto)
    {
        var session = OpenSession(EndpointUrl);
        
        var root = session.NodeCache.Find(Objects.RootFolder);
       
        var readValue = await session.ReadValueAsync(readValueDto.NodeId);
        
        if(readValue.StatusCode != StatusCodes.Good)
            Console.WriteLine($"Could not read {readValueDto.NodeId}");
        
        Console.WriteLine($"Read successful, Value: {readValue.Value}");

        await session.CloseAsync();

        return (bool) readValue.Value;
    }

    public async Task<bool> Write(WriteValueDto newValue)
    {
        var session = OpenSession(EndpointUrl);
        
        var root = session.NodeCache.Find(Objects.RootFolder);
        
        var writeValue = new WriteValue {
            NodeId = newValue.NodeId,
            AttributeId = Attributes.Value,
            Value = new DataValue(new Variant(newValue.Value))
        };

        var result = await session.WriteAsync(null, new[] { writeValue }, default);
        
        if (result.Results.All(StatusCode.IsGood))
            return true;
        
        Console.WriteLine("Error while writing data");
        return false;
    }

    private static Session OpenSession(string endpointUrl)
    {
        var endpoint = new ConfiguredEndpoint(null, new EndpointDescription(endpointUrl));
        var applicationConfiguration = new ApplicationConfiguration();
        var session = Session.Create(applicationConfiguration, endpoint, true, "YourSessionName", 60000, null, null)
            .Result;

        // Connect to the server
        session.Open(null, null);

        return session;
    }
}