namespace OpcServer.OpcClient;

public interface IOpcClient
{
    Task<bool> Read();
    Task<bool> Write(bool b);
}