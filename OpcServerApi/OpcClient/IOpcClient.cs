using OpcServerApi.DTO;

namespace OpcServerApi.OpcClient;

public interface IOpcClient
{
    Task<bool> Read(GetValueDto getValueDto);
    Task<bool> Write(PostValueDto b);
}