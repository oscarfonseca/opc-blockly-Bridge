using OpcServerApi.DTO;

namespace OpcServerApi.OpcClient;

public interface IOpcClient
{
    Task<bool> Read(ReadValueDto readValueDto);
    Task<bool> Write(WriteValueDto b);
}