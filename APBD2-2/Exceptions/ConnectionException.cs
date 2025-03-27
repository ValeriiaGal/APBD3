namespace APBD2;
/// <summary>
/// Device fails to connect to a network
/// </summary>
class ConnectionException : Exception
{
    public ConnectionException(string message) : base(message) {}
}