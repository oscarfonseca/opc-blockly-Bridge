using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;
using OpcServerApi.DTO;
using StatusCodes = Opc.Ua.StatusCodes;

namespace OpcServerApi.OpcClient;

/* ========================================================================
 * Copyright (c) 2005-2021 The OPC Foundation, Inc. All rights reserved.
 *
 * OPC Foundation MIT License 1.00
 * 
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 * 
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 *
 * The complete license agreement can be found here:
 * http://opcfoundation.org/License/MIT/1.00/
 * ======================================================================*/
public class OpcClient : IOpcClient
{
    const string EndpointUrl = "opc.tcp://172.16.0.1:4840";

    public async Task<bool> Read(GetValueDto getValueDto)
    {
        var session = await OpenSession(EndpointUrl);

        var root = session.NodeCache.Find(Objects.RootFolder);

        var readValue = await session.ReadValueAsync(getValueDto.NodeId);

        if (readValue.StatusCode != StatusCodes.Good)
            Console.WriteLine($"Could not read {getValueDto.NodeId}");

        Console.WriteLine($"Read successful, Value: {readValue.Value}");

        await session.CloseAsync();

        return (bool)readValue.Value;
    }

    public async Task<bool> Write(PostValueDto newValue)
    {
        var session = await OpenSession(EndpointUrl);

        var root = session.NodeCache.Find(Objects.RootFolder);

        var writeValue = new WriteValue
        {
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

    private static async Task<Session> OpenSession(string endpointUrl)
    {
        var endpoint = new ConfiguredEndpoint(null, new EndpointDescription(endpointUrl));
        var application = new ApplicationInstance
        {
            ApplicationName = "UA Sample Client",
            ApplicationType = ApplicationType.Client,
            ConfigSectionName = "OpcClient"
        };
        try
        {
            var applicationConfiguration = await application.LoadApplicationConfiguration(false);
            var session = Session.Create(applicationConfiguration, endpoint, true, "YourSessionName", 60000, null, null)
                .Result;

            return session;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}